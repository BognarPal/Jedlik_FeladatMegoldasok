var express = require('express');
var router = express.Router();
const fs = require('fs');
const path = require('path');

// ******************************************************************
// Publikus API
// ******************************************************************

/* GET alcím */
router.get('/alcim', function (req, res, next) {
    console.log("Api hívás érkezett: /alcim api kérés érkezett a frontendtől");
    res.send(JSON.stringify(
        {
            "alcim": "A 2019-es Formula–1 világbajnokság volt sorrendben a 70. Formula–1-es szezon. Összesen 21 futamot rendeztek, a szezon március 17-én az ausztrál nagydíjjal vette kezdetét a melbourne-i Albert Parkban, és december 1-jén ért véget az abu-dzabi nagydíjjal a Yas Marina öbölben.",
            "message": "Alcím olvasása sikeres!"
        }
    ))
});


/* Bejegyzések kezelése */
var uzenetekJsonPath = path.join(__dirname, '../uzenetek.json');
var uzenetek = [];

/* POST üzenetek */
router.post('/uzenet', function (req, res, next) {
    console.log("Api hívás érkezett: uzenet rögzítése: ", req.body);
    uzenetek.push({
        "nev": req.body.nev,
        "email": req.body.email,
        "uzenet": req.body.uzenet,
        "datum": new Date().toLocaleString()
    });
    fs.writeFileSync(uzenetekJsonPath, JSON.stringify(uzenetek, null, "\t"));
    res.send(JSON.stringify({"message": req.body}));
});

/* GET üzenetek */
router.get('/uzenet', function (req, res, next) {
    console.log("Api hívás érkezett: uzenetek lekérdezése.");
    try {
        uzenetek = JSON.parse(fs.readFileSync(uzenetekJsonPath));
    } catch (e) {
        console.log("Még nincs érvényes uzenetet tartalmazó fájl.")
    }
    if (uzenetek.length === 0) {
        res.send(JSON.stringify({"message": "Még nem érkezett üzenet."}));
    } else {
        res.send(JSON.stringify(uzenetek, null, 4));
    }
});

/* DELETE üzenetek */
router.delete('/uzenet', function (req, res, next) {
    console.log("Api hívás érkezett: üzenetek törlésére.");
    try{
        uzenetek = [];
        fs.unlinkSync(uzenetekJsonPath, JSON.stringify(uzenetek));
        res.send(JSON.stringify({'message':'Törlés sikeres'}));
    } catch (e) {
        res.send(JSON.stringify({'message':'Nem történt törlés, mert a fájl nem létezik.'}));
    }
});

// Az SQL felület API-jai
// SQL fájl felolvasása, paraméterek a szerverhez kapcsolódáshoz, Adatbázis név!
var mysql = require('mysql');
var sqlTasks = require('../assets/sqlTasks.json');
const getSqlTasks = require('../assets/sqlTasks');
var dbName = 'formula1_2019';
var db = {
    host: 'localhost',
    user: 'root',
    password: '',
    database: dbName
};


/* Általános SQL query */
const databaseQuery = sqlQuery => {
    return new Promise((resolve, reject) => {
        const connection = mysql.createConnection(db);
        connection.connect();
        // console.log('sqlQuery', sqlQuery);
        connection.query(sqlQuery, (error, lines, fields) => {
            if (error) reject(error);
            resolve(lines);
            connection.end();
        })
    })
};

/* GET sqlTasks */
router.get('/sqltasks', function (req, res, next) {
    res.send(JSON.stringify(sqlTasks))
});
/* fech data from database */
router.get('/lekerdezes/:id', function (req, res, next) {
    getSqlTasks().then(result => {
        console.log("API hívás érkezett: lekérdezés futtatása:", req.params['id'], ' sorszámmal.');
        const sqlTasks = result;
        const sqlTaskById = sqlTasks.filter(task => task.id == req.params.id);

        if (sqlTaskById) {
            sqlTask = sqlTaskById[0];
            if (sqlTask.sql) {
                databaseQuery(sqlTask.sql)
                    .then(result => {
                        res.send(JSON.stringify(result))
                    })
                    .catch(error => {
                        console.error("Hiba történt az SQL parancs végrehajtása során:", error.sqlMessage ? error.sqlMessage : error.code);
                        res.status(error.status || 500);
                        if (error.code && error.code === "ER_BAD_DB_ERROR") {
                            console.error("Még nincsen létrehozva a feladathoz szükséges adatbázis!");
                            res.send(JSON.stringify({error: error.code}))
                        } else if (error.code && error.code === "ER_NO_SUCH_TABLE") {
                            console.error("Még nincsen létrehozva a feladathoz tartozó tábla!");
                            res.send(JSON.stringify({error: error.code}))
                        } else if (error.code && error.code === "ECONNREFUSED") {
                            console.error("Nem sikerült a MySQL adatbázis szerverhez kapcsolódni.");
                            res.send(JSON.stringify({error: error.code}))
                        } else if (error.sqlMessage) {
                            console.log(error.sqlMessage);
                            res.send(JSON.stringify({error: error.sqlMessage}))
                        } else {
                            console.log(JSON.stringify(error));
                            res.send(JSON.stringify({error: 'Ismeretlen eredetű hiba!'}))
                        }
                    })
            } else {
                console.log("Ehhez a feladathoz még nem szerepel SQL lekérdezés a beadandó fájlban.");
                res.send(JSON.stringify({empty: true}))
            }
        }
    })
});


// Szerver monitoring endpointok
/* GET server status monitoring */
router.get('/serverStatus', function (req, res, next) {
    res.send(JSON.stringify({alive: true}))
});
/* GET SQL mysqlServerStatus monitoring */
router.get('/mysqlServerStatus', function (req, res, next) {
    new Promise((resolve, reject) => {
        const dbOnly = {
            host: 'localhost',
            user: 'root',
            password: '',
        };
        const sqlQuery = ' select version();';
        const connection = mysql.createConnection(dbOnly);
        connection.connect();
        connection.query(sqlQuery, (error, lines, fields) => {
            if (error) reject(error);
            resolve(lines);
            connection.end();
        })
    }).then(result => {
        res.send({alive: true})
    }).catch(error => {
        res.send({alive: false})
    });
});
/* GET SQL mysqlTableStatus monitoring */
router.get('/mysqlTableStatus', function (req, res, next) {
    databaseQuery(' select version();')
        .then(result => {
            res.send({alive: true})
        })
        .catch(error => {
            res.send({alive: false})
        })
});

// export
module.exports = router;

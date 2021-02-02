var express = require('express');
var router = express.Router();
const fs = require('fs');
const path = require('path');

/* GET nem elérhető autók listája. */
router.get('/nemelerheto', function (req, res, next) {
    console.log("Api hívás érkezett: /nemelerheto api kérés érkezett a Frontendtől");
    res.send(JSON.stringify(
        {
            nemElerhetoAutok: 'Ferrari Spider 1992, Moszkvics 408'
        }
    ))
});


/* Vélemény bejegyzések kezelése */
let velemenyekJsonPath = path.join(__dirname, '../assets/velemenyek.json');
var velemenyek = [];

initializeVelemenyek(velemenyekJsonPath);

function initializeVelemenyek(path) {
    try {
        velemenyek = require(path);
    } catch (e) {
        console.log("Még nincs érvényes véleményeket tartalmazó fájl.")
    }
}

/* POST velemeny */
router.post('/velemeny', function (req, res, next) {
    console.log("Api hívás érkezett: vásárlói vélemény rögzítése: ", req.body);

    velemenyek.push({
        "velemeny": req.body.velemeny,
        "date": new Date().toLocaleString()
    });
    fs.writeFileSync(velemenyekJsonPath, JSON.stringify(velemenyek));

    res.send(JSON.stringify(req.body));
});

/* GET velemeny */
router.get('/velemeny', function (req, res, next) {
    console.log("Api hívás érkezett: vásárlói vélemény lekérdezése.");
    if (velemenyek.length === 0) {
        res.send(JSON.stringify(
            [{
                "velemeny": "Még nem érkezett felhasználói bejegyzés.",
                "date": null
            }]
        ));
    } else {
        res.send(JSON.stringify(velemenyek));
    }
});

router.delete('/velemeny', function (req, res, next) {
    console.log("Api hívás érkezett: vásárlói vélemények törlése.");
    try{
        velemenyek = [];
        fs.unlinkSync(velemenyekJsonPath, JSON.stringify(velemenyek));
        res.send(JSON.stringify({'message':'Törlés sikeres'}));
    } catch (e) {
        res.send(JSON.stringify({'message':'Nem történt törlés, mert a fájl nem létezik (még).'}));
    }
});

// Admin eszközök
var mysql = require('mysql');
var sqlTasks = require('../assets/sqlTasks.json');
var db = {
    host: 'localhost',
    user: 'root',
    password: '',
    database: 'oldtimer'
};

const getSqlTasks = require('../assets/sqlTasks');
const databaseQuery = sqlQuery => {
    return new Promise((resolve, reject) => {
        const connection = mysql.createConnection(db);
        connection.connect();
        if (sqlQuery !== 'select version();') {
            console.log('sqlQuery futtatása:', sqlQuery);
        }
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
/* GET server status monitoring */
router.get('/serverStatus', function (req, res, next) {
    res.send(JSON.stringify({alive: true}))
});
/* GET SQL status monitoring */
router.get('/mysqlStatus', function (req, res, next) {
    databaseQuery('select version();')
        .then(result => {
            res.send({alive: true})
        })
        .catch(error => {
            res.send({alive: false})
        })

});


module.exports = router;

const fs = require('fs');
const path = require('path');
let sqlTasks = require('./sqlTasks.json');
const os = require('os');

const getSqlTasks = () => {
    return new Promise((resolve, reject) => {
        fs.readFile(path.join(__dirname, "../lekerdezesek/lekerdezesek.sql"), 'utf8', (err, data) => {
            if (err) {
                reject(err)
            } else {
                let sqlQueriesFromFile = data.split("***").splice(1);
                sqlQueriesFromFile.forEach(sqlQuery => {
                    let lines = sqlQuery.split(os.EOL);
                    const id = lines[1].split(".")[0];
                    lines = lines.splice(2);
                    if (lines.length > 0) {
                        const sqlQueryText = lines.reduce((sql, line) => sql + " " + line).trim();
                        let sqlTask = sqlTasks.filter(task => task.id === Number.parseInt(id))[0];
                        if (sqlTask) {
                            sqlTask.sql = sqlQueryText
                        } else {
                            console.error("Nem található ilyen ID-jű lekérdezés: ", id);
                        }
                    }
                });
                resolve(sqlTasks)
            }
        })
    })
};

module.exports = getSqlTasks;

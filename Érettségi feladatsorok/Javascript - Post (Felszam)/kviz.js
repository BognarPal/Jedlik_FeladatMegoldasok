var url = "http://localhost:84/kviz.php/"
function kategoriak() {
    var data = "";

    var xhr = new XMLHttpRequest();

    xhr.addEventListener("readystatechange", function () {
        if (this.readyState === 4) {
            kategoriak = JSON.parse(this.responseText);
            kategoriak.forEach(kategoria => {
                var selectElement = document.querySelector("#kategoria select");
                var optionElement = document.createElement("option");
                optionElement.value = kategoria;
                optionElement.innerText = kategoria;
                selectElement.appendChild(optionElement);
            });
        }
    });

    xhr.open("GET", url + "kategoriak");

    xhr.send(data);
}

function ujkerdes() {
    var selectElement = document.querySelector("#kategoria select");
    var data = JSON.stringify({ "kategoria": selectElement.value });
    var xhr = new XMLHttpRequest();

    xhr.addEventListener("readystatechange", function () {
        if (this.readyState === 4) {
            document.querySelector("#kerdes").innerText = JSON.parse(this.responseText).kerdes;
        }
    });

    xhr.open("POST", url + "kerdes");
    xhr.setRequestHeader("Content-Type", "application/json");

    xhr.send(data);
}

function ellenorzes() {

    var data = JSON.stringify({ 
        "kerdes": document.querySelector("#kerdes").innerText, 
        "valasz": document.querySelector('#valasz input[type="number"]').value 
    });

    var xhr = new XMLHttpRequest();

    xhr.addEventListener("readystatechange", function () {
        if (this.readyState === 4) {
            var valasz = JSON.parse(this.responseText);
            if (valasz.helyes) {
                alert('A válasz helyes');
            } else {
                alert('A válasz helytelen.\nA helyes válasz:' + valasz.helyesvalasz)
            }
        }
    });

    xhr.open("POST", url + "ellenorzes");
    xhr.setRequestHeader("Content-Type", "application/json");

    xhr.send(data);

}
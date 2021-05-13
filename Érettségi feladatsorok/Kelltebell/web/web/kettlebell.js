function arak()
{
    var xhr = new XMLHttpRequest();
    xhr.withCredentials = true;

    xhr.addEventListener("readystatechange", function() {
    if(this.readyState === 4) {
        var a = JSON.parse(this.responseText);
        document.getElementById('kiloar').innerText = a.kiloar;
        document.getElementById('edzoterv').innerText = a.edzoterv;
        document.getElementById('tankonyv').innerText = a.tankonyv;
        document.getElementById('kedvezmeny').innerText = a.kedvezmeny;
    }
    });

    xhr.open("GET", "/api/arak");

    xhr.send();
}

function gyakorlatGeneralas() {

    var ismetles = Math.floor(Math.random() * 30) + 1;
    gyakorlatok = [ 'Swing', 'Magasra húzás', 'Serleg guggolás', 'Szélmalom', 'Török felállás'];
    var gyakorlat = gyakorlatok[Math.floor(Math.random() * gyakorlatok.length )];

    document.getElementById('generaltGyakorlat').innerText = ismetles.toString() + ' darab ' + gyakorlat;
}
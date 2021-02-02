function nemElerheto() {
    var xhr = new XMLHttpRequest();
    xhr.withCredentials = true;

    xhr.addEventListener("readystatechange", function() {
        if(this.readyState === 4) {
            var eredmeny = JSON.parse(this.responseText);
            document.getElementById("nem-elerheto").innerText = eredmeny.nemElerhetoAutok;
        }
    });

    xhr.open("GET", "http://localhost:8000/api/nemelerheto");

    xhr.send();
}

function velemenyKuldes() {
    var data = JSON.stringify({
        velemeny: document.getElementById("velemenyInput").value
    });

    var xhr = new XMLHttpRequest();
    xhr.withCredentials = true;
    
    xhr.addEventListener("readystatechange", function() {
      if(this.readyState === 4) {
        alert("Véleménye fontos számunkra!");
        document.getElementById("velemenyInput").value = "";
      }
    });
    
    xhr.open("POST", "http://localhost:8000/api/velemeny");
    xhr.setRequestHeader("Content-Type", "application/json");
    
    xhr.send(data);


}

nemElerheto();    

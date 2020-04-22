function lekerdezAlcim() {
    var xhr = new XMLHttpRequest();
    xhr.addEventListener("readystatechange", function () {
        if (this.readyState === 4) {
            document.getElementById('alcim').innerText = JSON.parse(this.responseText).alcim;
            console.log(JSON.parse(this.responseText).message);
        }
    });

    xhr.open("GET", "http://localhost:8080/api/alcim");
    xhr.send();
}

function kuldUzenet() {
    if (!document.getElementById('inputNev').value || !document.getElementById('inputEmail').value || !document.getElementById('inputUzenet').value) {
        alert('Kérem töltse ki az összes mezőt!')
        return;
    }

    var data = JSON.stringify({ 
        nev: document.getElementById('inputNev').value, 
        email: document.getElementById('inputEmail').value, 
        uzenet: document.getElementById('inputUzenet').value 
    });

    var xhr = new XMLHttpRequest();
    xhr.addEventListener("readystatechange", function () {
        if (this.readyState === 4 && this.status === 200) {
            document.getElementById('inputNev').value = '';
            document.getElementById('inputEmail').value = '';
            document.getElementById('inputUzenet').value = '';
            alert('Köszönjük az üzenetét!');
            console.log(JSON.parse(this.responseText).message);
        }
    });

    xhr.open("POST", "http://localhost:8080/api/uzenet");
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send(data);
}


function lekerdezUzenetek() {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(this.responseText);
            console.log(JSON.parse(this.responseText));
        }
    };
    xhttp.open("GET", "/api/uzenet");
    xhttp.send();
}

function torolUzenetek() {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(this.responseText);
            console.log(JSON.parse(this.responseText));
        }
    };
    xhttp.open("DELETE", "/api/uzenet");
    xhttp.send();
}


$(document).ready(function () {
    $('.header').height($(window).height());
});

$('.test, .nav-link, .navbar-brand, .new-button').click(function () {
    var sectionTo = $(this).attr('href');
    $('html, body').animate({
        scrollTop: $(sectionTo).offset().top
    }, 100);
});

function kalkulal(){
    //Űrlapadatok
    const szelesseg = Number(document.getElementById('szelesseg').value );
    const magassag = Number(document.getElementById('magassag').value );
    const papir=document.getElementById('papirtipus').value;
    //Számítások
    let terulet=Math.round((szelesseg*magassag)/10000);    
    let koltseg=terulet*papir;
    //Megjelenítés
    document.getElementById('terulet').innerText = terulet;
    document.getElementById('papir').innerText = papir;
    document.getElementById('koltseg').innerText = koltseg;

    document.getElementById('valasz').style.visibility = 'visible';
}


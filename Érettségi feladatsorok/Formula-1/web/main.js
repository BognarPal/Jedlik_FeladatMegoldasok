$(document).ready(function () {
  $('.navbar-nav li').click(function (x) {
    $('.navbar-nav li').removeClass('active');
    $(event.target).parent().addClass('active');
  });
});

function showCircuit() {
    $('#circuit').change(function (o) {
      switch (o.target.value) {
        case 'ITA':
          $('#circuitimage').attr('src', 'img/monza.jpg');
          $('#f1sound')[0].play();
          break;
        case 'HUN':
          $('#circuitimage').attr('src', 'img/hungaroring.jpg');
          break;
        case 'BEL':
          $('#circuitimage').attr('src', 'img/spa.jpg');
          break;
        case 'MON':
          $('#circuitimage').attr('src', 'img/monaco.jpg');
          break;

        default:
          break;
      }
    });
  }

//---------------------------------------------------------

function calculate() {
    var track = document.getElementById('circuit').value;
    var laptime = Number(document.getElementById('laptime').value) / 3600;
    if ( track === 'HUN' )
      document.getElementById('averagespeed').value = (4.381 / laptime).toString() + ' km/h';
    else if (track === 'MON')
      document.getElementById('averagespeed').value = (3.337 / laptime).toString() + ' km/h';
    else if (track === 'BEL')
      document.getElementById('averagespeed').value = (7.004 / laptime).toString() + ' km/h';
    else if (track === 'ITA')
      document.getElementById('averagespeed').value = (5.793 / laptime).toString() + ' km/h';
    else 
      document.getElementById('averagespeed').value = '';
  }

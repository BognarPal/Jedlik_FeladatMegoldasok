using System;
using System.Collections.Generic;
using System.Text;

namespace Ultrabalaton_WPF
{
    class Eredmeny
    {
        public string Versenyzo { get; set; }
        public int Rajtszam { get; set; }
        public string Kategoria { get; set; }
        public string Versenyido { get; set; }
        public int TavSzazalek { get; set; }

        public Eredmeny(string sor)
        {
            //Acsadi Lajos;1;Ferfi;30:28:42;100
            var adatok = sor.Split(';');
            this.Versenyzo = adatok[0];
            this.Rajtszam = int.Parse(adatok[1]);
            this.Kategoria = adatok[2];
            this.Versenyido = adatok[3];
            this.TavSzazalek = int.Parse(adatok[4]);
        }

        public double IdőÓrában()
        {
            var adatok = this.Versenyido.Split(':');
            double ora = double.Parse(adatok[0]);
            double perc = double.Parse(adatok[1]);
            double mp = double.Parse(adatok[2]);

            return ora + perc / 60 + mp / 3600;
        }
    }
}

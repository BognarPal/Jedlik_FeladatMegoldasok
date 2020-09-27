using System;
using System.Collections.Generic;
using System.Text;

namespace KémiaWPF
{
    public class KemiaiElem
    {
        public string Ev { get; set; }
        public string Nev { get; set; }
        public string Vegyjel { get; set; }
        public int Rendszam { get; set; }
        public string Felfedezo { get; set; }

        public KemiaiElem(string sor)
        {
            var adatok = sor.Split(';');
            this.Ev = adatok[0];
            this.Nev = adatok[1];
            this.Vegyjel = adatok[2];
            this.Rendszam = int.Parse(adatok[3]);
            this.Felfedezo = adatok[4];
        }
    }
}

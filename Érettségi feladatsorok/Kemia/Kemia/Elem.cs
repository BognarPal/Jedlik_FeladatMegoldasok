using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemia
{
    class Elem
    {
        public string Ev { get; set; }
        public string Nev { get; set; }
        public string Vegyjel { get; set; }
        public int Rendszam { get; set; }
        public string Felfelfedezo { get; set; }


        public Elem(string sor)
        {
            //1825;Alumínium;Al;13;H. C. Oersted
            string[] adatok = sor.Split(';');
            Ev = adatok[0];
            Nev = adatok[1];
            Vegyjel = adatok[2];
            Rendszam = int.Parse(adatok[3]);
            Felfelfedezo = adatok[4];
        }
    }
}

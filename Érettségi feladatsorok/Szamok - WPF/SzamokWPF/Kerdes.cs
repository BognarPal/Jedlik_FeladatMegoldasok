using System;
using System.Collections.Generic;
using System.Text;

namespace SzamokWPF
{
    public  class Kerdes
    {
        public string KerdesSzovege { get; set; }
        public int HelyesValasz { get; set; }
        public int Pontszam { get; set; }
        public string Kategoria { get; set; }

        public Kerdes(string sor1, string sor2)
        {
            //Mikor volt a mohacsi vesz?   => sor1
            //1526 1 tortenelem            => sor2
            this.KerdesSzovege = sor1;
            string[] adatok = sor2.Split(' ');   // { "1526", "1", "tortenelem" }
            this.HelyesValasz = int.Parse(adatok[0]);
            this.Pontszam= int.Parse(adatok[1]);
            this.Kategoria = adatok[2];
        }


    }
}

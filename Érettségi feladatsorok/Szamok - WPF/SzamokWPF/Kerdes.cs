using System;
using System.Collections.Generic;
using System.Text;

namespace SzamokWPF
{
    public class Kerdes
    {
        public string KerdesSzovege { get; set; }
        public int Valasz { get; set; }
        public int Pont { get; set; }
        public string Kategoria { get; set; }

        public Kerdes(string sor1, string sor2)
        {
            KerdesSzovege = sor1;   
            Valasz = int.Parse(sor2.Split(' ')[0]);
            Pont = int.Parse(sor2.Split(' ')[1]);
            Kategoria = sor2.Split(' ')[2];
        }
    }
}

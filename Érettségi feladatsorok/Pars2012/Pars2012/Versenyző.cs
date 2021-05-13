using System;
using System.Collections.Generic;
using System.Text;

namespace Pars2012
{
    public class Versenyző
    {
        public string Név { get; set; }
        public string Csoport { get; set; }
        public string NemzetÉsKód { get; set; }
        public double D1 { get; set; }
        public double D2 { get; set; }
        public double D3 { get; set; }

        public string Kód { get; set; }
        public string Nemzet { get; set; }
        public string Sorozat { get; set; }

        public Versenyző(string sor)
        {
            var adatok = sor.Split(';');
            Név = adatok[0];
            Csoport = adatok[1];
            NemzetÉsKód = adatok[2];
            D1 = adatok[3] == "X" ? -1 : (adatok[3] == "-" ? -2 : double.Parse(adatok[3]));
            D2 = adatok[4] == "X" ? -1 : (adatok[4] == "-" ? -2 : double.Parse(adatok[4]));
            D3 = adatok[5] == "X" ? -1 : (adatok[5] == "-" ? -2 : double.Parse(adatok[5]));

            Kód = NemzetÉsKód.Substring(NemzetÉsKód.Length - 4, 3);
            Nemzet = NemzetÉsKód.Substring(0, NemzetÉsKód.Length - 6);
            Sorozat = string.Format("{0};{1};{2}", adatok[3], adatok[4], adatok[5]);
        }

        public double Eredmény
        {
            get
            {
                return Math.Max(D3, Math.Max(D1, D2));
            }
        }

    }
}

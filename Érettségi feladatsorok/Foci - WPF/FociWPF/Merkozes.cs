using System;
using System.Collections.Generic;
using System.Text;

namespace FociWPF
{
    public class Merkozes
    {
        public int Fordulo { get; set; }
        public int HazaiGolVegeredmeny { get; set; }
        public int VendegGolVegeredmeny { get; set; }
        public int HazaiGolFelido { get; set; }
        public int VendegGolFelido { get; set; }
        public string HazaiCsapat { get; set; }
        public string VendegCsapat { get; set; }

        public Merkozes(string sor)
        {
            var adatok = sor.Split(' ');
            this.Fordulo = int.Parse(adatok[0]);
            this.HazaiGolVegeredmeny = int.Parse(adatok[1]);
            this.VendegGolVegeredmeny = int.Parse(adatok[2]);
            this.HazaiGolFelido = int.Parse(adatok[3]);
            this.VendegGolFelido = int.Parse(adatok[4]);
            this.HazaiCsapat = adatok[5];
            this.VendegCsapat = adatok[6];
        }

        public override string ToString()
        {
            return $"{HazaiCsapat}-{VendegCsapat}: {HazaiGolVegeredmeny}-{VendegGolVegeredmeny} ({HazaiGolFelido}-{VendegGolFelido})";
        }
    }
}

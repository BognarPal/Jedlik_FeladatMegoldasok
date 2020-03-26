using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyszamjatekGui
{
    class Jatekos
    {
        public string Nev { get; set; }
        public List<int> Tippek { get; set; }

        public Jatekos(string sor)
        {
            Tippek = new List<int>();
            //Marci 2 12 1 8
            string[] adatok = sor.Split(' ');
            Nev = adatok[0];
            for (int i = 1; i < adatok.Length; i++)
            {
                Tippek.Add(int.Parse(adatok[i]));
            }
        }
    }
}

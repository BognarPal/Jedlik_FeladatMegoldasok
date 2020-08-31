using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootGolf
{
    class Versenyzo
    {
        public string Nev { get; set; }
        public string Kategoria { get; set; }
        public string Egyesulet { get; set; }
        public List<int> Pontszamok { get; set; }

        public Versenyzo(string sor)
        {
            this.Pontszamok = new List<int>();

            //Albert Laszlo;Felnott ferfi;HOLE HUNTERS;0;0;10;10;0;0;0;10
            string[] adatok = sor.Split(';');
            this.Nev = adatok[0];
            this.Kategoria = adatok[1];
            this.Egyesulet = adatok[2];

            for (int i = 3; i <= 10; i++)
            {
                Pontszamok.Add(int.Parse(adatok[i]));
            }
        }

       


    }
}

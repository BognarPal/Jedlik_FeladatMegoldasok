using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace London2012
{
    public class Sportag
    {
        public string Megnevezes { get; set; }
        public int[] Dontok { get; set; } = new int[16];

        public Sportag(string sor)
        {
            //Atlétika;0;0;0;0;0;0;2;5;7;5;4;4;5;6;8;1
            string[] adatok = sor.Split(';');
            Megnevezes = adatok[0];
            for (int i = 0; i < 16; i++)
            {
                Dontok[i] = int.Parse(adatok[i + 1]);
            }
        }


    }
}

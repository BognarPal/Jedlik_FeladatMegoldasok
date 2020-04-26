using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto
{
    class HetiSzamok
    {
        public int[] Szamok { get; set; }

        public HetiSzamok(string sor)
        {
            Szamok = new int[5];
            string[] adatok = sor.Split(' ');
            for (int i = 0; i < 5; i++)
            {
                Szamok[i] = int.Parse(adatok[i]);
            }
        }
    }
}

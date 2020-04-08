using System;
using System.Collections.Generic;
using System.Text;

namespace Iskola
{
    class Tanulo
    {
        public int Ev { get; set; }
        public string Osztaly { get; set; }
        public string Nev { get; set; }

        public Tanulo(string sor)
        {
            //2006;c;Bodnar Szilvia
            string[] adatok = sor.Split(';');
            Ev = int.Parse(adatok[0]);
            Osztaly = adatok[1];
            Nev = adatok[2];
        }

        public int NevHossza
        {
            get
            {
                return Nev.Replace(" ", "").Length;
            }

        }

        //6cbodszi
        public string Azonosito
        {
            get
            {
                string azon = (Ev % 1000).ToString();
                azon += Osztaly;
                string[] nevek = Nev.ToLower().Split(' ');
                azon += nevek[0].Substring(0, 3);
                azon += nevek[1].Substring(0, 3);

                return azon;
            }
        }
    }
}

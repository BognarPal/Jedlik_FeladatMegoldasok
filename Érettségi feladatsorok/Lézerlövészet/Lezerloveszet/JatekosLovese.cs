using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezerloveszet
{
    class JatekosLovese
    {
        public string Nev { get; set; }
        public int Sorszam { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public JatekosLovese(string sor, int sorszam)
        {
            //Gabi;29,90;28,92
            Sorszam = sorszam;
            string[] adatok = sor.Split(';'); // ["Gabi", "29,90", "28,92"]
            Nev = adatok[0];
            X = double.Parse(adatok[1]);
            Y = double.Parse(adatok[2]);
        }

        public double Tavolsag(double kpX, double kpY)
        {
            double dx = kpX - X;
            double dy = kpY - Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public double Pontszam(double kpX, double kpY)
        {
            double tavolsag = Tavolsag(kpX, kpY);
            if (tavolsag > 10)
            {
                return 0;
            }
            return Math.Round(10 - tavolsag, 2);
        }
    }
}

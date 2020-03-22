using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezerloveszet
{
    class Program
    {
        static void Main(string[] args)
        {
            List<JatekosLovese> lovesek = new List<JatekosLovese>();
            StreamReader sr = new StreamReader("lovesek.txt");
            string[] kozeppontKoordinata = sr.ReadLine().Split(';');
            int sorszam = 1;
            while (!sr.EndOfStream)
            {
                lovesek.Add(new JatekosLovese(sr.ReadLine(), sorszam++));
            }
            sr.Close();

            Console.WriteLine("5. feladat: Lövések száma: {0} db", lovesek.Count);

            double kpX = double.Parse(kozeppontKoordinata[0]);
            double kpY = double.Parse(kozeppontKoordinata[1]);

            int minIndex = 0;
            double minErtek = lovesek[0].Tavolsag(kpX, kpY);
            for (int i = 1; i < lovesek.Count; i++)
            {
                if (minErtek > lovesek[i].Tavolsag(kpX, kpY))
                {
                    minErtek = lovesek[i].Tavolsag(kpX, kpY);
                    minIndex = i;
                }
            }
            Console.WriteLine("7. feladat: Legpontosabb lövés:");
            Console.WriteLine("\t{0}.; {1}; x={2}; y={3}; távolság={4}",
                               lovesek[minIndex].Sorszam,
                               lovesek[minIndex].Nev,
                               lovesek[minIndex].X,
                               lovesek[minIndex].Y,
                               minErtek);

            int db = 0;
            foreach (var l in lovesek)
            {
                if (l.Pontszam(kpX, kpY) == 0)
                {
                    db++;
                }
            }
            Console.WriteLine("9. feladat: nulla pontos lövések száma: {0} db", db);

            Dictionary<string, int> lovesDarab = new Dictionary<string, int>();
            foreach (var l in lovesek)
            {
                if (lovesDarab.ContainsKey(l.Nev))
                {
                    lovesDarab[l.Nev]++;
                }
                else
                {
                    lovesDarab.Add(l.Nev, 1);
                }
            }

            Console.WriteLine("10. feladat: Játékosok száma: {0}", lovesDarab.Count);

            Console.WriteLine("11. feladat: Lövések száma:");
            foreach (var l in lovesDarab)
            {
                Console.WriteLine("\t{0} - {1} db", l.Key, l.Value);
            }


            Dictionary<string, double> lovesPontszam = new Dictionary<string, double>();
            foreach (var l in lovesek)
            {
                if (lovesPontszam.ContainsKey(l.Nev))
                {
                    lovesPontszam[l.Nev] += l.Pontszam(kpX, kpY);
                }
                else
                {
                    lovesPontszam.Add(l.Nev, l.Pontszam(kpX, kpY));
                }
            }

            Console.WriteLine("12. feladat: Átlagpontszámok");
            foreach (var l in lovesPontszam)
            {
                Console.WriteLine("\t{0} - {1}", l.Key, l.Value / lovesDarab[l.Key]);
            }

            double maxPontszam = -1;
            string maxNev = "";
            foreach (var l in lovesPontszam)
            {
                if (maxPontszam < l.Value / lovesDarab[l.Key])
                {
                    maxPontszam = l.Value / lovesDarab[l.Key];
                    maxNev = l.Key;
                }
            }
            Console.WriteLine("13. feladat: A játék nyertese: {0}", maxNev);
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;

namespace KaracsonyCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            List<NapiMunka> munkak = new List<NapiMunka>();
            StreamReader sr = new StreamReader("diszek.txt");
            while (!sr.EndOfStream)
            {
                munkak.Add(new NapiMunka(sr.ReadLine()));
            }
            sr.Close();

            int ossz = 0;
            foreach (var m in munkak)
            {
                ossz += m.FenyofaKesz + m.HarangKesz + m.AngyalkaKesz;
            }
            Console.WriteLine("4. feladat: Összesen {0} darab dísz készült", ossz);

            int i = 0;
            while (i < munkak.Count && munkak[i].FenyofaKesz + munkak[i].HarangKesz + munkak[i].AngyalkaKesz > 0)
                i++;
            if (i < munkak.Count)
            {
                Console.WriteLine("5. feladat: Volt olyan nap, amikor egyetlen dísz sem készült");
            }
            else
            {
                Console.WriteLine("5. feladat: Nem volt olyan nap, amikor egyetlen dísz sem készült");
            }

            Console.WriteLine("6. feladat");
            int nap = 0;
            do
            {
                Console.Write("Adja meg a keresett napot [1 ... 40]: ");
                int.TryParse(Console.ReadLine(), out nap);
            }
            while (nap < 1 || nap > 40);

            int harang = 0;
            int angyalka = 0;
            int fenyofa = 0;
            foreach (var m in munkak)
            {
                if (m.Nap <= nap)
                {
                    harang += (m.HarangKesz + m.HarangEladott);
                    angyalka += (m.AngyalkaKesz+ m.AngyalkaEladott);
                    fenyofa += (m.FenyofaKesz + m.FenyofaEladott);
                }
            }
            Console.WriteLine("\tA(z) {0}. nap végén {1} harang, {2} angyalka és {3} fenyőfa maradt készleten", 
                                nap, harang, angyalka, fenyofa);

            harang = 0;
            angyalka = 0;
            fenyofa = 0;
            foreach (var m in munkak)
            {
                harang -= m.HarangEladott;
                angyalka -= m.AngyalkaEladott;
                fenyofa -= m.FenyofaEladott;
            }
            int maxDarab;
            if (harang >= angyalka && harang >= fenyofa)
                maxDarab = harang;
            else if (angyalka >= harang && angyalka >= fenyofa)
                maxDarab = angyalka;
            else
                maxDarab = fenyofa;

            Console.WriteLine("7. feladat: a Legtöbbet eladott dísz: {0} darab", maxDarab);
            if (harang == maxDarab)
                Console.WriteLine("\tHarang");
            if (angyalka == maxDarab)
                Console.WriteLine("\tAngyalka");
            if (fenyofa == maxDarab)
                Console.WriteLine("\tFenyőfa");

            StreamWriter sw = new StreamWriter("bevetel.txt");
            int db = 0;
            foreach (var m in munkak)
            {
                if (m.NapiBevetel() >= 10000 )
                {
                    db++;
                    sw.WriteLine("{0}: {1}", m.Nap, m.NapiBevetel());
                }
            }
            sw.WriteLine("{0} napon volt legalább 10000 Ft a bevétel", db);
            sw.Close();
        }
    }
}

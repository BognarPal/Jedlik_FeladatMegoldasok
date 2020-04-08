using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Iskola
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tanulo> tanulok = new List<Tanulo>();
            StreamReader sr = new StreamReader("nevek.txt");
            while (!sr.EndOfStream)
            {
                tanulok.Add(new Tanulo(sr.ReadLine()));
            }
            sr.Close();

            //Console.WriteLine("3. feladat: Az iskolába {0} tanuló jár", tanulok.Count);
            Console.WriteLine($"3. feladat: Az iskolába {tanulok.Count} tanuló jár");

            int maxErtek = tanulok[0].NevHossza;
            for (int i = 1; i < tanulok.Count; i++)
            {
                if (maxErtek < tanulok[i].NevHossza)
                {
                    maxErtek = tanulok[i].NevHossza;
                }
            }
            //int maxErtek = tanulok.Max(t => t.NevHossza);

            Console.WriteLine("4. feladat: A leghosszab ({0} karakter) nevű tanuló(k):", maxErtek);
            foreach (Tanulo t in tanulok)
            {
                if (t.NevHossza == maxErtek)
                {
                    Console.WriteLine("\t{0}", t.Nev);
                }
            }

            Console.WriteLine("5. feladat: Azonosítók");
            Console.WriteLine("\tElső: {0} - {1}", tanulok[0].Nev, tanulok[0].Azonosito);
            Console.WriteLine("\tUtolsó: {0} - {1}", tanulok[tanulok.Count - 1].Nev, tanulok[tanulok.Count - 1].Azonosito);

            Console.Write("6. feladat: Kérek egy azonosítót [pl.: 4dvavkri]: ");
            string azon = Console.ReadLine();
            int j = 0;
            while (j < tanulok.Count && tanulok[j].Azonosito != azon)
            {
                j++;
            }
            if (j < tanulok.Count)
            {
                Console.WriteLine("\t{0} {1} {2}", tanulok[j].Ev, tanulok[j].Osztaly, tanulok[j].Nev);
            }
            else
            {
                Console.WriteLine("\tNincs megfelelő tanuló");
            }

            Random rand = new Random();
            JelszóGeneráló jelszoGeneralo = new JelszóGeneráló(rand);
            int index = rand.Next(tanulok.Count);
            Console.WriteLine("7. feladat: Jelszó generálása");
            Console.WriteLine("\t{0} - {1}", tanulok[index].Nev, jelszoGeneralo.Jelszó(8));

            Console.ReadKey();
        }
    }
}

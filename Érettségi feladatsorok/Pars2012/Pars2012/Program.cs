using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Pars2012
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Versenyző> versenyzok = new List<Versenyző>();
            StreamReader sr = new StreamReader("Selejtezo2012.txt", Encoding.UTF8);
            sr.ReadLine();
            while (!sr.EndOfStream)
                versenyzok.Add(new Versenyző(sr.ReadLine()));
            sr.Close();

            Console.WriteLine("5. feladat: Versenyző száma a selejtezőben: {0} fő", versenyzok.Count);

            int db = 0;
            foreach (var v in versenyzok)
            {
                if (v.D1 > 78 || v.D2 > 78)
                    db++;
            }
            Console.WriteLine("6. feladat: 78,00 méter feletti eredménnyel továbbjutott: {0} fő", db);

            int maxIndex = 0;
            double maxValue = versenyzok[0].Eredmény;
            for (int i = 1; i < versenyzok.Count; i++)
            {
                if (maxValue < versenyzok[i].Eredmény)
                {
                    maxValue = versenyzok[i].Eredmény;
                    maxIndex = i;
                }
            }
            Console.WriteLine("9. feladat: A selejtező nyertese:");
            Console.WriteLine("\tNév: {0}", versenyzok[maxIndex].Név);
            Console.WriteLine("\tCsoport: {0}", versenyzok[maxIndex].Csoport);
            Console.WriteLine("\tNemzet: {0}", versenyzok[maxIndex].Nemzet);
            Console.WriteLine("\tNemzet kód: {0}", versenyzok[maxIndex].Kód);
            Console.WriteLine("\tSorozat: {0}", versenyzok[maxIndex].Sorozat);
            Console.WriteLine("\tEredmény: {0}", versenyzok[maxIndex].Eredmény);

            StreamWriter sw = new StreamWriter("Dontos2012.txt", false, Encoding.UTF8);
            //List<Versenyző> top12 = versenyzok.OrderByDescending(v => v.Eredmény).Take(12).ToList();
            sw.WriteLine("Helyezés;Név;Csoport;Nemzet;NemzetKód;Sorozat;Eredmény");
            for (int j = 1; j <= 12; j++)
            {
                maxIndex = 0;
                maxValue = versenyzok[0].Eredmény;
                for (int i = 1; i < versenyzok.Count; i++)
                {
                    if (maxValue < versenyzok[i].Eredmény)
                    {
                        maxValue = versenyzok[i].Eredmény;
                        maxIndex = i;
                    }
                }
                sw.WriteLine("{0};{1};{2};{3};{4};{5};{6}", j, 
                                                            versenyzok[maxIndex].Név,
                                                            versenyzok[maxIndex].Csoport,
                                                            versenyzok[maxIndex].Nemzet,
                                                            versenyzok[maxIndex].Kód,
                                                            versenyzok[maxIndex].Sorozat,
                                                            versenyzok[maxIndex].Eredmény
                                 );
                versenyzok.RemoveAt(maxIndex);
            }

            sw.Close();


        }
    }
}

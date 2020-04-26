using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace Lotto
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] szamok52 = new int[5];
            List<HetiSzamok> lottoSzamok = new List<HetiSzamok>();

            Console.WriteLine("1. feladat: az 52. hét számai");
            for (int i = 0; i < 5; i++)
            {
                Console.Write("\t{0}. szám: ", i + 1);
                szamok52[i] = int.Parse(Console.ReadLine());
            }

            Array.Sort(szamok52);
            Console.WriteLine("2. feladat");
            for (int i = 0; i < 5; i++)
            {
                Console.Write("\t{0}", szamok52[i]);  
            }
            Console.WriteLine();

            Console.WriteLine("3. feladat");
            StreamReader sr = new StreamReader("lottosz.txt");
            while (!sr.EndOfStream)
            {
                lottoSzamok.Add(new HetiSzamok(sr.ReadLine()));
            }
            sr.Close();

            Console.Write("\tKérem a hetet: ");
            int het = int.Parse(Console.ReadLine());

            Console.WriteLine("4. feladat: A {0} hét nyerőszámai:", het);
            for (int i = 0; i < 5; i++)
            {
                Console.Write("\t{0}", lottoSzamok[het - 1].Szamok[i]);
            }
            Console.WriteLine();

            Console.WriteLine("5. feladat");
            List<int> mindenSzam = new List<int>();    //Lehetne HashSet is
            for (int i = 1; i <= 90; i++)
            {
                mindenSzam.Add(i);
            }

            foreach (var hetiSzam in lottoSzamok)
            {
                for (int i = 0; i < 5; i++)
                {
                    mindenSzam.Remove(hetiSzam.Szamok[i]);
                }
            }

            if (mindenSzam.Count > 0)
            {
                Console.WriteLine("\tVolt(ak) olyan szám(ok), amit nem húztak ki");
            }
            else
            {
                Console.WriteLine("\tMinden számot kihúztak legalább egyszer");
            }

            Console.WriteLine("6. feladat");
            int db = 0;
            foreach (var hetiSzam in lottoSzamok)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (hetiSzam.Szamok[i] % 2 == 1)
                    {
                        db++;
                    }
                }
            }
            Console.WriteLine("\t{0} db páratlan számot húztak ki", db);

            //7. feladat
            StreamWriter sw = new StreamWriter("lottosz52.txt");
            foreach (var hetiSzam in lottoSzamok)
            {
                for (int i = 0; i < 5; i++)
                {
                    sw.Write("{0} ", hetiSzam.Szamok[i]);
                }
                sw.WriteLine();
            }

            for (int i = 0; i < 5; i++)
            {
                sw.Write("{0} ", szamok52[i]);
            }
            sw.WriteLine();
            sw.Close();
        }


    }
}

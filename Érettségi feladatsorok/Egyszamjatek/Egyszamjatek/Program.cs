using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egyszamjatek
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Jatekos> jatekosok = new List<Jatekos>();
            StreamReader sr = new StreamReader("egyszamjatek1.txt");
            while (!sr.EndOfStream)
            {
                jatekosok.Add(new Jatekos(sr.ReadLine()));
            }
            sr.Close();

            Console.WriteLine("3. feladat: Játékosok száma: {0} fő", jatekosok.Count);

            Console.Write("4. feladat: Kérem a forduló sorszámát: ");
            int fordulo = int.Parse(Console.ReadLine());

            decimal osszeg = 0;
            foreach (var j in jatekosok)
            {
                osszeg += j.Tippek[fordulo - 1];
            }
            Console.WriteLine("5. feladat: A megadott forduló tippjeinek átlaga: {0:f2}", osszeg / jatekosok.Count);

            Console.ReadKey();
        }
    }
}

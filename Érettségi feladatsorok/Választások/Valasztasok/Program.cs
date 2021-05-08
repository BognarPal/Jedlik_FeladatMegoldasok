using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Valasztasok
{
    class Program
    {
        static void Main(string[] args)
        {
            var jeloltek = File.ReadAllLines("szavazatok.txt").Select(s => new Jelolt(s)).ToList();
            Console.WriteLine("2. feladat: A helyhatósági választáson {0} képviselőjelölt indult.", jeloltek.Count);

            Console.Write("3. feladat: Képviselő neve: ");
            string nev = Console.ReadLine();
            var jelolt = jeloltek.FirstOrDefault(j => j.Nev == nev);
            if (jelolt == null)
                Console.WriteLine("\tIlyen nevű képviselőjelölt nem szerepel a nyilvántartásban!");
            else
            {
                Console.WriteLine("\tA jelölt a {0}-s számú körzetben indult", jelolt.Korzet);
                Console.WriteLine("\tKapott szavazatok száma: {0}", jelolt.Szavazat);
            }

            var szavazokSzama = jeloltek.Sum(j => j.Szavazat);
            Console.WriteLine("4. feladat: A választáson {0} szavazó, a jogosultak {1:f2}%-a vett részt.", szavazokSzama, 100.0 * szavazokSzama / 12345);

            var partok = jeloltek.GroupBy(j => j.PartNeve).Select(p => new { Part = p.Key, Szavazat = p.Sum(j => j.Szavazat) }).ToList();
            Console.WriteLine("6. feladat:");
            partok.ForEach(p => Console.WriteLine("\t{0}: {1:f2}%", p.Part, 100.0 * p.Szavazat / szavazokSzama));

            Console.WriteLine("7. feladat: A legtöbb szavazat:");
            var maxSzavazat = jeloltek.Max(j => j.Szavazat);
            jeloltek.Where(j => j.Szavazat == maxSzavazat).ToList().ForEach(j =>
            {
                Console.WriteLine("\t{0} - {1}: {2} szavazat.", j.Nev, j.PartNeve, j.Szavazat);
            });

            var korzetek = jeloltek.GroupBy(j => j.Korzet)
                                   .Select(k => new { Korzet = k.Key, Nyertes = k.OrderByDescending(j => j.Szavazat).First() })
                                   .OrderBy(k => k.Korzet);

            File.WriteAllLines("kepviselok.csv", korzetek.Select(k => string.Format("{0};{1};{2}", k.Korzet, k.Nyertes.Nev, k.Nyertes.PartNeve)));


        }
    }
}

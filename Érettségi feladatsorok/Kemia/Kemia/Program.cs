using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemia
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Elem> elemek = new List<Elem>();
            StreamReader sr = new StreamReader("felfedezesek.csv", Encoding.UTF8);
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                elemek.Add(new Elem(sr.ReadLine()));
            }
            sr.Close();

            Console.WriteLine("3. feladat: Elemek száma: {0}", elemek.Count);

            int db = 0;
            foreach (var e in elemek)
            {
                if (e.Ev == "Ókor")
                {
                    db++;
                }
            }
            Console.WriteLine("4. feladat: Felfedezések száma az ókorban: {0}", db);

            string vegyjel = "";
            do
            {
                Console.Write("5. feladat: Kérek egy vegyjelet: ");
                vegyjel = Console.ReadLine();
            }
            /*while ( vegyjel.Length != 1 && vegyjel.Length != 2 || 
                    (vegyjel.ToUpper()[0] < 'A' || vegyjel.ToUpper()[0] > 'Z') ||
                    (vegyjel.Length == 2  &&  (vegyjel.ToUpper()[1] < 'A' || vegyjel.ToUpper()[1] > 'Z')) );*/
            while (!VegyjelOK(vegyjel));

            int i = 0;
            while (i < elemek.Count && elemek[i].Vegyjel.ToUpper() != vegyjel.ToUpper())
            {
                i++;
            }
            Console.WriteLine("6. Feladat: Keresés");
            if (i < elemek.Count)
            {
                Console.WriteLine("\tAz elem vegyjele: {0}", elemek[i].Vegyjel);
                Console.WriteLine("\tAz elem neve: {0}", elemek[i].Nev);
                Console.WriteLine("\tRendszám: {0}", elemek[i].Rendszam);
                Console.WriteLine("\tFelfedezés éve: {0}", elemek[i].Ev);
                Console.WriteLine("\tFelfedezők: {0}", elemek[i].Felfelfedezo);
            }
            else
            {
                Console.WriteLine("\tNincs ilyen elem az adatbázisban.");
            }

            int maxErtek = -1;
            for (int j = db; j < elemek.Count-1; j++)
            {
                if (int.Parse(elemek[j+1].Ev) - int.Parse(elemek[j].Ev) > maxErtek)
                {
                    maxErtek = int.Parse(elemek[j + 1].Ev) - int.Parse(elemek[j].Ev);
                }
            }
            Console.WriteLine("7. feladat: {0} év volt a leghosszab időszak két elem felfedezések között"
                , maxErtek);


            Dictionary<string, int> stat = new Dictionary<string, int>();
            for (int j = db; j < elemek.Count; j++)
            {
                string ev = elemek[j].Ev;
                if (stat.ContainsKey(ev))
                {
                    stat[ev]++;
                }
                else
                {
                    stat.Add(ev, 1);
                }
            }
            Console.WriteLine("8. feladat: Statisztika");
            foreach (var s in stat)
            {
                if (s.Value > 3)
                {
                    Console.WriteLine("\t{0}: {1} db", s.Key, s.Value);
                }
            }


            StreamWriter sw = new StreamWriter("fajlnev.txt");
            foreach (var s in stat)
            {
                sw.WriteLine("\t{0}: {1} db", s.Key, s.Value);
            }
            sw.Close();

            Console.ReadKey();
        }

        private static bool VegyjelOK(string vegyjel)
        {
            if (vegyjel.Length == 0)
                return false;
            if (vegyjel.Length > 2)
                return false;
            if (vegyjel.ToUpper()[0] < 'A' || vegyjel.ToUpper()[0] > 'Z')
                return false;
            if (vegyjel.Length == 1)
                return true;
            if (vegyjel.ToUpper()[1] < 'A' || vegyjel.ToUpper()[1] > 'Z')
                return false;
            return true;
        }
    }
}

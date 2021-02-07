using System;
using System.Collections.Generic;
using System.IO;

namespace sudokuCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Feladvany> feladvanyok = new List<Feladvany>();
            StreamReader sr = new StreamReader("feladvanyok.txt");
            while (!sr.EndOfStream)
            {
                feladvanyok.Add(new Feladvany(sr.ReadLine()));
            }
            sr.Close();
            Console.WriteLine("3. feladat: Beolvasva {0} feladvány", feladvanyok.Count);

            int meret;
            do
            {
                Console.Write("4. feladat: Kérem a feladvány méretét [4..9]: ");
            }
            while (!int.TryParse(Console.ReadLine(), out meret) || meret < 4 || meret > 9);


            List<Feladvany> nElemuFeladvanyok = new List<Feladvany>();
            foreach (var f in feladvanyok)
            {
                if (f.Meret == meret)
                {
                    nElemuFeladvanyok.Add(f);
                }
            }

            Console.WriteLine("{0}x{0} méretű feladványból {1} darab van tárolva", meret, nElemuFeladvanyok.Count);

            /*
            Random rand = new Random();
            int sorszam = rand.Next(db);
            int i = 0;
            int ii = 0;
            while ( ii < sorszam)
            {
                if (feladvanyok[i].Meret == meret)
                {
                    ii++;
                }
                i++;
            }
            var kivalasztottFeladvany = feladvanyok[i - 1];
            */
            Random rand = new Random();
            int index = rand.Next(nElemuFeladvanyok.Count);
            var kivalasztottFeladvany = nElemuFeladvanyok[index];

            Console.WriteLine("5. feladat: A kiválaszott feladvány: ");
            Console.WriteLine(kivalasztottFeladvany.Kezdo);

            double db = 0;
            foreach (char szamjegy in kivalasztottFeladvany.Kezdo)
            {
                if (szamjegy != '0')
                {
                    db++;
                }
            }
            Console.WriteLine("6. feladat: A feladvány kitöltöttsége: {0:f0}%)", 100 * db / kivalasztottFeladvany.Kezdo.Length);

            Console.WriteLine("7. feladat: A feladvány kirajzolva:");
            kivalasztottFeladvany.Kirajzol();

            string fajlNev = string.Format("sudoku{0}.txt", meret);
            StreamWriter sw = new StreamWriter(fajlNev);
            foreach (var f in nElemuFeladvanyok)
            {
                sw.WriteLine(f.Kezdo);
            }
            sw.Close();
            Console.WriteLine("8. feladat: {0} állomány {1} darab feladvánnyal létrehozva", fajlNev, nElemuFeladvanyok.Count);
        }
    }
}

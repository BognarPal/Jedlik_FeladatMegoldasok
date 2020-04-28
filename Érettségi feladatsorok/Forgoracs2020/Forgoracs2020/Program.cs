using System;
using System.IO;

namespace Forgoracs2020
{
    class Program
    {
        static void Main(string[] args)
        {
            string szoveg = File.ReadAllText("szoveg.txt");
            Console.WriteLine("3 feladat");
            Console.WriteLine("\t{0}", szoveg);

            Fracs fracs = new Fracs("kodlemez.txt", szoveg);
            Console.WriteLine("5. feladat");
            Console.WriteLine("\t{0}", fracs.Titkositando);

            Console.WriteLine("6. feladat");
            fracs.KiirKodlemez();

            fracs.Titkosit();
            Console.WriteLine("9. feladat");
            fracs.KiirKodoltSzoveg();
        }
    }
}

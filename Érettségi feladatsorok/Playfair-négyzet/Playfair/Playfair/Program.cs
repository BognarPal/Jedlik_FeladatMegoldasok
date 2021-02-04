using System;

namespace Playfair
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayfairKodolo playfairKodolo = new PlayfairKodolo("kulcstabla.txt");

            Console.Write("6. feladat - Kérek egy nagybetűt: ");
            char betu = Console.ReadLine()[0];
            Console.WriteLine("A karakter sorának indexe: {0}", playfairKodolo.SorIndex(betu));
            Console.WriteLine("A karakter oszlopának indexe: {0}", playfairKodolo.OszlopIndex(betu));

            Console.Write("8. feladat - Kérek egy karakterpárt: ");
            string karakterPar = Console.ReadLine();
            Console.WriteLine("Kódolva: {0}", playfairKodolo.KodolBetupar(karakterPar));


        }
    }
}

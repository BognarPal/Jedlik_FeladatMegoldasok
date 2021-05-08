using System;

namespace ADFGVX
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("2. feladat");
            Console.Write("\tKérem a kulcsot [HOLD]:");
            string kulcs = Console.ReadLine().ToUpper();
            if (kulcs == "")
            {
                kulcs = "HOLD";
            }
            Console.Write("\tKérem az üzenet [szeretem a csokit]:");
            string uzenet = Console.ReadLine().ToLower();
            if (uzenet == "")
            {
                uzenet = "szeretem a csokit";
            }

            ADFGVXrejtjel rejtjel = new ADFGVXrejtjel("kodtabla.txt", uzenet, kulcs);
            Console.WriteLine("5. feladat: Az átalakított üzenet: {0}", rejtjel.AtalakitottUzenet());

            Console.WriteLine("6. feladat: s->{0}, x->{1}", rejtjel.Betupar('s'), rejtjel.Betupar('x'));

            Console.WriteLine("7. feladat: A kódszöveg: {0}", rejtjel.Kodszoveg());

            Console.WriteLine("8. feladat: A kódolt üzenet: {0}", rejtjel.KodoltUzenet());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ADFGVX
{
    class ADFGVXrejtjel
    {
        private char[,] Kodtabla;
        private string Uzenet;
        private string Kulcs;

        public string AtalakitottUzenet()
        {
            // 5. feladat:
            string uzenet = Uzenet.Replace(" ", "");
            while (uzenet.Length % Kulcs.Length != 0)
            {
                uzenet += "x";
            }
            return uzenet;
        }

        public string Kodszoveg()
        {
            // 7. feladat:
            string atalakitottUzenet = AtalakitottUzenet();
            string kodoltSzoveg = "";
            foreach (char ch in atalakitottUzenet)
            {
                kodoltSzoveg += Betupar(ch);
            }
            return kodoltSzoveg;
        }

        public string KodoltUzenet()
        {
            string kodszoveg = Kodszoveg();
            int sorokSzama = kodszoveg.Length / Kulcs.Length;
            int oszlopokSzama = Kulcs.Length;
            char[,] m = new char[sorokSzama, oszlopokSzama];
            int index = 0;
            for (int sor = 0; sor < sorokSzama; sor++)
            {
                for (int oszlop = 0; oszlop < oszlopokSzama; oszlop++)
                {
                    m[sor, oszlop] = kodszoveg[index++];
                }
            }

            string kodoltUzenet = "";
            for (char ch = 'A'; ch <= 'Z'; ch++)
            {
                int oszlopIndex = Kulcs.IndexOf(ch);
                if (oszlopIndex != -1)
                {
                    for (int sorIndex = 0; sorIndex < sorokSzama; sorIndex++)
                    {
                        kodoltUzenet += m[sorIndex, oszlopIndex];
                    }
                }
            }
            return kodoltUzenet;
        }

        public ADFGVXrejtjel(string kodtablaFile, string uzenet, string kulcs)
        {
            Uzenet = uzenet;
            Kulcs = kulcs;

            Kodtabla = new char[6, 6];
            int sorIndex = 0;
            foreach (var sor in System.IO.File.ReadAllLines(kodtablaFile))
            {
                for (int oszlopIndex = 0; oszlopIndex < sor.Length; oszlopIndex++)
                {
                    Kodtabla[sorIndex, oszlopIndex] = sor[oszlopIndex];
                }
                sorIndex++;
            }
        }

        public string Betupar(char k)
        {
            string[] adfgvx = { "A", "D", "F", "G", "V", "X" };
            for (int sorIndex = 0; sorIndex <= 5; sorIndex++)
            {
                for (int oszlopIndex = 0; oszlopIndex <= 5; oszlopIndex++)
                {
                    if (Kodtabla[sorIndex, oszlopIndex] == k)
                    {
                        return adfgvx[sorIndex] + adfgvx[oszlopIndex];
                    }
                }
            }
            return "hiba";
        }
    }
}

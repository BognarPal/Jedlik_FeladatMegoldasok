using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Forgoracs2020
{
    class Fracs
    {
        private char[,] Kodlemez;
        private char[,] Titkositott;
        public string Titkositando { get; private set; }

        public Fracs(string fajlKodlemez, string titkositando)
        {
            Kodlemez = new char[8, 8];
            Titkositott = new char[8, 8];
            int sor = 0;
            foreach (var i in File.ReadAllLines(fajlKodlemez))
            {
                for (int oszlop = 0; oszlop < i.Length; oszlop++)
                {
                    Kodlemez[sor, oszlop] = i[oszlop];
                }
                sor++;
            }
            Titkositando = titkositando;
            Atalakit();
        }

        private void Atalakit()
        {
            foreach (var i in "., ")
            {
                Titkositando = Titkositando.Replace(i.ToString(), "");
            }
            if (Titkositando.Length > 64)
            {
                throw new Exception("Túl hosszú a titkosítandó szöveg!");
            }
            Titkositando = Titkositando.PadRight(64, 'X');
        }

        private void Kiir(char[,] matrix)
        {
            for (int sor = 0; sor < 8; sor++)
            {
                Console.Write("\t");
                for (int oszlop = 0; oszlop < 8; oszlop++)
                {
                    Console.Write(matrix[sor, oszlop]);
                }
                Console.WriteLine();
            }
        }

        public void KiirKodlemez() => Kiir(Kodlemez);
        public void KiirKodoltSzoveg() => Kiir(Titkositott);

        private char[,] ForgatKodlemez()
        {
            char[,] ujKodlemez = new char[8, 8];
            for (int sor = 0; sor <= 7; sor++)
            {
                for (int oszlop = 0; oszlop <= 7; oszlop++)
                {
                    ujKodlemez[7 - oszlop, sor] = Kodlemez[sor, oszlop];
                }
            }
            return ujKodlemez;
        }

        public void Titkosit()
        {
            int index = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int oszlop = 0; oszlop <= 7; oszlop++)
                {
                    for (int sor = 0; sor <= 7; sor++)
                    {
                        if (Kodlemez[sor, oszlop] == 'A')
                        {
                            Titkositott[sor, oszlop] = Titkositando[index++];
                        }
                    }
                }
                Kodlemez = ForgatKodlemez();
            }
        }


    }
}

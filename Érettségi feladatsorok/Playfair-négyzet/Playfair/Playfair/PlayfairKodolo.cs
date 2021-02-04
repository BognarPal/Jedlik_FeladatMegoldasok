using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Playfair
{
    class PlayfairKodolo
    {
        public char[,] Kulcstabla { get; set; }

        public PlayfairKodolo(string fajlNev)
        {
            this.Kulcstabla = new char[5, 5];
            string[] tartalom = File.ReadAllLines(fajlNev);
            for (int sor = 0; sor < 5; sor++)
            {
                for (int oszlop = 0; oszlop < 5; oszlop++)
                {
                    Kulcstabla[sor, oszlop] = tartalom[sor][oszlop];
                }
            }
        }

        public int SorIndex(char karakter)
        {
            for (int sor = 0; sor < 5; sor++)
            {
                for (int oszlop = 0; oszlop < 5; oszlop++)
                {
                    if (Kulcstabla[sor, oszlop] == karakter)
                    {
                        return sor;
                    }
                }
            }
            return -1;
        }

        public int OszlopIndex(char karakter)
        {
            for (int sor = 0; sor < 5; sor++)
            {
                for (int oszlop = 0; oszlop < 5; oszlop++)
                {
                    if (Kulcstabla[sor, oszlop] == karakter)
                    {
                        return oszlop;
                    }
                }
            }
            return -1;
        }

        public string KodolBetupar(string kodolando)
        {
            int sor0 = SorIndex(kodolando[0]);
            int sor1 = SorIndex(kodolando[1]);
            int oszlop0 = OszlopIndex(kodolando[0]);
            int oszlop1 = OszlopIndex(kodolando[1]);

            if (sor0 == sor1)
            {
                char helyette0 = Kulcstabla[sor0, (oszlop0 + 1) % 5];
                char helyette1 = Kulcstabla[sor1, (oszlop1 + 1) % 5];
                return string.Format("{0}{1}", helyette0, helyette1);
            }
            if (oszlop0 == oszlop1)
            {
                char helyette0 = Kulcstabla[(sor0 + 1) % 5, oszlop0];
                char helyette1 = Kulcstabla[(sor1 + 1) % 5, oszlop1];
                return string.Format("{0}{1}", helyette0, helyette1);
            }
            else
            {
                char helyette0 = Kulcstabla[sor0, oszlop1];
                char helyette1 = Kulcstabla[sor1, oszlop0];
                return string.Format("{0}{1}", helyette0, helyette1);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Valasztasok
{
    public class Jelolt
    {
        public int Korzet { get; set; }
        public int Szavazat { get; set; }
        public string Nev { get; set; }
        public string Part { get; set; }

        public Jelolt(string sor)
        {
            Korzet = int.Parse(sor.Split(' ')[0]);
            Szavazat = int.Parse(sor.Split(' ')[1]);
            Nev = sor.Split(' ')[2] + ' ' + sor.Split(' ')[3];
            Part = sor.Split(' ')[4];
        }

        Dictionary<string, string> partok = new Dictionary<string, string>()
        {
            {"-", "Független" },
            {"GYEP", "Gyümölcsevők Pártja" },
            {"HEP", "Húsevők Pártja" },
            {"TISZ", "Tejivók Szövetsége" },
            {"ZEP", "Zöldségevők Pártja" },
        };

        public string PartNeve => partok[Part];
    }
}

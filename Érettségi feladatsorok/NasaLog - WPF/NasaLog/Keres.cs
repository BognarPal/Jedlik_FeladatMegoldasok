using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaLog
{
    class Keres
    {
        public string Cim { get; set; }
        public string Idopont { get; set; }
        public string Fajlnev { get; set; }
        public string Valaszkod { get; set; }
        public string Meret { get; set; }

        public Keres(string sor)
        {
            //rsicker.lerc.nasa.gov*20/Jul/1995:10:40:14*GET /shuttle/missions/sts-70/images/KSC-95EC-1016.jpg*200 93923
            string[] adatok = sor.Split('*');    // ["rsicker.lerc.nasa.gov", "20/Jul/1995:10:40:14",  
                                                 // "GET /shuttle/missions/sts-70/images/KSC-95EC-1016.jpg", "200 93923"]
            string[] valaszEsMeret = adatok[3].Split(' ');  //["200", "93923"]
            Cim = adatok[0];
            Idopont = adatok[1];
            Fajlnev = adatok[2];
            Valaszkod = valaszEsMeret[0];
            Meret = valaszEsMeret[1];
        }

        public int ByteMeret
        {
            get
            {
                if (int.TryParse(Meret, out int meret))
                {
                    return meret;
                }
                return 0;

                /*
                if (Meret == "-")
                    return 0;

                return int.Parse(Meret);
                */
            }
        }

        public bool Domain()
        {
            char utolsoKarakter = Cim[Cim.Length - 1];
            if (utolsoKarakter >= '0' && utolsoKarakter <= '9')
            {
                return false;
            }
            return true;
        } 

        public int Ora
        {
            get
            {
                string[] idok = Idopont.Split(':');
                return int.Parse(idok[1]);
            }
        }
    }
}

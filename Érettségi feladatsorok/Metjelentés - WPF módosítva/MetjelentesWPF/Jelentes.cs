using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetjelentesWPF
{
    public class Jelentes
    {
        public string Telepules { get; set; }
        public string Ido { get; set; }
        public string Szelirany { get; set; }
        public int SzelErosseg { get; set; }
        public int Homerseklet { get; set; }

        public Jelentes(string sor)
        {
            //DC 0015 15005 23
            string[] adatok = sor.Split(' ');
            this.Telepules = adatok[0];
            this.Ido = adatok[1];
            this.Szelirany = adatok[2].Substring(0, 3);
            this.SzelErosseg = int.Parse(adatok[2].Substring(3, 2));
            this.Homerseklet = int.Parse(adatok[3]);
        }
    }
}

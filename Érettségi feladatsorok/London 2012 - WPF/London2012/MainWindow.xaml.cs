using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace London2012
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Sportag> sportagak = new List<Sportag>();
        int[] napok = { 28, 29, 30, 31, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        int[] napiDontok = new int[16];

        public MainWindow()
        {
            InitializeComponent();

            StreamReader sr = new StreamReader("London2012.txt");
            while (!sr.EndOfStream)
            {
                sportagak.Add(new Sportag(sr.ReadLine()));
            }
            sr.Close();

            //2. feladat
            int i = 0;
            while (i < sportagak.Count && sportagak[i].Megnevezes != "Atlétika")
            {
                i++;
            }
            if (i < sportagak.Count)
            {
                int db = 0;
                for (int j = 0; j < 16; j++)
                {
                    if (sportagak[i].Dontok[j] > 0)
                    {
                        db++;
                    }
                }
                lbl2.Content = string.Format("{0} darab", db);
                
            }
            else
            {
                lbl2.Content = "HIBA";
            }

            //3. feladat
            foreach (var sportag in sportagak)
            {
                cmb3.Items.Add(sportag.Megnevezes);
            }

            //4. feladat
            
            foreach ( var sportag in sportagak)
            {
                for (int j = 0; j < 16; j++)
                {
                    napiDontok[j] += sportag.Dontok[j];
                }
            }

            int maxIndex = 0;
            int maxErtek = napiDontok[0];
            for (int j = 1; j < 16; j++)
            {
                if (napiDontok[j] > maxErtek)
                {
                    maxErtek = napiDontok[j];
                    maxIndex = j;
                }
            }
            lbl4.Content = string.Format("A legtöbb döntő ({0} db) {1}-n volt.", maxErtek, napok[maxIndex]);

            //5. feladat
            int ossz = 0;
            for (int j = 0; j < 16; j++)
            {
                ossz += napiDontok[j];
            }
            lbl5.Content = string.Format("{0} db aranyérmet osztottak ki az olimpián.", ossz);

        }

        private void cmb3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sportagNeve = cmb3.SelectedItem.ToString();
            int i = 0;
            while (i < sportagak.Count && sportagak[i].Megnevezes != sportagNeve)
            {
                i++;
            }
            int db = 0;
            for (int j = 0; j < 16; j++)
            {
                db += sportagak[i].Dontok[j];
            }
            lbl3.Content = string.Format("Aranyérmek száma {0}-ban: {1} db", sportagNeve, db );
        }

        private void txt6_TextChanged(object sender, TextChangedEventArgs e)
        {
            int i = 0;
            while ( i < 16 && napok[i].ToString() != txt6.Text)
            {
                i++;
            }
            if (i < 16)
            {
                lbl6.Content = string.Format("{0}-n {1} db döntő volt", txt6.Text, napiDontok[i]);
            }
            else
            {
                lbl6.Content = "Nem létező nap";
            }
        }
    }
}

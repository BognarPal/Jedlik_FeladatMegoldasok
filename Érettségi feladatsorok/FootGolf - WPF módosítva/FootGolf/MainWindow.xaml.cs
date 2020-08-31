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

namespace FootGolf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Versenyzo> versenyzok = new List<Versenyzo>();
        public MainWindow()
        {
            InitializeComponent();

            StreamReader sr = new StreamReader("fob2016.txt");
            while (!sr.EndOfStream)
            {
                versenyzok.Add(new Versenyzo(sr.ReadLine()));
            }
            sr.Close();

            lbl4.Content = versenyzok.Count;
            double db = 0;
            foreach (var v in versenyzok)
            {
                if (v.Kategoria == "Noi")
                {
                    db++;
                }
            }
            lbl5.Content = string.Format("{0:f2} %", db * 100 / versenyzok.Count);

            int maxErtek = 0;
            int maxIndex = 0;
            for (int i = 0; i < versenyzok.Count; i++)
            {
                if (versenyzok[i].Kategoria == "Noi" && OsszPontszam(versenyzok[i].Pontszamok) > maxErtek)
                {
                    maxErtek = OsszPontszam(versenyzok[i].Pontszamok);
                    maxIndex = i;
                }
            }
            lbl7.Content = string.Format("{0}\n{1}\n{2}", versenyzok[maxIndex].Nev, versenyzok[maxIndex].Egyesulet, maxErtek);

            foreach (var v in versenyzok)
            {
                if (!cbo8.Items.Contains(v.Egyesulet))
                {
                    cbo8.Items.Add(v.Egyesulet);
                }
            }
        }

        public int OsszPontszam(List<int> pontszamok)
        {
            int osszpont = 0;
            pontszamok.Sort();
            for (int i = 2; i < 8; i++)
            {
                osszpont += pontszamok[i];
            }
            if (pontszamok[0] != 0)
                osszpont += 10;
            if (pontszamok[1] != 0)
                osszpont += 10;

            return osszpont;
        }

        private void cbo8_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lst8.Visibility = Visibility.Visible;
            lst8.Items.Clear();
            foreach (var v in versenyzok)
            {
                if (v.Egyesulet == cbo8.SelectedItem.ToString())
                {
                    lst8.Items.Add(v.Nev);
                }
            }
            Title = string.Format("Footgolf - {0}: {1} versenyző", cbo8.SelectedItem, lst8.Items.Count);
        }
    }
}

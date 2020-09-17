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

namespace MetjelentesWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Jelentes> jelentesek = new List<Jelentes>();

        public MainWindow()
        {
            InitializeComponent();
            Feladat1();
            Feladat3();
            Feladat4();
            Feladat5();
            Feladat6();
        }

        void Feladat1()
        {
            StreamReader sr = new StreamReader("tavirathu13.txt");
            while (!sr.EndOfStream)
            {
                jelentesek.Add(new Jelentes(sr.ReadLine()));
            }
            sr.Close();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            while (i < jelentesek.Count && jelentesek[i].Telepules != txt2.Text)
            {
                i++;
            }
            if (i < jelentesek.Count)   //van találat
            {
                int maxIndex = i;
                string maxErtek = jelentesek[maxIndex].Ido;
                for (int j = i + 1; j < jelentesek.Count; j++)
                {
                    if (jelentesek[j].Telepules == txt2.Text)
                    {
                        if (jelentesek[j].Ido.CompareTo(maxErtek) > 0)
                        {
                            maxErtek = jelentesek[j].Ido;
                            maxIndex = j;
                        }
                    }
                }
                lbl2.Content = string.Format("Az utolsó mérési adat {0}:{1}-kor érkezett",
                    maxErtek.Substring(0,2), maxErtek.Substring(2, 2));
            }
            else  //nincs találat
            {
                MessageBox.Show("Nem létező település");
            }
        }

        void Feladat3()
        {
            int maxErtek = jelentesek[0].Homerseklet;
            int maxIndex = 0;
            for (int i = 1; i < jelentesek.Count; i++)
            {
                if (jelentesek[i].Homerseklet > maxErtek)
                {
                    maxErtek = jelentesek[i].Homerseklet;
                    maxIndex = i;
                }
            }

            int minErtek = jelentesek[0].Homerseklet;
            int minIndex = 0;
            for (int i = 1; i < jelentesek.Count; i++)
            {
                if (jelentesek[i].Homerseklet < minErtek)
                {
                    minErtek = jelentesek[i].Homerseklet;
                    minIndex = i;
                }
            }

            lbl3Max.Content = string.Format("A legmagasabb hőmérséklet: {0} {1}:{2}  {3} fok",
                                jelentesek[maxIndex].Telepules,
                                jelentesek[maxIndex].Ido.Substring(0, 2),
                                jelentesek[maxIndex].Ido.Substring(2, 2),
                                jelentesek[maxIndex].Homerseklet);


            lbl3Min.Content = string.Format("A legalacsonyabb hőmérséklet: {0} {1}:{2}  {3} fok",
                                jelentesek[minIndex].Telepules,
                                jelentesek[minIndex].Ido.Substring(0, 2),
                                jelentesek[minIndex].Ido.Substring(2, 2),
                                jelentesek[minIndex].Homerseklet);
        }

        private void Feladat4()
        {
            foreach (var jelentes in jelentesek)
            {
                if (jelentes.SzelErosseg == 0 && jelentes.Szelirany == "000")
                {
                    lst4.Items.Add(string.Format("{0} {1}:{2}",
                                   jelentes.Telepules,
                                   jelentes.Ido.Substring(0, 2),
                                   jelentes.Ido.Substring(2, 2)));
                }
            }
        }

        private void Feladat5()
        {
            HashSet<string> telepulesek = new HashSet<string>();
            foreach (var jelentes in jelentesek)
            {
                telepulesek.Add(jelentes.Telepules);
            }
            foreach(string t in telepulesek)
            {
                cbo5.Items.Add(t);
            }
        }

        private void Cbo5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbo5.SelectedItem != null)
            {
                string telepules = cbo5.SelectedItem.ToString();
                double ossz = 0;
                int db = 0;
                foreach (var jelentes in jelentesek)
                {
                    if (jelentes.Telepules == telepules)
                    {
                        ossz += jelentes.Homerseklet;
                        db++;
                    }
                }
                int atlag = (int)(ossz / db + 0.5);

                int maxErtek = -999;
                int minErtek = 999;
                foreach (var jelentes in jelentesek)
                {
                    if (jelentes.Telepules == telepules)
                    {
                        if (jelentes.Homerseklet > maxErtek)
                        {
                            maxErtek = jelentes.Homerseklet;
                        }
                        if (jelentes.Homerseklet < minErtek)
                        {
                            minErtek = jelentes.Homerseklet;
                        }
                    }
                }
                lbl5.Content = string.Format("Középhőmérséklet: {0}; " +
                                             "Hőmérsékelt ingadozás: {1}",
                                             atlag, maxErtek - minErtek);
            }
        }

        private void Feladat6()
        {
            HashSet<string> telepulesek = new HashSet<string>();
            foreach (var jelentes in jelentesek)
            {
                telepulesek.Add(jelentes.Telepules);
            }
            foreach (string t in telepulesek)
            {
                StreamWriter sw = new StreamWriter(t + ".txt");
                sw.WriteLine(t);
                foreach (var jelentes in jelentesek)
                {
                    if (jelentes.Telepules == t)
                    {
                        sw.Write("{0}:{1} ", jelentes.Ido.Substring(0, 2),
                                             jelentes.Ido.Substring(2, 2));

                        for (int i = 0; i < jelentes.SzelErosseg; i++)
                        {
                            sw.Write("#");
                        }
                        sw.WriteLine();
                        //sw.WriteLine("".PadLeft(jelentes.SzelErosseg, '#'));
                    }
                }

                sw.Close();
            }
        }

    }
}

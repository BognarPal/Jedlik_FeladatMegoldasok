using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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

namespace SzamokWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Kerdes> kerdesek = new List<Kerdes>();
        Kerdes kerdes6 = null;

        public MainWindow()
        {
            InitializeComponent();
            Feladat1();
            Feladat2();
            Feladat3();
            Feladat4();
            Feladat5();
            Feladat7();
        }

        private void Feladat1()
        {
            StreamReader sr = new StreamReader("felszam.txt");
            while (!sr.EndOfStream)
            {
                kerdesek.Add(new Kerdes(sr.ReadLine(), sr.ReadLine()));
            }
            sr.Close();
        }

        private void Feladat2()
        {
            lbl2.Content = string.Format("Az állományban {0} db kérdés van", kerdesek.Count);
        }

        private void Feladat3()
        {
            int pont1 = 0;
            int pont2 = 0;
            int pont3 = 0;
            foreach (Kerdes k in kerdesek)
            {
                if (k.Kategoria == "matematika")
                {
                    if (k.Pontszam == 1)
                        pont1++;
                    else if (k.Pontszam == 2)
                        pont2++;
                    else /*if (k.Pontszam == 3)*/
                        pont3++;
                }   
            }
            lbl3.Content = string.Format("Az adatfájlban {0} matematika kérdés van\n" +
                                         "1 pontot ér {1} feladat\n" +
                                         "2 pontot ér {2} feladat\n" +
                                         "3 pontot ér {3} feladat)", 
                                         pont1 + pont2 + pont3, pont1, pont2, pont3);
        }

        private void Feladat4()
        {
            int minErtek = kerdesek[0].HelyesValasz;
            int maxErtek = kerdesek[0].HelyesValasz;
            for (int i = 1; i < kerdesek.Count; i++)
            {
                if (minErtek > kerdesek[i].HelyesValasz)
                {
                    minErtek = kerdesek[i].HelyesValasz;
                }
                if (maxErtek < kerdesek[i].HelyesValasz)
                {
                    maxErtek = kerdesek[i].HelyesValasz;
                }
            }
            lbl4.Content = string.Format("A legmagasabb érték a válaszok között: {0}\n" +
                                         "A legalacsonyabb érték a válaszok között: {1}", maxErtek, minErtek);
        }

        private void Feladat5()
        {
            HashSet<string> kategoriak = new HashSet<string>();
            foreach (Kerdes k in kerdesek)
            {
                kategoriak.Add(k.Kategoria);
            }

            foreach (string k in kategoriak)
            {
                cbo6.Items.Add(k);
            }

        }

        private void cbo6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Random rand = new Random();
            List<Kerdes> kategoriaKerdesek = new List<Kerdes>();
            foreach (Kerdes k in kerdesek)
            {
                if (k.Kategoria == cbo6.SelectedItem.ToString())
                    kategoriaKerdesek.Add(k);
            }
            int index = rand.Next(kategoriaKerdesek.Count);  //0 <= index < kategoriaKerdesek.Count
            this.kerdes6 = kategoriaKerdesek[index];
            lbl6.Content = this.kerdes6.KerdesSzovege;
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            if (txt6.Text == this.kerdes6.HelyesValasz.ToString())
            {
                MessageBox.Show(string.Format("Pontszám: {0}", this.kerdes6.Pontszam));
            }
            else
            {
                MessageBox.Show(string.Format("Pontszám: 0\nA helyes válasz: {0}", this.kerdes6.HelyesValasz));
            }
        }

        private void Feladat7()
        {
            Random rand = new Random();
            HashSet<int> indexek = new HashSet<int>();
            do
            {
                indexek.Add(rand.Next(kerdesek.Count));
            }
            while (indexek.Count < 10 && indexek.Count < kerdesek.Count);

            int osszPontszam = 0;
            foreach (int index in indexek)
            {
                osszPontszam += kerdesek[index].Pontszam;
            }

            StreamWriter sw = new StreamWriter("tesztfel.txt");
            foreach (int index in indexek)
            {
                sw.WriteLine("{0} {1} {2}",
                             kerdesek[index].Pontszam,
                             kerdesek[index].HelyesValasz,
                             kerdesek[index].KerdesSzovege);
            }
            sw.WriteLine("A feladatsorra összesen {0} pont adható", osszPontszam);
            sw.Close();

        }

    }
}

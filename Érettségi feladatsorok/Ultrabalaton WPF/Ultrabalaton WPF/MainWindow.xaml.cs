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

namespace Ultrabalaton_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Eredmeny> eredmenyek = new List<Eredmeny>();
        public MainWindow()
        {
            InitializeComponent();
            Feladat2();
            Feladat3();
            Feladat4();
            Feladat7();
            Feladat8();
        }

        private void Feladat2()
        {
            StreamReader sr = new StreamReader("ub2017egyeni.txt");
            sr.ReadLine();
            while (!sr.EndOfStream)
                eredmenyek.Add(new Eredmeny(sr.ReadLine()));
            sr.Close();
        }

        private void Feladat3()
        {
            lbl3.Content = $"{eredmenyek.Count} fő";
        }

        private void Feladat4()
        {
            int db = 0;
            foreach (var eredmeny in eredmenyek)
                if (eredmeny.Kategoria == "Noi" && eredmeny.TavSzazalek == 100)
                    db++;
            lbl4.Content = $"{db} fő";
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            while (i < eredmenyek.Count && eredmenyek[i].Versenyzo != txt5.Text)
                i++;
            if (i < eredmenyek.Count)
            {
                lbl5.Content = eredmenyek[i].TavSzazalek == 100 ? "Igen" : "Nem";
            }
            else
            {
                MessageBox.Show("Ilyen nevű versenyző nem nevezett");
                lbl5.Content = "";
            }
        }

        private void Feladat7()
        {
            int db = 0;
            double ossz = 0;
            foreach (var eredmeny in eredmenyek)
                if (eredmeny.Kategoria == "Ferfi" && eredmeny.TavSzazalek == 100)
                {
                    db++;
                    ossz += eredmeny.IdőÓrában();
                }
            lbl7.Content = $"{ossz / db:f2} óra";
        }

        private void Feladat8()
        {
            HashSet<string> kategoriak = new HashSet<string>();
            foreach (var eredmeny in eredmenyek)
                kategoriak.Add(eredmeny.Kategoria);

            foreach (var kategoria in kategoriak)
                cbo8.Items.Add(kategoria);
        }

        private void cbo8_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int minIndex = -1;
            double minErtek = double.MaxValue;
            for (int i = 0; i < eredmenyek.Count; i++)
            {
                if (eredmenyek[i].Kategoria == cbo8.SelectedValue.ToString() && eredmenyek[i].TavSzazalek == 100)
                {
                    if (minErtek > eredmenyek[i].IdőÓrában())
                    {
                        minErtek = eredmenyek[i].IdőÓrában();
                        minIndex = i;
                    }
                }
            }
            lbl8.Content = $"{eredmenyek[minIndex].Versenyzo} ({eredmenyek[minIndex].Rajtszam}.) - {eredmenyek[minIndex].Versenyido}";
        }
    }
}

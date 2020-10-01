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

namespace FociWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Merkozes> merkozesek = new List<Merkozes>();

        public MainWindow()
        {
            InitializeComponent();
            Feladat1();
            Feladat3();
            Feladat7();
        }

        private void Feladat1()
        {
            File.ReadAllLines("meccs.txt").Skip(1).ToList().ForEach(m => merkozesek.Add(new Merkozes(m)));
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            var m = merkozesek.Where(m => m.Fordulo.ToString() == txt2.Text);
            if (m.Count() == 0)
                MessageBox.Show("Ilyen forduló nem volt!");
            else
                lst2.ItemsSource = m.Select(m => m.ToString());
        }

        private void Feladat3()
        {
            lst3.ItemsSource = merkozesek.Where(m => m.HazaiGolVegeredmeny > m.VendegGolVegeredmeny && m.HazaiGolFelido < m.VendegGolFelido ).Select(m => m.HazaiCsapat)
                     .Union(
                               merkozesek.Where(m => m.HazaiGolVegeredmeny < m.VendegGolVegeredmeny && m.HazaiGolFelido > m.VendegGolFelido ).Select(m => m.VendegCsapat)
                           ).Distinct();                                         
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            lbl5.Content = $"Lőtt gólok: {merkozesek.Where(m => m.HazaiCsapat == txt4.Text).Sum(m => m.HazaiGolVegeredmeny) + merkozesek.Where(m => m.VendegCsapat == txt4.Text).Sum(m => m.VendegGolVegeredmeny)} " +
                          $"Kapott gólok:{merkozesek.Where(m => m.HazaiCsapat == txt4.Text).Sum(m => m.VendegGolVegeredmeny) + merkozesek.Where(m => m.VendegCsapat == txt4.Text).Sum(m => m.HazaiGolVegeredmeny)} ";

            int pont = 0;
            foreach (var m in merkozesek)
            {
                if (m.VendegGolVegeredmeny == m.HazaiGolVegeredmeny && (m.HazaiCsapat == txt4.Text || m.VendegCsapat == txt4.Text))
                    pont += 1;
                else if (m.VendegGolVegeredmeny > m.HazaiGolVegeredmeny && m.VendegCsapat == txt4.Text)
                    pont += 3;
                else if (m.VendegGolVegeredmeny < m.HazaiGolVegeredmeny && m.HazaiCsapat == txt4.Text)
                    pont += 3;
            }
            lbl6.Content = $"Csapat pontszáma: {pont} pont";
        }

        private void Feladat7()
        {
            var lines = merkozesek.Select(m => m.HazaiGolVegeredmeny > m.VendegGolVegeredmeny ? $"{m.HazaiGolVegeredmeny}-{m.VendegGolVegeredmeny}" : $"{m.VendegGolVegeredmeny}-{m.HazaiGolVegeredmeny}")
                                  .GroupBy(s => s)
                                  .Select(g => $"{g.Key}: {g.Count()} darab")
                                  .ToArray();
            File.WriteAllLines("stat.txt", lines);
        }

    }
}

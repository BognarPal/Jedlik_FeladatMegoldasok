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
                kerdesek.Add(new Kerdes(sr.ReadLine(), sr.ReadLine()));
            sr.Close();
        }

        private void Feladat2()
        {
            lbl2.Content = $"Az állományban {kerdesek.Count} db kérdés van";
        }

        private void Feladat3()
        {
            int pont1 = kerdesek.Count(k => k.Kategoria == "matematika" && k.Pont == 1);
            int pont2 = kerdesek.Count(k => k.Kategoria == "matematika" && k.Pont == 2);
            int pont3 = kerdesek.Count(k => k.Kategoria == "matematika" && k.Pont == 3);
            lbl3.Content = $"Az adatfajlban {pont1 + pont2 + pont3} matematika feladat van.\n" +
                           $"1 pontot ér {pont1} feladat\n" +
                           $"2 pontot ér {pont2} feladat\n" +
                           $"3 pontot ér {pont3} feladat\n";
        }

        private void Feladat4()
        {
            lbl4.Content = $"A legmagasabb érték a válaszok között: {kerdesek.Min(k => k.Valasz)}\n" +
                           $"A legalacsonyabb érték a válaszok között: {kerdesek.Max(k => k.Valasz)}";
        }

        private void Feladat5()
        {
            cbo6.ItemsSource = kerdesek.Select(k => k.Kategoria).Distinct();
            btn6.IsEnabled = false;
            lbl6.Content = "";
        }

        private void cbo6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<int> indexek = kerdesek.Where(k => k.Kategoria == cbo6.SelectedItem.ToString()).Select(k => kerdesek.IndexOf(k)).ToList();
            Random rand = new Random();
            kerdes6 = kerdesek[indexek[rand.Next(indexek.Count)]];
            lbl6.Content = kerdes6.KerdesSzovege;
            btn6.IsEnabled = true;
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            if (txt6.Text == kerdes6.Valasz.ToString())
                MessageBox.Show($"Pontszám: {kerdes6.Pont}");
            else 
                MessageBox.Show($"Pontszám: 0\nA helyes válasz: {kerdes6.Valasz}");
        }

        private void Feladat7()
        {
            HashSet<int> indexek = new HashSet<int>();
            Random rand = new Random();
            while (indexek.Count != 10 && indexek.Count < kerdesek.Count)
                indexek.Add(rand.Next(kerdesek.Count));

            List<string> klist = indexek.Select(i => $"{kerdesek[i].Pont} {kerdesek[i].Valasz} {kerdesek[i].KerdesSzovege}").ToList();
            klist.Add($"A feladatsorra osszesen {indexek.Sum(i => kerdesek[i].Pont)} pont adhato");
            File.WriteAllLines("tesztfel.txt", klist);
        }
    }
}

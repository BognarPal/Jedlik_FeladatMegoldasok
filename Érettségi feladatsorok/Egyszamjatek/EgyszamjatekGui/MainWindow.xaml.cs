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

namespace EgyszamjatekGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Jatekos> jatekosok = new List<Jatekos>();
        int tippekSzama = 0;
        public MainWindow()
        {
            InitializeComponent();
            StreamReader sr = new StreamReader("egyszamjatek2.txt");
            while (!sr.EndOfStream)
            {
                jatekosok.Add(new Jatekos(sr.ReadLine()));
            }
            sr.Close();
        }

        private void txtTippek_TextChanged(object sender, TextChangedEventArgs e)
        {
            string tippek = txtTippek.Text.TrimEnd();
            tippekSzama = tippek.Split(' ').Length;
            lblTippekSzama.Content = string.Format("{0} db", tippekSzama);
        }

        private void btnHozzaadas_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            while (i < jatekosok.Count && jatekosok[i].Nev != txtNev.Text)
            {
                i++;
            }
            if (i < jatekosok.Count )
            {
                MessageBox.Show("Van már ilyen nevű játékos", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (tippekSzama != jatekosok[0].Tippek.Count)
            {
                MessageBox.Show("A tippek száma nem megfelelő", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string sor = string.Format("{0} {1}", txtNev.Text, txtTippek.Text.TrimEnd());
            jatekosok.Add(new Jatekos(sor));
            StreamWriter sw = new StreamWriter("egyszamjatek2.txt", true);
            sw.WriteLine(sor);
            sw.Close();
            MessageBox.Show("Az állomány bővítése sikeres volt", "Üzenet", MessageBoxButton.OK, MessageBoxImage.Information);
            txtNev.Text = "";
            txtTippek.Text = "";
        }
    }
}

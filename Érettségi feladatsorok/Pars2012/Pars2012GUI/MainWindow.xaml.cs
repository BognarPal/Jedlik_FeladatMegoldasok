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

namespace Pars2012GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Versenyző> versenyzok = new List<Versenyző>();

        public MainWindow()
        {
            InitializeComponent();

            StreamReader sr = new StreamReader("Selejtezo2012.txt", Encoding.UTF8);
            sr.ReadLine();
            while (!sr.EndOfStream)
                versenyzok.Add(new Versenyző(sr.ReadLine()));
            sr.Close();

            foreach (var v in versenyzok)
            {
                cboVersenyzo.Items.Add(v.Név);
            }
            cboVersenyzo.SelectedItem = "Pars Krisztián";
        }

        private void cboVersenyzo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nev = cboVersenyzo.SelectedItem.ToString();
            int i = 0;
            while (i < versenyzok.Count && versenyzok[i].Név != nev)
                i++;
            lblCsoport.Content = versenyzok[i].Csoport;
            lblNemzet.Content = versenyzok[i].Nemzet;
            lblKod.Content = versenyzok[i].Kód;
            lblSorozat.Content = versenyzok[i].Sorozat;
            lblEredmeny.Content = versenyzok[i].Eredmény;
            Uri uri = new Uri("Images/" + versenyzok[i].Kód + ".png", UriKind.Relative);
            ImageZászló.Source = new BitmapImage(uri);
        }
    }
}

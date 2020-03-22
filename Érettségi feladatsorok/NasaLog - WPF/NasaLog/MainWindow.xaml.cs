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

namespace NasaLog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Keres> keresek = new List<Keres>();

        public MainWindow()
        {
            InitializeComponent();

            StreamReader sr = new StreamReader("NASAlog.txt");
            while (!sr.EndOfStream)
            {
                keresek.Add(new Keres(sr.ReadLine()));
            }
            sr.Close();

            lbl5.Content = string.Format("Kérések száma: {0}", keresek.Count);

            int osszeg = 0;
            foreach (var k in keresek)
            {
                osszeg += k.ByteMeret;
            }
            lbl6.Content = string.Format("Válaszok összes mérete: {0} bájt", osszeg);

            int db = 0;
            foreach (var k in keresek)
            {
                if (k.Domain())
                {
                    db++;
                }
            }
            lbl8.Content = string.Format("Domain-es kérések száma: {0:f2} %", 100.0 * db / keresek.Count );

            Dictionary<string, int> stat = new Dictionary<string, int>();
            foreach (var k in keresek)
            {
                if (stat.ContainsKey(k.Valaszkod))
                {
                    stat[k.Valaszkod]++;
                }
                else
                {
                    stat.Add(k.Valaszkod, 1);
                }
            }
            foreach (var s in stat)
            {
                lst9.Items.Add(string.Format("{0}: {1} db", s.Key, s.Value));
            }
            lbl10.Content = "";
        }

        private void txt10_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt10.Text == "")
            {
                lbl10.Content = "";
            }
            else
            {
                if (int.TryParse(txt10.Text, out int ora) && ora >= 0 && ora <= 23)
                {
                    int db = 0;
                    foreach (var k in keresek)
                    {
                        if (k.Ora == ora)
                        {
                            db++;
                        }
                    }
                    lbl10.Content = string.Format("{0}:00:00 és {0}:59:59 között {1} kérés érkezett", ora, db);
                    lbl10.Foreground = Brushes.Black;
                }
                else
                {
                    lbl10.Content = "Hibás adat";
                    lbl10.Foreground = Brushes.Red;
                }
            }
        }
    }
}

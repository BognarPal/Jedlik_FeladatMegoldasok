using Microsoft.Win32;
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

namespace ADFGVXgui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBetoltes_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                string[] kodtabla = File.ReadAllLines(dialog.FileName);
                megjelenit(kodtabla);
                lstEredmeny.Items.Clear();
                if (kodtabla.Length != 6)
                {
                    lstEredmeny.Items.Add("Hiba a mátrix méretében!");
                }
                else
                {
                    foreach (string sor in kodtabla)
                    {
                        if (sor.Length != 6)
                        {
                            lstEredmeny.Items.Add("Hiba a mátrix méretében!");
                            break;
                        }
                    }
                }

                foreach (string sor in kodtabla)
                {
                    foreach (char karakter in sor)
                    {
                        if (!(( karakter >= '0' && karakter <= '9') || (karakter >= 'a' && karakter <= 'z')))
                        {
                            lstEredmeny.Items.Add(string.Format("Hibás karakter ({0}) van a mátrixban", karakter));
                        }
                    }
                }

                Dictionary<char, int> karakterek = new Dictionary<char, int>();
                for (char ch = 'a'; ch <= 'z'; ch++)
                {
                    karakterek.Add(ch, 0);
                }
                for (char ch = '0'; ch <= '9'; ch++)
                {
                    karakterek.Add(ch, 0);
                }
                foreach (string sor in kodtabla)
                {
                    foreach (char karakter in sor)
                    {
                        if ((karakter >= '0' && karakter <= '9') || (karakter >= 'a' && karakter <= 'z'))
                        {
                            karakterek[karakter]++;
                        }
                    }
                }

                foreach (var item in karakterek)
                {
                    if (item.Value != 1)
                    {
                        lstEredmeny.Items.Add(string.Format("A(z) {0} karakter {1}x szerepel a mátrixban!", item.Key, item.Value));
                    }
                }

                if (lstEredmeny.Items.Count == 0)
                {
                    lstEredmeny.Items.Add("A mátrix megfelelő!");
                }
            }
        }

        private void megjelenit(string[] kodtabla)
        {
            lblKodtabla.Content = "";
            foreach (string sor in kodtabla)
            {
                foreach (char karakter in sor)
                {
                    lblKodtabla.Content += karakter + "  ";
                }
                lblKodtabla.Content += "\n";
            }
        }
    }
}

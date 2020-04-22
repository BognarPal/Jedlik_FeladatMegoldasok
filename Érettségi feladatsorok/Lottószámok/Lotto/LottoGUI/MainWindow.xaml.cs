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

namespace LottoGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<HetiSzamok> lottoSzamok = new List<HetiSzamok>();

        public MainWindow()
        {
            InitializeComponent();

            StreamReader sr = new StreamReader("lottosz52.txt");
            while (!sr.EndOfStream)
            {
                lottoSzamok.Add(new HetiSzamok(sr.ReadLine()));
            }
            sr.Close();
        }

        private void btn10_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<int, int> stat = new Dictionary<int, int>();
            foreach (var hetiSzam in lottoSzamok)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (stat.ContainsKey(hetiSzam.Szamok[i]))
                    {
                        stat[hetiSzam.Szamok[i]]++;
                    }
                    else
                    {
                        stat.Add(hetiSzam.Szamok[i], 1);
                    }
                }
            }
            foreach (var item in stat)
            {
                lstStat.Items.Add(string.Format("{0}: {1} db", item.Key, item.Value));
            }
        }

        private void btn11_Click(object sender, RoutedEventArgs e)
        {
            List<int> primek = new List<int>() { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89 };

            foreach (var hetiSzam in lottoSzamok)
            {
                for (int i = 0; i < 5; i++)
                {
                    primek.Remove(hetiSzam.Szamok[i]);
                }
            }

            foreach (var prim in primek)
            {
                lstPrim.Items.Add(prim);
            }
        }
    }
}

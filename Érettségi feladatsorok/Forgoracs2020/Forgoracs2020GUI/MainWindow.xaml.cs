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
using System.Windows.Documents.Serialization;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Forgoracs2020GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int ablakokSzama = 0;
        CheckBox[,] matrix = new CheckBox[8, 8];
        public MainWindow()
        {
            InitializeComponent();
            KodlemezRajzol();
        }

        private void KodlemezRajzol()
        {
            StreamReader sr = new StreamReader("kodlemez.txt");
            for (int sor = 0; sor <= 7; sor++)
            {
                string kodsor = sr.ReadLine();
                for (int oszlop = 0; oszlop <= 7; oszlop++)
                {
                    CheckBox ch = new CheckBox();
                    Canvas.SetLeft(ch, oszlop * 30 + 10);
                    Canvas.SetTop(ch, sor * 30 + 50);
                    if (kodsor[oszlop] == '#')
                    {
                        ch.IsChecked = true;
                    }
                    else
                    {
                        ablakokSzama++;
                    }
                    ch.Click += Ch_Click;
                    canvas.Children.Add(ch);
                    matrix[sor, oszlop] = ch;
                }
            }
            sr.Close();
            Title = string.Format("GUI - {0}", ablakokSzama);
        }

        private void Ch_Click(object sender, RoutedEventArgs e)
        {
            CheckBox ch = (CheckBox)sender;
            if (ch.IsChecked == true)
            {
                ablakokSzama--;
            }
            else
            {
                ablakokSzama++;
            }
            Title = string.Format("GUI - {0}", ablakokSzama);
            btnMentes.IsEnabled = ablakokSzama == 16;
        }

        private void btnMentes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StreamWriter sw = new StreamWriter("kodlemezNEW.txt");
                for (int sor = 0; sor <= 7; sor++)
                {
                    for (int oszlop = 0; oszlop <= 7; oszlop++)
                    {
                        sw.Write(matrix[sor, oszlop].IsChecked == true ? '#' : 'A');
                    }
                    sw.WriteLine();
                }

                sw.Close();
                MessageBox.Show("Sikeres mentés!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

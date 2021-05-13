using System;
using System.Collections.Generic;
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

namespace KaracsonyGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int keszlet = 0;

        public MainWindow()
        {
            InitializeComponent();
            for (int i = 1; i <= 40; i++)
            {
                cboNap.Items.Add(i);
            }
        }

        private void btnHozzaad_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(txtElkeszitett.Text) < 0 || int.Parse(txtEladott.Text) < 0)
            {
                lblHiba.Content = "Negatív számot nem adhat meg!";
                return;
            }
            if (keszlet + int.Parse(txtElkeszitett.Text) < int.Parse(txtEladott.Text))
            {
                lblHiba.Content = "Túl sok az eladott angyalka";
                return;
            }
            lblHiba.Content = "";
            keszlet += int.Parse(txtElkeszitett.Text);
            keszlet -= int.Parse(txtEladott.Text);

            string sor = string.Format("{0}.nap\t+{1}\t-{2}\t=\t{3}", cboNap.SelectedItem.ToString(),
                                                                      txtElkeszitett.Text,
                                                                      txtEladott.Text,
                                                                      keszlet);
            lstInfo.Items.Add(sor);
            txtEladott.Text = "0";
            txtElkeszitett.Text = "0";
            cboNap.SelectedIndex = -1;
        }
    }
}

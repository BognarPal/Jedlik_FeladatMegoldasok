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

namespace playfairGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string megengedettKarakterek = "WERTZUIOPASDFGHJKLYXCVBNM";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var ertek = TextBox.Text;
            string[] karakterParok = ertek.Split(' ');
            int i = 0;
            while (i < karakterParok.Length && karakterParok[i].Length == 2)
            {
                i++;
            }
            if (i < karakterParok.Length)
            {
                Label.Foreground = Brushes.Red;
            }    
            else
            {
                foreach (var kp in karakterParok)
                {
                    if (!megengedettKarakterek.Contains(kp[0]) || !megengedettKarakterek.Contains(kp[1]))
                    {
                        Label.Foreground = Brushes.Blue;
                        return;
                    }
                }

                foreach (var kp in karakterParok)
                {
                    if (kp[0] == kp[1])
                    {
                        Label.Foreground = Brushes.Magenta;
                        return;
                    }
                }

                Label.Foreground = Brushes.Green;
            }
        }
    }
}

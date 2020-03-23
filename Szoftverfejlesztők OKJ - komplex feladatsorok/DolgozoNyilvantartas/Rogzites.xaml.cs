using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DolgozoNyilvantartas
{
    /// <summary>
    /// Interaction logic for Rogzites.xaml
    /// </summary>
    public partial class Rogzites : Window
    {
        public Rogzites(Dolgozo dolgozo)
        {
            InitializeComponent();
            Loaded += Rogzites_Loaded;
            this.DataContext = dolgozo;
        }

        private void Rogzites_Loaded(object sender, RoutedEventArgs e)
        {
            cboSzervEgys.ItemsSource = MainWindow.AppDbContext.SzervezetiEgysegek.Local.ToObservableCollection();
            cboSzervEgys.DisplayMemberPath = "Nev";
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Dolgozo dolgozo = (Dolgozo)this.DataContext;
            if (dolgozo.AdoazonositoJel.Length != 10 ) //TODO ellenőrzés: adóazon jel csak számjegyekből áll
            {
                txtAdoAzonJel.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(dolgozo.Nev))
            {
                txtNev.Focus();
                return;
            }
            if (Validation.GetHasError(txtSzabadsag))
            {
                txtSzabadsag.Focus();
                return;
            }
            if (dolgozo.SzervezetiEgyseg == null)
            {
                cboSzervEgys.IsDropDownOpen = true;
                return;
            }
            this.DialogResult = true;
            this.Close();
        }

        private void btnMegsem_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}

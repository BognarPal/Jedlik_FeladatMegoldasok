using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DolgozoNyilvantartas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ApplicationDbContext AppDbContext { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AppDbContext = new ApplicationDbContext();
            AppDbContext.SzervezetiEgysegek.Load();
            cboSzervezetiEgyseg.ItemsSource = AppDbContext.SzervezetiEgysegek.Local.ToObservableCollection();
        }

        private void btnKeres_Click(object sender, RoutedEventArgs e)
        {
            var lista = AppDbContext.Dolgozok.Where(d =>
               (d.AdoazonositoJel == txtAdoazonJel.Text || string.IsNullOrWhiteSpace(txtAdoazonJel.Text))
               &&
               (d.SzervezetiEgyseg == cboSzervezetiEgyseg.SelectedItem || cboSzervezetiEgyseg.SelectedItem == null)
            );
            dgLista.ItemsSource = new ObservableCollection<Dolgozo>(lista);
        }

        private void btnFeltTorlese_Click(object sender, RoutedEventArgs e)
        {
            txtAdoazonJel.Text = "";
            cboSzervezetiEgyseg.SelectedItem = null;
            btnKeres_Click(null, null);
        }

        private void btnUj_Click(object sender, RoutedEventArgs e)
        {
            Dolgozo dolgozo = new Dolgozo();
            Rogzites rogzites = new Rogzites(dolgozo);
            if (rogzites.ShowDialog() == true)
            {
                AppDbContext.Dolgozok.Add(dolgozo);
                AppDbContext.SaveChanges();
                btnKeres_Click(null, null);
            }

        }

        private void btnModositas_Click(object sender, RoutedEventArgs e)
        {
            if (dgLista.SelectedItem != null )
            {
                Dolgozo dolgozo = (Dolgozo)dgLista.SelectedItem;
                Rogzites rogzites = new Rogzites(dolgozo);
                
                if (rogzites.ShowDialog() == true)
                {
                    AppDbContext.Entry(dolgozo).State = EntityState.Modified;
                    AppDbContext.SaveChanges();
                }
                else
                {
                    AppDbContext.Entry(dolgozo).State = EntityState.Unchanged;
                    dgLista.Items.Refresh();
                }
            }
        }

        private void btnTorles_Click(object sender, RoutedEventArgs e)
        {
            if (dgLista.SelectedItem != null)
            {
                if (MessageBox.Show("Biztosan törölni szeretné a kijelölt dolgozót?", "Hiba", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Dolgozo dolgozo = (Dolgozo)dgLista.SelectedItem;
                    AppDbContext.Dolgozok.Remove(dolgozo);
                    AppDbContext.SaveChanges();
                    btnKeres_Click(null, null);
                }
            }
        }

       
    }
}

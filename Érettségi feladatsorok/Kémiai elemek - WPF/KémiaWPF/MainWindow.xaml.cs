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

namespace KémiaWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<KemiaiElem> elemek = new List<KemiaiElem>();
        public MainWindow()
        {
            InitializeComponent();

            //2.feladat
            File.ReadLines("felfedezesek.csv", Encoding.UTF8).Skip(1).ToList().ForEach(s => elemek.Add(new KemiaiElem(s)));

            //3.feladat
            lbl3.Content = elemek.Count;

            //4.feladat
            lbl4.Content = elemek.Count(e => e.Ev == "Ókor");

            //7.feladat
            int maxErtek = -1;
            for (int i = 0; i < elemek.Count - 1; i++)
            {
                if (int.TryParse(elemek[i].Ev, out int ev1) && int.TryParse(elemek[i+1].Ev, out int ev2))
                {
                    if (ev2 - ev1 > maxErtek)
                        maxErtek = ev2 - ev1;
                }
            }
            lbl7.Content = $"{maxErtek} év volt a leghosszabb időszak két elem felfedezése között";

            //8.feladat
            lst8.ItemsSource = elemek.Where(e => e.Ev != "Ókor").GroupBy(e => e.Ev).Where(g => g.Count() > 3).Select(g => $"{g.Key}: {g.Count()}");

            //9. feladat
            File.WriteAllLines("XX század.txt", elemek.Where(e => e.Ev.StartsWith("19")).Select(e => $"{e.Vegyjel}\t{e.Rendszam}\t{e.Nev}").ToArray(), Encoding.UTF8);

        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            lbl6.Content = "";
            if (txt5.Text.Length == 0)
                return;
            if (txt5.Text.Length > 2 || txt5.Text.ToUpper()[0] < 'A' || txt5.Text.ToUpper()[0] > 'Z' || (txt5.Text.Length > 1 && (txt5.Text.ToUpper()[1] < 'A' || txt5.Text.ToUpper()[1] > 'Z')))
            {                
                MessageBox.Show("Formai hiba");
            }
            else
            {
                var elem = elemek.FirstOrDefault(e => e.Vegyjel.ToUpper() == txt5.Text.ToUpper());
                if (elem == null)
                    lbl6.Content = "Nincs ilyen elem az adatforrásban";
                else
                    lbl6.Content = $"Az elem vegyjele: {elem.Vegyjel} \n" +
                                   $"Az elem neve: {elem.Nev} \n" +
                                   $"Rendszáma: {elem.Rendszam} \n" +
                                   $"Felfedezés éve: {elem.Ev} \n" +
                                   $"Felfedező: {elem.Felfedezo}";
            }
        }
    }
}

using SpeedIO.Klasy;
using SQLite;
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
using System.Windows.Shapes;

namespace SpeedIO.Widoki
{
    /// <summary>
    /// Interaction logic for Wyniki.xaml
    /// </summary>
    public partial class Wyniki : Window
    {
        private SQLiteConnection polaczenieZBaza;

        public Wyniki()
        {
            InitializeComponent();

            string sciezkaBazy = System.IO.Path.Combine("wynikiGry.db");

            polaczenieZBaza = new SQLiteConnection(sciezkaBazy);

            polaczenieZBaza.CreateTable<WynikGry>();

            var wyniki = PobierzWyniki();

            foreach (var wynik in wyniki)
            {
                string wynikTekst = $"{wynik.ImieGracza}: {wynik.CzasReakcji} ({wynik.Milisekundy} ms)";
                WynikiListBox.Items.Add(wynikTekst);
            }
        }
        private List<WynikGry> PobierzWyniki()
        {
            return polaczenieZBaza.Table<WynikGry>().OrderBy(w => w.Milisekundy).ToList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

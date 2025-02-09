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

            // Ścieżka do bazy danych SQLite
            string sciezkaBazy = System.IO.Path.Combine("wynikiGry.db");

            // Inicjalizowanie połączenia z bazą
            polaczenieZBaza = new SQLiteConnection(sciezkaBazy);

            // Tworzenie tabeli, jeśli nie istnieje
            polaczenieZBaza.CreateTable<WynikGry>();

            // Pobierz wyniki z bazy danych
            var wyniki = PobierzWyniki();

            // Dodaj wyniki do ListBox
            foreach (var wynik in wyniki)
            {
                string wynikTekst = $"{wynik.ImieGracza}: {wynik.CzasReakcji} ({wynik.Milisekundy} ms)";
                WynikiListBox.Items.Add(wynikTekst);
            }
        }

        // Funkcja pobierająca wyniki z bazy danych, posortowane rosnąco
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for TestSzybkosci.xaml
    /// </summary>
    public partial class TestSzybkosci : Window
    {
        private string currentWord = "";
    private Stopwatch stopwatch = new Stopwatch();
    private Random random = new Random();
        private string[] words = {
    "komputer", "szybkość", "klawiatura", "programowanie", "internet", "okno", "słowo", "test",
    "myszka", "monitor", "procesor", "pamięć", "serwer", "oprogramowanie", "algorytm", "klucz",
    "hasło", "kodowanie", "debugowanie", "sieć", "aplikacja", "grafika", "system", "dysk",
    "modem", "router", "laptop", "smartfon", "tablet", "drukarka", "bateria", "mikrofon",
    "kamera", "piksel", "rozdzielczość", "synchronizacja", "konfiguracja", "aktualizacja",
    "cyfrowy", "automatyzacja", "sztuczna", "inteligencja", "bazadanych", "firewall",
    "zabezpieczenia", "oprogramowanie", "zapis", "odczyt", "komunikacja", "szyfrowanie",
    "informatyka", "technologia"
};

        public TestSzybkosci()
    {
        InitializeComponent();
    }

    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        GenerateRandomWord();
        WordTextBlock.Text = currentWord;
        UserInputTextBox.Clear();
        ResultTextBlock.Text = "";
        stopwatch.Restart();
    }

    private void CheckButton_Click(object sender, RoutedEventArgs e)
    {
        stopwatch.Stop();
        string userAnswer = UserInputTextBox.Text.Trim();

        if (userAnswer.Equals(currentWord, StringComparison.OrdinalIgnoreCase))
        {
            ResultTextBlock.Text = $"Brawo! Twój czas: {stopwatch.ElapsedMilliseconds} ms";
            ResultTextBlock.Foreground = System.Windows.Media.Brushes.Green;
        }
        else
        {
            ResultTextBlock.Text = $"Błąd! Poprawne słowo: {currentWord}";
            ResultTextBlock.Foreground = System.Windows.Media.Brushes.Red;
        }
    }

    private void GenerateRandomWord()
    {
        currentWord = words[random.Next(words.Length)];
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
            WyborOpcji wyborOpcji = new WyborOpcji();
            wyborOpcji.Show();
        this.Close();
        
    }
}
}

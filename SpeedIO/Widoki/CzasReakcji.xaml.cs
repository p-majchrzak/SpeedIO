using SpeedIO.Klasy;
using SQLite;
using System;
using System.IO;
using System.IO.Ports;
using System.Windows;

namespace SpeedIO.Widoki
{

    public partial class CzasReakcji : Window
    {
        private SerialPort serialPort;  // Obiekt portu szeregowego
        private bool isGameRunning = false;  // Flaga informująca, czy gra jest aktywna
        private string playerName;  // Imię gracza
        private SQLiteConnection polaczenieZBaza;
        public CzasReakcji()
        {
            InitializeComponent();

            string sciezkaBazy = Path.Combine("wynikiGry.db");

            // Inicjalizowanie połączenia z bazą
            polaczenieZBaza = new SQLiteConnection(sciezkaBazy);

            // Tworzenie tabeli, jeśli nie istnieje
            polaczenieZBaza.CreateTable<WynikGry>();

            serialPort = new SerialPort("COM5", 9600);  // Wybierz odpowiedni port szeregowy
            serialPort.DataReceived += SerialPort_DataReceived;  // Dodanie obsługi zdarzenia odbierania danych
        }

        // Obsługuje kliknięcie przycisku "Rozpocznij Grę"

        private void ZapiszWynik(string czasReakcji, int milisekundy)
        {
            // Tworzymy nowy obiekt reprezentujący wynik gry
            var wynikGry = new WynikGry
            {
                ImieGracza = PlayerNameTextBox.Text,  // Imię gracza
                CzasReakcji = czasReakcji,  // Czas reakcji w formie tekstowej
                Milisekundy = milisekundy  // Czas reakcji w milisekundach
            };

            // Wstawienie rekordu do bazy danych
            polaczenieZBaza.Insert(wynikGry);

            // Możesz dodać jakąś informację do UI, że zapisano wynik (jeśli chcesz)
            MessageBox.Show("Wynik został zapisany do bazy danych.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            playerName = PlayerNameTextBox.Text.Trim();  // Pobieramy imię gracza z pola tekstowego
            if (string.IsNullOrEmpty(playerName))
            {
                MessageBox.Show("Proszę wpisać swoje imię.", "Błąd wejściowy", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!serialPort.IsOpen)
            {
                serialPort.Open();  // Otwórz port szeregowy
                ResultTextBlock.Text = "Oczekiwanie na wynik gry...";
                StartButton.IsEnabled = false;  // Zablokuj przycisk "Rozpocznij Grę"
                StopButton.IsEnabled = true;  // Włącz przycisk "Zatrzymaj Grę"
                isGameRunning = true;  // Ustaw flagę na true, by gra była aktywna
                serialPort.WriteLine("START");  // Wyślij komendę "START" do Arduino
            }
        }

        // Obsługuje kliknięcie przycisku "Zatrzymaj Grę"
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (isGameRunning)
            {
                serialPort.WriteLine("STOP");  // Wysyłamy komendę do zatrzymania gry na Arduino
                StopButton.IsEnabled = false;  // Zablokuj przycisk "Zatrzymaj Grę"
                ResultTextBlock.Text = "Gra zatrzymana.";  // Wyświetl komunikat
                StartButton.IsEnabled = true;  // Włącz przycisk "Rozpocznij Grę"
                isGameRunning = false;  // Zatrzymaj grę
            }
        }

        // Obsługuje odbieranie danych z portu szeregowego (z Arduino)
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string wiadomosc = serialPort.ReadLine();  // Odczytujemy linię danych z portu szeregowego
            Dispatcher.Invoke(() =>
            {
                if (int.TryParse(wiadomosc.Trim(), out int czasReakcji))
                {
                    ResultTextBlock.Text = $"Czas reakcji: {czasReakcji} ms";  // Wyświetlamy czas reakcji
                    ZapiszWynik(czasReakcji.ToString() + " ms", czasReakcji);  // Zapisujemy wynik w bazie danych
                }
                else if (wiadomosc.Contains("Za wczesnie"))
                {
                    ResultTextBlock.Text = "Za wcześnie! Spróbuj ponownie.";  // Wyświetlamy komunikat o błędzie
                    ZapiszWynik("Za wcześnie", 0);  // Zapisujemy wynik "Za wcześnie"
                }
            });
        }

        private void PokazWyniki_Click(object sender, RoutedEventArgs e)
        {
            Wyniki wyniki = new Wyniki();
            wyniki.ShowDialog();
        }
    }
}

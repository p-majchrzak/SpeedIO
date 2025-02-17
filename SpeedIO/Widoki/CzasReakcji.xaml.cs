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
        private SerialPort serialPort;  
        private bool isGameRunning = false; 
        private string playerName; 
        private SQLiteConnection polaczenieZBaza;
        public CzasReakcji()
        {
            InitializeComponent();

            string sciezkaBazy = Path.Combine("wynikiGry.db");

            polaczenieZBaza = new SQLiteConnection(sciezkaBazy);

            polaczenieZBaza.CreateTable<WynikGry>();

            serialPort = new SerialPort("COM5", 9600); 
            serialPort.DataReceived += SerialPort_DataReceived;  
        }
        private void ZapiszWynik(string czasReakcji, int milisekundy)
        {
            var wynikGry = new WynikGry
            {
                ImieGracza = PlayerNameTextBox.Text, 
                CzasReakcji = czasReakcji, 
                Milisekundy = milisekundy 
            };

            polaczenieZBaza.Insert(wynikGry);

            MessageBox.Show("Uzyskano wynik!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            playerName = PlayerNameTextBox.Text.Trim();  
            if (string.IsNullOrEmpty(playerName))
            {
                MessageBox.Show("Proszę wpisać swoje imię.", "Błąd wejściowy", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!serialPort.IsOpen)
            {
                try
                {
                    serialPort.Open();
                    ResultTextBlock.Text = "Oczekiwanie na wynik gry...";
                    StartButton.IsEnabled = false;
                    StopButton.IsEnabled = true;
                    isGameRunning = true;
                    serialPort.WriteLine("START");
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Nie udało się otworzyć portu szeregowego. Sprawdź, czy urządzenie jest podłączone.", "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (IOException)
                {
                    MessageBox.Show("Błąd w komunikacji z portem szeregowym. Sprawdź, czy urządzenie jest prawidłowo podłączone.", "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (isGameRunning)
            {
                serialPort.WriteLine("STOP");  
                StopButton.IsEnabled = false; 
                ResultTextBlock.Text = "Gra zatrzymana."; 
                StartButton.IsEnabled = true;  
                isGameRunning = false; 
            }
        }
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string wiadomosc = serialPort.ReadLine(); 
            Dispatcher.Invoke(() =>
            {
                if (int.TryParse(wiadomosc.Trim(), out int czasReakcji))
                {
                    ResultTextBlock.Text = $"Czas reakcji: {czasReakcji} ms"; 
                    ZapiszWynik(czasReakcji.ToString() + " ms", czasReakcji); 
                }
                else if (wiadomosc.Contains("Za wczesnie"))
                {
                    ResultTextBlock.Text = "Za wcześnie! Spróbuj ponownie."; 
                    ZapiszWynik("Za wcześnie", 0);  
                }
            });
        }
        private void Powrot_Click(object sender, RoutedEventArgs e)
        {
            WyborOpcji wyborOpcji = new WyborOpcji();
            wyborOpcji.Show();
            Close();
        }
    }
}

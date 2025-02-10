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
using System.Windows.Threading;

namespace SpeedIO.Widoki
{
    /// <summary>
    /// Interaction logic for Zapamietywanie.xaml
    /// </summary>
    public partial class Zapamietywanie : Window
    {
        private string currentSequence = ""; // Przechowywana sekwencja do zapamiętania
        private Random random = new Random();
        private DispatcherTimer timer;
        private int displayTime = 3; // Czas wyświetlania sekwencji w sekundach

        public Zapamietywanie()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateRandomSequence();
            SequenceTextBlock.Text = currentSequence;
            UserInputTextBox.Clear();
            ResultTextBlock.Text = "";

            // Uruchamiamy timer do ukrycia sekwencji
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(displayTime);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Ukrywamy sekwencję po upływie czasu
            SequenceTextBlock.Text = "*****";
            timer.Stop();
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            string userAnswer = UserInputTextBox.Text.Trim();

            if (userAnswer.Equals(currentSequence, StringComparison.OrdinalIgnoreCase))
            {
                ResultTextBlock.Text = "Brawo! Prawidłowo zapamiętana sekwencja.";
                ResultTextBlock.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                ResultTextBlock.Text = $"Błąd! Poprawna sekwencja: {currentSequence}";
                ResultTextBlock.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void GenerateRandomSequence()
        {
            int length = 6; // Długość sekwencji do zapamiętania
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                sb.Append((char)random.Next(65, 91)); // Losowa litera A-Z
            }

            currentSequence = sb.ToString();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            WyborOpcji wyborOpcji = new WyborOpcji();
            wyborOpcji.Show();
            this.Close(); // Zamykamy okno i wracamy do poprzedniego widoku
        }
    }
}


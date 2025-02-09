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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpeedIO.Widoki
{
    /// <summary>
    /// Interaction logic for StronaLadowania.xaml
    /// </summary>
    public partial class StronaLadowania : Window
    {
        public StronaLadowania()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animmacja = new DoubleAnimation();
            animmacja.From = pasekLadowania.Value;
            animmacja.To = 100;
            animmacja.Duration = TimeSpan.FromSeconds(3);
            animmacja.Completed += Zakonczono;
            pasekLadowania.BeginAnimation(ProgressBar.ValueProperty, animmacja);
        }
        private void Zakonczono(object sender, EventArgs e)
        {
            StronaGlowna sg = new StronaGlowna();
            sg.Show();
            Close();
        }
    }
}

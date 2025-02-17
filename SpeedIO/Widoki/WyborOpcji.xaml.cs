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
    /// Interaction logic for WyborOpcji.xaml
    /// </summary>
    public partial class WyborOpcji : Window
    {
        public WyborOpcji()
        {
            InitializeComponent();
        }
        private void Game1Button_Click(object sender, RoutedEventArgs e)
        {
            CzasReakcji czasReakcji = new CzasReakcji();
            czasReakcji.Show();
            Close();
        }
        private void Game2Button_Click(object sender, RoutedEventArgs e)
        {
            Zapamietywanie zapamietywanie = new Zapamietywanie();
            zapamietywanie.Show();
            Close();
        }
        private void Game3Button_Click(object sender, RoutedEventArgs e)
        {
            TestSzybkosci testSzybkosci = new TestSzybkosci();
            testSzybkosci.Show();
            Close();
        }
        private void StatsButton_Click(object sender, RoutedEventArgs e)
        {
            Wyniki wyniki = new Wyniki();
            wyniki.ShowDialog();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            StronaGlowna stronaGlowna = new StronaGlowna();
            stronaGlowna.Show();
            Close();
        }
    }
}

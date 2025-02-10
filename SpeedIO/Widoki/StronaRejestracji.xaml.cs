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
    /// Interaction logic for StronaRejestracji.xaml
    /// </summary>
    public partial class StronaRejestracji : Window
    {
        public StronaRejestracji()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StronaLogowania stronaLogowania = new StronaLogowania();
            stronaLogowania.Show();
            Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            StronaGlowna stronaGlowna = new StronaGlowna();
            stronaGlowna.Show();
            Close();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

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
    /// Interaction logic for StronaLogowania.xaml
    /// </summary>
    public partial class StronaLogowania : Window
    {
        public StronaLogowania()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            StronaGlowna stronaGlowna = new StronaGlowna();
            stronaGlowna.Show();
            Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            WyborOpcji wyborOpcji = new WyborOpcji();
            wyborOpcji.Show();
            Close();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StronaRejestracji stronaRejestracji = new StronaRejestracji();
            stronaRejestracji.Show();
            Close();
        }
    }
}

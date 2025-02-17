using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace SpeedIO.Widoki
{
    public partial class StronaGlowna : Window
    {
        public StronaGlowna()
        {
            InitializeComponent();
        }
        private void Regulamin_Click(object sender, RoutedEventArgs e)
        {
            Regulamin regulamin = new Regulamin();
            regulamin.ShowDialog();
        }
        private void Rozpocznij_Click(object sender, RoutedEventArgs e)
        {
            WyborOpcji wyborOpcji = new WyborOpcji();
            wyborOpcji.Show();
            Close();
        }
    }
}

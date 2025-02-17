using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace SpeedIO.Widoki
{
    public partial class StronaGlowna : Window
    {
        private Random _random = new Random();
        private DispatcherTimer _timer;
        private DispatcherTimer _closeTimer;
        private DispatcherTimer _explosionTimer;
        private List<Zegnaj> _windowList = new List<Zegnaj>();
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

        private void Uciekninier_Click(object sender, RoutedEventArgs e)
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(50); 
            _timer.Tick += Timer_Tick;
            _timer.Start();
            _explosionTimer = new DispatcherTimer();
            _explosionTimer.Interval = TimeSpan.FromMilliseconds(500); 
            _explosionTimer.Tick += ExplosionTimer_Tick;
            _explosionTimer.Start();
            _closeTimer = new DispatcherTimer();
            _closeTimer.Interval = TimeSpan.FromSeconds(5);
            _closeTimer.Tick += (s, args) =>
            {
                _closeTimer.Stop();
                _timer.Stop();
                _explosionTimer.Stop();
                foreach (var window in _windowList)
                {
                    window.Close();
                }
                this.Close(); 
            };
            _closeTimer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double randomX = _random.NextDouble() * (screenWidth - this.Width); 
            double randomY = _random.NextDouble() * (screenHeight - this.Height);
            var newWindow = new Zegnaj();
            newWindow.Left = randomX;
            newWindow.Top = randomY;
            newWindow.Show();
            _windowList.Add(newWindow); 
            MoveWindowInRandomDirection(newWindow);
        }
        private void MoveWindowInRandomDirection(Zegnaj window)
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double randomX = _random.NextDouble() * (screenWidth - window.Width);
            double randomY = _random.NextDouble() * (screenHeight - window.Height); 
            var moveAnimationX = new DoubleAnimation
            {
                To = randomX,
                Duration = TimeSpan.FromMilliseconds(100),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };
            var moveAnimationY = new DoubleAnimation
            {
                To = randomY,
                Duration = TimeSpan.FromMilliseconds(100),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };
            window.BeginAnimation(Window.LeftProperty, moveAnimationX);
            window.BeginAnimation(Window.TopProperty, moveAnimationY);
        }

        private void ExplosionTimer_Tick(object sender, EventArgs e)
        {
            var opacityAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(200),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            var scaleAnimationX = new DoubleAnimation
            {
                From = 1,
                To = 2,
                Duration = TimeSpan.FromMilliseconds(200),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            var scaleAnimationY = new DoubleAnimation
            {
                From = 1,
                To = 2,
                Duration = TimeSpan.FromMilliseconds(200),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            var scaleTransform = new ScaleTransform();
            this.RenderTransform = scaleTransform;
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimationX);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimationY);
            this.BeginAnimation(OpacityProperty, opacityAnimation);
        }
    }
}
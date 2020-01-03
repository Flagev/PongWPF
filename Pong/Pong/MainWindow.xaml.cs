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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;

namespace Pong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Ball ball;
        public MainWindow()
        {
            InitializeComponent();
            Events();
            ball = new Ball(30);
            ;
        }
        public void Events()
        {
            //EventManager - obsluga przyciskow z klawiatury
            EventManager.RegisterClassHandler(typeof(Window),
            Keyboard.KeyUpEvent, new KeyEventHandler(KeyUpEventH), true);
            EventManager.RegisterClassHandler(typeof(Window),
            Keyboard.KeyDownEvent, new KeyEventHandler(KeyDownEventH), true);
            //Timer - cykliczne wykonywanie
            Timer myTimer = new Timer();
            myTimer.Elapsed += new ElapsedEventHandler(ElapsedEventH);
            myTimer.Interval = 10; // 1000 ms is one second
            myTimer.Start();
        }
        public void ElapsedEventH (object source, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
  
            Thickness ballMargin = ballImg.Margin;
            ball.CalcPos();
            ballMargin.Left = ball.x;
            //ballMargin.Top = ball.x;
            ballImg.Margin = ballMargin;

            });
        }

        private void BStart_Click(object sender, RoutedEventArgs e)
        {
            bStart.Content = "START";
            ball.Start();
            scoreBoard.Text = ball.xSpeed.ToString();
        }
        private void KeyUpEventH(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemComma)
            {
                MessageBox.Show("YAY!!!");
            }
        }
        private void KeyDownEventH(object sender, KeyEventArgs e)
        {
            int moveDistance = 5;
            Thickness rightPalletMargin = rightPalletImg.Margin;
            Thickness leftPalletMargin = leftPalletImg.Margin;
            scoreBoard.Text = rightPalletMargin.Top.ToString();
            if (Keyboard.IsKeyDown(Key.Up))
            {
                rightPalletMargin.Top -= moveDistance;
                if ((rightPalletMargin.Top - rightPalletImg.Height) < -1*mainGrid.ActualHeight)
                {
                    rightPalletMargin.Top = -1*mainGrid.ActualHeight + rightPalletImg.Height;
                }
            }
            if (Keyboard.IsKeyDown(Key.Down))
            {
                rightPalletMargin.Top += moveDistance;
                if ((rightPalletMargin.Top + rightPalletImg.Height) > mainGrid.ActualHeight)
                {
                    rightPalletMargin.Top = mainGrid.ActualHeight - rightPalletImg.Height;
                }
            }
            if (Keyboard.IsKeyDown(Key.W))
            {
                leftPalletMargin.Top -= moveDistance;
                if ((leftPalletMargin.Top - leftPalletImg.Height) < -1 * mainGrid.ActualHeight)
                {
                    leftPalletMargin.Top = -1 * mainGrid.ActualHeight + leftPalletImg.Height;
                }
            }
            if (Keyboard.IsKeyDown(Key.S) && leftPalletMargin.Top + moveDistance + leftPalletImg.Height <= mainGrid.ActualHeight)
            {
                leftPalletMargin.Top += moveDistance;
            }
            rightPalletImg.Margin = rightPalletMargin;
            leftPalletImg.Margin = leftPalletMargin;
        }
    }
}


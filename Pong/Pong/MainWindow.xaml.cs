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
        Pallet rightPallet;
        Pallet leftPallet;
        public MainWindow()
        {
            InitializeComponent();
            Events();
            ball = new Ball(ballImg.Width,20);
            rightPallet = new Pallet(rightPalletImg.Margin.Left);
            leftPallet = new Pallet(leftPalletImg.Margin.Left);
            Pallet.maxY = Math.Abs(mainGrid.Height);
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
        public void ElapsedEventH(object source, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                //Obliczanie wspolrzedncych pilki
                ball.CalcPos();
                //Sprawdzanie czy pilka dotyka paletki
                ball.CheckPalletTouched(rightPallet);
                ball.CheckPalletTouched(leftPallet);
                ball.CheckBoundary();
                //Aktualizacja elementow WPF
                Thickness ballMargin = ballImg.Margin;
                Thickness rightPalletMargin = rightPalletImg.Margin;
                Thickness leftPalletMargin = leftPalletImg.Margin;
                ballMargin.Left = ball.x;
                ballMargin.Top = ball.y;
                ballImg.Margin = ballMargin;
                rightPalletMargin.Left = rightPallet.x;
                rightPalletMargin.Top = rightPallet.y;
                rightPalletImg.Margin = rightPalletMargin;
                leftPalletMargin.Left = leftPallet.x;
                leftPalletMargin.Top = leftPallet.y;
                leftPalletImg.Margin = leftPalletMargin;
                //Wyswietlanie wspolrzednych w polach tesktowych
                ballX.Text = ball.x.ToString("N1");
                ballY.Text = ball.y.ToString("N1");
                leftPalletX.Text = leftPallet.x.ToString("N1");
                leftPalletY.Text = leftPallet.y.ToString("N1");
                rightPalletX.Text = rightPallet.x.ToString("N1");
                rightPalletY.Text = rightPallet.y.ToString("N1");
                leftPalletDistance.Text = leftPallet.ballDistance.ToString("N1");
                rightPalletDistance.Text = rightPallet.ballDistance.ToString("N1");
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
            if (Keyboard.IsKeyDown(Key.Up))
            {
                rightPallet.MoveDown();
            }
            if (Keyboard.IsKeyDown(Key.Down))
            {
                rightPallet.MoveUp();
            }
            if (Keyboard.IsKeyDown(Key.W))
            {
                leftPallet.MoveDown();
            }
            if (Keyboard.IsKeyDown(Key.S))
            {
                leftPallet.MoveUp();
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            // close all active threads
            Environment.Exit(0);
        }
    }
}


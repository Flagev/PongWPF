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
        Match match;
        public MainWindow()
        {
            InitializeComponent();
            Events();
            ball = new Ball(ballImg.Width,20);
            rightPallet = new Pallet(rightPalletImg.Margin.Left);
            leftPallet = new Pallet(leftPalletImg.Margin.Left);
            match = new Match(3);
            Pallet.maxY = Math.Abs(mainGrid.Height);
            Ball.maxY = Math.Abs(mainGrid.Height);
            Ball.maxX = Math.Abs(mainGrid.Width);
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
                ball.CheckBoundary(match);
                (bool leftVictory,bool rightVictory)=match.CheckVictory();
                if (leftVictory || rightVictory) 
                {
                    ball.Init();
                }
                if (leftVictory)
                {
                    MessageBox.Show("LEFT Victory!");
                }
                if (rightVictory)
                {
                    ball.Init();
                    MessageBox.Show("RIGHT Victory!");
                }
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

                leftScore.Text = match.leftScore.ToString();
                rightScore.Text = match.rightScore.ToString();
            });
        }

        private void BStart_Click(object sender, RoutedEventArgs e)
        {
            bStart.Content = "START";
            ball.Start();
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


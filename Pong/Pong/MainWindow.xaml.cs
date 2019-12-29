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

namespace Pong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            EventManager.RegisterClassHandler(typeof(Window),
            Keyboard.KeyUpEvent, new KeyEventHandler(KeyUpEventH), true);
            EventManager.RegisterClassHandler(typeof(Window),
            Keyboard.KeyDownEvent, new KeyEventHandler(KeyDownEventH), true);
        }


        private void BStart_Click(object sender, RoutedEventArgs e)
        {
            bStart.Content = "START";
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
            Thickness rightPalletMargin = rightPallet.Margin;
            Thickness leftPalletMargin = leftPallet.Margin;
            if (Keyboard.IsKeyDown(Key.Up) && rightPalletMargin.Top>=moveDistance) 
            {
                rightPalletMargin.Top -= moveDistance;
            }
            if (Keyboard.IsKeyDown(Key.Down) && rightPalletMargin.Top+moveDistance+rightPallet.Height<=mainGrid.ActualHeight)
            {
                rightPalletMargin.Top += moveDistance;
            }
            if (Keyboard.IsKeyDown(Key.W) && leftPalletMargin.Top >= moveDistance)
            {
                leftPalletMargin.Top -= moveDistance;

            }
            if (Keyboard.IsKeyDown(Key.S) && leftPalletMargin.Top + moveDistance + leftPallet.Height <= mainGrid.ActualHeight)
            {
                leftPalletMargin.Top += moveDistance;
            }
            rightPallet.Margin = rightPalletMargin;
            leftPallet.Margin = leftPalletMargin;
        }
    }
}


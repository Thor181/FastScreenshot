using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace FastScreenshot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int Counter = 1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateScreenshot(string folder, int number)
        {
            var bounds = new System.Drawing.Rectangle(new System.Drawing.Point((int)Left, (int)Top), new System.Drawing.Size((int)Width, (int)Height));
            using var bitmap = new Bitmap(bounds.Width, bounds.Height);
            using var g = Graphics.FromImage(bitmap);

            g.CopyFromScreen(new System.Drawing.Point(bounds.Left, bounds.Top), System.Drawing.Point.Empty, bounds.Size);
            bitmap.Save($"{folder}/screen{number}.png", ImageFormat.Png);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F8)
                CreateScreenshot(@"D:\Screen", Counter++);

            if (e.Key == Key.F9)
            {
                var main = new MainWindow()
                {
                    AllowsTransparency = !AllowsTransparency,
                    WindowStyle = WindowStyle == WindowStyle.SingleBorderWindow ? WindowStyle.None : WindowStyle.SingleBorderWindow,
                    Left = this.Left,
                    Top = this.Top,
                    Width = this.Width,
                    Height = this.Height,
                    Opacity = this.Opacity,
                    Background = this.Background

                };
                this.Close();
                main.Show();

            }

            if (e.Key == Key.Down)
                Opacity -= 0.1;

            if (e.Key == Key.Up)
                Opacity += 0.1;

            if (e.Key == Key.F6)
                Background = Background == System.Windows.Media.Brushes.Black ? System.Windows.Media.Brushes.White: System.Windows.Media.Brushes.Black;


        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();


        }
    }
}

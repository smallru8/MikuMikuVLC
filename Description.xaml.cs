using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MikuMikuVLC
{
    /// <summary>
    /// Description.xaml 的互動邏輯
    /// </summary>
    public partial class Description : Window
    {

        public Description()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            AllowsTransparency = true;
            ImageBrush bg1 = new ImageBrush();
            bg1.ImageSource = MainWindow.config.skins["MusicLsWindow.png"];
            bg1.Stretch = Stretch.Uniform;
            Background = bg1;
            Close.Source = MainWindow.config.skins["Close.png"];
            image.Source = MainWindow.config.skins["smallru8New.png"];
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void Close_MouseEnter(object sender, MouseEventArgs e)
        {
            Close.Source = MainWindow.config.skins["CloseH.png"];
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Visibility = Visibility.Hidden;
        }

        private void Close_MouseLeave(object sender, MouseEventArgs e)
        {
            Close.Source = MainWindow.config.skins["Close.png"];
        }

        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/smallru8");
        }
    }
}

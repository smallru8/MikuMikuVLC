using System;
using System.Collections.Generic;
using System.Drawing.Text;
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
    /// SettingWindow.xaml 的互動邏輯
    /// </summary>
    public partial class SettingWindow : Window
    {
        public MainWindow mw;

        public SettingWindow(MainWindow mwTmp)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            AllowsTransparency = true;
            mw = mwTmp;

            AutoOpacity.IsChecked = MainWindow.config.AutoOpacity;
            MostTop.IsChecked = MainWindow.config.MostTop;

            ImageBrush bgSw = new ImageBrush();
            bgSw.ImageSource = MainWindow.config.skins["Player.png"];
            bgSw.Stretch = Stretch.Uniform;
            Background = bgSw;
            Close.Source = MainWindow.config.skins["Close.png"];

            foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)///列舉Fonts
            {
                FontComboBox.Items.Add(fontFamily.Source);
                if (FontComboBox.Items[FontComboBox.Items.Count - 1].ToString() == MainWindow.config.fontName)
                    FontComboBox.SelectedIndex = FontComboBox.Items.Count - 1;
            }

            for(int i = 0; i < MainWindow.config.dirs.Count; i++)///列舉skinPacks
            {
                string packName = MainWindow.config.dirs[i].Split('\\')[MainWindow.config.dirs[i].Split('\\').Length - 1];
                SkinPacksComboBox.Items.Add(packName);
                if (packName.Equals(MainWindow.config.skinPackName.Split('\\')[MainWindow.config.skinPackName.Split('\\').Length - 2]))
                    SkinPacksComboBox.SelectedIndex = SkinPacksComboBox.Items.Count - 1;
            }
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)///拖曳
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mw.settingW = false;
            MainWindow.config.editMode = false;
            mw.mouseDownFlag = false;

            MainWindow.config.setUpUI(mw);

            Close();
        }

        private void Close_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Image)sender).Source = MainWindow.config.skins["CloseH.png"];
        }

        private void Close_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Image)sender).Source = MainWindow.config.skins["Close.png"];
        }

        private void OK1_Click(object sender, RoutedEventArgs e)///套用選定skinPack
        {
            MainWindow.config.changeSkinPack(SkinPacksComboBox.SelectedItem.ToString());
        }

        private void OK2_Click(object sender, RoutedEventArgs e)///套用選定字型
        {
            MainWindow.config.changeFont(FontComboBox.SelectedItem.ToString());
        }

        private void editMode_Checked(object sender, RoutedEventArgs e)///啟動編輯模式
        {
            MainWindow.config.editMode = true;
        }

        private void editMode_Unchecked(object sender, RoutedEventArgs e)
        {
            MainWindow.config.editMode = false;
            mw.mouseDownFlag = false;
        }

        private void OK3_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.config.editMode = false;
            mw.mouseDownFlag = false;
            editMode.IsChecked = false;
            MainWindow.config.changeCoordinate(mw);
        }

        private void OK4_Click(object sender, RoutedEventArgs e)
        {
            if (MostTop.IsChecked == true)
                MainWindow.config.MostTop = true;
            else
                MainWindow.config.MostTop = false;
            if (AutoOpacity.IsChecked == true)
                MainWindow.config.AutoOpacity = true;
            else
            {
                MainWindow.config.AutoOpacity = false;
                mw.Opacity = 1;
            }
            MainWindow.config.changeOptions();
        }
    }
}

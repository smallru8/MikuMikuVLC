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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Resources;
using System.Globalization;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using Vlc.DotNet.Wpf;
using System.ComponentModel;
using System.Windows.Media.Animation;

namespace MikuMikuVLC
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public static string ver = "Beta-1901.1";

        public System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer Playtimer;
        private System.Windows.Forms.Timer marqueeTimer;
        public static Config config;
        public static VlcForm vlcPlayer;///播放器視窗
        public static PlayListWindow plw;///PlayListWindow
        private bool marqueeMove;

        public bool settingW;
        public bool dcW;

        private SettingWindow sw;
        private Description dc;
        public bool mouseDownFlag;
        private string mouseDownName;
        private Thickness margin;

        public MainWindow()
        {
            InitializeComponent();

            this.Topmost = true;///維持在最上層

            WindowStyle = WindowStyle.None;
            AllowsTransparency = true;
            AllowDrop = true;

            config = new Config();
            config.checkCfg();
            config.loadCfg();
            config.loadDefaultUI();
            config.loadUI();
            config.listSkinPacks();
            config.loadOptions();

            vlcPlayer = new VlcForm();
            plw = new PlayListWindow(marqueeLable);
            settingW = false;
            config.setUpUI(this);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += timerTick;
            timer.Start();

            Playtimer = new System.Windows.Forms.Timer();
            Playtimer.Interval = 500;
            Playtimer.Tick += PlaytimerTick;
            Playtimer.Start();

            marqueeTimer = new System.Windows.Forms.Timer();
            marqueeTimer.Interval = 200;
            marqueeTimer.Tick += marqueeTimerTick;
            marqueeTimer.Start();
            marqueeMove = true;
            marqueeLable.FontFamily = new FontFamily(config.fontName);

            mouseDownName = "";
            mouseDownFlag = false;
            vol10.Width = 32;

            PlayPause.MouseDown += Controls_MouseDown;
            setting.MouseDown += Controls_MouseDown;
            Stop.MouseDown += Controls_MouseDown;
            Close.MouseDown += Controls_MouseDown;
            Next.MouseDown += Controls_MouseDown;
            Previous.MouseDown += Controls_MouseDown;
            OpenPlayerWindow.MouseDown += Controls_MouseDown;
            Re.MouseDown += Controls_MouseDown;
            OpenMusicLs.MouseDown += Controls_MouseDown;
            Question.MouseDown += Controls_MouseDown;
            vol10.MouseDown += Controls_MouseDown;

            setting.MouseMove += Controls_MouseMove;
            Stop.MouseMove += Controls_MouseMove;
            Close.MouseMove += Controls_MouseMove;
            Next.MouseMove += Controls_MouseMove;
            Previous.MouseMove += Controls_MouseMove;
            OpenPlayerWindow.MouseMove += Controls_MouseMove;
            Re.MouseMove += Controls_MouseMove;
            OpenMusicLs.MouseMove += Controls_MouseMove;
            Question.MouseMove += Controls_MouseMove;
            vol10.MouseMove += Controls_MouseMove;

            marqueeLable.MouseDown += Controls_Lab_MouseDown;
            marqueeLable.MouseMove += Controls_Lab_MouseMove;

            dc = new Description();
            this.Topmost = config.MostTop;

        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
        private void PlaytimerTick(object sender, EventArgs e)
        {
            pbBar.Value = vlcPlayer.vlcControl1.VlcMediaPlayer.Position*100;
            if(plw.pL.currentIndex!=-1 && !marqueeLable.Content.Equals(plw.pL.PQ.ElementAt(plw.pL.currentIndex).Key))
                marqueeLable.Content = plw.pL.PQ.ElementAt(plw.pL.currentIndex).Key;
        }

        private void timerTick(object sender, EventArgs e)
        {
            if (vlcPlayer.vlcControl1.VlcMediaPlayer.IsPlaying())
            {
                PlayPause.Source = config.skins["Pause.png"];
            }
            else
            {
                PlayPause.Source = config.skins["Play.png"];
            }
        }
        private void marqueeTimerTick(object sender, EventArgs e)
        {
            if (marqueeMove)
            {
                Thickness margin = marqueeLable.Margin;
                if(marqueeLable.Margin.Left+ marqueeLable.ActualWidth < 0)
                    margin.Left = 287;
                else
                    margin.Left -= 3;
                marqueeLable.Margin = margin;
            }
        }

        private void Controls_MouseDown(object sender, MouseEventArgs e)
        {
            if (config.editMode)
            {
                if (!mouseDownFlag)
                {
                    mouseDownFlag = true;
                    mouseDownName = ((Image)sender).Name;
                }
                else
                {
                    if (mouseDownName.Equals("PlayPause"))
                    {
                        Thickness margin = g2.Margin;
                        margin.Left = PlayPause.Margin.Left - 1;
                        margin.Top = PlayPause.Margin.Top - 1;
                        g2.Margin = margin;
                    }
                    mouseDownFlag = false;
                    GC.Collect();
                }
            }
        }

        private void Controls_MouseMove(object sender, MouseEventArgs e)
        {
            if (config.editMode && mouseDownFlag && ((Image)sender).Name.Equals(mouseDownName))
            {
                margin = ((Image)sender).Margin;
                margin.Left = e.GetPosition(this).X -20;
                margin.Top = e.GetPosition(this).Y -20;
                ((Image)sender).Margin = margin;
            }
        }
        private void Controls_Lab_MouseDown(object sender, MouseEventArgs e)
        {
            if (config.editMode)
            {
                if (!mouseDownFlag)
                {
                    mouseDownFlag = true;
                    mouseDownName = ((Label)sender).Name;
                }
            }
        }

        private void Controls_Lab_MouseMove(object sender, MouseEventArgs e)
        {
            if (config.editMode && mouseDownFlag && ((Label)sender).Name.Equals(mouseDownName))
            {
                margin = ((Label)sender).Margin;
                margin.Left = e.GetPosition(this).X - 20;
                margin.Top = e.GetPosition(this).Y - 20;
                ((Label)sender).Margin = margin;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)///拖曳
        {
            if (e.LeftButton == MouseButtonState.Pressed&&!config.editMode)
                this.DragMove();
        }

        private void BtnMouseEnter(object sender, MouseEventArgs e)///高亮
        {
            ((Image)sender).Source = config.skins[((Image)sender).Name + "H.png"];
        }
        private void BtnMouseLeave(object sender, MouseEventArgs e)///正常
        {
            ((Image)sender).Source = config.skins[((Image)sender).Name + ".png"];
        }

        private void PlayPause_MouseEnter(object sender, MouseEventArgs e)///高亮
        {
            if (vlcPlayer.vlcControl1.VlcMediaPlayer.IsPlaying())
            {
                ((Image)sender).Source = config.skins["PauseH.png"];
            }
            else
            {
                ((Image)sender).Source = config.skins["PlayH.png"];
            }
        }

        private void PlayPause_MouseLeave(object sender, MouseEventArgs e)///正常
        {
            if (vlcPlayer.vlcControl1.VlcMediaPlayer.IsPlaying())
            {
                ((Image)sender).Source = config.skins["Pause.png"];
            }
            else
            {
                ((Image)sender).Source = config.skins["Play.png"];
            }
        }

        private void Re_MouseEnter(object sender, MouseEventArgs e)
        {
            if(vlcPlayer.cycle)
                ((Image)sender).Source = config.skins["ReTH.png"];
            else
                ((Image)sender).Source = config.skins["ReH.png"];
        }

        private void Re_MouseLeave(object sender, MouseEventArgs e)
        {
            if (vlcPlayer.cycle)
                ((Image)sender).Source = config.skins["ReT.png"];
            else
                ((Image)sender).Source = config.skins["Re.png"];
        }

        private void Re_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(!config.editMode)
                vlcPlayer.cycle = !vlcPlayer.cycle;
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!config.editMode)
            {
                timer.Stop();
                marqueeTimer.Stop();
                vlcPlayer.Close();
                plw.Close();
                if (sw != null)
                    sw.Close();
                if (dc != null)
                    dc.Close();
                this.Close();
            }
        }

        private void OpenPlayerWindow_MouseDown(object sender, MouseButtonEventArgs e)///顯示影像
        {
            if (!config.editMode)
            {
                if (vlcPlayer.Visible)
                    vlcPlayer.Visible = false;
                else
                    vlcPlayer.Visible = true;
            }
        }

        private void PlayPause_MouseDown(object sender, MouseButtonEventArgs e)///控制暫停/播放
        {
            if (!config.editMode)
            {
                if (vlcPlayer.vlcControl1.VlcMediaPlayer.GetMedia() == null)
                    plw.pL.Q_play("-1");
                if (vlcPlayer.vlcControl1.VlcMediaPlayer.IsPlaying())
                    vlcPlayer.vlcControl1.VlcMediaPlayer.Pause();
                else
                    vlcPlayer.vlcControl1.VlcMediaPlayer.Play();
            }
        }

        private void Stop_MouseDown(object sender, MouseButtonEventArgs e)///停止
        {
            if (!config.editMode)
            {
                vlcPlayer.vlcControl1.VlcMediaPlayer.Stop();
                PlayPause.Source = config.skins["Play.png"];
            }
        }

        private void OpenMusicLs_MouseDown(object sender, MouseButtonEventArgs e)///顯示歌單視窗
        {
            if (!config.editMode)
            {
                if (plw.IsVisible)
                    plw.Visibility = Visibility.Hidden;
                else
                    plw.Visibility = Visibility.Visible;
            }
        }

        private void Window_DragEnter(object sender, DragEventArgs e)///檔案拖曳
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.All;
            else
                e.Effects = DragDropEffects.None;
        }

        private void Window_Drop(object sender, DragEventArgs e)///檔案拖曳
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (s.Length == 1 && s[0].EndsWith(".SmaMu"))
            {
                plw.pL.PQ.Clear();
                plw.listBox.Items.Clear();
                plw.pL.currentIndex = 0;

                vlcPlayer.vlcControl1.VlcMediaPlayer.Stop();

                FileStream fs = new FileStream(@s[0], FileMode.Open);
                StreamReader reader = new StreamReader(fs);
                while (!reader.EndOfStream)
                {
                    string s1 = reader.ReadLine();
                    string tmp = s1.Split('\\')[s1.Split('\\').Length - 1];
                    while (plw.pL.PQ.ContainsKey(tmp))
                        tmp += "_";
                    plw.listBox.Items.Add(tmp);
                    plw.pL.PQ.Add(tmp, s1);
                }
                reader.Close();
                fs.Close();
                vlcPlayer.vlcControl1.VlcMediaPlayer.SetMedia(new FileInfo(plw.pL.PQ.ElementAt(plw.pL.currentIndex).Value));
            }
            else
            {
                for (int i = 0; i < s.Length; i++)
                {
                    string tmp = s[i].Split('\\')[s[i].Split('\\').Length - 1];
                    while (plw.pL.PQ.ContainsKey(tmp))
                        tmp += "_";
                    plw.listBox.Items.Add(tmp);
                    plw.pL.PQ.Add(tmp, s[i]);
                }
            }
        }

        private void vol10_MouseWheel(object sender, MouseWheelEventArgs e)///音量調整
        {
            if (e.Delta > 0&&vlcPlayer.vlcControl1.VlcMediaPlayer.Audio.Volume<100)
            {
                vlcPlayer.vlcControl1.VlcMediaPlayer.Audio.Volume++;
                
                ((Image)sender).Width = ((Double)vlcPlayer.vlcControl1.VlcMediaPlayer.Audio.Volume / 100 * 64);
            }
            else if (e.Delta < 0&& vlcPlayer.vlcControl1.VlcMediaPlayer.Audio.Volume>0)
            {
                vlcPlayer.vlcControl1.VlcMediaPlayer.Audio.Volume--;
                ((Image)sender).Width = ((Double)vlcPlayer.vlcControl1.VlcMediaPlayer.Audio.Volume / 100 * 64);
            }
            if (((Image)sender).Width < 5)
                ((Image)sender).Width = 5;
            if (((Image)sender).Width > 32)
                Background = config.bg2;
            else
                Background = config.bg1;
        }

        private void Next_MouseDown(object sender, MouseButtonEventArgs e)///下一首
        {
            if (!config.editMode)
                plw.pL.Q_next();
        }

        private void Previous_MouseDown(object sender, MouseButtonEventArgs e)///上一首
        {
            if (!config.editMode)
                plw.pL.Q_previous();
        }

        private void marqueeLable_MouseEnter(object sender, MouseEventArgs e)
        {
            marqueeMove = false;
        }

        private void marqueeLable_MouseLeave(object sender, MouseEventArgs e)
        {
            marqueeMove = true;
        }

        private void setting_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!config.editMode)
            {
                if (!settingW)
                {
                    settingW = true;
                    sw = new SettingWindow(this);
                    sw.Show();
                }
            }
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            if (config.AutoOpacity)
            {
                DoubleAnimation daV = new DoubleAnimation(.3, 1, new Duration(TimeSpan.FromSeconds(1)));
                this.BeginAnimation(UIElement.OpacityProperty, daV);
            }
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            if (config.AutoOpacity)
            {
                DoubleAnimation daV = new DoubleAnimation(1, .3, new Duration(TimeSpan.FromSeconds(1)));
                this.BeginAnimation(UIElement.OpacityProperty, daV);
            }
        }

        private void Question_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dc.Visibility = Visibility.Visible;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
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
    /// PlayListWindow.xaml 的互動邏輯
    /// </summary>
    public partial class PlayListWindow : Window
    {
        public PlayList pL;

        public PlayListWindow(Label lTmp)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            AllowsTransparency = true;
            Visibility = Visibility.Hidden;
            listBox.AllowDrop = true;
            pL = new PlayList();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void listBox_DragEnter(object sender, DragEventArgs e)///丟檔案進去
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.All;
            else
                e.Effects = DragDropEffects.None;
        }

        private void listBox_Drop(object sender, DragEventArgs e)///丟檔案進去
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (s.Length == 1 && s[0].EndsWith(".SmaMu"))
            {
                pL.PQ.Clear();
                listBox.Items.Clear();
                pL.currentIndex = 0;

                MainWindow.vlcPlayer.vlcControl1.VlcMediaPlayer.Stop();

                FileStream fs = new FileStream(@s[0], FileMode.Open);
                StreamReader reader = new StreamReader(fs);
                while (!reader.EndOfStream)
                {
                    string s1 = reader.ReadLine();
                    string tmp = s1.Split('\\')[s1.Split('\\').Length - 1];
                    while (pL.PQ.ContainsKey(tmp))
                        tmp += "_";
                    listBox.Items.Add(tmp);
                    pL.PQ.Add(tmp, s1);
                }
                reader.Close();
                fs.Close();
                MainWindow.vlcPlayer.vlcControl1.VlcMediaPlayer.SetMedia(new FileInfo(pL.PQ.ElementAt(pL.currentIndex).Value));
            }
            else
            {
                for (int i = 0; i < s.Length; i++)
                {
                    string tmp = s[i].Split('\\')[s[i].Split('\\').Length - 1];
                    while (pL.PQ.ContainsKey(tmp))
                        tmp += "_";
                    listBox.Items.Add(tmp);
                    pL.PQ.Add(tmp, s[i]);
                }
            }
        }

        private void BtnMouseEnter(object sender, MouseEventArgs e)
        {
            ((Image)sender).Source = MainWindow.config.skins[((Image)sender).Name + "H.png"];
        }
        private void BtnMouseLeave(object sender, MouseEventArgs e)
        {
            ((Image)sender).Source = MainWindow.config.skins[((Image)sender).Name + ".png"];
        }


        private void listBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)///跳至...
        {
            if(listBox.SelectedItem!=null)
                pL.Q_play(listBox.SelectedItem.ToString());
        }

        private void listBox_KeyDown(object sender, KeyEventArgs e)///Delete
        {
            if (e.Key == Key.Delete && listBox.SelectedItem != null && listBox.Items.Count > 1)
            {
                pL.PQ.Remove(listBox.SelectedItem.ToString());
                listBox.Items.Remove(listBox.SelectedItem);
            }
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e)///隱藏此視窗
        {
            Visibility = Visibility.Hidden;
        }

        private void LoadList_MouseDown(object sender, MouseButtonEventArgs e)///載入SmaMu
        {
            ///.NET BUG
            /*pL.PQ.Clear();
            listBox.Items.Clear();
            pL.currentIndex = 0;

            MainWindow.vlcPlayer.vlcControl1.VlcMediaPlayer.Stop();
            
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Small MusicLs |*.SmaMu|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                var fileStream = openFileDialog.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    while (!reader.EndOfStream)
                    {
                        string s = reader.ReadLine();
                        string tmp = s.Split('\\')[s.Split('\\').Length - 1];
                        while (pL.PQ.ContainsKey(tmp))
                            tmp += "_";
                        listBox.Items.Add(tmp);
                        pL.PQ.Add(tmp, s);
                    }
                    reader.Close();
                    fileStream.Close();
                }
                MainWindow.vlcPlayer.vlcControl1.VlcMediaPlayer.SetMedia(new FileInfo(pL.PQ.ElementAt(pL.currentIndex).Value));
            }*/
        }

        private void SaveList_MouseDown(object sender, MouseButtonEventArgs e)///輸出SmaMu
        {
            if (pL.PQ.Count != 0)
            {
                System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
                saveFileDialog1.Filter = "Small MusicLs |*.SmaMu";
                saveFileDialog1.Title = "Save an music list";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    FileStream fs = (FileStream)saveFileDialog1.OpenFile();
                    for (int i = 0; i < pL.PQ.Count; i++)
                    {
                        fs.Write(Encoding.UTF8.GetBytes(pL.PQ.ElementAt(i).Value + "\n"), 0, Encoding.UTF8.GetBytes(pL.PQ.ElementAt(i).Value + "\n").Length);
                    }
                    fs.Close();
                }
            }
        }
    }
}

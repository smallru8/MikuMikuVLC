using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Vlc.DotNet.Forms;
using System.Windows.Media;
using System.Threading;

namespace MikuMikuVLC
{
    public partial class VlcForm: Form
    {
        public bool cycle;//重複撥放
        private Point mouse_offset;
        public VlcControl vlcControl1;

        public VlcForm()
        {
            InitializeComponent();
            vlcControl1 = new VlcControl();
            vlcControl1.BeginInit();
            vlcControl1.BackColor = System.Drawing.Color.Black;
            vlcControl1.Location = new System.Drawing.Point(12, 12);
            vlcControl1.Name = "vlcControl1";
            vlcControl1.Size = new System.Drawing.Size(634, 347);
            vlcControl1.Spu = -1;
            vlcControl1.TabIndex = 0;
            vlcControl1.Text = "vlcControl1";
            vlcControl1.VlcLibDirectory = new DirectoryInfo("./vlclib");
            vlcControl1.VlcMediaplayerOptions = null;
            vlcControl1.EndInit();
            Controls.Add(vlcControl1);

            cycle = false;
            Visible = false;
            BackgroundImage = MainWindow.config.VLC_skins["Player.png"];
            pictureBox1.Image = MainWindow.config.VLC_skins["PlayerBtnX.png"];
            pictureBox2.Image = MainWindow.config.VLC_skins["Max.png"];
            pictureBox3.Image = MainWindow.config.VLC_skins["Min.png"];
            vlcControl1.BeginInit();
            vlcControl1.VlcMediaPlayer.EndReached += vlcEndReached;
            //vlcControl1.PositionChanged += VlcPositionChanged;
            vlcControl1.BackColor = System.Drawing.Color.Black;
            vlcControl1.EndInit();

            //FileInfo fi = new FileInfo("H:\\mp3\\歌單\\relax\\【 3D環繞】請戴上耳機，感受聽覺的震撼 - YouTube (480p).mp4");
            //vlcControl1.VlcMediaPlayer.SetMedia(fi);

            vlcControl1.VlcMediaPlayer.Audio.Volume = 10;
            vlcControl1.VlcMediaPlayer.Position = 0.5f;
            vlcControl1.VlcMediaPlayer.Audio.Volume = 50;
        }

        private void vlcEndReached(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object state) { vlcControl1.VlcMediaPlayer.Stop(); }), null);
            if (cycle)
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object state) { vlcControl1.VlcMediaPlayer.Stop(); vlcControl1.VlcMediaPlayer.Play(); }), null);
            else
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object state) { MainWindow.plw.pL.Q_next(); }), null);
        }
        private void VlcForm_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void VlcForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                Location = mousePos;
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = MainWindow.config.VLC_skins["PlayerBtnXH.png"];
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = MainWindow.config.VLC_skins["PlayerBtnX.png"];
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Image = MainWindow.config.VLC_skins["MaxH.png"];
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = MainWindow.config.VLC_skins["Max.png"];
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.Image = MainWindow.config.VLC_skins["MinH.png"];
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = MainWindow.config.VLC_skins["Min.png"];
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }
    }
}

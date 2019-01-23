using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace MikuMikuVLC
{
    public class PlayList
    {

        public Dictionary<string, string> PQ;
        public int currentIndex;
        public PlayList()
        {
            PQ = new Dictionary<string, string>();
            currentIndex = -1;
        }

        public void Q_play(string trackName)
        {
            if (PQ.Count != 0)
            {
                if (trackName.Equals("-1"))
                {
                    currentIndex = 0;
                    MainWindow.vlcPlayer.vlcControl1.VlcMediaPlayer.SetMedia(new System.IO.FileInfo(PQ.ElementAt(currentIndex).Value));
                    MainWindow.vlcPlayer.vlcControl1.VlcMediaPlayer.Play();
                }
                else
                {
                    currentIndex = Array.IndexOf(PQ.Values.ToArray(), PQ[trackName]);
                    MainWindow.vlcPlayer.vlcControl1.VlcMediaPlayer.SetMedia(new System.IO.FileInfo(PQ[trackName]));
                    MainWindow.vlcPlayer.vlcControl1.VlcMediaPlayer.Play();
                }
            }
        }
        public void Q_next()
        {
            if (currentIndex < PQ.Count - 1)
            {
                MainWindow.vlcPlayer.vlcControl1.VlcMediaPlayer.SetMedia(new System.IO.FileInfo(PQ.ElementAt(++currentIndex).Value));
                MainWindow.vlcPlayer.vlcControl1.VlcMediaPlayer.Play();
            }
            else
            {
                currentIndex = 0;
                MainWindow.vlcPlayer.vlcControl1.VlcMediaPlayer.SetMedia(new System.IO.FileInfo(PQ.ElementAt(currentIndex).Value));
                MainWindow.vlcPlayer.vlcControl1.VlcMediaPlayer.Play();
            }
        }
        public void Q_previous()
        {
            if(currentIndex > 0)
            {
                MainWindow.vlcPlayer.vlcControl1.VlcMediaPlayer.SetMedia(new System.IO.FileInfo(PQ.ElementAt(--currentIndex).Value));
                MainWindow.vlcPlayer.vlcControl1.VlcMediaPlayer.Play();
            }
            else
            {
                currentIndex = PQ.Count - 1;
                MainWindow.vlcPlayer.vlcControl1.VlcMediaPlayer.SetMedia(new System.IO.FileInfo(PQ.ElementAt(currentIndex).Value));
                MainWindow.vlcPlayer.vlcControl1.VlcMediaPlayer.Play();
            }
        }
    }
}

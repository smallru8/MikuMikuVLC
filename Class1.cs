using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Resources;
using System.Globalization;
using System.Collections;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace MikuMikuVLC
{
    public class Config
    {
        //存skinPack名稱
        public string skinPackName;
        //偵測到的skinPacks
        public List<string> dirs;
        //skin存檔
        public Dictionary<string, BitmapImage> skins;
        public Dictionary<string, Image> VLC_skins;
        public ImageBrush bg1;
        public ImageBrush bg2;
        //Font
        public string fontName;
        //存介面座標
        public Dictionary<string, Double[]> coordinate;
        //編輯狀態
        public bool editMode;
        //Options
        public bool AutoOpacity;
        public bool MostTop;

        public Config()
        {
            fontName = "";
            AutoOpacity = false;
            MostTop = false;
            coordinate = new Dictionary<string, Double[]>();
            editMode = false;
            skins = new Dictionary<string, BitmapImage>();
            VLC_skins = new Dictionary<string, Image>();
            skinPackName = Environment.ExpandEnvironmentVariables(" % AppData % ") + "\\MikuMikuVLC\\Skins\\Default\\";
        }

        public bool checkCfg()
        {
            if (File.Exists(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\skin.yml") && Directory.Exists(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\Skins\\Default")&& File.Exists(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\Skins\\Default\\config.yml")&& File.Exists(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\Skins\\Default\\font.yml"))
            {
                return true;
            }
            else
            {
                if (!Directory.Exists(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC"))
                {
                    Directory.CreateDirectory(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC");
                }
                if (!Directory.Exists(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\Skins"))
                {
                    Directory.CreateDirectory(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\Skins");
                }
                if (!File.Exists(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\skin.yml"))
                {
                    BinaryWriter bw = new BinaryWriter(File.OpenWrite(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\skin.yml"));///Setup default skin.yml
                    bw.Write(Properties.Resources.skin);
                    bw.Flush();
                    bw.Close();
                    FileStream fs = File.Create(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\options.yml");
                    fs.Close();
                    StreamWriter sw = new StreamWriter(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\options.yml");
                    sw.WriteLine("AutoOpacity:1");
                    sw.WriteLine("MostTop:1");
                    sw.Flush();
                    sw.Close();
                }
                if (!Directory.Exists(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\Skins\\Default"))
                {
                    Directory.CreateDirectory(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\Skins\\Default");
                    ResourceSet resourceSet = Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
                    foreach (DictionaryEntry entry in resourceSet)
                    {
                        if (entry.Value is Bitmap)
                        {
                            ((Bitmap)entry.Value).Save(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\Skins\\Default\\" + entry.Key.ToString() + ".png");///Setup default skins
                        }
                    }
                }
                if(!File.Exists(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\Skins\\Default\\config.yml"))
                {
                    FileStream fs = File.Create(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\Skins\\Default\\config.yml");
                    fs.Close();
                    StreamWriter sw = new StreamWriter(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\Skins\\Default\\config.yml");
                    sw.WriteLine("[MikuMikuVLC-" + MainWindow.ver + "]");
                    sw.WriteLine("setting:99 43");
                    sw.WriteLine("PlayPause:13 200");
                    sw.WriteLine("Stop:68 210");
                    sw.WriteLine("Close:163 35");
                    sw.WriteLine("Next:148 210");
                    sw.WriteLine("Previous:108 210");
                    sw.WriteLine("OpenPlayerWindow:131 250");
                    sw.WriteLine("shuffle:0 0");
                    sw.WriteLine("Re:52 250");
                    sw.WriteLine("OpenMusicLs:163 250");
                    sw.WriteLine("Question:131 43");
                    sw.WriteLine("vol10:200 250");
                    sw.WriteLine("marqueeLable:13 167");
                    sw.Flush();
                    sw.Close();
                }
                if (!File.Exists(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\Skins\\Default\\font.yml"))
                {
                    FileStream fs = File.Create(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\Skins\\Default\\font.yml");
                    fs.Close();
                    StreamWriter sw = new StreamWriter(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\Skins\\Default\\font.yml");
                    sw.WriteLine("Font:SetoFont");
                    sw.Flush();
                    sw.Close();
                }
            }
            return true;
        }
        public void loadOptions()
        {
            StreamReader sr = new StreamReader(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\options.yml");
            if (sr.ReadLine().Split(':')[1].Equals("1"))//AutoOpacity
                AutoOpacity = true;
            if (sr.ReadLine().Split(':')[1].Equals("1"))//MostTop
                MostTop = true;
        }
        public void loadCfg()///讀skinName
        {
            StreamReader sr1 = new StreamReader(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\skin.yml");
            sr1.ReadLine();
            skinPackName = sr1.ReadLine();
            skinPackName = skinPackName.Split(' ')[1];
            skinPackName = Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\Skins\\" + skinPackName + "\\";
            sr1.Close();
        }
        public void loadDefaultUI()///載入預設座標&Skin
        {
            StreamReader sr3 = new StreamReader(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\Skins\\Default\\" + "font.yml");
            fontName = sr3.ReadLine().Split(':')[1];
            sr3.Close();
            StreamReader sr2 = new StreamReader(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\Skins\\Default\\" + "config.yml");
            sr2.ReadLine();
            while (!sr2.EndOfStream)
            {
                string[] nameCoor;
                string[] leftTop;
                nameCoor = sr2.ReadLine().Split(':');
                leftTop = nameCoor[1].Split(' ');
                coordinate.Add(nameCoor[0], new Double[] { Convert.ToDouble(leftTop[0]), Convert.ToDouble(leftTop[1]) });
            }
            sr2.Close();

            String[] FileCollection;
            FileInfo fi;
            FileCollection = Directory.GetFiles(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\Skins\\Default\\");
            for (int i = 0; i < FileCollection.Length; i++)
            {
                fi = new FileInfo(FileCollection[i]);
                if (fi.Name.EndsWith(".png"))
                {
                    if (fi.Name.StartsWith("Player") || fi.Name.StartsWith("Max") || fi.Name.StartsWith("Min"))
                        VLC_skins.Add(fi.Name, Image.FromFile(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\Skins\\Default\\" + fi.Name));
                    skins.Add(fi.Name, new BitmapImage(new Uri(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\Skins\\Default\\" + fi.Name, UriKind.Absolute)));
                }
            }
        }
        public void loadUI()///加載自訂座標&Skin
        {
            StreamReader sr3 = new StreamReader(skinPackName + "font.yml");
            fontName = sr3.ReadLine().Split(':')[1];
            sr3.Close();
            StreamReader sr2 = new StreamReader(skinPackName + "config.yml");
            sr2.ReadLine();
            while (!sr2.EndOfStream)
            {
                string[] nameCoor;
                string[] leftTop;
                nameCoor = sr2.ReadLine().Split(':');
                leftTop = nameCoor[1].Split(' ');
                coordinate[nameCoor[0]][0] = Convert.ToDouble(leftTop[0]);
                coordinate[nameCoor[0]][1] = Convert.ToDouble(leftTop[1]);
            }
            sr2.Close();

            String[] FileCollection;
            FileInfo fi;
            FileCollection = Directory.GetFiles(skinPackName);
            for(int i = 0; i < FileCollection.Length; i++)
            {
                fi =new FileInfo(FileCollection[i]);
                if (fi.Name.EndsWith(".png"))
                {
                    if (fi.Name.StartsWith("Player") || fi.Name.StartsWith("Max") || fi.Name.StartsWith("Min"))
                    {
                        VLC_skins[fi.Name] = Image.FromFile(skinPackName + fi.Name);
                    }
                    skins[fi.Name] = null;
                    skins[fi.Name] = new BitmapImage(new Uri(skinPackName + fi.Name, UriKind.Absolute));
                }
            }
        }
        private void setUpCoordinate(System.Windows.Controls.Image image)
        {
            Thickness margin;
            margin = image.Margin;
            margin.Left = coordinate[image.Name][0];
            margin.Top = coordinate[image.Name][1];
            image.Margin = margin;
        }
        public void setUpUI(MainWindow mw)
        {      
            bg1 = new ImageBrush();
            bg1.ImageSource = skins["BG1.png"];
            bg1.Stretch = Stretch.Uniform;
            bg2 = new ImageBrush();
            bg2.ImageSource = skins["BG2.png"];
            bg2.Stretch = Stretch.Uniform;
            mw.Background = bg1;

            mw.Close.Source = skins["Close.png"];
            mw.Close.Stretch = Stretch.Uniform;
            setUpCoordinate(mw.Close);

            mw.Question.Source = skins["Question.png"];
            mw.Question.Stretch = Stretch.Uniform;
            setUpCoordinate(mw.Question);

            mw.PlayPause.Source = skins["Play.png"];
            mw.PlayPause.Stretch = Stretch.Uniform;
            setUpCoordinate(mw.PlayPause);

            mw.Stop.Source = skins["Stop.png"];
            mw.Stop.Stretch = Stretch.Uniform;
            setUpCoordinate(mw.Stop);

            mw.Previous.Source = skins["Previous.png"];
            mw.Previous.Stretch = Stretch.Uniform;
            setUpCoordinate(mw.Previous);

            mw.Next.Source = skins["Next.png"];
            mw.Next.Stretch = Stretch.Uniform;
            setUpCoordinate(mw.Next);

            mw.OpenPlayerWindow.Source = skins["OpenPlayerWindow.png"];
            mw.OpenPlayerWindow.Stretch = Stretch.Uniform;
            setUpCoordinate(mw.OpenPlayerWindow);

            mw.OpenMusicLs.Source = skins["OpenMusicLs.png"];
            mw.OpenMusicLs.Stretch = Stretch.Uniform;
            setUpCoordinate(mw.OpenMusicLs);

            mw.setting.Source = skins["setting.png"];
            mw.setting.Stretch = Stretch.Uniform;
            setUpCoordinate(mw.setting);

            mw.vol10.Source = skins["vol10.png"];
            mw.vol10.Stretch = Stretch.UniformToFill;
            setUpCoordinate(mw.vol10);

            mw.Re.Source = skins["Re.png"];
            mw.Re.Stretch = Stretch.Uniform;
            setUpCoordinate(mw.Re);

            Thickness margin;
            margin = mw.marqueeLable.Margin;
            margin.Left = coordinate[mw.marqueeLable.Name][0];
            margin.Top = coordinate[mw.marqueeLable.Name][1];
            mw.marqueeLable.Margin = margin;

            MainWindow.plw.Close.Source = skins["Close.png"];
            MainWindow.plw.Close.Stretch = Stretch.Uniform;

            MainWindow.plw.LoadList.Source = skins["LoadList.png"];
            MainWindow.plw.LoadList.Stretch = Stretch.Uniform;

            MainWindow.plw.SaveList.Source = skins["SaveList.png"];
            MainWindow.plw.SaveList.Stretch = Stretch.Uniform;

            Thickness margin1 = mw.g2.Margin;
            margin1.Left = mw.PlayPause.Margin.Left - 1;
            margin1.Top = mw.PlayPause.Margin.Top - 1;
            mw.g2.Margin = margin1;
        }
        public void listSkinPacks()
        {
            try
            {
                string docPath = Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\Skins";

                dirs = new List<string>(Directory.EnumerateDirectories(docPath));

                foreach (var dir in dirs)
                {
                    Console.WriteLine("{0}", dir.Substring(dir.LastIndexOf("\\") + 1));
                }
                Console.WriteLine("{0} directories found.", dirs.Count);
            }
            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
            }
        }

        public void changeSkinPack(string packName)
        {
            StreamWriter sw = new StreamWriter(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\skin.yml");
            sw.WriteLine("[MikuMikuVLC]");
            sw.WriteLine("skin: " + packName);
            sw.Flush();
            sw.Close();
        }
        public void changeFont(string fontName)
        {
            StreamWriter sw = new StreamWriter(skinPackName + "font.yml");
            sw.WriteLine("Font:" + fontName);
            sw.Flush();
            sw.Close();
        }
        public void changeOptions()
        {
            StreamWriter sw = new StreamWriter(Environment.ExpandEnvironmentVariables("%AppData%") + "\\MikuMikuVLC\\options.yml");
            if(AutoOpacity)
                sw.WriteLine("AutoOpacity:1");
            else
                sw.WriteLine("AutoOpacity:0");
            if (MostTop)
                sw.WriteLine("MostTop:1");
            else
                sw.WriteLine("MostTop:0");
            sw.Flush();
            sw.Close();
        }
        public void changeCoordinate(MainWindow mw)
        {
            coordinate[mw.Close.Name][0] = mw.Close.Margin.Left;
            coordinate[mw.Close.Name][1] = mw.Close.Margin.Top;

            coordinate[mw.Question.Name][0] = mw.Question.Margin.Left;
            coordinate[mw.Question.Name][1] = mw.Question.Margin.Top;

            coordinate[mw.PlayPause.Name][0] = mw.PlayPause.Margin.Left;
            coordinate[mw.PlayPause.Name][1] = mw.PlayPause.Margin.Top;

            coordinate[mw.Stop.Name][0] = mw.Stop.Margin.Left;
            coordinate[mw.Stop.Name][1] = mw.Stop.Margin.Top;

            coordinate[mw.Previous.Name][0] = mw.Previous.Margin.Left;
            coordinate[mw.Previous.Name][1] = mw.Previous.Margin.Top;

            coordinate[mw.Next.Name][0] = mw.Next.Margin.Left;
            coordinate[mw.Next.Name][1] = mw.Next.Margin.Top;

            coordinate[mw.OpenPlayerWindow.Name][0] = mw.OpenPlayerWindow.Margin.Left;
            coordinate[mw.OpenPlayerWindow.Name][1] = mw.OpenPlayerWindow.Margin.Top;

            coordinate[mw.OpenMusicLs.Name][0] = mw.OpenMusicLs.Margin.Left;
            coordinate[mw.OpenMusicLs.Name][1] = mw.OpenMusicLs.Margin.Top;

            coordinate[mw.setting.Name][0] = mw.setting.Margin.Left;
            coordinate[mw.setting.Name][1] = mw.setting.Margin.Top;

            coordinate[mw.vol10.Name][0] = mw.vol10.Margin.Left;
            coordinate[mw.vol10.Name][1] = mw.vol10.Margin.Top;

            coordinate[mw.Re.Name][0] = mw.Re.Margin.Left;
            coordinate[mw.Re.Name][1] = mw.Re.Margin.Top;

            coordinate[mw.marqueeLable.Name][0] = mw.marqueeLable.Margin.Left;
            coordinate[mw.marqueeLable.Name][1] = mw.marqueeLable.Margin.Top;

            StreamWriter sw = new StreamWriter(skinPackName + "config.yml");
            sw.WriteLine("[MikuMikuVLC-" + MainWindow.ver + "]");
            for(int i = 0; i < coordinate.Count; i++)
            {
                sw.WriteLine(coordinate.ElementAt(i).Key + ":" + coordinate.ElementAt(i).Value[0] + " " + coordinate.ElementAt(i).Value[1]);
            }
            sw.Flush();
            sw.Close();
        }
    }

}

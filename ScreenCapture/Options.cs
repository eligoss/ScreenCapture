using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace ScreenCapture
{
    public class Options : ViewModels.ViewModelBase
    {
        #region [Fields]

        Dictionary<string, string> audio;
        Dictionary<string, string> video;
        Dictionary<string, SizeExtension> size;
        Dictionary<string, AudioVideoExtension> audioCodecs;
        Dictionary<string, AudioVideoExtension> videoCodecs;
        SerializableProperties sProperties;
        String tempFolderPath = "";
        Dictionary<string, string> themes;


        #endregion // [Fields]

        #region [Properties]
        [XmlIgnore]
        public Dictionary<string, string> Audio
        {
            get { return audio; }
            set { audio = value; }
        }
        [XmlIgnore]
        public Dictionary<string, string> Video
        {
            get { return video; }
            set { video = value; }
        }
        [XmlIgnore]
        public Dictionary<string, SizeExtension> Size
        {
            get { return size; }
            set { size = value; }
        }
        [XmlIgnore]
        public Dictionary<string, AudioVideoExtension> AudioCodecs
        {
            get { return audioCodecs; }
            set { audioCodecs = value; }
        }
        [XmlIgnore]
        public Dictionary<string, AudioVideoExtension> VideoCodecs
        {
            get { return videoCodecs; }
            set { videoCodecs = value; }
        }
        [XmlIgnore]
        public Dictionary<string, string> Themes
        {
            get { return themes; }
            set { themes = value; }
        }

        [XmlAttribute]
        public SerializableProperties SProperties
        {
            get { return sProperties; }
            set
            {
                sProperties = value;
                OnPropertyChanged("SProperties");
            }
        }




        #endregion // [Properties]

        #region [Constructors/Destrructors]

        ~Options()
        {
            try
            {
                if (!File.Exists(tempFolderPath + "\\serializedData.xml"))
                {
                    using (File.Create(tempFolderPath + "\\serializedData.xml")) { }
                }
                using (XmlWriter writer = new XmlTextWriter(tempFolderPath + "\\serializedData.xml", System.Text.Encoding.UTF8))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(SerializableProperties));
                    serializer.Serialize(writer, this.SProperties);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        #endregion // [Constructors/Destrructors]

        #region [P-n Singleton]

        private static Options instance;

        public static Options Instance
        {
            get { return instance != null ? instance : new Options(); }
        }

        private Options()
        {
            tempFolderPath = System.Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.Machine);

            // System.Windows.Forms.MessageBox.Show("Path To Temp=" + tempFolderPath);
            instance = this;
        }
        #endregion // [P-n Singleton]

        #region [Methods]

        public void InitializeOptions()
        {
            InitializeAudio();
            InitializeVideo();
            InitializeSize();
            InitializeAudioCodecs();
            InitializeVideoCodecs();
            InitializeThemes();
            if (File.Exists(tempFolderPath + "\\serializedData.xml"))
            {
                using (TextReader reader = new StreamReader(tempFolderPath + "\\serializedData.xml", Encoding.UTF8))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(SerializableProperties));
                    SerializableProperties o = (SerializableProperties)serializer.Deserialize(reader);
                    this.SProperties = o;
                    o = null;
                }
            }
            else
                SetDefaultValues();
        }

        public void SetDefaultValues()
        {
            if (SProperties == null)
                SProperties = new SerializableProperties();
            SProperties.SelectedAudioCodec = AudioCodecs["libmp3lame MP3 (MPEG audio layer 3)"];
            SProperties.SelectedVideoCodec = VideoCodecs["libx264 H.264 / AVC / MPEG-4 AVC / MPEG-4 part 10"];
            SProperties.SelectedSize = Size["HD(1280x720) 16:9"];
            //SProperties.SelectedVideo = Video["UScreenCapture"];
            SProperties.SelectedAudio = Audio.FirstOrDefault().Value;
            SProperties.SelectedTheme = Themes["Office Silver"];
        }

        private void InitializeAudio()
        {
            //Audio = new Dictionary<string, string>();
            //Audio.Add("No Audio", String.Empty);
            //foreach (var item in DsDevice.GetDevicesOfCat(FilterCategory.AudioInputDevice))
            //    Audio.Add(item.Name, item.Name);
        }

        private void InitializeVideo()
        {
            //Video = new Dictionary<string, string>();
            //foreach (var item in DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice))
            //    Video.Add(item.Name, item.Name);
        }

        private void InitializeSize()
        {
            Size = new Dictionary<string, SizeExtension>();
            Size.Add("SD(640x480) 4:3", new SizeExtension(4, 3, 640, 480));
            Size.Add("SD(640x360) 16:9", new SizeExtension(16, 9, 640, 360));
            Size.Add("HD(1280x720) 16:9", new SizeExtension(16, 9, 1280, 720));
            Size.Add("HD(1920x1080) 16:9", new SizeExtension(16, 9, 1920, 1080));
        }

        private void InitializeAudioCodecs()
        {
            AudioCodecs = new Dictionary<string, AudioVideoExtension>();
            // AudioCodecs.Add("Advanced Audio Coding(AAC)", new AudioVideoExtension("aac", String.Empty));
            AudioCodecs.Add("libmp3lame MP3 (MPEG audio layer 3)", new AudioVideoExtension("libmp3lame", String.Empty));
        }

        private void InitializeVideoCodecs()
        {
            VideoCodecs = new Dictionary<string, AudioVideoExtension>();
            VideoCodecs.Add("libx264 H.264 / AVC / MPEG-4 AVC / MPEG-4 part 10", new AudioVideoExtension("libx264", "flv"));
            //VideoCodecs.Add("libxvidcore MPEG-4 part 2", new AudioVideoExtension("libxvid", "avi"));
            VideoCodecs.Add("MPEG-2 video", new AudioVideoExtension("mpeg2video", "mpg"));
        }

        private void InitializeThemes()
        {
            Themes = new Dictionary<string, string>();
            Themes.Add("Office Black", "Office_Black");
            Themes.Add("Office Blue", "Office_Blue");
            Themes.Add("Office Silver", "Office_Silver");
            Themes.Add("Expression Dark", "Expression_Dark");
            Themes.Add("Summer", "Summer");
            Themes.Add("Transparent", "Transparent");
            Themes.Add("Vista", "Vista");
            Themes.Add("Windows 7", "Windows7");
            //Themes.Add("Metro", "Metro");
        }

        #endregion // [Methods]
    }

    #region [SubClasses]

    [XmlRoot]
    public class SerializableProperties : ViewModels.ViewModelBase
    {
        bool showSaveDialog = false;
        string selectedAudio;
        string selectedVideo;
        SizeExtension selectedSize;
        AudioVideoExtension selectedAudioCodec;
        AudioVideoExtension selectedVideoCodec;
        string selectedTheme;



        public string SelectedAudio
        {
            get { return selectedAudio; }
            set
            {
                selectedAudio = value;
                OnPropertyChanged("SelectedAudio");
            }
        }

        public string SelectedVideo
        {
            get { return selectedVideo; }
            set
            {
                selectedVideo = value;
                OnPropertyChanged("SelectedVideo");
            }
        }

        public SizeExtension SelectedSize
        {
            get { return selectedSize; }
            set
            {
                selectedSize = value;
                OnPropertyChanged("SelectedSize");
            }
        }

        public AudioVideoExtension SelectedAudioCodec
        {
            get { return selectedAudioCodec; }
            set
            {
                selectedAudioCodec = value;
                OnPropertyChanged("SelectedAudioCodec");
            }
        }

        public AudioVideoExtension SelectedVideoCodec
        {
            get { return selectedVideoCodec; }
            set
            {
                selectedVideoCodec = value;
                OnPropertyChanged("SelectedVideoCodec");
            }
        }

        public bool ShowSaveDialog
        {
            get { return showSaveDialog; }
            set
            {
                showSaveDialog = value;
                OnPropertyChanged("ShowSaveDialog");
            }
        }

        public string SelectedTheme
        {
            get { return selectedTheme; }
            set
            {
                selectedTheme = value;
                OnPropertyChanged("SelectedTheme");
                SetTheme(value);
            }
        }


        private void SetTheme(string themeName)
        {
        //    switch (themeName)
        //    {
        //        case "Office_Silver":
        //            {
        //                Application.Current.Resources.MergedDictionaries.Clear();
        //                Application.Current.Resources.MergedDictionaries.Add(
        //                    new ResourceDictionary()
        //                    {
        //                        Source = new Uri("Styles\\Office_SilverMergedDictionary.xaml",
        //                        UriKind.RelativeOrAbsolute)
        //                    });
        //                Application.Current.Resources.MergedDictionaries.Add(
        //                   new ResourceDictionary()
        //                   {
        //                       Source = new Uri("Styles\\ScreenGrabberButtonStyles.xaml",
        //                       UriKind.RelativeOrAbsolute)
        //                   });
        //                break;
        //            }
        //        case "Office_Black":
        //            {
        //                Application.Current.Resources.MergedDictionaries.Clear();
        //                Application.Current.Resources.MergedDictionaries.Add(
        //                    new ResourceDictionary()
        //                    {
        //                        Source = new Uri("Styles\\Office_BlackMergedDictionary.xaml",
        //                        UriKind.RelativeOrAbsolute)
        //                    });
        //                Application.Current.Resources.MergedDictionaries.Add(
        //                  new ResourceDictionary()
        //                  {
        //                      Source = new Uri("Styles\\ScreenGrabberButtonStyles.xaml",
        //                      UriKind.RelativeOrAbsolute)
        //                  });
        //                break;
        //            }
        //        case "Office_Blue":
        //            {
        //                Application.Current.Resources.MergedDictionaries.Clear();
        //                Application.Current.Resources.MergedDictionaries.Add(
        //                    new ResourceDictionary()
        //                    {
        //                        Source = new Uri("Styles\\Office_BlueMergedDictionary.xaml",
        //                        UriKind.RelativeOrAbsolute)
        //                    });
        //                Application.Current.Resources.MergedDictionaries.Add(
        //                  new ResourceDictionary()
        //                  {
        //                      Source = new Uri("Styles\\ScreenGrabberButtonStyles.xaml",
        //                      UriKind.RelativeOrAbsolute)
        //                  });
        //                break;
        //            }
        //        case "Expression_Dark":
        //            {
        //                Application.Current.Resources.MergedDictionaries.Clear();
        //                Application.Current.Resources.MergedDictionaries.Add(
        //                    new ResourceDictionary()
        //                    {
        //                        Source = new Uri("Styles\\Expression_DarkMergedDictionary.xaml",
        //                        UriKind.RelativeOrAbsolute)
        //                    });
        //                Application.Current.Resources.MergedDictionaries.Add(
        //                  new ResourceDictionary()
        //                  {
        //                      Source = new Uri("Styles\\ScreenGrabberButtonStyles.xaml",
        //                      UriKind.RelativeOrAbsolute)
        //                  });
        //                break;
        //            }
        //        case "Summer":
        //            {
        //                Application.Current.Resources.MergedDictionaries.Clear();
        //                Application.Current.Resources.MergedDictionaries.Add(
        //                    new ResourceDictionary()
        //                    {
        //                        Source = new Uri("Styles\\SummerMergedDictionary.xaml",
        //                        UriKind.RelativeOrAbsolute)
        //                    });
        //                Application.Current.Resources.MergedDictionaries.Add(
        //                  new ResourceDictionary()
        //                  {
        //                      Source = new Uri("Styles\\ScreenGrabberButtonStyles.xaml",
        //                      UriKind.RelativeOrAbsolute)
        //                  });
        //                break;
        //            }
        //        case "Transparent":
        //            {
        //                Application.Current.Resources.MergedDictionaries.Clear();
        //                Application.Current.Resources.MergedDictionaries.Add(
        //                    new ResourceDictionary()
        //                    {
        //                        Source = new Uri("Styles\\TransparentMergedDictionary.xaml",
        //                        UriKind.RelativeOrAbsolute)
        //                    });
        //                Application.Current.Resources.MergedDictionaries.Add(
        //                new ResourceDictionary()
        //                {
        //                    Source = new Uri("Styles\\ScreenGrabberButtonStyles.xaml",
        //                    UriKind.RelativeOrAbsolute)
        //                });
        //                break;
        //            }
        //        case "Metro":
        //            {
        //                Application.Current.Resources.MergedDictionaries.Clear();
        //                Application.Current.Resources.MergedDictionaries.Add(
        //                    new ResourceDictionary()
        //                    {
        //                        Source = new Uri("Styles\\MetroMergedDictionary.xaml",
        //                        UriKind.RelativeOrAbsolute)
        //                    });
        //                break;
        //            }
        //        case "Vista":
        //            {
        //                Application.Current.Resources.MergedDictionaries.Clear();
        //                Application.Current.Resources.MergedDictionaries.Add(
        //                    new ResourceDictionary()
        //                    {
        //                        Source = new Uri("Styles\\VistaMergedDictionary.xaml",
        //                        UriKind.RelativeOrAbsolute)
        //                    });
        //                Application.Current.Resources.MergedDictionaries.Add(
        //                  new ResourceDictionary()
        //                  {
        //                      Source = new Uri("Styles\\ScreenGrabberButtonStyles.xaml",
        //                      UriKind.RelativeOrAbsolute)
        //                  });
        //                break;
        //            }
        //        case "Windows7":
        //            {
        //                Application.Current.Resources.MergedDictionaries.Clear();
        //                Application.Current.Resources.MergedDictionaries.Add(
        //                    new ResourceDictionary()
        //                    {
        //                        Source = new Uri("Styles\\Windows7MergedDictionary.xaml",
        //                        UriKind.RelativeOrAbsolute)
        //                    });
        //                Application.Current.Resources.MergedDictionaries.Add(
        //                  new ResourceDictionary()
        //                  {
        //                      Source = new Uri("Styles\\ScreenGrabberButtonStyles.xaml",
        //                      UriKind.RelativeOrAbsolute)
        //                  });
        //                break;
        //            }
        //    }
        }

        public SerializableProperties() { }
    }

    [Serializable]
    public class AudioVideoExtension
    {
        string _firstParam;
        string _secondParam;

        [XmlAttribute]
        public string FirstParam
        {
            get { return _firstParam; }
            set { _firstParam = value; }
        }
        [XmlAttribute]
        public string SecondParam
        {
            get { return _secondParam; }
            set { _secondParam = value; }
        }

        public AudioVideoExtension(string first, string second)
        {
            FirstParam = first;
            SecondParam = second;
        }


        private AudioVideoExtension() { }

        public override bool Equals(object obj)
        {
            AudioVideoExtension a = obj as AudioVideoExtension;
            if (a == null)
                return false;
            if (a.FirstParam == this.FirstParam && a.SecondParam == a.SecondParam)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    [Serializable]
    public class SizeExtension
    {
        int _aspectRationWidth;
        int _aspectRationHeight;
        int _width;
        int _height;

        [XmlAttribute]
        public int AspectRationWidth
        {
            get { return _aspectRationWidth; }
            set { _aspectRationWidth = value; }
        }
        [XmlAttribute]
        public int AspectRationHeight
        {
            get { return _aspectRationHeight; }
            set { _aspectRationHeight = value; }
        }
        [XmlAttribute]
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        [XmlAttribute]
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }



        public SizeExtension(int aspectRationWidth, int aspectRationHeight, int width, int height)
        {
            AspectRationWidth = aspectRationWidth;
            AspectRationHeight = aspectRationHeight;
            Width = width;
            Height = height;
        }

        private SizeExtension()
        { }

        public override bool Equals(object obj)
        {
            SizeExtension s = obj as SizeExtension;
            if (s == null)
                return false;
            if (s.Width == this.Width &&
              s.Height == this.Height &&
              s.AspectRationHeight == this.AspectRationHeight &&
              s.AspectRationWidth == this.AspectRationWidth)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

    #endregion //[SubClasses]
}

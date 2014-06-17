using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ScreenCapture.ViewModels
{
    public class VideoWindowViewModel : ViewModelBase
    {
        #region [Fields]

        double left;
        double top;
        double width;
        double height;
        double panelLeft;
        double panelTop;
        double panelWidth;
        double panelHeight;
        Timer seconds;
        string time;

        public static bool videoIsUploading;

        public int timerMilisecondCount = 0;
        public int secondCount;
        #endregion // [Fields      ]


        private int ValidateSize(double value)
        {
            while (value % 4 != 0)
            {
                --value;
            }
            
            return (int)value;
        }

        #region [Properties]

        public System.Drawing.Rectangle Rectangle
        {
            get
            {
                return new System.Drawing.Rectangle((int)Left, (int)Top, ValidateSize(Width + 1), ValidateSize(Height + 1));
            }
        }

        public double Left
        {
            get { return left; }
            set
            {
                left = value;
                OnPropertyChanged("Left");
            }
        }

        public double Top
        {
            get { return top; }
            set
            {
                top = value;
                OnPropertyChanged("Top");
            }
        }

        public double Width
        {
            get { return width; }
            set
            {
                width = value;
                OnPropertyChanged("Width");
            }
        }

        public double Height
        {
            get { return height; }
            set
            {
                height = value;
                OnPropertyChanged("Height");
            }
        }

        public double PanelLeft
        {
            get { return panelLeft; }
            set
            {
                panelLeft = value;
                OnPropertyChanged("PanelLeft");
            }
        }

        public double PanelTop
        {
            get { return panelTop; }
            set
            {
                panelTop = value;
                OnPropertyChanged("PanelTop");
            }
        }

        public double PanelWidth
        {
            get { return panelWidth; }
            set
            {
                panelWidth = value;
                OnPropertyChanged("PanelWidth");
            }
        }

        public double PanelHeight
        {
            get { return panelHeight; }
            set
            {
                panelHeight = value;
                OnPropertyChanged("PanelHeight");
            }
        }

        public Timer Seconds
        {
            get { return seconds; }
        }

        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged("Time");
            }
        }


        #endregion // [Properties]

        #region [Constructors]

        public VideoWindowViewModel(CaptureWindowViewModel captureVindow)
        {
            Left = captureVindow.SelectedRect.Left;
            Top = captureVindow.SelectedRect.Top;
            Width = captureVindow.SelectedRect.Width;
            Height = captureVindow.SelectedRect.Height;
            PanelLeft = Left;
            PanelWidth = captureVindow.Width;
            PanelHeight = captureVindow.Height;
            if (PanelHeight - captureVindow.SelectedRect.Bottom > 80)
                PanelTop = captureVindow.SelectedRect.Bottom + 5;
            else
                PanelTop = captureVindow.SelectedRect.Bottom - 65;
            seconds = new Timer();
            seconds.Interval = 100;
            seconds.Elapsed += seconds_Elapsed;
        }

        ~VideoWindowViewModel()
        {
            seconds.Elapsed -= seconds_Elapsed;
            seconds = null;
        }
        #endregion //[Constructors]

        void seconds_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (timerMilisecondCount == 9)
            {
                timerMilisecondCount = 0;
                secondCount++;
                TimeSpan t = TimeSpan.FromSeconds((double)secondCount);
                Time = t.ToString();
            }
            else
                timerMilisecondCount++;
        }

    }
}

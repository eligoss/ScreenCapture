using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ScreenCapture.ViewModels
{
    public class CaptureWindowViewModel : ViewModelBase
    {
        #region [Fields]

        double width;
        double height;
        Rect fullSreenRect;
        Rect selectedRect;
        double borderWidth;
        double borderHeight;
        double borderLeft;
        double borderTop;
        double popupTop;
        double popupTopStart;
        string size;


        #endregion //[Fields]

        #region [Properties]

        public double BorderWidth
        {
            get { return borderWidth; }
            set
            {
                borderWidth = value;
                OnPropertyChanged("BorderWidth");
            }
        }

        public double BorderHeight
        {
            get { return borderHeight; }
            set
            {
                borderHeight = value;
                OnPropertyChanged("BorderHeight");
            }
        }

        public double BorderLeft
        {
            get { return borderLeft; }
            set
            {
                borderLeft = value;
                OnPropertyChanged("BorderLeft");
            }
        }

        public double BorderTop
        {
            get { return borderTop; }
            set
            {
                borderTop = value;
                OnPropertyChanged("BorderTop");
            }
        }

        public Rect FullSreenRect
        {
            get { return fullSreenRect; }
            set
            {
                fullSreenRect = value;
                OnPropertyChanged("FullSreenRect");
            }
        }

        public Rect SelectedRect
        {
            get { return selectedRect; }
            set
            {
                selectedRect = value;
                BorderHeight = value.Height + 2;
                BorderWidth = value.Width + 2;
                BorderLeft = value.Left - 2;
                BorderTop = value.Top - 2;
                PopupTopStart = value.BottomLeft.Y;
                Size = value.Width.ToString() + "\\" + value.Height.ToString();
                if (Height - value.BottomLeft.Y > 80)
                    PopupTopEnd = value.BottomLeft.Y + 5;
                else
                    PopupTopEnd = value.BottomLeft.Y - 65;
                OnPropertyChanged("SelectedRect");
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

        public double PopupTopStart
        {
            get { return popupTopStart; }
            set
            {
                popupTopStart = value;
                OnPropertyChanged("PopupTopStart");
            }
        }

        public double PopupTopEnd
        {
            get { return popupTop; }
            set
            {
                popupTop = value;
                OnPropertyChanged("PopupTopEnd");
            }
        }

        public string Size
        {
            get { return size; }
            set
            {
                size = value;
                OnPropertyChanged("Size");
            }
        }

        public bool IsFullScreen
        {
            get;
            set;
        }

        #endregion // [Properties]

        #region [Constructors]

        public CaptureWindowViewModel(bool isFullScreen)
        {
            Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            if (isFullScreen)
            {
                FullSreenRect = new Rect(0, 0, Width, Height);
                SelectedRect = new Rect(0, 0, Width, Height);
            }
            else
            {
                FullSreenRect = new Rect(0, 0, Width, Height);
                SelectedRect = new Rect(0, 0, 1, 1);
            }
            IsFullScreen = isFullScreen;
        }
        
        #endregion //[Constructors]
    }
}

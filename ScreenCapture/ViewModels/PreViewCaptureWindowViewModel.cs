using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ScreenCapture.ViewModels
{
    public class PreViewCaptureWindowViewModel : ViewModelBase
    {
        #region [Fields]

        CaptureWindowViewModel _parent;
        Brush backgraundBrush;
        ImageSource _source;
        string fileName;


        #endregion// [Fields]

        #region [Priperties]

        double windowWidth;

        public double WindowWidth
        {
            get { return windowWidth; }
            set
            {
                windowWidth = value;
                OnPropertyChanged("WindowWidth");
            }
        }

        public double WindowHeight
        {
            get;
            set;
        }

        public Brush BackgraundBrush
        {
            get { return backgraundBrush; }
            set
            {

                backgraundBrush = value;
                OnPropertyChanged("BackgraundBrush");
            }
        }

        public ImageSource SourceImage
        {
            get { return _source; }
            set
            {
                _source = value;
                OnPropertyChanged("Source");
            }
        }

        public System.Drawing.Bitmap Bmp
        {
            get;
            set;
        }

        public string FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                OnPropertyChanged("FileName");
            }
        }

        #endregion //[Priperties]

        #region [Constructors]

        public PreViewCaptureWindowViewModel(double width, double height, ImageSource source, CaptureWindowViewModel parent, System.Drawing.Bitmap bmp)
        {
            _parent = parent;
            if (width > parent.Width * 0.7d)
                WindowWidth = parent.Width * 0.7d;
            else
                WindowWidth = width + 70;
            if (height > _parent.Height * 0.7)
                WindowHeight = _parent.Height * 0.7;
            else
                WindowHeight = height + 140;
            SourceImage = source;
            Bmp = bmp;
            BackgraundBrush = new ImageBrush(source);
            string tempstr = DateTime.Now.ToBinary().ToString();
            tempstr = tempstr.Replace(".", "_");
            tempstr = tempstr.Replace("-", String.Empty);
            FileName = tempstr.Replace(":", "_");

        }

        #endregion // [Constructors]
    }
}

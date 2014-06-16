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
        private CaptureWindowViewModel pParentWindow;
        private Brush pBackgraundBrush;
        private ImageSource pImageSource;
        private string pfileName;
        private double pwindowWidth;
        private double pwindowHeight;
        private double pImageWidth;
        private double pImageHeight;


        public PreViewCaptureWindowViewModel(double width, double height, ImageSource source, CaptureWindowViewModel parent)
        {
            WindowsSizeConfiguration(width, height, parent);
            pParentWindow = parent;
            ImageHeight = source.Height * Render.PixelSize;
            ImageWidth = source.Width * Render.PixelSize;
            SourceImage = source;

            string tempstr = DateTime.Now.ToBinary().ToString();


            tempstr = tempstr.Replace(".", "_");
            tempstr = tempstr.Replace("-", String.Empty);
            FileName = tempstr.Replace(":", "_");

        }



        public double ImageWidth
        {
            get { return pImageWidth; }
            set
            {
                pImageWidth = value;
                OnPropertyChanged("ImageWidth");
            }
        }

        public double ImageHeight
        {
            get { return pImageHeight; }
            set
            {
                pImageHeight = value;
                OnPropertyChanged("ImageHeight");
            }
        }

        public double WindowWidth
        {
            get { return pwindowWidth; }
            set
            {
                pwindowWidth = value;
                OnPropertyChanged("WindowWidth");
            }
        }

        public double WindowHeight
        {
            get { return pwindowHeight; }
            set
            {
                pwindowHeight = value;
                OnPropertyChanged("WindowHeight");
            }
        }

        public ImageSource SourceImage
        {
            get { return pImageSource; }
            set
            {
                pImageSource = value;
                OnPropertyChanged("Source");
            }
        }

        public string FileName
        {
            get { return pfileName; }
            set
            {
                pfileName = value;
                OnPropertyChanged("FileName");
            }
        }


        private void WindowsSizeConfiguration(double width, double height, CaptureWindowViewModel parent)
        {
            //Delete this shit.
            if (width > parent.Width * 0.7d)
                WindowWidth = parent.Width * 0.7d;
            else
                WindowWidth = width + 70;
            if (height > pParentWindow.Height * 0.7)
                WindowHeight = pParentWindow.Height * 0.7;
            else
                WindowHeight = height + 140;
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenCapture.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        #region [Fields]

        double left = 0;
        double top = 0;
        double height = 50;
        double width = 50;
        double to;


        private string tag;

        public string Tag
        {
            get { return tag; }
            set
            {
                tag = value;
                OnPropertyChanged("Tag");
            }
        }



        #endregion // [Fields]

        #region [Properties]



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

        public double Height
        {
            get { return height; }
            set
            {
                height = value;
                OnPropertyChanged("Height");
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

        public double To
        {
            get { return to; }
            set { to = value; }
        }

        public WindowStateEnum MainWindowStateEnum
        {
            get;
            set;
        }

        #endregion  [Properties]

        #region [Constructors\Destructors]

        public MainViewModel()
        {
            Options.Instance.InitializeOptions();
        }

        ~MainViewModel()
        {

        }

        #endregion //[Constructors\Destructors]

    }
}

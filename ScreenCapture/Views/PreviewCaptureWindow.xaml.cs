using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ScreenCapture.Views
{
    /// <summary>
    /// Interaction logic for PreviewCaptureWindow.xaml
    /// </summary>
    public partial class PreviewCaptureWindow : Window
    {
        public PreviewCaptureWindow()
        {
            InitializeComponent();
        }

        private void Move_Window_Handler(object sender, MouseButtonEventArgs e)
        {
            Window w = Window.GetWindow(this);
            this.DragMove();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            (sender as Window).WindowState = System.Windows.WindowState.Normal;
        }
    }
}

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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ScreenCapture.ViewModels;
using ScreenCaptureAPI;

namespace ScreenCapture.Views
{
    /// <summary>
    /// Interaction logic for CaptureWindow.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for CaptureWindow.xaml
    /// </summary>
    public partial class CaptureWindow : Window
    {
        private bool mouseIsPressed = false;
        private Point startPoint;
        private Storyboard anim;
        private CaptureAPI screenManager;

        public CaptureWindow()
        {
            InitializeComponent();
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((DataContext as CaptureWindowViewModel).IsFullScreen)
                return;
            popup.Visibility = System.Windows.Visibility.Collapsed;
            mouseIsPressed = true;
            startPoint = e.GetPosition(DrawCanvas);
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if ((DataContext as CaptureWindowViewModel).IsFullScreen)
                return;
            if ((DataContext as CaptureWindowViewModel).Height - (DataContext as CaptureWindowViewModel).SelectedRect.BottomLeft.Y > 80)
                anim = FindResource("popupAnimationDown") as Storyboard;
            else
                anim = FindResource("popupAnimationUp") as Storyboard;
            mouseIsPressed = false;
            popup.Visibility = System.Windows.Visibility.Visible;
            anim.Completed += new EventHandler(anim_Completed);
            anim.Begin();
        }

        void anim_Completed(object sender, EventArgs e)
        {

        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsPressed)
            {
                Point endPoint = e.GetPosition(DrawCanvas);
                (DataContext as CaptureWindowViewModel).SelectedRect = new Rect(startPoint, endPoint);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Window.GetWindow(this).Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if ((DataContext as CaptureWindowViewModel).IsFullScreen)
            {
                popup.Visibility = System.Windows.Visibility.Visible;
                anim = FindResource("popupAnimationUp") as Storyboard;
                anim.Begin();
            }
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            (sender as Window).WindowState = System.Windows.WindowState.Normal;
        }
    }
}

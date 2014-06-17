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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ScreenCapture.ViewModels;
using ScreenCaptureAPI;
using ScreenCaptureAPI.Models;

namespace ScreenCapture.Views
{
    /// <summary>
    /// Interaction logic for VideoWindowButtonsPanel.xaml
    /// </summary>
    public partial class VideoWindowButtonsPanel : UserControl
    {
        private CaptureAPI captureAPI = ContainerManager.Resolve<CaptureAPI>();

        public VideoWindowButtonsPanel()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as VideoWindowViewModel).Seconds.Start();
            StartButton.Visibility = System.Windows.Visibility.Collapsed;
            StopButton.Visibility = System.Windows.Visibility.Visible;
            captureAPI.StartRecording
                (new ScreenCaptureConfigModel
                {
                    screenRectangle = (DataContext as VideoWindowViewModel).Rectangle
                });
        }

        private void Stop_Button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as VideoWindowViewModel).Seconds.Stop();
            captureAPI.StopRecording();

            Window.GetWindow(this).Close();
        }

        private void Resume_Button_Click(object sender, RoutedEventArgs e)
        {
            PauseButton.Visibility = System.Windows.Visibility.Visible;
            ResumeButton.Visibility = System.Windows.Visibility.Collapsed;
            captureAPI.ResumeRecording();
        }

        private void Pause_Button_Click(object sender, RoutedEventArgs e)
        {
            PauseButton.Visibility = System.Windows.Visibility.Collapsed;
            ResumeButton.Visibility = System.Windows.Visibility.Visible;
            captureAPI.PauseRecording();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as VideoWindowViewModel).Seconds.Stop();
            (DataContext as VideoWindowViewModel).timerMilisecondCount = 0;
            (DataContext as VideoWindowViewModel).secondCount = 0;
            (DataContext as VideoWindowViewModel).Time = "";
            captureAPI.Dispose();

            StartButton.Visibility = System.Windows.Visibility.Visible;
            StopButton.Visibility = System.Windows.Visibility.Collapsed;
            PauseButton.Visibility = System.Windows.Visibility.Visible;
            ResumeButton.Visibility = System.Windows.Visibility.Collapsed;

            Window.GetWindow(this).Close();
        }

        private void Thumb_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {

        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {

        }

        private void Thumb_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {

        }
    }
}

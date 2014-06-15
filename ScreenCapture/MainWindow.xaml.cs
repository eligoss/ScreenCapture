using System.Threading;
using System.Windows;
using ScreenCaptureAPI;

namespace ScreenCapture
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CaptureAPI screenManager;

        public MainWindow()
        {
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            InitializeComponent();
            Bootstrapper.Initialise();
            screenManager = ContainerManager.Resolve<CaptureAPI>();

            screenManager.StartRecording();

            //    Thread workerOne = new Thread(() => screenManager.StartRecording());
            //   workerOne.Priority = ThreadPriority.Highest;           
            //    workerOne.Start();




            //workerOne.Priority = ThreadPriority.AboveNormal();




            //   using (ScreenManager screenManager = ContainerManager.Resolve<ScreenManager>())
            //   {



            //    }


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            screenManager.StopRecording();
        }
    }
}

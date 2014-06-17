using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows;
using ScreenCapture.ViewModels;
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
            Bootstrapper.Initialise();
            screenManager = ContainerManager.Resolve<CaptureAPI>();

            //this.DataContext = new MainViewModel();
            int countofProcess = RunningInstance();
            if (countofProcess == 1)
            {
                System.Windows.Forms.MessageBox.Show("ScreenGrabberNet already run.", "ScreenGrabberNet");
                this.Close();
                return;
            }
            if (countofProcess >= 2)
            {
                this.Close();
                return;
            }

            InitializeComponent();
        }

        public static int RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            int count = 0;
            //Просматриваем все процессы 
            foreach (Process process in processes)
            {
                //Игнорируем текущий процесс
                if (process.Id != current.Id)
                {
                    //Проверяем, что процесс запущен из того же файла 
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        count++;
                        //Да, это и есть копия нашего приложения
                    }
                }
            }
            //Нет, таких же процессов не найдено
            return count;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void Window_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void Capture_Button_Click(object sender, RoutedEventArgs e)
        {
            Views.CaptureWindow captureWindow = new Views.CaptureWindow();
            captureWindow.Owner = this.Owner;
            captureWindow.DataContext = new CaptureWindowViewModel(false);
            captureWindow.ShowDialog();
        }

        private void MainWindowName_Loaded(object sender, RoutedEventArgs e)
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Left = screenWidth / 2 - 150;
        }

        private void FullScreenCapture_Button_Click(object sender, RoutedEventArgs e)
        {
            Views.CaptureWindow captureWindow = new Views.CaptureWindow();
            captureWindow.Owner = this.Owner;
            captureWindow.DataContext = new CaptureWindowViewModel(true);
            captureWindow.ShowDialog();
        }

        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {
            Views.SettingsWindow optWindow = new Views.SettingsWindow();
            optWindow.Owner = this.Owner;
            optWindow.DataContext = new SettingsViewModel(ContainerManager.Resolve<IConfigManager>());
            optWindow.ShowDialog();
        }

        private void Quit_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
            screenManager.Dispose();
        }

        private void MainWindowName_StateChanged(object sender, System.EventArgs e)
        {
            (sender as Window).WindowState = System.Windows.WindowState.Normal;
        }


    }
}

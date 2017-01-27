using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


namespace WinTimerInfo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public WinTimers WinTimers;
        private bool autoTestEnabled;

        public MainWindow()
        {
            Trace.Listeners.Add(new TextWriterTraceListener("Result.log", "myListener"));

            WinTimers = new WinTimers();

            InitializeComponent();
            this.DataContext = WinTimers;

            ButtonStop.IsEnabled = false;

            string[] args = Environment.GetCommandLineArgs();

            // Process command line args
            for (int i = 0; i != args.Length; ++i)
            {
                if (args[i] == "-a")
                {
                    autoTestEnabled = true;
                }
            }
        }

        void OnClickStart(object sender, RoutedEventArgs e)
        {
            ButtonStart.IsEnabled = false;
            ButtonStop.IsEnabled = true;

            WinTimers.Start();
        }

        void OnClickPause(object sender, RoutedEventArgs e)
        {
            ButtonStart.IsEnabled = true;
            ButtonStop.IsEnabled = false;

            WinTimers.Pause();
        }

        void OnClickReset(object sender, RoutedEventArgs e)
        {
            WinTimers.Reset();
        }

        void OnClickLog(object sender, RoutedEventArgs e)
        {
            WinTimers.Log();
        }

        void AutoTest()
        {
            WinTimers.Start();
            Thread.Sleep(15000);
            WinTimers.Pause();
            WinTimers.Log();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (autoTestEnabled)
            {
                ButtonStart.IsEnabled = false;
                ButtonStop.IsEnabled = false;
                ButtonReset.IsEnabled = false;

                await Task.Run(() => AutoTest());

                ButtonStart.IsEnabled = true;
                ButtonStop.IsEnabled = false;
                ButtonReset.IsEnabled = true;

                Application.Current.Shutdown();
            }
        }
    }
}

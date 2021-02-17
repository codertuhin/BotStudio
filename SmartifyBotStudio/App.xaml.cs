using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Ofir_Shtainfeld
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        // give the mutex a  unique name
        private const string MutexName = "##||LDP||##";
        // declare the mutex
        private readonly Mutex _mutex;
        // overload the constructor
        bool createdNew;

        public App()
            : base()
        {
            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;

            _mutex = new Mutex(true, MutexName, out createdNew);
            if (!createdNew)
            {
                // if the mutex already exists, notify and quit
                // MessageBox.Show(String.Format("{0} is already running.", GISCLASS.MainClass.ApplicationName), GISCLASS.MainClass.ApplicationName, MessageBoxButton.OK, MessageBoxImage.Exclamation);

                //MessageBox.Show("Another instance of the application is running.", FluentTest.Properties.Resources.ApplicationTitle, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Application.Current.Shutdown(0);
            }

        }



        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {


            var st = new StackTrace(e.Exception, true);
            var query = st.GetFrames()         // get the frames
                  .Select(frame => new
                  {                   // get the info
                      FileName = frame.GetFileName(),
                      LineNumber = frame.GetFileLineNumber(),
                      ColumnNumber = frame.GetFileColumnNumber(),
                      Method = frame.GetMethod(),
                      Class = frame.GetMethod().DeclaringType,
                  }).First();

            var errorMessage = string.Format("Message: {0} \nClass: {1} \nColumn: {2} \nLine: {3} \nFile: {4} \nMethod: {5}", e.Exception.Message, query.Class, query.ColumnNumber, query.LineNumber, query.FileName, query.Method);

            string LogFileName = DateTime.Today.ToShortDateString().Replace("/", "");
            LogFileName += DateTime.Now.ToShortTimeString().Replace(":", "");
            LogFileName = LogFileName.Replace("AM", "");
            LogFileName = LogFileName.Replace("PM", "");
            LogFileName += ".log";

            //if (!Directory.Exists(System.IO.Path.Combine(Utilities.StaticVariables.LocalLDPFolder, @"Logs\")))
            //{
            //    Directory.CreateDirectory(System.IO.Path.Combine(Utilities.StaticVariables.LocalLDPFolder, @"Logs\"));
            //}


            //File.WriteAllText(System.IO.Path.Combine(Utilities.StaticVariables.LocalLDPFolder, @"Logs\", LogFileName.Trim()), errorMessage);

            //errorMessage = String.Format("An error occured while running the application. Please check the '{0}' for more details.\nWould you like to continue running the application?", LogFileName);
            //errorMessage = ""
            //errorMessage = string.Format("An unknown error has occured. Please correct all information of a Beam, Column and Foundation then try again.", e.Exception.Message);

            if (MessageBox.Show(errorMessage, "Error", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.No)
            {
                Application.Current.Shutdown();
            }

            //if (MessageBox.Show(errorMessage, "Error", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.No)
            //{
            //    Application.Current.Shutdown();
            //}

            e.Handled = true;
        }


    }
}


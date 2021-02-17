using MahApps.Metro.Controls;
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
using SmartifyBotStudio.RobotDesigner.Variable;

namespace SmartifyBotStudio
{
    /// <summary>
    /// Interaction logic for StartupPage.xaml
    /// </summary>
    public partial class StartupPage : UserControl
    {
        public StartupPage()
        {
            InitializeComponent();
           

        }

        private void btnCaptureTask_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBuildTask_Click(object sender, RoutedEventArgs e)
        {
            //VariableStorage.InitializeWithNull();
            this.Cursor = Cursors.Wait;
            MetroWindow window = new MetroWindow()
            {
                Content = new SmartifyBotStudio.MainPage(),
            };

            window.SizeToContent = SizeToContent.WidthAndHeight;
            //window.WindowState = WindowState.Normal;
            window.WindowState = WindowState.Maximized;
            window.Title = "Smartify Bot Studio - Main";
            window.Show();

            //(this.Parent as MetroWindow).Close();  

            this.Cursor = Cursors.Arrow;
        }
    }
}

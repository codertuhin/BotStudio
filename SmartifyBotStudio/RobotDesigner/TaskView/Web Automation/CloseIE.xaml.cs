using MahApps.Metro.Controls;
using SmartifyBotStudio.RobotDesigner.TaskModel;
using SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation;
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
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace SmartifyBotStudio.RobotDesigner.TaskView.Web_Automation
{
    /// <summary>
    /// Interaction logic for LaunchNewIE.xaml
    /// </summary>
    public partial class CloseIE : UserControl
    {

        public RobotDesigner.TaskModel.Web_Automation.CloseIE CloseIEData { get; set; }

        public CloseIE()
        {
            InitializeComponent();
            Loaded += CloseIE_loaded;
        }

        private void CloseIE_loaded(object sender, RoutedEventArgs e)
        {
            webBrowserInstanceVar_dropdown.ItemsSource = VariableStorage.WebDriverVar.Keys;

            if (CloseIEData.flag)
            {
                webBrowserInstanceVar_dropdown.SelectedIndex = CloseIEData.dropDownIndex;
            }
           
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {





            CloseIEData.webDriverInstanceVar = webBrowserInstanceVar_dropdown.Text;
            CloseIEData.dropDownIndex = webBrowserInstanceVar_dropdown.SelectedIndex;
            CloseIEData.flag = true;


                ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
            






        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

    }
}

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
    public partial class LaunchNewIE : UserControl
    {

        public RobotDesigner.TaskModel.Web_Automation.LaunchNewIE LaunchNewIEData { get; set; }

        public LaunchNewIE()
        {
            InitializeComponent();
            Loaded += LaunchNewIE_loaded;
        }

        private void LaunchNewIE_loaded(object sender, RoutedEventArgs e)
        {
            // browser_dropdown.Text = LaunchNewIEData.Operation;


            url_text_iebrowser.Text = LaunchNewIEData.URL_New_IEBrowser;

            webInstance_text.Text = LaunchNewIEData.webDriverInstanceVar;

            //window_state_dropdown_iebrowser.Text=LaunchNewIEData.Window_state ;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {
            LaunchNewIEData.URL_New_IEBrowser = url_text_iebrowser.Text;
            LaunchNewIEData.Window_state = window_state_dropdown_iebrowser.SelectedIndex;
            LaunchNewIEData.webDriverInstanceVar = webInstance_text.Text.Trim();

            bool existanceFlag = true;


            if (VariableStorage.WebDriverVar.ContainsKey(webInstance_text.Text.Trim()))
            {
                MessageBox.Show("Variable name already exists." + "\n" + "     Please try another.");
                existanceFlag = false;
            }

            if (string.IsNullOrEmpty(url_text_iebrowser.Text.Trim()))
            {
                MessageBox.Show("Please, enter a valid URL.");
            }


            if (string.IsNullOrEmpty(webInstance_text.Text.Trim()))
            {
                MessageBox.Show("Please, Enter variable name to store IE instance.");
            }

            if (existanceFlag && !string.IsNullOrEmpty(webInstance_text.Text.Trim()) && !string.IsNullOrEmpty(url_text_iebrowser.Text.Trim()))
            {


                VariableStorage.WebDriverVar.Add(webInstance_text.Text.Trim(), Tuple.Create(LaunchNewIEData.ID, (object)"string for preventing open browser before executing"));
                VariableStorage.Url_tracker.Add(webInstance_text.Text.Trim(), url_text_iebrowser.Text.Trim());

                ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
            }






        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

    }
}

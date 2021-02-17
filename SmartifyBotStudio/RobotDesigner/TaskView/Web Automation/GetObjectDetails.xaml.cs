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
    public partial class GetObjectDetails : UserControl
    {

        public RobotDesigner.TaskModel.Web_Automation.GetObjectDetails GetObjectDetailsData { get; set; }

        HtmlElementInfo htmlElementInfo;
        public GetObjectDetails()
        {
            InitializeComponent();
            Loaded += GetObjectDetails_loaded;
        }

        private void GetObjectDetails_loaded(object sender, RoutedEventArgs e)
        {
            webBrowserInstance_combo.ItemsSource = VariableStorage.WebDriverVar.Keys;
            webBrowserInstance_combo.SelectedIndex = GetObjectDetailsData.WebBrowserInstanceIndex;
            address_textBlock.Text=GetObjectDetailsData.Address;
            control_textBlock.Text=GetObjectDetailsData.Control;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void open_automate_webBrowser(object sender, RoutedEventArgs e)
        {


            ABForScreenshot browser = new ABForScreenshot(webBrowserInstance_combo.Text);

            new MetroWindow() { Content = browser, Title = "Get Object Details" }.ShowDialog();


            htmlElementInfo = browser.screenshot_HtmlElementInfo;

            address_textBlock.Text = browser.selenium_url;
            control_textBlock.Text = "<" + browser.screenshot_HtmlElementInfo.Html_element_tag + "> id=\"" + browser.screenshot_HtmlElementInfo.Html_element_id + "\" class=\"" +
                browser.screenshot_HtmlElementInfo.Html_element_class + "\" name=\"" + browser.screenshot_HtmlElementInfo.Html_element_name+"\"";



        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {


            GetObjectDetailsData.WebElementDetailsStoreVar =VariableStorage.VariableNameFormator( attribureStore_text.Text.Trim());
            GetObjectDetailsData.htmlElementInfo = htmlElementInfo;
            GetObjectDetailsData.WebDriverInstanceVar = webBrowserInstance_combo.Text;
            GetObjectDetailsData.Address = address_textBlock.Text;
            GetObjectDetailsData.Control = control_textBlock.Text;


            if (VariableStorage.WebElementDetailsVar.ContainsKey(GetObjectDetailsData.WebElementDetailsStoreVar))
            {
                VariableStorage.WebDetailsVar.Remove(GetObjectDetailsData.WebElementDetailsStoreVar);
            }
            VariableStorage.WebElementDetailsVar.Add(GetObjectDetailsData.WebElementDetailsStoreVar, Tuple.Create(GetObjectDetailsData.ID, ""));


            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

    }
}

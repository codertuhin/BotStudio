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
    public partial class HoverOnWebObject : UserControl
    {

        public RobotDesigner.TaskModel.Web_Automation.HoverOnWebObject HoverOnWebObjectData { get; set; }

        HtmlElementInfo htmlElementInfo;
        public HoverOnWebObject()
        {
            InitializeComponent();
            Loaded += HoverOnWebObject_loaded;
        }

        private void HoverOnWebObject_loaded(object sender, RoutedEventArgs e)
        {
            webBrowserInstance_combo.ItemsSource = VariableStorage.WebDriverVar.Keys;
            webBrowserInstance_combo.SelectedIndex = HoverOnWebObjectData.WebBrowserInstanceIndex;
            address_textBlock.Text = HoverOnWebObjectData.Address;
            control_textBlock.Text = HoverOnWebObjectData.Control;
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
                browser.screenshot_HtmlElementInfo.Html_element_class + "\" name=\"" + browser.screenshot_HtmlElementInfo.Html_element_name + "\""+"XPath= "+ browser.screenshot_HtmlElementInfo.Html_element_XPATH;



        }

        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            HoverOnWebObjectData.htmlElementInfo = htmlElementInfo;
            HoverOnWebObjectData.WebDriverInstanceVar = webBrowserInstance_combo.Text;
            HoverOnWebObjectData.Address = address_textBlock.Text;
            HoverOnWebObjectData.Control = control_textBlock.Text;


            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

    }
}

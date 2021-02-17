﻿using System;
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
using SmartifyBotStudio.RobotDesigner.TaskModel;
using SmartifyBotStudio.RobotDesigner.TaskView;
using SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation;
using MahApps.Metro.Controls;
using SmartifyBotStudio.RobotDesigner.Variable;
using System.Collections.ObjectModel;

namespace SmartifyBotStudio.RobotDesigner.TaskView.Web_Automation
{
    /// <summary>
    /// Interaction logic for PopulateTextFiedOnWeb.xaml
    /// </summary>
    public partial class PopulateTextFiedOnWeb : UserControl
    {

        public RobotDesigner.TaskModel.Web_Automation.PopulateTextFiedOnWeb PopulateTextData { get; set; }


        //public List<HtmlElementInfo> HtmlInfoList;
        public ObservableCollection<HtmlElementInfo> HtmlInfoList;



        public string selenium_url { get; set; }
        public string selenium_id { get; set; }
        public string selenium_name { get; set; }
        public string selenium_input_text { get; set; }
        public string title { get; set; }
        
        


        public PopulateTextFiedOnWeb()
        {
            InitializeComponent();
            Loaded += populate_text_file_Loaded;

        }

        private void populate_text_file_Loaded(object sender, RoutedEventArgs e)
        {

            webBrowserInstance_dropdown.ItemsSource = VariableStorage.WebDriverVar.Keys;
            input_text_listView.ItemsSource = PopulateTextData.PopulateTextField_HtmlElemtnInfo_list;
            title_text_field_textBlock.Text = PopulateTextData.selenium_title;
        }

        
        private void open_automate_webBrowser(object sender, RoutedEventArgs e)
        {


           // ABForScreenshot browser = new ABForScreenshot(webBrowserInstance_dropdown.Text);
            AutomationBrowser browser = new AutomationBrowser(webBrowserInstance_dropdown.Text);

            new MetroWindow() { Content = browser, Title = "Automation Browser" }.ShowDialog();
            
                title = browser.title;
                title_text_field_textBlock.Text = title;
                //selenium_url = browser.url_text.Text.Trim();

            string url = browser.url_text.Text.Trim();
            Uri uri;


            if ((Uri.TryCreate(url, UriKind.Absolute, out uri) || Uri.TryCreate("http://" + url, UriKind.Absolute, out uri) || Uri.TryCreate("https://" + url, UriKind.Absolute, out uri)) &&
                (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            {
                selenium_url = uri.ToString();
            }

            //MessageBox.Show(selenium_url);
            

            selenium_id = browser.selenium_id;
                selenium_name = browser.selenium_name;

                HtmlInfoList = browser.AutomationBrowser_HtmlElementInfo;

                input_text_listView.ItemsSource = HtmlInfoList;

            



        }



        private void btnOpenFiles_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            PopulateTextData.selenium_url = selenium_url;
                     
            PopulateTextData.PopulateTextField_HtmlElemtnInfo_list = HtmlInfoList;

            PopulateTextData.webDriverInstanceVar = webBrowserInstance_dropdown.Text;




            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;

        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
    }

}

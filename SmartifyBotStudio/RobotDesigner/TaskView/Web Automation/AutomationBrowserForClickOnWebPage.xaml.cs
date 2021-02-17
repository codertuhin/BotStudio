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
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Microsoft.VisualBasic;
using MahApps.Metro.Controls;
using SmartifyBotStudio.RobotDesigner.Variable;
using System.Collections.ObjectModel;
using SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation;
using mshtml;

namespace SmartifyBotStudio.RobotDesigner.TaskView.Web_Automation
{
    /// <summary>
    /// Interaction logic for AutomationBrowser.xaml
    /// </summary>
    /// 

    

    public partial class AutomationBrowserForClickOnWebPage : UserControl
    {
        private System.Windows.Forms.WebBrowser automation_browser;

        mshtml.IHTMLDocument2 htmlDocument2;
        private System.Windows.Forms.HtmlDocument document;
        public string selenium_url { get; set; }
        public string selenium_id { get; set; }
        public string selenium_name { get; set; }
        public string selenium_class { get; set; }


        int parentCounting;

        public AutomationBrowserForClickOnWebPage()
        {
            InitializeComponent();
            //url_text.Text = WebDriverGenerator.working_driver.Url;
            //automation_browser.Navigate(WebDriverGenerator.working_driver.Url);

            automation_browser = new System.Windows.Forms.WebBrowser
            {
                //disable the default context menu
                //IsWebBrowserContextMenuEnabled = false
            };



           



            automation_browser.DocumentCompleted +=
        new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(web_DocumentCompleted);

            windowsFormsHost.Child = automation_browser;

        }


        public AutomationBrowserForClickOnWebPage(string webInstanceVar)
        {
            InitializeComponent();

            automation_browser = new System.Windows.Forms.WebBrowser
            {
                //disable the default context menu
                //IsWebBrowserContextMenuEnabled = false
            };







            automation_browser.DocumentCompleted +=
        new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(web_DocumentCompleted);

            windowsFormsHost.Child = automation_browser;


            if (!string.IsNullOrEmpty(webInstanceVar))
            {
                if (!string.IsNullOrEmpty(VariableStorage.Url_tracker[webInstanceVar]))
                {
                    url_text.Text = VariableStorage.Url_tracker[webInstanceVar];
                    selenium_url = url_text.Text;
                    automation_browser.Navigate(url_text.Text);
                }
            }


        }


        private void MenuItemOnClick(object sender, EventArgs eventArgs)
        {
            MessageBox.Show("Hello !!!");
        }






       




        //public List<HtmlElementInfo> AutomationBrowser_HtmlElementInfo = new List<HtmlElementInfo>();
        public ObservableCollection<HtmlElementInfo> AutomationBrowser_HtmlElementInfo = new ObservableCollection<HtmlElementInfo>();

        public List<string> input_id_check_str = new List<string>();
        public string title { get; set; }



        private void web_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {


            mshtml.IHTMLDocument2 htmlDocument = automation_browser.Document.DomDocument as mshtml.IHTMLDocument2;
            htmlDocument2 = htmlDocument;



            this.document = this.automation_browser.Document;
            this.document.MouseDown += new System.Windows.Forms.HtmlElementEventHandler(document_MouseDown);
            //this.document.MouseOver += new System.Windows.Forms.HtmlElementEventHandler(document_MouseOver);
            //this.document.MouseLeave += new System.Windows.Forms.HtmlElementEventHandler(document_MouseLeave);

            
           

        }



        void document_MouseDown(object sender, System.Windows.Forms.HtmlElementEventArgs e)
        {

            System.Windows.Forms.HtmlElement element = this.automation_browser.Document.GetElementFromPoint(e.ClientMousePosition);

            //if (e.MouseButtonsPressed == System.Windows.Forms.MouseButtons.Right)
            //{
            //    //System.Windows.Forms.ContextMenu BrowserContextMenu = new System.Windows.Forms.ContextMenu();

               


            //    //System.Windows.Forms.MenuItem MenuItem1 = new System.Windows.Forms.MenuItem { Text = "SELECT THIS OBJECT" };
            //    //MenuItem1.Click += new EventHandler(delegate { MessageBox.Show("L1 Click"); });

         

               



            //    ////MenuItem1.PerformClick()

            //    //BrowserContextMenu.MenuItems.Add(MenuItem1);

            //    //System.Windows.Forms.MenuItem MenuItem2 = new System.Windows.Forms.MenuItem { Text = "SELECT THIS OBJECT" };
            //    //MenuItem2.Click += MenuItemOnClick;
            //    //BrowserContextMenu.MenuItems.Add(MenuItem2);

            //    //automation_browser.ContextMenu = BrowserContextMenu;

            //    //put the browser control in the windows forms host
                

            //}

            //******************* for testing XPATH *****************



            var savedId = element.Id;
            var uniqueId = Guid.NewGuid().ToString();
            element.Id = uniqueId;

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(element.Document.GetElementsByTagName("html")[0].OuterHtml);
            element.Id = savedId;

            var node = doc.GetElementbyId(uniqueId);
            string xpath = node.XPath;



            string[] parentNodes = xpath.Split('/');

            parentCounting = 0;

            System.Windows.Forms.HtmlElement starting_element = Finding_Parent_With_Identifier(element);

            string starting_element_idName = starting_element.GetAttribute("id");

            string startingPattern = @"//*[@";
            string middlePattern = "=" + '"'; //"="";
            string endingPattern = '"' + "]"; //""]";

            string XPATH = startingPattern + "id" + middlePattern + starting_element_idName + endingPattern;

            for (int i = parentNodes.Length - parentCounting + 1; i < parentNodes.Length; i++)
            {
                string tmp = '/' + parentNodes[i];
                XPATH += tmp;
            }



            //******************* for testing XPATH *****************

            if (e.MouseButtonsPressed == System.Windows.Forms.MouseButtons.Right)
            {

                string element_tagName = element.TagName.ToString();

                selenium_id = element.GetAttribute("id").Trim();

                selenium_class= element.GetAttribute("className").Trim();
                if (!input_id_check_str.Contains(selenium_id.ToLower()))
                {
                    string msgtext = "Select this Object?";
                    string txt = Application.Current.MainWindow.Title;
                    MessageBoxButton button = MessageBoxButton.YesNoCancel;
                    MessageBoxResult result = MessageBox.Show(msgtext, txt, button);

                    switch (result)
                    {
                        case MessageBoxResult.Yes:


                            input_id_check_str.Add(selenium_id.ToLower());
                            selenium_name = element.GetAttribute("name").Trim();
                            AutomationBrowser_HtmlElementInfo.Add(new HtmlElementInfo()
                            {
                                Html_element_id = selenium_id,
                                Html_element_name = selenium_name,
                                Html_element_class = selenium_class,
                                Html_element_XPATH = XPATH


                            });

                            //(this.Parent as MetroWindow).DialogResult = true;
                            //(this.Parent as MetroWindow).Close();
                            break;
                        case MessageBoxResult.No:

                            break;
                        case MessageBoxResult.Cancel:

                            break;
                    }

                }
            }



        }



        System.Windows.Forms.HtmlElement Finding_Parent_With_Identifier(System.Windows.Forms.HtmlElement element)
        {
            // System.Windows.Forms.HtmlElement parent = element.Parent;

            string className = element.GetAttribute("className");
            string idName = element.GetAttribute("id");
            string name = element.GetAttribute("name").Trim();
            string tagName = element.TagName;

            parentCounting++;

            return idName != string.Empty ? element : Finding_Parent_With_Identifier(element.Parent);


        }

        void EnterKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                string url = url_text.Text.Trim();
                Uri uri;

               // MessageBox.Show(url);
                if ((Uri.TryCreate(url, UriKind.Absolute, out uri) || Uri.TryCreate("http://" + url, UriKind.Absolute, out uri) || Uri.TryCreate("https://" + url, UriKind.Absolute, out uri)) &&
                    (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
                {
                    automation_browser.Navigate(uri);
                }


                selenium_url = uri.ToString();
                //MessageBox.Show(selenium_url);
                e.Handled = true;
            }
        }

    }
}

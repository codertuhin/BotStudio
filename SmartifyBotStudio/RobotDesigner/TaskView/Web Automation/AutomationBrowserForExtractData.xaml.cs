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

using MahApps.Metro.Controls.Dialogs;
using HtmlAgilityPack;

namespace SmartifyBotStudio.RobotDesigner.TaskView.Web_Automation
{
    /// <summary>
    /// Interaction logic for AutomationBrowser.xaml
    /// </summary>
    public partial class AutomationBrowserForExtractData : UserControl
    {

        mshtml.IHTMLDocument2 htmlDocument2;
        private System.Windows.Forms.HtmlDocument document;
        public string selenium_url { get; set; }
        public string selenium_id { get; set; }
        public string selenium_name { get; set; }

        public List<string> DataList;//{ get; set; }
        public List<string> HtmlList;// { get; set; }
        public List<string> WebData;
        public List<System.Windows.Forms.HtmlElement> Element;// { get; set; }

        // private System.Windows.Forms.HtmlDocument document;

        private IDictionary<System.Windows.Forms.HtmlElement, string> elementStyles = new Dictionary<System.Windows.Forms.HtmlElement, string>();

        //public List<HtmlElementInfo> AutomationBrowser_HtmlElementInfo = new List<HtmlElementInfo>();
        public ObservableCollection<HtmlElementInfo> AutomationBrowser_HtmlElementInfo = new ObservableCollection<HtmlElementInfo>();

        public List<string> input_id_check_str = new List<string>();
        public string title { get; set; }

        int parentCounting;


        public AutomationBrowserForExtractData()
        {
            InitializeComponent();
            //url_text.Text = WebDriverGenerator.working_driver.Url;
            //automation_browser.Navigate(WebDriverGenerator.working_driver.Url);

        }


        public AutomationBrowserForExtractData(string webInstanceVar)
        {
            InitializeComponent();

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

      


        private void web_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {


            mshtml.IHTMLDocument2 htmlDocument = automation_browser.Document.DomDocument as mshtml.IHTMLDocument2;
            htmlDocument2 = htmlDocument;

            DataList = new List<string>();
            HtmlList = new List<string>();
            WebData = new List<string>();
            Element = new List<System.Windows.Forms.HtmlElement>();

            this.document = this.automation_browser.Document;
            this.document.MouseDown += new System.Windows.Forms.HtmlElementEventHandler(document_MouseDown);
            this.document.MouseOver += new System.Windows.Forms.HtmlElementEventHandler(document_MouseOver);
            this.document.MouseLeave += new System.Windows.Forms.HtmlElementEventHandler(document_MouseLeave);


        }



        void document_MouseDown(object sender, System.Windows.Forms.HtmlElementEventArgs e)
        {

            System.Windows.Forms.HtmlElement element = this.automation_browser.Document.GetElementFromPoint(e.ClientMousePosition);

            string className = element.GetAttribute("className");
            string tagName = element.TagName;


            if (e.MouseButtonsPressed == System.Windows.Forms.MouseButtons.Right)
            {

                //string innerhtml = element.InnerHtml;
                if (!Element.Contains(element))
                {




                    List<string> AttributeName = new List<string>() { "name", "id", "value", "href", "src", "title", "type", "text", "class" };


                    System.Windows.Forms.HtmlElementCollection links = automation_browser.Document.GetElementsByTagName(tagName);



                    //MessageBox.Show( tagName+" "+ className);




                    string msgtext = "Select this Element?";
                    string txt = Application.Current.MainWindow.Title;
                    MessageBoxButton button = MessageBoxButton.YesNoCancel;
                    MessageBoxResult result = MessageBox.Show(msgtext, txt, button);

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            Element.Add(element);
                            if (className != "")
                            {
                                foreach (System.Windows.Forms.HtmlElement link in links)
                                {
                                    if (link.GetAttribute("className") == className)
                                    {
                                        DataList.Add(link.InnerText);
                                        HtmlList.Add(link.OuterHtml);
                                    }
                                }
                            }
                            else
                            {
                                foreach (System.Windows.Forms.HtmlElement link in links)
                                {
                                    if (link.TagName == tagName)
                                    {
                                        DataList.Add(link.InnerText);
                                        HtmlList.Add(link.OuterHtml);
                                    }
                                }
                            }



                            break;
                        case MessageBoxResult.No:



                            break;
                        case MessageBoxResult.Cancel:

                            break;
                    }

                }



            }



        }

        private void document_MouseOver(object sender, System.Windows.Forms.HtmlElementEventArgs e)
        {
            System.Windows.Forms.HtmlElement element = e.ToElement;


            string dozmiany = element.OuterHtml;

            //textEditor1.Text = "ttttttttttttt" + dozmiany;


            if (!this.elementStyles.ContainsKey(element))
            {
                string style = element.Style;
                this.elementStyles.Add(element, style);
                //element.SetAttribute("data-tooltip", "tooooltip");

                //element.Style = style + "background-color: #ffc;border: 2px dotted #000000;background-clip:border-box";   // border - color: coral;  border-style: solid;
                // element.Style = style + ";data-tooltip=\"Custom tooltip text.\";";                                                            // element.Style = style + "; padding: 15px;";
                // data-tooltip="Custom tooltip text."


                if (element.GetAttribute("className") != "")
                {
                    element.Style = style + "background-color: rgba(30, 163, 97, 0.25);border: 2px dotted #000000;background-clip:border-box";

                }
                else
                {
                    element.Style = style + "background-color: rgba(255, 2, 2, 0.25);border: 2px dotted #000000;background-clip:border-box";
                }


            }


            //string ffff = wartosc.XPath;

        }


        private void document_MouseLeave(object sender, System.Windows.Forms.HtmlElementEventArgs e)
        {
            System.Windows.Forms.HtmlElement element = e.FromElement;

            if (this.elementStyles.ContainsKey(element))
            {
                string style = this.elementStyles[element];
                this.elementStyles.Remove(element);
                element.Style = style;

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
                //MessageBox.Show(url);

                if ((Uri.TryCreate(url, UriKind.Absolute, out uri) || Uri.TryCreate("http://" + url, UriKind.Absolute, out uri) || Uri.TryCreate("https://" + url, UriKind.Absolute, out uri)) &&
                    (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
                {
                    automation_browser.Navigate(uri);
                }

                //MessageBox.Show(uri.ToString());
                selenium_url = uri.ToString();
                //MessageBox.Show(selenium_url);
                e.Handled = true;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HtmlAgilityPack;
using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;


using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace SmartifyBotStudio.RobotDesigner.TaskView.Web_Automation
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1
    {
        #region Fields

        private OpenFileDialog _fileDialog = new OpenFileDialog();
        private HtmlDocument _html = new HtmlDocument();

        #endregion
        private System.Windows.Forms.HtmlDocument document;

        private IDictionary<System.Windows.Forms.HtmlElement, string> elementStyles = new Dictionary<System.Windows.Forms.HtmlElement, string>();

        public UserControl1()
        {
            InitializeComponent();
            try
            {
                //txtHtml.Text = File.ReadAllText("mshome.htm");
            }
            catch
            {
            }
            InitializeFileDialog();
        }


        private void ParseHtml()
        {
            //if (txtHtml.Text.IsEmpty()) return;

            _html = new HtmlDocument();
            _html.LoadHtml(txtHtml.Text);

            hapTree.BaseNode = _html.DocumentNode;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            HtmlNode node = _html.DocumentNode;
            if (chkFromCurrent.IsChecked == true
                && hapTree.SelectedItem != null
                && hapTree.SelectedItem is TreeViewItem
                && ((TreeViewItem)hapTree.SelectedItem).DataContext is HtmlNode)
                node = ((TreeViewItem)hapTree.SelectedItem).DataContext as HtmlNode;

            SearchFromNode(node);
        }

        private void btnTestCode_Click(object sender, RoutedEventArgs e)
        {
            var mainPage = GetHtml("http://htmlagilitypack.codeplex.com");
            var homepage = new HtmlDocument();
            homepage.LoadHtml(mainPage);

            var nodes =
                homepage.DocumentNode.Descendants("a").Where(x => x.Id.ToLower().Contains("releasestab")).FirstOrDefault
                    ();
            var link = nodes.Attributes["href"].Value;

            var dc = new HtmlDocument();
            try
            {
                Cursor = Cursors.Wait;
                var req = (HttpWebRequest)WebRequest.Create(link);
                using (var resp = req.GetResponse().GetResponseStream())
                using (var read = new StreamReader(resp))
                {
                    dc.LoadHtml(read.ReadToEnd());
                    var span =
                        dc.DocumentNode.Descendants("span").Where(
                            x => x.Id.ToLower().Contains("releasedownloadsliteral")).FirstOrDefault();
                    MessageBox.Show(
                        int.Parse(span.InnerHtml.ToLower().Replace("downloads", string.Empty).Trim()).ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading file: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error,
                                MessageBoxResult.OK);
            }
        }

        private string GetHtml(string link)
        {
            var req = (HttpWebRequest)WebRequest.Create(link);
            using (var resp = req.GetResponse().GetResponseStream())
            using (var read = new StreamReader(resp))
            {
                return read.ReadToEnd();
            }
        }


        private void hapTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {


            if (!(e.NewValue is TreeViewItem)) return;

            var t = e.NewValue as TreeViewItem;

            if (t.DataContext is HtmlNode)
            {
                HtmlAttributeViewer1.Visibility = Visibility.Hidden;
                HtmlNodeViewer1.DataContext = null;
                HtmlNodeViewer1.Visibility = Visibility.Visible;
                HtmlNodeViewer1.DataContext = t.DataContext;
                return;
            }
            if (t.DataContext is HtmlAttribute)
            {
                HtmlNodeViewer1.Visibility = Visibility.Hidden;
                HtmlAttributeViewer1.DataContext = null;
                HtmlAttributeViewer1.Visibility = Visibility.Visible;
                HtmlAttributeViewer1.DataContext = t.DataContext;
                return;
            }
        }

        private void InitializeFileDialog()
        {
            _fileDialog.FileName = "Document"; // Default file name
            _fileDialog.DefaultExt = ".html"; // Default file extension
            _fileDialog.Filter = "Text documents (.html,.htm,.aspx)|*.html;*.htm;*.aspx"; // Filter files by extension
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            //Close();
        }





        private void SearchFromNode(HtmlNode baseNode)
        {

            IEnumerable<HtmlNode> nodes = Enumerable.Empty<HtmlNode>();

            if (!_html.DocumentNode.HasChildNodes)
                ParseHtml();

            if (chkXPath.IsChecked == true)
                nodes = baseNode.SelectNodes(txtSearchTag.Text);
            else
                nodes = baseNode.Descendants(txtSearchTag.Text);

            if (nodes == null) return;

            listResults.Items.Clear();

            foreach (var node in nodes)
            {
               // var tr = new NodeTreeView { BaseNode = node };
                var lvi = new ListBoxItem();
                var pnl = new StackPanel();
                pnl.Children.Add(new Label
                {
                    Content =
                        string.Format("id:{0} name:{1} children{2}", node.Id, node.Name,
                                      node.ChildNodes.Count),
                    FontWeight = FontWeights.Bold
                });
               // pnl.Children.Add(tr);
                lvi.Content = pnl;
                listResults.Items.Add(lvi);
            }
            tabControl1.SelectedItem = tabSearchResults;
        }

        private void btnParse_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ParseHtml();
        }

        private void MenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void mnuOpenFile_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var result = _fileDialog.ShowDialog();
            if (result != true) return;
            try
            {
                Cursor = Cursors.Wait;
                var file = _fileDialog.FileName;
               // txtHtml.Text = File.ReadAllText(file);
              //  textEditor.Text = File.ReadAllText(file);
                Uri adres = new System.Uri(_fileDialog.FileName);

                web.Url = adres;
            }
            catch (FileNotFoundException fEx)
            {
                MessageBox.Show("Error loading file: " + fEx.Message, "Error", MessageBoxButton.OK,
                                MessageBoxImage.Error, MessageBoxResult.OK);
            }
            catch (FileLoadException fEx)
            {
                MessageBox.Show("Error loading file: " + fEx.Message, "Error", MessageBoxButton.OK,
                                MessageBoxImage.Error, MessageBoxResult.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading file: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error,
                                MessageBoxResult.OK);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void mnuOpenUrl_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string test_url = @"https://accounts.google.com/SignUp?hl=en-GB";
           // var dialog = new UrlDialog();
            //MessageBox.Show("ssss");
            //if (dialog.ShowDialog() != true) return;
            try
            {
                Cursor = Cursors.Wait;
                var req = (HttpWebRequest)WebRequest.Create(test_url);
                using (var resp = req.GetResponse().GetResponseStream())
                using (var read = new StreamReader(resp))
                {
                    var txt = read.ReadToEnd();
                    txtHtml.Text = txt;
                    textEditor.Text = txt;
                    
                    
                }
                var hw = new HtmlWeb();
                //_html = hw.Load(dialog.Url);
                _html = hw.Load(test_url);
                 hapTree.BaseNode = _html.DocumentNode;

               // Uri adres = new System.Uri(dialog.Url);
                Uri adres = new System.Uri(test_url);

                web.Url = adres;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading file: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error,
                                MessageBoxResult.OK);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ParseHtml();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void txtHtml_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void HtmlAttributeViewer1_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }



        mshtml.IHTMLDocument2 htmlDocument2;
        string selenium_url;
        string selenium_text;
        string selenium_id;
        string selenium_name;
        string selenium_tagName;

        private void web_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {

            //web.Navigate("javascript: window.external.CallServerSideCode();");
            mshtml.IHTMLDocument2 htmlDocument = web.Document.DomDocument as mshtml.IHTMLDocument2;
            htmlDocument2 = htmlDocument;
            //web.D
            //richTextBox1.AppendText(htmlDocument.body.parentElement.outerHTML);
            this.document = this.web.Document;
            this.document.MouseDown += new System.Windows.Forms.HtmlElementEventHandler(document_MouseDown);
            this.document.MouseOver += new System.Windows.Forms.HtmlElementEventHandler(document_MouseOver);
            this.document.MouseLeave += new System.Windows.Forms.HtmlElementEventHandler(document_MouseLeave);

        }
        void document_MouseDown(object sender, System.Windows.Forms.HtmlElementEventArgs e)
        {

            System.Windows.Forms.HtmlElement element = this.web.Document.GetElementFromPoint(e.ClientMousePosition);
            //string right_button_capture = element.GetAttribute("id");
            //selenium_id= htmlDocument2.body.document.document.getElementById("username");
            selenium_id = element.GetAttribute("id").Trim();
            selenium_name = element.GetAttribute("name").Trim();
            selenium_tagName = element.TagName.ToString();

            //MessageBox.Show("tag name=" + selenium_tagName + " name=" + selenium_name + " id=" + selenium_id);

            if (e.MouseButtonsPressed == System.Windows.Forms.MouseButtons.Right)
            {
                //MessageBox.Show(right_button_capture);

            }
        }

        private void animate(object sender, RoutedEventArgs e)
        {
            selenium_url = url_text.Text;

            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl(selenium_url);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);


            IWebElement element;

            if (selenium_id != "")
                element = driver.FindElement(By.Id(selenium_id));
            else
                element = driver.FindElement(By.Name(selenium_name));

            element.SendKeys(input_text.Text);

            driver.FindElement(By.Id(selenium_id)).SendKeys(Keys.Enter);
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

        private void document_MouseOver(object sender, System.Windows.Forms.HtmlElementEventArgs e)
        {
            System.Windows.Forms.HtmlElement element = e.ToElement;


            string dozmiany = element.OuterHtml;

            //textEditor1.Text = "ttttttttttttt" + dozmiany;


            if (!this.elementStyles.ContainsKey(element))
            {
                string style = element.Style;
                this.elementStyles.Add(element, style);
                element.Style = style + "; background-color: #ffc;";

                HtmlDocument aaa = new HtmlDocument();
                try
                {
                    string pol = htmlDocument2.body.parentElement.outerHTML;
                    string result = pol.Replace(dozmiany, element.OuterHtml);//Regex.Replace(htmlDocument2.body.parentElement.outerHTML, dozmiany, element.OuterHtml, RegexOptions.IgnoreCase);4
                                                                             //richTextBox2.AppendText(result);
                    aaa.LoadHtml(result);



                }
                catch (Exception wyjatek)
                {
                    MessageBox.Show(wyjatek.ToString());
                }

                label2.Content = dozmiany;
                // MessageBox.Show(dozmiany);
                //textEditor1.Text ="ttttttttttttt"+ dozmiany;

                //MessageBox.Show(element.GetAttribute("id"));

                HtmlNode ddd = aaa.DocumentNode;

                //richTextBox1.AppendText(ddd.OuterHtml);
                try
                {
                    HtmlNode ggg = ddd.SelectSingleNode("//*[contains(@style,'BACKGROUND-COLOR: #ffc')]");
                    if (ggg != null)
                    {
                        string lll = ggg.XPath;
                        txtSearchTag.Text = lll;
                        label3.Content = lll;
                    }
                    else
                    {
                        ggg = ddd.SelectSingleNode("//*[contains(@style,'background-color: rgb(255, 255, 204)')]");
                        if (ggg != null)
                        {
                            string lll = ggg.XPath;
                            txtSearchTag.Text = lll;
                            label3.Content = lll;
                        }
                        else
                        {
                            //richTextBox2.AppendText(result);
                            label3.Content = "Nie znaleziono";
                        }


                    }
                }
                catch (Exception sel)
                {
                    MessageBox.Show(sel.ToString());
                }
                //this.Text = element.Id ?? "(no id)";


                label1.Content = element.OuterHtml;
            }


            //string ffff = wartosc.XPath;

        }




        private void label1_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }
        FoldingManager foldingManager;
        //AbstractFoldingStrategy foldingStrategy;
        private void textEditor_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //LoadSourceCode();
            //foldingStrategy = new XmlFoldingStrategy();
           // textEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.DefaultIndentationStrategy();
            //foldingManager = FoldingManager.Install(textEditor.TextArea);
           // foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
        }
    }
}

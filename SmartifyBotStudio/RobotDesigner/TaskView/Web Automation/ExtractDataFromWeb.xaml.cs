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
using SmartifyBotStudio.RobotDesigner.TaskModel;
using SmartifyBotStudio.RobotDesigner.TaskView;
using SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation;
using MahApps.Metro.Controls;
using SmartifyBotStudio.RobotDesigner.Variable;
using System.Collections.ObjectModel;

namespace SmartifyBotStudio.RobotDesigner.TaskView.Web_Automation
{
    /// <summary>
    /// Interaction logic for ClickOnWebPage.xaml    ClickOnWebPage
    /// </summary>
    public partial class ExtractDataFromWeb : UserControl
    {

        public RobotDesigner.TaskModel.Web_Automation.ExtractDataFromWeb ExtractDataFromWebTextData { get; set; }


        public ObservableCollection<HtmlElementInfo> HtmlInfoList;

        public List<string> DataList;

        public List<string> HtmlList;

        public List<string> WebData;

        public string selenium_url { get; set; }
        public string selenium_id { get; set; }
        public string selenium_name { get; set; }
        public string selenium_input_text { get; set; }
        public string title { get; set; }





        public ExtractDataFromWeb()
        {
            InitializeComponent();
            Loaded += extractDatafromWeb_Loaded;

        }

        private void extractDatafromWeb_Loaded(object sender, RoutedEventArgs e)
        {
            webDriver_dropdown.ItemsSource = VariableStorage.WebDriverVar.Keys;

            dataRadiobtn.IsChecked = ExtractDataFromWebTextData.Data;
            htmlRadiobtn.IsChecked = ExtractDataFromWebTextData.Html;
            destination_text.Text = ExtractDataFromWebTextData.Path;
            fileName_text.Text = ExtractDataFromWebTextData.ExcelFileName;

            DataList = ExtractDataFromWebTextData.DataList;
            HtmlList = ExtractDataFromWebTextData.HtmlList;

            address_level.Content = ExtractDataFromWebTextData.Address;

            saveAs_combo.SelectedIndex=ExtractDataFromWebTextData.SaveAsIndex;

            int counter = 0;

            if (DataList != null && HtmlList != null)
            {
                foreach (string str in DataList)
                {
                    if (str == null || str == "")
                        counter++;
                }

                if (counter == DataList.Count)
                    isDataExist_level.Content = "No data found";
                else
                    isDataExist_level.Content = DataList.Count.ToString() + " data found";

                isHtmlExist_level.Content = HtmlList.Count.ToString() + " data found";
            }






        }

        private void btnOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog()
            {
                ShowNewFolderButton = true,
            };

            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                destination_text.Text = folderBrowserDialog.SelectedPath;
            }
        }
        private void open_automate_webBrowser(object sender, RoutedEventArgs e)
        {

            DataList = new List<string>();
            HtmlList = new List<string>();
            WebData = new List<string>();

            //webDriver_dropdown.Text

            AutomationBrowserForExtractData browser = new AutomationBrowserForExtractData(webDriver_dropdown.Text);

            new MetroWindow() { Content = browser, Title = "Automation Browser" }.ShowDialog();

            title = browser.title;
            title_text_field_textBlock.Text = title;
            selenium_url = browser.url_text.Text.Trim();
            address_level.Content = browser.url_text.Text;
            DataList = browser.DataList;
            HtmlList = browser.HtmlList;

            int counter = 0;

            if (DataList != null)
            {
                foreach (string str in DataList)
                {
                    if (str == null || str == "")
                        counter++;
                }

                if (counter == DataList.Count)
                    isDataExist_level.Content = "0 data found";
                else
                    isDataExist_level.Content = (DataList.Count - counter).ToString() + " data found";

            }


            if (HtmlList != null)
            {
                isHtmlExist_level.Content = HtmlList.Count.ToString() + " data found";
            }







            // WebData = browser.WebData;

            string url = browser.url_text.Text;
            Uri uri;


            if ((Uri.TryCreate(url, UriKind.Absolute, out uri) || Uri.TryCreate("http://" + url, UriKind.Absolute, out uri) || Uri.TryCreate("https://" + url, UriKind.Absolute, out uri)) &&
                (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            {
                selenium_url = uri.ToString();
            }
            selenium_name = browser.selenium_name;

            HtmlInfoList = browser.AutomationBrowser_HtmlElementInfo;

            //input_text_listView.ItemsSource = HtmlInfoList;





        }



        private void btnOpenFiles_Click(object sender, RoutedEventArgs e)
        {

        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ok_button_Click(object sender, RoutedEventArgs e)
        {
            if (fileName_text.Text.Trim() == "")
            {
                MessageBox.Show("Please, Enter a valid File Name.");
            }
            if (destination_text.Text.Trim() == "")
            {
                MessageBox.Show("Please, Enter a valid Path.");
            }


            ExtractDataFromWebTextData.SaveAsIndex = saveAs_combo.SelectedIndex;


            if (DataList != null && HtmlList != null)
            {


                ExtractDataFromWebTextData.DataList = DataList;
                ExtractDataFromWebTextData.HtmlList = HtmlList;
                // ExtractDataFromWebTextData.WebData = WebData;
                ExtractDataFromWebTextData.ExcelFileName = fileName_text.Text;
                ExtractDataFromWebTextData.Path = destination_text.Text;

                ExtractDataFromWebTextData.Data = (bool)dataRadiobtn.IsChecked;
                ExtractDataFromWebTextData.Html = (bool)htmlRadiobtn.IsChecked;

                ExtractDataFromWebTextData.Address = selenium_url;

                ExtractDataFromWebTextData.DataNumber = DataList.Count;
                ExtractDataFromWebTextData.HtmlNumber = HtmlList.Count;

            }


            if (destination_text.Text.Trim() != "" && fileName_text.Text.Trim() != "")
            {
                ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;

            }



        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            if (DataList != null && HtmlList != null)
            {
                ExtractDataFromWebTextData.DataList = DataList;
                ExtractDataFromWebTextData.HtmlList = HtmlList;
                // ExtractDataFromWebTextData.WebData = WebData;
                ExtractDataFromWebTextData.ExcelFileName = fileName_text.Text;
                ExtractDataFromWebTextData.Path = destination_text.Text;

                ExtractDataFromWebTextData.Data = (bool)dataRadiobtn.IsChecked;
                ExtractDataFromWebTextData.Html = (bool)htmlRadiobtn.IsChecked;

                ExtractDataFromWebTextData.Address = selenium_url;

                ExtractDataFromWebTextData.DataNumber = DataList.Count;
                ExtractDataFromWebTextData.HtmlNumber = HtmlList.Count;

            }

            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
    }
}

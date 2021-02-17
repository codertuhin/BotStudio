using MahApps.Metro.Controls;
using SmartifyBotStudio.RobotDesigner.TaskModel;
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
//using System.Windows.Forms;

namespace SmartifyBotStudio.RobotDesigner.TaskView.Web_Automation
{
    /// <summary>
    /// Interaction logic for TakeScreenShotOfWebPage.xaml
    /// </summary>
    public partial class TakeScreenShotOfWebPage : UserControl
    {
        public RobotDesigner.TaskModel.Web_Automation.TakeScreenShotOfWebPage TakeScreenShotOfWebPageData { get; set; }

        HtmlElementInfo htmlElementInfo;
        public TakeScreenShotOfWebPage()
        {
            InitializeComponent();
            Loaded += TakeScreenShotOfWebPage_loaded;
        }

        private void TakeScreenShotOfWebPage_loaded(object sender, RoutedEventArgs e)
        {
            webBrowserInstanceCombo.ItemsSource = VariableStorage.WebDriverVar.Keys;
            webBrowserInstanceCombo.SelectedIndex = TakeScreenShotOfWebPageData.WebDriverInstanceComboIndex;
            operationCombo.SelectedIndex = TakeScreenShotOfWebPageData.OperationIndex;

            saveImageToCombo.SelectedIndex = TakeScreenShotOfWebPageData.SaveImageToIndex;
            if (TakeScreenShotOfWebPageData.SaveImageToIndex==1)
            {                
                destination_text.Text = System.IO.Path.GetDirectoryName(TakeScreenShotOfWebPageData.ImgFileName);
                fileToSaveImage_text.Text = System.IO.Path.GetFileNameWithoutExtension(TakeScreenShotOfWebPageData.ImgFileName);
            }
        }

        private void open_automate_webBrowser(object sender, RoutedEventArgs e)
        {
            

            ABForScreenshot browser = new ABForScreenshot();

            new MetroWindow() { Content = browser, Title = "Automation Browser" }.ShowDialog();


            htmlElementInfo = browser.screenshot_HtmlElementInfo;




        }
        private void more_button_Click(object sender, RoutedEventArgs e)
        {

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
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {
            

           
            TakeScreenShotOfWebPageData.WebDriverInstanceVar = webBrowserInstanceCombo.Text;
            TakeScreenShotOfWebPageData.WebDriverInstanceComboIndex = webBrowserInstanceCombo.SelectedIndex;
            TakeScreenShotOfWebPageData.OperationIndex = operationCombo.SelectedIndex;
            if (operationCombo.SelectedIndex == 0)
            {

            }
            else
            {
                TakeScreenShotOfWebPageData.htmlElementInfo = htmlElementInfo;
            }
            TakeScreenShotOfWebPageData.ImgFormatIndex = imageFormatCombo.SelectedIndex;
            TakeScreenShotOfWebPageData.SaveImageToIndex = saveImageToCombo.SelectedIndex;
            //if (saveImageToCombo.SelectedIndex == 0)
            //{

            //}
            //else
            //{

            //}
            TakeScreenShotOfWebPageData.ImgFileName = destination_text.Text.Trim() + fileToSaveImage_text.Text.Trim();



            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }
    }
}

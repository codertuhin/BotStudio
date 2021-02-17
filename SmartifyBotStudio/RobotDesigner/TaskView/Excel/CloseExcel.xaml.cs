using MahApps.Metro.Controls;
using SmartifyBotStudio.RobotDesigner.TaskModel;
using SmartifyBotStudio.RobotDesigner.TaskModel.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SmartifyBotStudio.RobotDesigner.Variable;

namespace SmartifyBotStudio.RobotDesigner.TaskView.Excel
{
    /// <summary>
    /// Interaction logic for LaunchExcel.xaml
    /// </summary>
    public partial class CloseExcel : System.Windows.Controls.UserControl
    {

        public RobotDesigner.TaskModel.Excel.CloseExcel CloseExcelData { get; set; }


        public CloseExcel()
        {
            InitializeComponent();
            Loaded += CloseExcelFile_loaded;
        }




        private void CloseExcelFile_loaded(object sender, RoutedEventArgs e)
        {

            excelInstanceCombo.ItemsSource = VariableStorage.ExcelVar.Keys;

            excelInstanceCombo.SelectedIndex=CloseExcelData.ExcelInstanceComboIndex ;


            if (CloseExcelData.ExcelClosingSchemaIndex == 0)
            {
                beforeClosingCombo.SelectedIndex = 0;
            }
            else if (CloseExcelData.ExcelClosingSchemaIndex == 1)
            {
                beforeClosingCombo.SelectedIndex = 1;
            }
            else if (CloseExcelData.ExcelClosingSchemaIndex == 2)
            {
                documentPath_text.Text = System.IO.Path.GetDirectoryName(CloseExcelData.newFileName);
                newFileName.Text = System.IO.Path.GetFileNameWithoutExtension(CloseExcelData.newFileName);
                docFormatCombo.SelectedIndex=CloseExcelData.newFileNameExtIndex;
                beforeClosingCombo.SelectedIndex = 2;
            }




        }





        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }


        private void ok_button_Click(object sender, RoutedEventArgs e)
        {



            CloseExcelData.ExcelFileToCloseVar = excelInstanceCombo.Text.Trim();
            CloseExcelData.ExcelInstanceComboIndex = excelInstanceCombo.SelectedIndex;




            if (beforeClosingCombo.SelectedIndex == 0)
            {
                CloseExcelData.ExcelClosingSchemaIndex = 0;
            }
            else if (beforeClosingCombo.SelectedIndex == 1)
            {
                CloseExcelData.ExcelClosingSchemaIndex = 1;
            }
            else if (beforeClosingCombo.SelectedIndex == 2)
            {

                CloseExcelData.ExcelClosingSchemaIndex = 2;


                string extensionText = docFormatCombo.Text;
                CloseExcelData.newFileNameExtIndex = docFormatCombo.SelectedIndex;

                string[] chunk = extensionText.Split(new Char[] { '(' }, StringSplitOptions.RemoveEmptyEntries);

                string extension = chunk[1].Trim(new Char[] { ')' });

                CloseExcelData.newFileName = documentPath_text.Text + newFileName.Text + extension;

            }






            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;





        }

        private void btnOpenFiles_Click(object sender, RoutedEventArgs e)
        {

        }

        //btnOpenFolder_Click



        private void btnOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
            {
                ShowNewFolderButton = true,
            };

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                documentPath_text.Text = folderBrowserDialog.SelectedPath;
            }
        }


        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }


    }
}

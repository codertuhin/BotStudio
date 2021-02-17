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
    public partial class LaunchExcel : System.Windows.Controls.UserControl
    {

        public RobotDesigner.TaskModel.Excel.LaunchExcel LaunchExcelData { get; set; }


        public LaunchExcel()
        {
            InitializeComponent();
            Loaded += LaunchExcelFile_loaded;
        }




        private void LaunchExcelFile_loaded(object sender, RoutedEventArgs e)
        {


            



            if (LaunchExcelData.ExcelSchemaIndex==0)
            {
                excelFileName_text.Text = System.IO.Path.GetFileNameWithoutExtension(LaunchExcelData.ExcelFileName); // System.IO.Path.GetFileName(LaunchExcelData.ExcelFileName);
                destinationFolder_text.Text = System.IO.Path.GetDirectoryName(LaunchExcelData.ExcelFileName);
                openExcelCombo.SelectedIndex = 0;

            }
            else if (LaunchExcelData.ExcelSchemaIndex==1)
            {
                opentExcelDoc_Text.Text = LaunchExcelData.ExcelFileName;
                openExcelCombo.SelectedIndex = 1;
            }

            makevisible_checkBox.IsChecked = LaunchExcelData.MakeVisible;
            excelStoreVar_text.Text=LaunchExcelData.ExcelVar;


        }





        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }


        private void ok_button_Click(object sender, RoutedEventArgs e)
        {



            //  LaunchExcelData.ExcelFileName = excelFileName_text.Text;
            Microsoft.Office.Interop.Excel.Application excel_application = new Microsoft.Office.Interop.Excel.Application();


            if (openExcelCombo.SelectedIndex==0)
            {
                LaunchExcelData.ExcelFileName = destinationFolder_text.Text  +"\\"+ excelFileName_text.Text;  //+ ".xlsx";
                LaunchExcelData.ExcelSchemaIndex = 0;

                System.Diagnostics.Process excelProcessVar = new System.Diagnostics.Process();
                excelProcessVar.StartInfo = new System.Diagnostics.ProcessStartInfo(LaunchExcelData.ExcelFileName);

                string excelInstanceName = VariableStorage.VariableNameFormator(excelStoreVar_text.Text.Trim());
                
                LaunchExcelData.ExcelVar = excelInstanceName;
                
                if (VariableStorage.ExcelVar.ContainsKey(excelInstanceName))
                {
                    VariableStorage.ExcelVar.Remove(excelInstanceName);
                }
                VariableStorage.ExcelVar.Add(excelInstanceName, Tuple.Create(LaunchExcelData.ID, excel_application));



            }
            else if(openExcelCombo.SelectedIndex == 1)
            {
                LaunchExcelData.ExcelFileName = opentExcelDoc_Text.Text;
                LaunchExcelData.ExcelSchemaIndex = 1;

                System.Diagnostics.Process excelProcessVar = new System.Diagnostics.Process();
                excelProcessVar.StartInfo = new System.Diagnostics.ProcessStartInfo(LaunchExcelData.ExcelFileName);
                if (VariableStorage.ExcelVar.ContainsKey(excelStoreVar_text.Text.Trim()))
                {
                    VariableStorage.ExcelVar.Remove(excelStoreVar_text.Text.Trim());
                }
                VariableStorage.ExcelVar.Add(excelStoreVar_text.Text.Trim(), Tuple.Create(LaunchExcelData.ID, excel_application));

                LaunchExcelData.ExcelVar= excelStoreVar_text.Text.Trim();
            }

            LaunchExcelData.MakeVisible = (bool)makevisible_checkBox.IsChecked;

            if (string.IsNullOrEmpty(excelStoreVar_text.Text))
            {
                System.Windows.MessageBox.Show("Please, enter a name to store excel file.");

            }



            if (!string.IsNullOrEmpty(excelStoreVar_text.Text))
            {
                ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;

            }



        }

        private void btnOpenFiles_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {

                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                opentExcelDoc_Text.Text = openFileDialog.FileName;

                string sourceFile = openFileDialog.FileName;


            }
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
                destinationFolder_text.Text = folderBrowserDialog.SelectedPath;
            }
        }


        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }


    }
}

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
using System.Text.RegularExpressions;

namespace SmartifyBotStudio.RobotDesigner.TaskView.Excel
{
    /// <summary>
    /// Interaction logic for LaunchExcel.xaml
    /// </summary>
    public partial class WriteToExcel : System.Windows.Controls.UserControl
    {

        public RobotDesigner.TaskModel.Excel.WriteToExcel WriteToExcelData { get; set; }


        public WriteToExcel()
        {
            InitializeComponent();
            Loaded += WriteToExcel_loaded;
        }




        private void WriteToExcel_loaded(object sender, RoutedEventArgs e)
        {

            excelInstanceCombo.ItemsSource = VariableStorage.ExcelVar.Keys;
            excelInstanceCombo.SelectedIndex = WriteToExcelData.ExcelInstanceComboIndex;
            textToWrite_text.Text=WriteToExcelData.TextToWrite;

            if (WriteToExcelData.WriteMoodIndex == 0)
            {
                cell_text.Text = WriteToExcelData.Cell.ToString();
                row_text.Text = WriteToExcelData.Row.ToString();

                writeMood_Combo.SelectedIndex = 0;
            }
            else if (WriteToExcelData.WriteMoodIndex == 1)
            {
                writeMood_Combo.SelectedIndex = 1;
            }


        }





        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }


        private void ok_button_Click(object sender, RoutedEventArgs e)
        {
            WriteToExcelData.ExcelFileToWriteVar = excelInstanceCombo.Text;
            WriteToExcelData.TextToWrite = textToWrite_text.Text;
            WriteToExcelData.ExcelInstanceComboIndex = excelInstanceCombo.SelectedIndex;

            int n;
            bool isCellNumeric = int.TryParse(cell_text.Text.Trim(), out n);


            bool isRowNumeric = int.TryParse(row_text.Text.Trim(), out n);

            if (writeMood_Combo.SelectedIndex == 0)
            {
                //if (!isCellNumeric)
                //{
                //    System.Windows.Forms.MessageBox.Show("Please, Enter a valid Column");
                //}
                if (!isRowNumeric)
                {
                   // System.Windows.Forms.MessageBox.Show("Please, Enter a valid Row");
                }
                

                if ( isRowNumeric)
                {
                    WriteToExcelData.Cell =cell_text.Text.Trim();
                    WriteToExcelData.Row = Convert.ToInt32(row_text.Text.Trim());

                    WriteToExcelData.WriteMoodIndex = 0;

                }


            }
            else if (writeMood_Combo.SelectedIndex == 1)
            {
                WriteToExcelData.WriteMoodIndex = 1;

            }



            if (isRowNumeric)
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
                // opentExcelDoc_Text.Text = openFileDialog.FileName;

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
                // destinationFolder_text.Text = folderBrowserDialog.SelectedPath;
            }
        }


        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void NumericalValidation(object sender, TextCompositionEventArgs e)
        {
          
                Regex regex = new Regex("[^0-9]+");
                e.Handled = regex.IsMatch(e.Text);
            
        }

        private void CharacterValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

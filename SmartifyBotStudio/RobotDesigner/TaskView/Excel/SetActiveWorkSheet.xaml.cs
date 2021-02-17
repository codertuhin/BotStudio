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
    public partial class SetActiveWorkSheet : System.Windows.Controls.UserControl
    {

        public RobotDesigner.TaskModel.Excel.SetActiveWorkSheet SetActiveWorkSheetData { get; set; }


        public SetActiveWorkSheet()
        {
            InitializeComponent();
            Loaded += SetActiveWorkSheet_loaded;
        }




        private void SetActiveWorkSheet_loaded(object sender, RoutedEventArgs e)
        {

            excelInstanceCombo.ItemsSource = VariableStorage.ExcelVar.Keys;


            if (SetActiveWorkSheetData.ExcelSheetActivateIndex == 0)
            {
                workSheetName_text.Text = SetActiveWorkSheetData.ByName;
                ActiveWorkSheetwithCombo.SelectedIndex = 0;
            }
            else if (SetActiveWorkSheetData.ExcelSheetActivateIndex == 1)
            {
                workSheetIndex_text.Text = SetActiveWorkSheetData.ByIndex.ToString();
                ActiveWorkSheetwithCombo.SelectedIndex = 1;
            }





        }





        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }


        private void ok_button_Click(object sender, RoutedEventArgs e)
        {
            SetActiveWorkSheetData.ExcelFileToSetActiveSheetVar = excelInstanceCombo.Text;

            if (ActiveWorkSheetwithCombo.SelectedIndex == 0)
            {
                SetActiveWorkSheetData.ByName = workSheetName_text.Text.Trim();
                SetActiveWorkSheetData.ExcelSheetActivateIndex = 0;
            }
            else if (ActiveWorkSheetwithCombo.SelectedIndex == 1)
            {
                SetActiveWorkSheetData.ByIndex = Convert.ToInt32(workSheetIndex_text.Text.Trim());
                SetActiveWorkSheetData.ExcelSheetActivateIndex = 1;
            }



            //if (!string.IsNullOrEmpty(excelStoreVar_text.Text))
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


    }
}

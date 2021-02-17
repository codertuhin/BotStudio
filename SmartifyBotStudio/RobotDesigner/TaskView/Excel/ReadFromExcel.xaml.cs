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
    public partial class ReadFromExcel : System.Windows.Controls.UserControl
    {

        public RobotDesigner.TaskModel.Excel.ReadFromExcel ReadFromExcelData { get; set; }

        VariableCollectionModel startColvar;
        VariableCollectionModel startRowvar;

        VariableCollectionModel endColvar;
        VariableCollectionModel endRowvar;

        VariableCollectionModel singleColvar;
        VariableCollectionModel singleRowvar;


        public ReadFromExcel()
        {
            InitializeComponent();
            Loaded += ReadFromExcel_loaded;
        }




        private void ReadFromExcel_loaded(object sender, RoutedEventArgs e)
        {

            excelInstanceCombo.ItemsSource = VariableStorage.ExcelVar.Keys;
            excelStoreVar.Text = ReadFromExcelData.ExcelStoreVar;

            if (ReadFromExcelData.RetrieveSchemaIndex == 0)
            {
                singleCellColumn_text.Text = ReadFromExcelData.SingleCol.ToString();
                singleCellRow_text.Text = ReadFromExcelData.SingleRow.ToString();

                retrieve_Combo.SelectedIndex = 0;
            }
            else if (ReadFromExcelData.RetrieveSchemaIndex == 1)
            {
                RangeStartatCol_text.Text = ReadFromExcelData.RangeStartsatCol.ToString();
                RangeStartatRow_text.Text = ReadFromExcelData.RangeStartsatRow.ToString();

                RangeEndsatCol_text.Text = ReadFromExcelData.RangeEndssatCol.ToString();
                RangeEndsatRow_text.Text = ReadFromExcelData.RangeEndssatRow.ToString();

                retrieve_Combo.SelectedIndex = 1;
            }
            else if (ReadFromExcelData.RetrieveSchemaIndex == 2)
            {
                retrieve_Combo.SelectedIndex = 2;

            }




        }





        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }


        private void ok_button_Click(object sender, RoutedEventArgs e)
        {
            int value;
            int counter = 0;
            ReadFromExcelData.ExcelFileToReadVar = excelInstanceCombo.Text;

            if (retrieve_Combo.SelectedIndex == 0)
            {
                if (string.IsNullOrEmpty(singleCellColumn_text.Text.Trim()))
                {
                    System.Windows.Forms.MessageBox.Show("Please enter a valid Column");
                }
                else
                {
                    if (singleColvar != null)
                    {
                        ReadFromExcelData.SingleCol = singleColvar.ObjectValue.ToString();
                    }
                    else
                        ReadFromExcelData.SingleCol = singleCellColumn_text.Text.Trim().ToUpper();// Convert.ToInt32(singleCellColumn_text.Text.Trim());

                }

                if (int.TryParse(singleCellRow_text.Text, out value))
                {
                    if (singleRowvar != null)
                    {
                        ReadFromExcelData.SingleRow = Convert.ToInt32(singleRowvar.ObjectValue);
                    }
                    else
                        ReadFromExcelData.SingleRow = Convert.ToInt32(singleCellRow_text.Text.Trim());
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Please, Enter a valid Row");
                }

                ReadFromExcelData.RetrieveSchemaIndex = 0;

            }
            else if (retrieve_Combo.SelectedIndex == 1)
            {
                if (string.IsNullOrEmpty(RangeStartatCol_text.Text.Trim()))
                {
                    System.Windows.Forms.MessageBox.Show("Please, Enter a Starting Column");
                }
                else
                {
                    if (startColvar != null)
                    {
                        ReadFromExcelData.RangeStartsatCol = startColvar.ObjectValue.ToString();
                    }
                    else
                        ReadFromExcelData.RangeStartsatCol = RangeStartatCol_text.Text.Trim().ToUpper(); //Convert.ToInt32(RangeStartatCol_text.Text.Trim());

                }
                //if (int.TryParse(RangeStartatRow_text.Text, out value))
                //{
                if (startRowvar != null)
                {
                    ReadFromExcelData.RangeStartsatRow = Convert.ToInt32(startRowvar.ObjectValue);
                }
                else
                    ReadFromExcelData.RangeStartsatRow = Convert.ToInt32(RangeStartatRow_text.Text.Trim());
                //}
                //else
                //{
                //    System.Windows.Forms.MessageBox.Show("Please, Enter a valid Starting Row");
                //}
                if (string.IsNullOrEmpty(RangeEndsatCol_text.Text.Trim()))
                {
                    System.Windows.Forms.MessageBox.Show("Please, Enter a Ending Column");
                }
                else
                {
                    if (endColvar != null)
                    {
                        ReadFromExcelData.RangeEndssatCol = endColvar.ObjectValue.ToString();
                    }
                    else
                        ReadFromExcelData.RangeEndssatCol = RangeEndsatCol_text.Text.Trim().ToUpper();//Convert.ToInt32(RangeEndsatCol_text.Text.Trim());

                }
                //if (int.TryParse(RangeEndsatRow_text.Text, out value))
                //{
                if (endRowvar != null)
                {
                    ReadFromExcelData.RangeEndssatRow = Convert.ToInt32(endRowvar.ObjectValue);
                }
                else
                    ReadFromExcelData.RangeEndssatRow = Convert.ToInt32(RangeEndsatRow_text.Text.Trim());
                //}
                //else
                //{
                //    System.Windows.Forms.MessageBox.Show("Please, Enter a valid Ending Row");
                //}


                ReadFromExcelData.RetrieveSchemaIndex = 1;
            }
            else if (retrieve_Combo.SelectedIndex == 2)
            {
                ReadFromExcelData.RetrieveSchemaIndex = 2;

            }


            if (string.IsNullOrEmpty(excelStoreVar.Text))
            {
                //excelStoreVar.ToolTip = "Please!, enter a  name to store result.";
                System.Windows.Forms.MessageBox.Show("Please, Enter a  name to store result.");

            }

            ReadFromExcelData.ExcelStoreVar = excelStoreVar.Text.Trim();

            if (!string.IsNullOrEmpty(excelStoreVar.Text))
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

        private void startColPick_click(object sender, RoutedEventArgs e)
        {
            startColvar = VariableStorage.Pick();
            if (startColvar != null)
            {
                RangeStartatCol_text.Text = startColvar.VariableName;
            }
        }

        private void startRowPick_click(object sender, RoutedEventArgs e)
        {
            startRowvar = VariableStorage.Pick();
            if (startRowvar != null)
            {
                RangeStartatRow_text.Text = startRowvar.VariableName;
            }
        }

        private void endColPick_click(object sender, RoutedEventArgs e)
        {
            endColvar = VariableStorage.Pick();
            if (endColvar != null)
            {
                RangeEndsatCol_text.Text = endColvar.VariableName;
            }
        }

        private void endRowPick_click(object sender, RoutedEventArgs e)
        {
            endRowvar = VariableStorage.Pick();
            if (endRowvar != null)
            {
                RangeEndsatRow_text.Text = endRowvar.VariableName;
            }
        }

        private void singleColPick_click(object sender, RoutedEventArgs e)
        {
            singleColvar = VariableStorage.Pick();
            if (singleColvar != null)
            {
                singleCellColumn_text.Text = singleColvar.VariableName;
            }
        }

        private void singleRowPick_click(object sender, RoutedEventArgs e)
        {
            singleRowvar = VariableStorage.Pick();
            if (singleRowvar != null)
            {
                singleCellRow_text.Text = singleRowvar.VariableName;
            }
        }
    }
}

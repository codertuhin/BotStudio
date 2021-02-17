using MahApps.Metro.Controls;
using SmartifyBotStudio.RobotDesigner.TaskModel;
using SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation;
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
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace SmartifyBotStudio.RobotDesigner.TaskView.Text
{
    /// <summary>
    /// Interaction logic for LaunchNewIE.xaml
    /// </summary>
    public partial class GetSubText : UserControl
    {

        public RobotDesigner.TaskModel.Text.GetSubText GetSubTextData { get; set; }

        VariableCollectionModel working_variable;
        VariableCollectionModel startPosition_variable;
        VariableCollectionModel numberOfChar_variable;

        public GetSubText()
        {
            InitializeComponent();
            Loaded += GetSubText_loaded;
        }

        private void GetSubText_loaded(object sender, RoutedEventArgs e)
        {
            if (GetSubTextData.Length == 0)
            {
                numberOfChar_text.Text = "";
            }
            else
            {
                numberOfChar_text.Text = GetSubTextData.Length.ToString();
            }

            if (GetSubTextData.StartIndex == 0)
            {
                startPositon_text.Text = "";
            }
            else
            {
                startPositon_text.Text = GetSubTextData.StartIndex.ToString();
            }

            originalText_text.Text = GetSubTextData.OriginalText;

            startIndex_combo.SelectedIndex = GetSubTextData.StartIndexComboIndex;
            length_combo.SelectedIndex = GetSubTextData.LengthComboIndex;

            subtextStoreVar_text.Text = GetSubTextData.SubTextStorVar;





        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            GetSubTextData.StartIndexComboIndex = startIndex_combo.SelectedIndex;
            GetSubTextData.LengthComboIndex = length_combo.SelectedIndex;

            if (working_variable != null)
            {
                GetSubTextData.OriginalText = working_variable.ObjectValue.ToString();
            }
            else
                GetSubTextData.OriginalText = originalText_text.Text.Trim();

            int integer;

            if (startIndex_combo.SelectedIndex == 1)
            {
                if (startPosition_variable != null)
                {
                    GetSubTextData.StartIndex = Convert.ToInt32(startPosition_variable.ObjectValue);

                }
                else
                    GetSubTextData.StartIndex = Convert.ToInt32(startPositon_text.Text.Trim());
            }
            else
            {
                GetSubTextData.StartIndex = 0;
            }

            if (length_combo.SelectedIndex == 1)
            {
                if (numberOfChar_variable != null)
                {
                    GetSubTextData.Length = Convert.ToInt32(numberOfChar_variable.ObjectValue);
                }
                else
                    GetSubTextData.Length = Convert.ToInt32(numberOfChar_text.Text.Trim());
            }
            else
            {
                GetSubTextData.Length = originalText_text.Text.Trim().Length;
            }

            GetSubTextData.SubTextStorVar = VariableStorage.VariableNameFormator(subtextStoreVar_text.Text.Trim());

            if (!string.IsNullOrEmpty(subtextStoreVar_text.Text.Trim()))
            {
                if (VariableStorage.SubTextVar.ContainsKey(GetSubTextData.SubTextStorVar))
                {
                    VariableStorage.SubTextVar.Remove(GetSubTextData.SubTextStorVar);
                }
                VariableStorage.SubTextVar.Add(GetSubTextData.SubTextStorVar, Tuple.Create(GetSubTextData.ID, "value has not been set yet"));
            }

            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void originalText_click(object sender, RoutedEventArgs e)
        {
            working_variable = VariableStorage.Pick();
            if (working_variable != null)
            {
                originalText_text.Text = working_variable.VariableName;
            }

        }

        private void NumericalValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void charachterPosition_click(object sender, RoutedEventArgs e)
        {
            startPosition_variable = VariableStorage.Pick();
            if (startPosition_variable != null)
            {
                startPositon_text.Text = startPosition_variable.VariableName;
            }
        }

        private void numberofCharacter_click(object sender, RoutedEventArgs e)
        {
            numberOfChar_variable = VariableStorage.Pick();
            if (numberOfChar_variable != null)
            {
                numberOfChar_text.Text = numberOfChar_variable.VariableName;
            }
        }
    }
}

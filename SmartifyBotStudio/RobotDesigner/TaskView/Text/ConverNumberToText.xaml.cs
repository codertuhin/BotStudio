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
using SmartifyBotStudio.RobotDesigner.TaskView.VariableView;

namespace SmartifyBotStudio.RobotDesigner.TaskView.Text
{
    /// <summary>
    /// Interaction logic for LaunchNewIE.xaml
    /// </summary>
    public partial class ConverNumberToText : UserControl
    {

        public RobotDesigner.TaskModel.Text.ConverNumberToText ConverNumberToTextData { get; set; }

        VariableCollectionModel working_variable;

        public ConverNumberToText()
        {
            InitializeComponent();
            Loaded += ConverNumberToText_loaded;
        }

        private void ConverNumberToText_loaded(object sender, RoutedEventArgs e)
        {

            textToConvert_text.Text = ConverNumberToTextData.TextToConvert;
            useSeparator_checkBox.IsChecked = ConverNumberToTextData.UseSeparator;
            numericDropDown.Value = ConverNumberToTextData.NumberOfDecimalPlaces;
            storeResultInto_text.Text = ConverNumberToTextData.ResultStoreVar;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            ConverNumberToTextData.TextToConvert = textToConvert_text.Text;
            ConverNumberToTextData.UseSeparator = (bool)useSeparator_checkBox.IsChecked;
            ConverNumberToTextData.NumberOfDecimalPlaces = Convert.ToInt32(numericDropDown.Value);
            ConverNumberToTextData.ResultStoreVar = VariableStorage.VariableNameFormator(storeResultInto_text.Text.Trim());

            if (!string.IsNullOrEmpty(storeResultInto_text.Text.Trim()))
            {
                if (VariableStorage.NumberToTextVar.ContainsKey(ConverNumberToTextData.ResultStoreVar))
                {
                    VariableStorage.NumberToTextVar.Remove(ConverNumberToTextData.ResultStoreVar);
                }
                VariableStorage.NumberToTextVar.Add(ConverNumberToTextData.ResultStoreVar, Tuple.Create(ConverNumberToTextData.ID, "value has not been set yet"));

            }

            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }
        private string variable_picker_Click(object sender, RoutedEventArgs e)
        {
            VariablePicker variablePicker = new VariablePicker();

            new MetroWindow() { Content = variablePicker, Height = 350, Width = 300, ShowMaxRestoreButton = false, ShowMinButton = false, ShowCloseButton = false, WindowStartupLocation = WindowStartupLocation.Manual }.ShowDialog();

            if (variablePicker.selectedVariable != null)
            {
                //textToParse_text.Text = variablePicker.selectedVariable.VariableName;
                return "";

            }
            return "";

        }

        private void text_to_convert_pick(object sender, RoutedEventArgs e)
        {
            textToConvert_text.Text = VariableStorage.Pick().VariableName;
        }
    }
}

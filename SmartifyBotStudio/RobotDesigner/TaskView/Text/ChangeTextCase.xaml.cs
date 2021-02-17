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
    public partial class ChangeTextCase : UserControl
    {

        public RobotDesigner.TaskModel.Text.ChangeTextCase ChangeTextCaseData { get; set; }

        VariableCollectionModel working_variable;

        public ChangeTextCase()
        {
            InitializeComponent();
            Loaded += ChangeTextCase_loaded;
        }

        private void ChangeTextCase_loaded(object sender, RoutedEventArgs e)
        {
            textToConvert_text.Text = ChangeTextCaseData.TextToConvert;
            convertIntoCombo.SelectedIndex = ChangeTextCaseData.ConvertIntoCaseComboIndex;
            storeConvertCase_text.Text = ChangeTextCaseData.ConveredTextStoreVar;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            ChangeTextCaseData.TextToConvert = textToConvert_text.Text;
            ChangeTextCaseData.ConvertIntoCaseComboIndex = convertIntoCombo.SelectedIndex;
            ChangeTextCaseData.ConveredTextStoreVar = VariableStorage.VariableNameFormator(storeConvertCase_text.Text.Trim());

            if (!string.IsNullOrEmpty(storeConvertCase_text.Text.Trim()))
            {
                if (VariableStorage.CaseConvetedTextVar.ContainsKey(ChangeTextCaseData.ConveredTextStoreVar))
                {
                    VariableStorage.CaseConvetedTextVar.Remove(ChangeTextCaseData.ConveredTextStoreVar);
                }

                VariableStorage.CaseConvetedTextVar.Add(ChangeTextCaseData.ConveredTextStoreVar, Tuple.Create(ChangeTextCaseData.ID, "value has not set yet"));

                
                //if (TestVar.TestVariable.ContainsKey(ChangeTextCaseData.ConveredTextStoreVar))
                //{
                //    TestVar.TestVariable.Remove(ChangeTextCaseData.ConveredTextStoreVar);
                //}
                //TestVar.TestVariable.Add(ChangeTextCaseData.ConveredTextStoreVar, "value has not set yet");

            }


            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void text_to_convert_pick(object sender, RoutedEventArgs e)
        {
            textToConvert_text.Text = VariableStorage.Pick().VariableName;
        }
    }
}

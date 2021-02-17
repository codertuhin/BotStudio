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

namespace SmartifyBotStudio.RobotDesigner.TaskView.Text
{
    /// <summary>
    /// Interaction logic for LaunchNewIE.xaml
    /// </summary>
    public partial class GetTextLength : UserControl
    {

        public RobotDesigner.TaskModel.Text.GetTextLength GetTextLengthData { get; set; }

        VariableCollectionModel working_variable;
        public GetTextLength()
        {
            InitializeComponent();
            Loaded += GetTextLength_loaded;
        }

        private void GetTextLength_loaded(object sender, RoutedEventArgs e)
        {
            textToMeasure_text.Text = GetTextLengthData.TextToMeasure;
            storeLengthInto_text.Text = GetTextLengthData.TextLengthStoreVar;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            GetTextLengthData.TextToMeasure = textToMeasure_text.Text;
            GetTextLengthData.TextLengthStoreVar = VariableStorage.VariableNameFormator(storeLengthInto_text.Text.Trim());

            if (!string.IsNullOrEmpty(storeLengthInto_text.Text.Trim()))
            {
                if (VariableStorage.TextLengthVar.ContainsKey(GetTextLengthData.TextLengthStoreVar))
                {
                    VariableStorage.TextLengthVar.Remove(GetTextLengthData.TextLengthStoreVar);
                }
                VariableStorage.TextLengthVar.Add(GetTextLengthData.TextLengthStoreVar, Tuple.Create(GetTextLengthData.ID, -1));  // -1 is dummy data
            }


            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void textToMeasure_click(object sender, RoutedEventArgs e)
        {
            textToMeasure_text.Text = VariableStorage.Pick().VariableName;
        }
    }
}

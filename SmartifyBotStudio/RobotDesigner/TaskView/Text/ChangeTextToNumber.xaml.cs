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
    public partial class ChangeTextToNumber : UserControl
    {

        public RobotDesigner.TaskModel.Text.ChangeTextToNumber ChangeTextToNumberData { get; set; }

        VariableCollectionModel working_variable;

        public ChangeTextToNumber()
        {
            InitializeComponent();
            Loaded += ChangeTextToNumber_loaded;
        }

        private void ChangeTextToNumber_loaded(object sender, RoutedEventArgs e)
        {
            textToConvert_text.Text = ChangeTextToNumberData.TextToConvert;
            store_text.Text = ChangeTextToNumberData.TextToNumverStoreVar;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {


            ChangeTextToNumberData.TextToConvert = textToConvert_text.Text;
            ChangeTextToNumberData.TextToNumverStoreVar = VariableStorage.VariableNameFormator(store_text.Text.Trim());

            if (!string.IsNullOrEmpty(store_text.Text.Trim()))
            {
                if (VariableStorage.TextToNumberVar.ContainsKey(ChangeTextToNumberData.TextToNumverStoreVar))
                {
                    VariableStorage.TextToNumberVar.Remove(ChangeTextToNumberData.TextToNumverStoreVar);
                }

                VariableStorage.TextToNumberVar.Add(ChangeTextToNumberData.TextToNumverStoreVar, Tuple.Create(ChangeTextToNumberData.ID, -1));     // -1 is a dummy data

            }

            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void variable_picker_Click(object sender, RoutedEventArgs e)
        {
            VariablePicker variablePicker = new VariablePicker();

            new MetroWindow() { Content = variablePicker, Height = 350, Width = 300, ShowMaxRestoreButton = false, ShowMinButton = false, ShowCloseButton = false, WindowStartupLocation = WindowStartupLocation.Manual }.ShowDialog();

            if (variablePicker.selectedVariable != null)
            {
                // textToParse_text.Text = variablePicker.selectedVariable.VariableName;

            }

        }

        private void text_to_convert_pick(object sender, RoutedEventArgs e)
        {
            textToConvert_text.Text = VariableStorage.Pick().VariableName;
        }
    }
}

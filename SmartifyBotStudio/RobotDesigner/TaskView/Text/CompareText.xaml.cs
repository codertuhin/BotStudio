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
    public partial class CompareText : UserControl
    {

        public RobotDesigner.TaskModel.Text.CompareText CompareTextData { get; set; }

        VariableCollectionModel working_variable;
        public CompareText()
        {
            InitializeComponent();
            Loaded += CompareText_loaded;
        }

        private void CompareText_loaded(object sender, RoutedEventArgs e)
        {
            textToCompare_text.Text = CompareTextData.TextToCompare;
            compareWith_text.Text = CompareTextData.CompareWtih;
            ignoreCase_checkbox.IsChecked = CompareTextData.IgnoreCase;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            CompareTextData.TextToCompare = textToCompare_text.Text;
            CompareTextData.CompareWtih = compareWith_text.Text;
            CompareTextData.IgnoreCase = (bool)ignoreCase_checkbox.IsChecked;

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

        private void text_to_compare_pick(object sender, RoutedEventArgs e)
        {
            textToCompare_text.Text = VariableStorage.Pick().VariableName;
        }
    }
}

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
    public partial class ReverseText : UserControl
    {

        public RobotDesigner.TaskModel.Text.ReverseText ReverseTextData { get; set; }

        VariableCollectionModel working_variable;

        public ReverseText()
        {
            InitializeComponent();
            Loaded += ReverseText_loaded;
        }

        private void ReverseText_loaded(object sender, RoutedEventArgs e)
        {
            textToReverse_text.Text = ReverseTextData.TextToReverse;
            reverseTextStore_text.Text = ReverseTextData.ReversedTextStoreVar;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {
            ReverseTextData.TextToReverse = textToReverse_text.Text;
            ReverseTextData.ReversedTextStoreVar = VariableStorage.VariableNameFormator(reverseTextStore_text.Text.Trim());

            if (!string.IsNullOrEmpty(reverseTextStore_text.Text.Trim()))
            {
                if (VariableStorage.ReversedTextVar.ContainsKey(ReverseTextData.ReversedTextStoreVar))
                {
                    VariableStorage.ReversedTextVar.Remove(ReverseTextData.ReversedTextStoreVar);
                }
                VariableStorage.ReversedTextVar.Add(ReverseTextData.ReversedTextStoreVar, Tuple.Create(ReverseTextData.ID, "value has not been set yet"));
            }

            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void textToReverse_click(object sender, RoutedEventArgs e)
        {
            textToReverse_text.Text = VariableStorage.Pick().VariableName;
        }
    }
}

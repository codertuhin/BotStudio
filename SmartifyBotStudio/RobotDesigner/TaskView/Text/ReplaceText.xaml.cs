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
    public partial class ReplaceText : UserControl
    {

        public RobotDesigner.TaskModel.Text.ReplaceText ReplaceTextData { get; set; }

        VariableCollectionModel working_variable;

        public ReplaceText()
        {
            InitializeComponent();
            Loaded += ReplaceText_loaded;
        }

        private void ReplaceText_loaded(object sender, RoutedEventArgs e)
        {
            textToPasrse_text.Text = ReplaceTextData.TextToParse;
            textToFind_text.Text = ReplaceTextData.TextToFind;

            textToReplace_rtb.Document.Blocks.Clear();
            textToReplace_rtb.Document.Blocks.Add(new Paragraph(new Run(ReplaceTextData.ReplaceWith)));



            useRegulerExpression_checkBox.IsChecked = ReplaceTextData.UseRegulerExpression;
            ignoreCase_checkbox.IsChecked = ReplaceTextData.IgnoreCase;

            replacedTextStoreVar_text.Text = ReplaceTextData.ReplaceTextStoreVar;

            activeEscapteSequence_checkBox.IsChecked = ReplaceTextData.ActiveEscapeSequence;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            ReplaceTextData.TextToParse = textToPasrse_text.Text;
            ReplaceTextData.TextToFind = textToFind_text.Text;

            TextRange textRange = new TextRange(textToReplace_rtb.Document.ContentStart, textToReplace_rtb.Document.ContentEnd);

            ReplaceTextData.ReplaceWith = textRange.Text.Trim();

            ReplaceTextData.UseRegulerExpression = (bool)useRegulerExpression_checkBox.IsChecked;
            ReplaceTextData.IgnoreCase = (bool)ignoreCase_checkbox.IsChecked;

            ReplaceTextData.ReplaceTextStoreVar = VariableStorage.VariableNameFormator(replacedTextStoreVar_text.Text.Trim());

            ReplaceTextData.ActiveEscapeSequence = (bool)activeEscapteSequence_checkBox.IsChecked;


            if (!string.IsNullOrEmpty(replacedTextStoreVar_text.Text.Trim()))
            {
                if (VariableStorage.ReplaceTextVar.ContainsKey(ReplaceTextData.ReplaceTextStoreVar))
                {
                    VariableStorage.ReplaceTextVar.Remove(ReplaceTextData.ReplaceTextStoreVar);
                }

                VariableStorage.ReplaceTextVar.Add(ReplaceTextData.ReplaceTextStoreVar, Tuple.Create(ReplaceTextData.ID, "value has not been set yet"));
            }


            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void textToParse_click(object sender, RoutedEventArgs e)
        {
            textToPasrse_text.Text = VariableStorage.Pick().VariableName;
        }

        private void textToFind_click(object sender, RoutedEventArgs e)
        {
            textToFind_text.Text = VariableStorage.Pick().VariableName;
        }
    }
}

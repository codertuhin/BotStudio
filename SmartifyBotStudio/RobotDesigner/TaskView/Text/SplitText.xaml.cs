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
    public partial class SplitText : UserControl
    {

        public RobotDesigner.TaskModel.Text.SplitText SplitTextData { get; set; }

        VariableCollectionModel working_variable;

        public SplitText()
        {
            InitializeComponent();
            Loaded += SplitText_loaded;
        }

        private void SplitText_loaded(object sender, RoutedEventArgs e)
        {
            textToSplit_text.Text = SplitTextData.TextToSplit;
            delimiterOption_combo.SelectedIndex = SplitTextData.DelimiterOption;
            standardDelimitercombo.SelectedIndex = SplitTextData.StandardDelimeterIndex;

            regulerExpressionCheckBox.IsChecked = SplitTextData.IsRegulerExpression;

            customDelimeter_text.Text = SplitTextData.CustomDelimiter;


            splittedTextStoreVar_text.Text = SplitTextData.SplitTextStoreVar;

            numericDropDown.Value = SplitTextData.DelimiterOccurneceNumber;

        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            SplitTextData.TextToSplit = textToSplit_text.Text;
            SplitTextData.DelimiterOption = delimiterOption_combo.SelectedIndex;
            SplitTextData.StandardDelimeterIndex = standardDelimitercombo.SelectedIndex;
            SplitTextData.DelimiterOccurneceNumber = Convert.ToInt32(numericDropDown.Value);

            if (delimiterOption_combo.SelectedIndex == 0)
            {

                if (standardDelimitercombo.SelectedIndex == 0)
                {
                    SplitTextData.Delimiter = " ";

                }
                else if (standardDelimitercombo.SelectedIndex == 1)
                {
                    SplitTextData.Delimiter = "\t";

                }
                else if (standardDelimitercombo.SelectedIndex == 2)
                {
                    SplitTextData.Delimiter = "\n";

                }
            }
            else if (delimiterOption_combo.SelectedIndex == 1)
            {

                SplitTextData.IsRegulerExpression = (bool)regulerExpressionCheckBox.IsChecked;

                SplitTextData.CustomDelimiter = customDelimeter_text.Text;


            }

            SplitTextData.SplitTextStoreVar = VariableStorage.VariableNameFormator(splittedTextStoreVar_text.Text.Trim());

            if (!string.IsNullOrEmpty(splittedTextStoreVar_text.Text.Trim()))
            {
                if (VariableStorage.SplitTextVar.ContainsKey(SplitTextData.SplitTextStoreVar))
                {
                    VariableStorage.SplitTextVar.Remove(SplitTextData.SplitTextStoreVar);
                }

                VariableStorage.SplitTextVar.Add(SplitTextData.SplitTextStoreVar, Tuple.Create(SplitTextData.ID, new string[] { }));
            }

            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void textToSplit_click(object sender, RoutedEventArgs e)
        {
            textToSplit_text.Text = VariableStorage.Pick().VariableName;
        }
    }
}

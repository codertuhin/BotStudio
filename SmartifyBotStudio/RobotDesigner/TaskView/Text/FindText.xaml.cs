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
using Microsoft.Win32;
//using System.Windows.Forms;
using SmartifyBotStudio.RobotDesigner.TaskView.VariableView;

namespace SmartifyBotStudio.RobotDesigner.TaskView.Text
{
    /// <summary>
    /// Interaction logic for LaunchNewIE.xaml
    /// </summary>
    public partial class FindText : UserControl
    {

        public RobotDesigner.TaskModel.Text.FindText FindTextData { get; set; }

        VariableCollectionModel working_variable;

        public FindText()
        {
            InitializeComponent();
            Loaded += FindText_loaded;
        }

        private void FindText_loaded(object sender, RoutedEventArgs e)
        {
            textToParse_text.Text = FindTextData.TextToParse;
            textToFind_text.Text = FindTextData.TextToFind;
            useRegulerExpression_checkBox.IsChecked = FindTextData.UseRegulerExpression;
            ignoreCase_checkBox.IsChecked = FindTextData.IgnoreCase;
            firstOccurence_checkBox.IsChecked = FindTextData.FirstOccurenceOnly;
            storeVar_text.Text = FindTextData.ResultStoreVar;

            startingPosition_text.Text = FindTextData.StartingPoint.ToString();
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            FindTextData.TextToParse = textToParse_text.Text;
            FindTextData.TextToFind = textToFind_text.Text;
            FindTextData.UseRegulerExpression = (bool)useRegulerExpression_checkBox.IsChecked;
            FindTextData.IgnoreCase = (bool)ignoreCase_checkBox.IsChecked;
            FindTextData.FirstOccurenceOnly = (bool)firstOccurence_checkBox.IsChecked;
            FindTextData.ResultStoreVar = VariableStorage.VariableNameFormator(storeVar_text.Text.Trim()); ;

            int integer;
            if (int.TryParse(startingPosition_text.Text.Trim(), out integer))
            {
                FindTextData.StartingPoint = Convert.ToInt32(startingPosition_text.Text.Trim());
            }


            if (!string.IsNullOrEmpty(storeVar_text.Text.Trim()))
            {


                if (VariableStorage.FindTextVar.ContainsKey(FindTextData.ResultStoreVar))
                {
                    VariableStorage.FindTextVar.Remove(FindTextData.ResultStoreVar);
                }

                VariableStorage.FindTextVar.Add(FindTextData.ResultStoreVar, Tuple.Create(FindTextData.ID, new List<int>()));
            }

            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }

        private void variable_picker_Click(object sender, RoutedEventArgs e)
        {
            textToParse_text.Text = VariableStorage.Pick().VariableName;

        }




        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }


        private void text_to_find_click(object sender, RoutedEventArgs e)
        {
            textToFind_text.Text = VariableStorage.Pick().VariableName;

        }
    }
}

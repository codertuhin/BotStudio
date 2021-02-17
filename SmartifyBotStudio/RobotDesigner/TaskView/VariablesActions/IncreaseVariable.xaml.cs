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

namespace SmartifyBotStudio.RobotDesigner.TaskView.VariablesActions
{
    /// <summary>
    /// Interaction logic for LaunchNewIE.xaml
    /// </summary>
    public partial class IncreaseVariable : UserControl
    {

        public RobotDesigner.TaskModel.VariablesActions.IncreaseVariable IncreaseVariableData { get; set; }

        VariableCollectionModel working_variable;

        VariableCollectionModel increaseBy_variable;

        public IncreaseVariable()
        {
            InitializeComponent();
            Loaded += IncreaseVariableData_loaded;
        }

        private void IncreaseVariableData_loaded(object sender, RoutedEventArgs e)
        {
            variableName_text.Text = IncreaseVariableData.WorkingVarName;

            increaseByVariable_text.Text = IncreaseVariableData.IncreaseByVarName;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            IncreaseVariableData.WorkingVariable = working_variable;

            IncreaseVariableData.IncreaseByVariable = increaseBy_variable;

            IncreaseVariableData.WorkingVarName = variableName_text.Text.Trim();

            IncreaseVariableData.IncreaseByVarName = increaseByVariable_text.Text.Trim();


            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void useRegulerExpression_checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void variable_name_click(object sender, RoutedEventArgs e)
        {
            working_variable = VariableStorage.Pick();
            if (working_variable != null)
                variableName_text.Text = working_variable.VariableName;
        }

        private void increaseByVarialbe_click(object sender, RoutedEventArgs e)
        {
            increaseBy_variable = VariableStorage.Pick();
            if (increaseBy_variable != null)
                increaseByVariable_text.Text = increaseBy_variable.VariableName;
        }
    }
}

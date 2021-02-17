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
    public partial class DecreaseVariable : UserControl
    {

        public RobotDesigner.TaskModel.VariablesActions.DecreaseVariable DecreaseVariableData { get; set; }

        VariableCollectionModel working_variable;

        VariableCollectionModel decreaseBy_variable;
        public DecreaseVariable()
        {
            InitializeComponent();
            Loaded += DecreaseVariableData_loaded;
        }

        private void DecreaseVariableData_loaded(object sender, RoutedEventArgs e)
        {
            variableName_text.Text = DecreaseVariableData.WorkingVarName;

            decreaseByVariable_text.Text = DecreaseVariableData.IncreaseByVarName;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            DecreaseVariableData.WorkingVariable = working_variable;

            DecreaseVariableData.DecreaseByVariable = decreaseBy_variable;

            DecreaseVariableData.WorkingVarName = variableName_text.Text.Trim();

            DecreaseVariableData.IncreaseByVarName = decreaseByVariable_text.Text.Trim();


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

        private void decreaseByVarialbe_click(object sender, RoutedEventArgs e)
        {
            decreaseBy_variable = VariableStorage.Pick();
            if (decreaseBy_variable != null)
                decreaseByVariable_text.Text = decreaseBy_variable.VariableName;
        }
    }
}

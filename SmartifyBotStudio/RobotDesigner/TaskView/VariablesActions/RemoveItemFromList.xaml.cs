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
    public partial class RemoveItemFromList : UserControl
    {

        public RobotDesigner.TaskModel.VariablesActions.RemoveItemFromList RemoveItemFromListData { get; set; }

        VariableCollectionModel working_variable;

        VariableCollectionModel index_variable;
        public RemoveItemFromList()
        {
            InitializeComponent();
            Loaded += Template_loaded;
        }

        private void Template_loaded(object sender, RoutedEventArgs e)
        {
            listFrom_text.Text = RemoveItemFromListData.WorkingListVariableStr;

            removeIndex_text.Text = RemoveItemFromListData.IndexVariableStr;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            RemoveItemFromListData.WorkingListVariable = working_variable;

            RemoveItemFromListData.IndexVariable = index_variable;

            RemoveItemFromListData.WorkingListVariableStr = listFrom_text.Text;

            RemoveItemFromListData.IndexVariableStr = removeIndex_text.Text;




            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void useRegulerExpression_checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void list_from_click(object sender, RoutedEventArgs e)
        {
            working_variable = VariableStorage.Pick();
            if (working_variable != null)
                listFrom_text.Text = working_variable.VariableName;
        }

        private void remove_index_click(object sender, RoutedEventArgs e)
        {
            index_variable = VariableStorage.Pick();
            if (index_variable != null)
                removeIndex_text.Text = index_variable.VariableName;
        }
    }
}

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
    public partial class ClearList : UserControl
    {

        public RobotDesigner.TaskModel.VariablesActions.ClearList ClearListData { get; set; }

        VariableCollectionModel working_list;
        public ClearList()
        {
            InitializeComponent();
            Loaded += ClearListData_loaded;
        }

        private void ClearListData_loaded(object sender, RoutedEventArgs e)
        {
            clearList_text.Text = ClearListData.ListToClearName;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {


            ClearListData.ListToClear = working_list;
            ClearListData.ListToClearName = clearList_text.Text;


            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void useRegulerExpression_checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }


        private void clearList_click(object sender, RoutedEventArgs e)
        {
            working_list = VariableStorage.Pick();
            if (working_list != null)
                clearList_text.Text = working_list.VariableName;
        }
    }
}

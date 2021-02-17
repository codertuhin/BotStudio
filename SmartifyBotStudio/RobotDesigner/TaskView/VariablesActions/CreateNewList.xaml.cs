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
    public partial class CreateNewList : UserControl
    {

        public RobotDesigner.TaskModel.VariablesActions.CreateNewList CreateNewListData { get; set; }

        public CreateNewList()
        {
            InitializeComponent();
            Loaded += CreateNewListData_loaded;
        }

        private void CreateNewListData_loaded(object sender, RoutedEventArgs e)
        {
            listName_text.Text = CreateNewListData.EmptyListName;

        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {
            CreateNewListData.EmptyListName = VariableStorage.VariableNameFormator(listName_text.Text.Trim());

            if (VariableStorage.CreateNewListVar.ContainsKey(listName_text.Text.Trim()))
            {
                VariableStorage.CreateNewListVar.Remove(listName_text.Text.Trim());
            }

            VariableStorage.CreateNewListVar.Add(VariableStorage.VariableNameFormator(listName_text.Text.Trim()), Tuple.Create(CreateNewListData.ID, new List<object>(), typeof(List<object>)));



            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void useRegulerExpression_checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}

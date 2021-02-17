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
    public partial class AddTtemToList : UserControl
    {

        public RobotDesigner.TaskModel.VariablesActions.AddTtemToList AddTtemToListData { get; set; }

        VariableCollectionModel working_list;

        VariableCollectionModel itemToAdd;

        public AddTtemToList()
        {
            InitializeComponent();
            Loaded += AddTtemToListData_loaded;
        }

        private void AddTtemToListData_loaded(object sender, RoutedEventArgs e)
        {

            intoList_text.Text = AddTtemToListData.WorkingListStr;

            addItem_text.Text = AddTtemToListData.ItemToAddStr;

            if (AddTtemToListData.WorkingList != null)
            {
                working_list = AddTtemToListData.WorkingList;

            }

            if (AddTtemToListData.ItemToAdd != null)
            {
                itemToAdd = AddTtemToListData.ItemToAdd;

            }

        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            AddTtemToListData.WorkingList = working_list;
            AddTtemToListData.ItemToAdd = itemToAdd;

            AddTtemToListData.WorkingListStr = intoList_text.Text;
            AddTtemToListData.ItemToAddStr = addItem_text.Text;


            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void useRegulerExpression_checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void addItem_click(object sender, RoutedEventArgs e)
        {
            itemToAdd = VariableStorage.Pick();
            if (itemToAdd != null)
                addItem_text.Text = itemToAdd.VariableName;
        }

        private void intoList_click(object sender, RoutedEventArgs e)
        {
            working_list = VariableStorage.Pick();
            if (working_list != null)
                intoList_text.Text = working_list.VariableName;
        }
    }
}

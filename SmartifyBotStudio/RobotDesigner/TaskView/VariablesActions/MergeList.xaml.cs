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
    public partial class MergeList : UserControl
    {

        public RobotDesigner.TaskModel.VariablesActions.MergeLists MergeListsData { get; set; }

        VariableCollectionModel FirstList;
        VariableCollectionModel SecondList;
        public MergeList()
        {
            InitializeComponent();
            Loaded += Template_loaded;
        }

        private void Template_loaded(object sender, RoutedEventArgs e)
        {
            FirstList = MergeListsData.FirstList;
            SecondList = MergeListsData.SecondList;

            firstList.Text = MergeListsData.FirstListStr;
            secondList.Text = MergeListsData.SecondListStr;

            outputList_text.Text = MergeListsData.ResultStoreVar;


        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrEmpty(outputList_text.Text.Trim()))
            {



                MergeListsData.FirstList = FirstList;
                MergeListsData.SecondList = SecondList;

                MergeListsData.FirstListStr = firstList.Text;
                MergeListsData.SecondListStr = secondList.Text;

                MergeListsData.ResultStoreVar = VariableStorage.VariableNameFormator( outputList_text.Text.Trim());

                if (VariableStorage.CreateNewListVar.ContainsKey(MergeListsData.ResultStoreVar))
                {
                    VariableStorage.CreateNewListVar.Remove(MergeListsData.ResultStoreVar);
                }

                VariableStorage.CreateNewListVar.Add(MergeListsData.ResultStoreVar, Tuple.Create(MergeListsData.ID, new List<object>(), typeof(List<object>)));

                ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;

            }
        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void useRegulerExpression_checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void firstList_click(object sender, RoutedEventArgs e)
        {
            FirstList = VariableStorage.Pick();
            if (FirstList != null)
                firstList.Text = FirstList.VariableName;
        }

        private void secondList_click(object sender, RoutedEventArgs e)
        {
            SecondList = VariableStorage.Pick();
            if (SecondList != null)
                secondList.Text = SecondList.VariableName;
        }
    }
}

﻿using MahApps.Metro.Controls;
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
    public partial class ReverseList : UserControl
    {

        public RobotDesigner.TaskModel.VariablesActions.ReverseList ReverseListData { get; set; }

        VariableCollectionModel working_list;
        public ReverseList()
        {
            InitializeComponent();
            Loaded += ClearListData_loaded;
        }

        private void ClearListData_loaded(object sender, RoutedEventArgs e)
        {
            working_list=ReverseListData.ListToReverse;

            reverseList_text.Text = ReverseListData.ListToReverseStr;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {


            ReverseListData.ListToReverse = working_list;
            ReverseListData.ListToReverseStr = reverseList_text.Text;


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
                reverseList_text.Text = working_list.VariableName;
        }
    }
}

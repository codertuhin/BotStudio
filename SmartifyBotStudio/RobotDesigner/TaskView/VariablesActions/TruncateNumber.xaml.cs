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
    public partial class TruncateNumber : UserControl
    {

        public RobotDesigner.TaskModel.VariablesActions.TruncateNumber TruncateNumberData { get; set; }

        VariableCollectionModel working_variable;

        public TruncateNumber()
        {
            InitializeComponent();
            Loaded += Template_loaded;
        }

        private void Template_loaded(object sender, RoutedEventArgs e)
        {
            trancate_combo.SelectedIndex = TruncateNumberData.TruncateSchema;

            storeVar_text.Text = TruncateNumberData.ResultStoreVar;

            numericDropDown.Value=TruncateNumberData.RoundUp;
                       
            numberToTruncate_text.Text = TruncateNumberData.WorkingVariableStr;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {
            TruncateNumberData.WorkingVariable = working_variable;

            TruncateNumberData.NumberToTruncate = numberToTruncate_text.Text.Trim();

            TruncateNumberData.TruncateSchema = trancate_combo.SelectedIndex;

            TruncateNumberData.ResultStoreVar =VariableStorage.VariableNameFormator( storeVar_text.Text.Trim());

            TruncateNumberData.WorkingVariableStr=numberToTruncate_text.Text;

            TruncateNumberData.RoundUp =Convert.ToInt32( numericDropDown.Value);

            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void useRegulerExpression_checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void truncate_number_click(object sender, RoutedEventArgs e)
        {
            working_variable = VariableStorage.Pick();
            if (working_variable != null)
                numberToTruncate_text.Text = working_variable.VariableName;
        }
    }
}

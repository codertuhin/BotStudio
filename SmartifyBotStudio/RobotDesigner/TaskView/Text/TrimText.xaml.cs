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

namespace SmartifyBotStudio.RobotDesigner.TaskView.Text
{
    /// <summary>
    /// Interaction logic for LaunchNewIE.xaml
    /// </summary>
    public partial class TrimText : UserControl
    {

        public RobotDesigner.TaskModel.Text.TrimText TrimTextData { get; set; }

        VariableCollectionModel working_variable;

        public TrimText()
        {
            InitializeComponent();
            Loaded += TrimText_loaded;
        }

        private void TrimText_loaded(object sender, RoutedEventArgs e)
        {
            textToTrim_text.Text = TrimTextData.TrimToText;
            whatToTrim_combo.SelectedIndex = TrimTextData.WhatToTrimComboIndex;
            trimmedTextStoreVar_text.Text = TrimTextData.TrimmedTextStoreVar;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            TrimTextData.TrimToText = textToTrim_text.Text;
            TrimTextData.WhatToTrimComboIndex = whatToTrim_combo.SelectedIndex;
            TrimTextData.TrimmedTextStoreVar = VariableStorage.VariableNameFormator(trimmedTextStoreVar_text.Text.Trim());

            if (!string.IsNullOrEmpty(trimmedTextStoreVar_text.Text.Trim()))
            {
                if (VariableStorage.TrimmedTextVar.ContainsKey(TrimTextData.TrimmedTextStoreVar))
                {
                    VariableStorage.TrimmedTextVar.Remove(TrimTextData.TrimmedTextStoreVar);
                }
                VariableStorage.TrimmedTextVar.Add(TrimTextData.TrimmedTextStoreVar, Tuple.Create(TrimTextData.ID, "value has not been set yet"));
            }

            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void textToTrim_click(object sender, RoutedEventArgs e)
        {
            working_variable = VariableStorage.Pick();
            textToTrim_text.Text = working_variable.VariableName;
        }
    }
}

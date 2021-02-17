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
using SmartifyBotStudio.Models;

namespace SmartifyBotStudio.RobotDesigner.TaskView.VariablesActions
{
    /// <summary>
    /// Interaction logic for LaunchNewIE.xaml
    /// </summary>
    public partial class SetVariable : UserControl
    {

        public RobotDesigner.TaskModel.VariablesActions.SetVariable SetVariableData { get; set; }

        public SetVariable()
        {
            InitializeComponent();
            Loaded += SetVariableData_loaded;
        }

        private void SetVariableData_loaded(object sender, RoutedEventArgs e)
        {
            VarName_text.Text = SetVariableData.VarName;
            value_text.Text = SetVariableData.ValueStr;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(VarName_text.Text.Trim()))
                {


                    SetVariableData.VarName = VariableStorage.VariableNameFormator(VarName_text.Text.Trim());
                    SetVariableData.ValueStr = value_text.Text.Trim();


                    double doub;



                    if (double.TryParse(value_text.Text.Trim(), out doub))
                    {
                        SetVariableData.DataType = typeof(double);
                        if (VariableStorage.SetVariableVar.ContainsKey(VarName_text.Text.Trim()))
                        {
                            VariableStorage.SetVariableVar.Remove(VarName_text.Text.Trim());
                        }
                        VariableStorage.SetVariableVar.Add(VariableStorage.VariableNameFormator(VarName_text.Text.Trim()), Tuple.Create(SetVariableData.ID, (object)value_text.Text, typeof(double)));
                    }
                    else
                    {
                        SetVariableData.DataType = typeof(string);

                        if (VariableStorage.SetVariableVar.ContainsKey(VarName_text.Text.Trim()))
                        {
                            VariableStorage.SetVariableVar.Remove(VarName_text.Text.Trim());
                        }
                        VariableStorage.SetVariableVar.Add(VariableStorage.VariableNameFormator(VarName_text.Text.Trim()), Tuple.Create(SetVariableData.ID, (object)value_text.Text, typeof(string)));

                    }

                    if (RobotActionBase.testVariable.ContainsKey(SetVariableData.VarName))
                    {
                        RobotActionBase.testVariable.Remove(SetVariableData.VarName);
                    }
                    RobotActionBase.testVariable.Add(SetVariableData.VarName, Tuple.Create(SetVariableData.ID, SetVariableData.VarName));

                }


            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
            }
            catch (Exception)
            {

                throw;
            }

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

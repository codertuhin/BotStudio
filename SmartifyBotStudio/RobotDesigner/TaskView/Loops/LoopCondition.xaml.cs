using MahApps.Metro.Controls;
using SmartifyBotStudio.RobotDesigner.TaskModel;
using SmartifyBotStudio.RobotDesigner.Variable;
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

namespace SmartifyBotStudio.RobotDesigner.TaskView.Loops
{
    /// <summary>
    /// Interaction logic for LoopCondition.xaml
    /// </summary>
    public partial class LoopCondition : UserControl
    {
        public RobotDesigner.TaskModel.Loops.LoopCondition LoopConditionData { get; set; }

        VariableCollectionModel firstOperand;
        VariableCollectionModel secondOperand;

        public LoopCondition()
        {
            InitializeComponent();
            Loaded += LoopConditionData_loaded;
        }

        private void LoopConditionData_loaded(object sender, RoutedEventArgs e)
        {
            firstOperand = LoopConditionData.FirstOperand;

            if (LoopConditionData.FirstOperand != null)
            {
                first_operand.Text = LoopConditionData.FirstOperand.VariableName;
            }


            secondOperand = LoopConditionData.SecondOperand;

            if (LoopConditionData.SecondOperand != null)
            {
                second_operand.Text = LoopConditionData.SecondOperand.VariableName;
            }


            condition_combo.SelectedIndex = LoopConditionData.ConditionIndex;

            if (LoopConditionData.ConditionIndex < 4)
            {
                secondOperand_grid.SetValue(Grid.RowProperty, 3);
                ignoreCase_grid.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (LoopConditionData.ConditionIndex == 11 && LoopConditionData.ConditionIndex == 12)
            {
                secondOperand_grid.Visibility = System.Windows.Visibility.Collapsed;
                ignoreCase_grid.Visibility = System.Windows.Visibility.Collapsed;

            }
            else
            {
                ignoreCase_grid.Visibility = System.Windows.Visibility.Visible;

            }


        }


        private void ok_button_Click(object sender, RoutedEventArgs e)
        {


            LoopConditionData.FirstOperand = firstOperand;
            LoopConditionData.SecondOperand = secondOperand;

            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }
        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void useRegulerExpression_checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void first_operand_click(object sender, RoutedEventArgs e)
        {
            firstOperand = VariableStorage.Pick();
            if (firstOperand != null)
                first_operand.Text = firstOperand.VariableName;
        }

        private void second_operand_click(object sender, RoutedEventArgs e)
        {
            secondOperand = VariableStorage.Pick();
            if (secondOperand != null)
                second_operand.Text = secondOperand.VariableName;
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            //System.Windows.Forms.ComboBox cmb = sender as System.Windows.Forms.ComboBox;
            // handle = !cmb.IsDropDownOpen;
            //Handle();

            switch (condition_combo.SelectedIndex)
            {
                case 0:
                    secondOperand_grid.SetValue(Grid.RowProperty, 3);
                    secondOperand_grid.Visibility = System.Windows.Visibility.Visible;
                    ignoreCase_grid.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case 1:
                    secondOperand_grid.SetValue(Grid.RowProperty, 3);
                    secondOperand_grid.Visibility = System.Windows.Visibility.Visible;
                    ignoreCase_grid.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case 2:
                    secondOperand_grid.SetValue(Grid.RowProperty, 3);
                    secondOperand_grid.Visibility = System.Windows.Visibility.Visible;
                    ignoreCase_grid.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case 3:
                    secondOperand_grid.SetValue(Grid.RowProperty, 3);
                    secondOperand_grid.Visibility = System.Windows.Visibility.Visible;
                    ignoreCase_grid.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case 4:
                    secondOperand_grid.SetValue(Grid.RowProperty, 3);
                    secondOperand_grid.Visibility = System.Windows.Visibility.Visible;
                    ignoreCase_grid.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case 5:
                    secondOperand_grid.SetValue(Grid.RowProperty, 4);
                    secondOperand_grid.Visibility = System.Windows.Visibility.Visible;
                    ignoreCase_grid.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 6:
                    secondOperand_grid.SetValue(Grid.RowProperty, 4);
                    secondOperand_grid.Visibility = System.Windows.Visibility.Visible;
                    ignoreCase_grid.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 7:
                    secondOperand_grid.SetValue(Grid.RowProperty, 4);
                    secondOperand_grid.Visibility = System.Windows.Visibility.Visible;
                    ignoreCase_grid.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 8:
                    secondOperand_grid.SetValue(Grid.RowProperty, 4);
                    secondOperand_grid.Visibility = System.Windows.Visibility.Visible;
                    ignoreCase_grid.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 9:
                    secondOperand_grid.SetValue(Grid.RowProperty, 4);
                    secondOperand_grid.Visibility = System.Windows.Visibility.Visible;
                    ignoreCase_grid.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 10:
                    secondOperand_grid.SetValue(Grid.RowProperty, 4);
                    secondOperand_grid.Visibility = System.Windows.Visibility.Visible;
                    ignoreCase_grid.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 11:
                    secondOperand_grid.Visibility = System.Windows.Visibility.Collapsed;
                    ignoreCase_grid.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case 12:
                    secondOperand_grid.Visibility = System.Windows.Visibility.Collapsed;
                    ignoreCase_grid.Visibility = System.Windows.Visibility.Collapsed;
                    break;



            }
        }
    }
}

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
using SmartifyBotStudio.RobotDesigner.TaskModel;
using MahApps.Metro.Controls;

namespace SmartifyBotStudio.RobotDesigner.TaskView.VariableView
{
    /// <summary>
    /// Interaction logic for VariablePicker.xaml
    /// </summary>
    public partial class VariablePicker : UserControl
    {
        public VariableCollectionModel selectedVariable;

        public VariablePicker()
        {
            InitializeComponent();
            Loaded += VariablePicker_loaded;

            variableGrid.AutoGeneratingColumn += (sender, e) =>
            {
                if (e.PropertyName == "ActionID")
                {
                    e.Column.Visibility = Visibility.Collapsed;
                }
                if (e.PropertyName == "Type")
                {
                    e.Column.Visibility = Visibility.Collapsed;
                }

                if (e.PropertyName == "ObjectValue")
                {
                    e.Column.Visibility = Visibility.Collapsed;
                }
                if (e.PropertyName == "AdditionalInfo_1")
                {
                    e.Column.Visibility = Visibility.Collapsed;
                }

                if (e.PropertyName == "AdditionalInfo_2")
                {
                    e.Column.Visibility = Visibility.Collapsed;
                }


                if (e.PropertyName == "VariableName")
                {
                    e.Column.Header = "Variable Name";
                }

                if (e.PropertyName == "TypeString")
                {
                    e.Column.Header = "Type";
                }


            };
        }

        private void VariablePicker_loaded(object sender, RoutedEventArgs e)
        {
            variableGrid.ItemsSource = VariableStorage.GetAllVariables();
        }

        private void ok_button_Click(object sender, RoutedEventArgs e)
        {
            if (variableGrid.SelectedItem == null) return;
            selectedVariable = variableGrid.SelectedItem as VariableCollectionModel;

            Window.GetWindow(this).Close();
            // (((Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();

            //((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }
    }
}

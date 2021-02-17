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

namespace SmartifyBotStudio.RobotDesigner.TaskView.Flow_Control
{
    /// <summary>
    /// Interaction logic for GoTo.xaml
    /// </summary>
    public partial class GoTo : UserControl
    {

        public RobotDesigner.TaskModel.Flow_Control.GoTo GoToData { get; set; }

        public GoTo()
        {
            InitializeComponent();
            Loaded += Template_loaded;
        }


        private void Template_loaded(object sender, RoutedEventArgs e)
        {
            if (VariableStorage.LabelVar != null)
            {
                labelName_combo.ItemsSource = VariableStorage.LabelVar.Keys;
                labelName_combo.SelectedIndex = GoToData.LabelNameIndex;
            }
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            GoToData.LabelNameToGo = labelName_combo.Text;
            GoToData.LabelToGo = labelName_combo.Text;
            GoToData.LabelNameIndex = labelName_combo.SelectedIndex;


            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }
    }
}

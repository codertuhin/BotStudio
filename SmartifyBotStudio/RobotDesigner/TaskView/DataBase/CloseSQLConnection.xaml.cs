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
using SmartifyBotStudio.RobotDesigner.TaskView.Web_Automation;
using SmartifyBotStudio.RobotDesigner.TaskModel;
using MahApps.Metro.Controls;

namespace SmartifyBotStudio.RobotDesigner.TaskView.DataBase
{
    /// <summary>
    /// Interaction logic for CloseSQLConnection.xaml
    /// </summary>
    public partial class CloseSQLConnection : UserControl //SmartifyBotStudio.RobotDesigner.TaskView.Web_Automation.LaunchNewIE
    {

        public RobotDesigner.TaskModel.DataBase.CloseSQLConnection CloseSQLConnectionTaskData { get; set; }
        

        public CloseSQLConnection()
        {
            InitializeComponent();
            Loaded += CloseSQLConnectionLoad;
        }

        private void CloseSQLConnectionLoad(object sender, RoutedEventArgs e)
        {
            connStrCombobox.ItemsSource = VariableStorage.DataBaseConnStrVar.Keys;
        }

            //ok_button_Click

            private void ok_button_Click(object sender, RoutedEventArgs e)
        {
            CloseSQLConnectionTaskData.ConnectionToClose = connStrCombobox.Text.ToString();

            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }


        private void more_button_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }


    }
}

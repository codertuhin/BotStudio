using MahApps.Metro.Controls;
using SmartifyBotStudio.RobotDesigner.TaskModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace SmartifyBotStudio.RobotDesigner.TaskView.Conditional
{
    /// <summary>
    /// Interaction logic for IfProcessExists.xaml
    /// </summary>
    public partial class IfProcessExists : UserControl
    {

        public RobotDesigner.TaskModel.Conditional.IfProcessExists IfProcessExistsData { get; set; }

        public IfProcessExists()
        {
            InitializeComponent();
            Loaded += IfProcessExistsData_loaded;
        }

        private void IfProcessExistsData_loaded(object sender, RoutedEventArgs e)
        {
            process_combo.ItemsSource = GetAllProcessName();

            runOrNotRun_combo.SelectedIndex = IfProcessExistsData.RunOrNotRun == true ? 0 : 1;

        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            IfProcessExistsData.ProcessName = process_combo.Text;

            IfProcessExistsData.RunOrNotRun = runOrNotRun_combo.SelectedIndex == 0 ? true : false;


            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }
        private void refress_Click(object sender, RoutedEventArgs e)
        {

            process_combo.ItemsSource = GetAllProcessName();
        }

        private List<string> GetAllProcessName()
        {
            Process[] processes = Process.GetProcesses();

            List<string> processName = new List<string>();

            foreach (var item in processes)
            {
                processName.Add(item.ProcessName);
            }

            return processName;

        }

    }
}

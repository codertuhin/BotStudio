using MahApps.Metro.Controls;
using SmartifyBotStudio.RobotDesigner.TaskModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
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
    /// Interaction logic for IfService.xaml
    /// </summary>
    public partial class IfService : UserControl
    {

        public RobotDesigner.TaskModel.Conditional.IfService IfServiceData { get; set; }

        public IfService()
        {
            InitializeComponent();
            Loaded += IfServiceData_loaded;
        }

        private void IfServiceData_loaded(object sender, RoutedEventArgs e)
        {
            serviceStatus_combo.SelectedIndex = IfServiceData.StatusIndex;

            ServiceController[] scServices;
            scServices = ServiceController.GetServices();

            List<string> serviceName = new List<string>();

            foreach (var item in scServices)
            {
                
                serviceName.Add(item.ServiceName);
            }

            serviceName_combo.ItemsSource = serviceName;

            serviceStatus_combo.SelectedIndex=IfServiceData.StatusIndex;

            serviceName_combo.Text = IfServiceData.ServiceName;

            if (IfServiceData.ServiceNameIndex==-1)
            {
                serviceName_combo.SelectedIndex = IfServiceData.ServiceNameIndex;
                serviceName_combo.Text = IfServiceData.ServiceName;
            }

        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            IfServiceData.StatusIndex = serviceStatus_combo.SelectedIndex;

            IfServiceData.ServiceName = serviceName_combo.Text;

            IfServiceData.ServiceNameIndex = serviceName_combo.SelectedIndex;

            if (serviceStatus_combo.SelectedIndex == 0)
            {
                IfServiceData.IsRunning = true;
            }
            if (serviceStatus_combo.SelectedIndex == 1)
            {
                IfServiceData.IsPaused = true;
            }
            if (serviceStatus_combo.SelectedIndex == 2)
            {
                IfServiceData.IsStopped = true;
            }
            if (serviceStatus_combo.SelectedIndex == 3)
            {
                IfServiceData.IsInstalled = true;
            }
            if (serviceStatus_combo.SelectedIndex == 4)
            {
                IfServiceData.IsNotInstalled = true;
            }

            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }
    }
}

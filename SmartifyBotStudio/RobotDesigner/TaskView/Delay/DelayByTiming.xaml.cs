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
using System.Text.RegularExpressions;

namespace SmartifyBotStudio.RobotDesigner.TaskView.Delay
{
    /// <summary>
    /// Interaction logic for LaunchNewIE.xaml
    /// </summary>
    public partial class DelayByTiming : UserControl
    {

        public RobotDesigner.TaskModel.Delay.DelayByTiming DelayByTimingData { get; set; }

        public DelayByTiming()
        {
            InitializeComponent();
            Loaded += DelayByTimingData_loaded;
        }

        private void DelayByTimingData_loaded(object sender, RoutedEventArgs e)
        {
            timeToDelay_text.Text=DelayByTimingData.TimeToDelay.ToString();
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            DelayByTimingData.TimeToDelay = Convert.ToInt32( timeToDelay_text.Text);


            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void useRegulerExpression_checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void NumericalValidation(object sender, TextCompositionEventArgs e)
        {

            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }

        
    }
}

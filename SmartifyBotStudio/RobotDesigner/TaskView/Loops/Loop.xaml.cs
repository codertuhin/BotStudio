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

namespace SmartifyBotStudio.RobotDesigner.TaskView.Loops
{
    /// <summary>
    /// Interaction logic for LaunchNewIE.xaml
    /// </summary>
    public partial class Loop : UserControl
    {

        public RobotDesigner.TaskModel.Loops.Loop LoopData { get; set; }

        public Loop()
        {
            InitializeComponent();
            Loaded += LoopData_loaded;
        }

        private void LoopData_loaded(object sender, RoutedEventArgs e)
        {
            startFrom_text.Text=LoopData.Start.ToString();
            endsTo_text.Text=LoopData.End.ToString();
            incrementBy_text.Text = LoopData.IncrementBy.ToString();
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            LoopData.Start = Convert.ToInt32(startFrom_text.Text);
            LoopData.End = Convert.ToInt32(endsTo_text.Text);
            LoopData.IncrementBy = Convert.ToInt32(incrementBy_text.Text);


            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


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

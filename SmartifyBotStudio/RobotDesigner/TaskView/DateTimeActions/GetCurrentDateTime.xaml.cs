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

namespace SmartifyBotStudio.RobotDesigner.TaskView.DateTimeActions
{

    public partial class GetCurrentDateTime : UserControl
    {

        public RobotDesigner.TaskModel.DateTimeActions.GetCurrentDateTime GetCurrentDateTimeData { get; set; }

        public GetCurrentDateTime()
        {
            InitializeComponent();
            Loaded += GetCurrentDateTime_loaded;
        }

        private void GetCurrentDateTime_loaded(object sender, RoutedEventArgs e)
        {
            retrieveOption_combo.SelectedIndex=GetCurrentDateTimeData.DateRetrieveOption;
            storeVar_text.Text=GetCurrentDateTimeData.ResultStoreVar;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            GetCurrentDateTimeData.DateRetrieveOption = retrieveOption_combo.SelectedIndex;
            GetCurrentDateTimeData.ResultStoreVar = storeVar_text.Text.Trim();


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

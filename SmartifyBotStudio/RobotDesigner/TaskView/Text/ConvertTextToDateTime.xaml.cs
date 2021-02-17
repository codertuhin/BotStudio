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

namespace SmartifyBotStudio.RobotDesigner.TaskView.Text
{
    /// <summary>
    /// Interaction logic for LaunchNewIE.xaml
    /// </summary>
    public partial class ConvertTextToDateTime : UserControl
    {

        public RobotDesigner.TaskModel.Text.ConvertTextToDateTime ConvertTextToDateTimeData { get; set; }

        VariableCollectionModel working_variable;

        public ConvertTextToDateTime()
        {
            InitializeComponent();
            Loaded += ConvertTextToDateTime_loaded;
        }

        private void ConvertTextToDateTime_loaded(object sender, RoutedEventArgs e)
        {

            textToConvert_text.Text = ConvertTextToDateTimeData.TextToDate;
            customFormat_checkBox.IsChecked = ConvertTextToDateTimeData.UseCustomFormat;
            customFormat_text.Text = ConvertTextToDateTimeData.CustomFormat;
            storeVar_text.Text = ConvertTextToDateTimeData.DateToStoreVar;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            ConvertTextToDateTimeData.TextToDate = textToConvert_text.Text;
            ConvertTextToDateTimeData.UseCustomFormat = (bool)customFormat_checkBox.IsChecked;
            ConvertTextToDateTimeData.CustomFormat = customFormat_text.Text;
            ConvertTextToDateTimeData.DateToStoreVar = VariableStorage.VariableNameFormator(storeVar_text.Text.Trim());

            if (!string.IsNullOrEmpty(storeVar_text.Text.Trim()))
            {
                if (VariableStorage.TextToDateTimeVar.ContainsKey(ConvertTextToDateTimeData.DateToStoreVar))
                {
                    VariableStorage.TextToDateTimeVar.Remove(ConvertTextToDateTimeData.DateToStoreVar);
                }
                VariableStorage.TextToDateTimeVar.Add(ConvertTextToDateTimeData.DateToStoreVar, Tuple.Create(ConvertTextToDateTimeData.ID, DateTime.Now));
            }

            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void text_to_convert_pick(object sender, RoutedEventArgs e)
        {
            textToConvert_text.Text = VariableStorage.Pick().VariableName;
        }
    }
}

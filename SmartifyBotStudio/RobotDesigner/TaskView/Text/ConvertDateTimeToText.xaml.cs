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
    public partial class ConvertDateTimeToText : UserControl
    {

        public RobotDesigner.TaskModel.Text.ConvertDateTimeToText ConvertDateTimeToTextData { get; set; }

        public ConvertDateTimeToText()
        {
            InitializeComponent();
            Loaded += ConvertDateTimeToText_loaded;
        }

        private void ConvertDateTimeToText_loaded(object sender, RoutedEventArgs e)
        {
            dateTiemToConvert_combo.ItemsSource = VariableStorage.TextToDateTimeVar.Keys;
            dateTiemToConvert_combo.SelectedIndex = ConvertDateTimeToTextData.DateTimeToCovertVarComboIndex;

            standardFormat_Combo.SelectedIndex = ConvertDateTimeToTextData.StandardFormatIndex;
            customFromat_Combo.Text = ConvertDateTimeToTextData.CustomFormat;

            storeVar_text.Text = ConvertDateTimeToTextData.ResultStoreVar;

            formatToUse_Combo.SelectedIndex = ConvertDateTimeToTextData.DateTiemToConvertOptionComboIndex;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {


            ConvertDateTimeToTextData.DateTimeToConvertVar = dateTiemToConvert_combo.Text;
            ConvertDateTimeToTextData.DateTimeToCovertVarComboIndex = dateTiemToConvert_combo.SelectedIndex;

            ConvertDateTimeToTextData.StandardFormatIndex = standardFormat_Combo.SelectedIndex;
            ConvertDateTimeToTextData.CustomFormat = customFromat_Combo.Text;


            ConvertDateTimeToTextData.DateTiemToConvertOptionComboIndex = formatToUse_Combo.SelectedIndex;


            ConvertDateTimeToTextData.ResultStoreVar = VariableStorage.VariableNameFormator(storeVar_text.Text.Trim());


            if (!string.IsNullOrEmpty(storeVar_text.Text.Trim()))
            {
                if (VariableStorage.DateToTextVar.ContainsKey(ConvertDateTimeToTextData.ResultStoreVar))
                {
                    VariableStorage.DateToTextVar.Remove(ConvertDateTimeToTextData.ResultStoreVar);
                }

                VariableStorage.DateToTextVar.Add(ConvertDateTimeToTextData.ResultStoreVar, Tuple.Create(ConvertDateTimeToTextData.ID, "value has not been set yet"));

            }

            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

    }
}

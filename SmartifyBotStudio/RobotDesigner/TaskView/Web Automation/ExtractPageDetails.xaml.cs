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

namespace SmartifyBotStudio.RobotDesigner.TaskView.Web_Automation
{
    /// <summary>
    /// Interaction logic for LaunchNewIE.xaml
    /// </summary>
    public partial class ExtractPageDetails : UserControl
    {

        public RobotDesigner.TaskModel.Web_Automation.ExtractPageDetails ExtractPageDetailsData { get; set; }

        public ExtractPageDetails()
        {
            InitializeComponent();
            Loaded += GetObjectDetails_loaded;
        }

        private void GetObjectDetails_loaded(object sender, RoutedEventArgs e)
        {
            webDriverInstanceCombo.ItemsSource = VariableStorage.WebDriverVar.Keys;
            webDriverInstanceCombo.SelectedIndex = ExtractPageDetailsData.WebDriverInstanceComboIndex;
            operationCombo.SelectedIndex = ExtractPageDetailsData.OperatinIndex;
            propertyStoreVar_text.Text = ExtractPageDetailsData.PropertyStoreVar;

        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {
            ExtractPageDetailsData.WebDriverInstanceComboIndex = webDriverInstanceCombo.SelectedIndex;
            ExtractPageDetailsData.WebDriverInstanceVar = webDriverInstanceCombo.Text;
            ExtractPageDetailsData.PropertyStoreVar = VariableStorage.VariableNameFormator( propertyStoreVar_text.Text.Trim());
            ExtractPageDetailsData.OperatinIndex = operationCombo.SelectedIndex;

            if (VariableStorage.WebDetailsVar.ContainsKey(ExtractPageDetailsData.PropertyStoreVar))
            {
                VariableStorage.WebDetailsVar.Remove(ExtractPageDetailsData.PropertyStoreVar);
            }
            VariableStorage.WebDetailsVar.Add(ExtractPageDetailsData.PropertyStoreVar, Tuple.Create(ExtractPageDetailsData.ID, ""));


            if (string.IsNullOrEmpty(propertyStoreVar_text.Text.Trim()))
            {
                MessageBox.Show("Please, Enter a variable name to store data.");
            }
            if (!string.IsNullOrEmpty(propertyStoreVar_text.Text.Trim()))
            {
                ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
            }



        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

    }
}

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
//using System.Windows.Forms;

namespace SmartifyBotStudio.RobotDesigner.TaskView.MessageBoxes
{
    /// <summary>
    /// Interaction logic for LaunchNewIE.xaml
    /// </summary>
    public partial class DisplayMessage : UserControl
    {

        public RobotDesigner.TaskModel.MessageBoxes.DisplayMessage DisplayMessageData { get; set; }


        public const uint MaxValue = 4294967295;

        VariableCollectionModel working_variable;

        public DisplayMessage()
        {
            InitializeComponent();
            Loaded += DisplayMessage_loaded;
        }

        private void DisplayMessage_loaded(object sender, RoutedEventArgs e)
        {
            title_text.Text = DisplayMessageData.Title;


            msg_rtb.Document.Blocks.Clear();
            if (DisplayMessageData.WorkingVariable != null)
            {
                msg_rtb.Document.Blocks.Add(new Paragraph(new Run(DisplayMessageData.WorkingVariable.VariableName)));
            }
            else
            {
                msg_rtb.Document.Blocks.Add(new Paragraph(new Run(DisplayMessageData.Message)));

            }

            closeMessageAutomatically_checkBox.IsChecked = DisplayMessageData.CloseMsgAutomatically;

            if (DisplayMessageData.CloseMsgAutomatically)
            {
                closeTimeUpDown.Value = DisplayMessageData.Time;

            }
            else
            {
                closeTimeUpDown.Value = 1;
            }



            if (DisplayMessageData.ButtonIndex == 0)                  //OK 
            {

                msgBtn_combo.SelectedIndex = 0;

            }
            else if (DisplayMessageData.ButtonIndex == 1)           // Ok - Cancel      
            {
                msgBtn_combo.SelectedIndex = 1;
            }
            if (DisplayMessageData.ButtonIndex == 4)         //y-N  
            {
                msgBtn_combo.SelectedIndex = 2;
            }
            else if (DisplayMessageData.ButtonIndex == 3)
            {
                msgBtn_combo.SelectedIndex = 3;     //y-n-c
            }
            else if (DisplayMessageData.ButtonIndex == 2)      //a-r-i
            {
                msgBtn_combo.SelectedIndex = 4;
            }
            else if (DisplayMessageData.ButtonIndex == 5)   //r-c
            {
                msgBtn_combo.SelectedIndex = 5;
            }





            if (DisplayMessageData.IconIndex == 0)
            {
                msgIcon_combo.SelectedIndex = 0;
            }
            else if (DisplayMessageData.IconIndex == 64)
            {
                msgIcon_combo.SelectedIndex = 1;
            }
            else if (DisplayMessageData.IconIndex == 32)
            {
                msgIcon_combo.SelectedIndex = 2;
            }
            else if (DisplayMessageData.IconIndex == 48)
            {
                msgIcon_combo.SelectedIndex = 3;
            }
            else if (DisplayMessageData.IconIndex == 16)
            {
                msgIcon_combo.SelectedIndex = 4;
            }


            if (DisplayMessageData.DefaultButtonIndex == 0)
            {
                msgDefalut_combo.SelectedIndex = 0;
            }
            else if (DisplayMessageData.DefaultButtonIndex == 256)
            {
                msgDefalut_combo.SelectedIndex = 1;
            }
            else if (DisplayMessageData.DefaultButtonIndex == 512)
            {
                msgDefalut_combo.SelectedIndex = 2;
            }

        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            DisplayMessageData.WorkingVariable = working_variable;

            DisplayMessageData.Title = title_text.Text;

            TextRange textRange = new TextRange(msg_rtb.Document.ContentStart, msg_rtb.Document.ContentEnd);

            DisplayMessageData.Message = textRange.Text.Trim();

            DisplayMessageData.CloseMsgAutomatically = (bool)closeMessageAutomatically_checkBox.IsChecked;
            DisplayMessageData.Time = Convert.ToUInt32(closeTimeUpDown.Value);



            if (msgBtn_combo.SelectedIndex == 0)                  //OK 
            {
                DisplayMessageData.ButtonIndex = 0;
            }
            else if (msgBtn_combo.SelectedIndex == 1)           // Ok - Cancel      
            {
                DisplayMessageData.ButtonIndex = 1;
            }
            if (msgBtn_combo.SelectedIndex == 2)         //y-N  
            {
                DisplayMessageData.ButtonIndex = 4;
            }
            else if (msgBtn_combo.SelectedIndex ==3)
            {
                DisplayMessageData.ButtonIndex = 3;     //y-n-c
            }
            else if (msgBtn_combo.SelectedIndex == 4)      //a-r-i
            {
                DisplayMessageData.ButtonIndex = 2;
            }
            else if (msgBtn_combo.SelectedIndex == 5)   //r-c
            {
                DisplayMessageData.ButtonIndex = 5;
            }





            if (msgIcon_combo.SelectedIndex == 0)
            {
                DisplayMessageData.IconIndex = 0;
            }
            else if (msgIcon_combo.SelectedIndex == 1)
            {
                DisplayMessageData.IconIndex = 64;
            }
            else if (msgIcon_combo.SelectedIndex == 2)
            {
                DisplayMessageData.IconIndex = 32;
            }
            else if (msgIcon_combo.SelectedIndex == 3)
            {
                DisplayMessageData.IconIndex = 48;
            }
            else if (msgIcon_combo.SelectedIndex == 4)
            {
                DisplayMessageData.IconIndex = 16;
            }


            if (msgDefalut_combo.SelectedIndex == 0)
            {
                DisplayMessageData.DefaultButtonIndex = 0;
            }
            else if (msgDefalut_combo.SelectedIndex == 1)
            {
                DisplayMessageData.DefaultButtonIndex = 256;
            }
            else if (msgDefalut_combo.SelectedIndex == 2)
            {
                DisplayMessageData.DefaultButtonIndex = 512;
            }



            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void useRegulerExpression_checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void pickVar_click(object sender, RoutedEventArgs e)
        {
            working_variable = VariableStorage.Pick();

            if (working_variable != null)
            {
                msg_rtb.Document.Blocks.Clear();
                msg_rtb.Document.Blocks.Add(new Paragraph(new Run(working_variable.VariableName)));
            }


        }
    }
}

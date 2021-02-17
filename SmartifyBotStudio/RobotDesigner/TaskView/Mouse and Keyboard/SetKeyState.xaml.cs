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

namespace SmartifyBotStudio.RobotDesigner.TaskView.Mouse_and_Keyboard
{
    /// <summary>
    /// Interaction logic for LaunchNewIE.xaml
    /// </summary>
    public partial class SetKeyState : UserControl
    {

        public RobotDesigner.TaskModel.Mouse_and_Keyboard.SetKeyState SetKeyStateData { get; set; }

        public SetKeyState()
        {
            InitializeComponent();
            Loaded += SetKeyState_loaded;
        }

        private void SetKeyState_loaded(object sender, RoutedEventArgs e)
        {
            key_combo.SelectedIndex = SetKeyStateData.KeyIndex;
            state_combo.SelectedIndex = SetKeyStateData.OnOff == true ? 0 : 1;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {


            SetKeyStateData.KeyIndex = key_combo.SelectedIndex;
            SetKeyStateData.OnOff = state_combo.SelectedIndex == 0 ? true : false;


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

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
    public partial class PressReleaseKey : UserControl
    {

        public RobotDesigner.TaskModel.Mouse_and_Keyboard.PressReleaseKey PressReleaseKeyData { get; set; }

        public PressReleaseKey()
        {
            InitializeComponent();
            Loaded += PressReleaseKey_loaded;
        }

        private void PressReleaseKey_loaded(object sender, RoutedEventArgs e)
        {
            if (PressReleaseKeyData.PressOrRelease == true)
            {
                pressRelease_combo.SelectedIndex = 0;
            }
            else
            {
                pressRelease_combo.SelectedIndex = 1;
            }

            ctrl_checkBox.IsChecked=PressReleaseKeyData.Control;
            alt_checkBox.IsChecked=PressReleaseKeyData.Alt;
            shift_checkBox.IsChecked=PressReleaseKeyData.Shift;
            win_checkBox.IsChecked=PressReleaseKeyData.Win;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            if (pressRelease_combo.SelectedIndex==0)
            {
                PressReleaseKeyData.PressOrRelease = true;
            }
            else
            {
                PressReleaseKeyData.PressOrRelease = false;
            }

            PressReleaseKeyData.Control = (bool)ctrl_checkBox.IsChecked;
            PressReleaseKeyData.Alt = (bool)alt_checkBox.IsChecked;
            PressReleaseKeyData.Shift = (bool)shift_checkBox.IsChecked;
            PressReleaseKeyData.Win = (bool)win_checkBox.IsChecked;




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

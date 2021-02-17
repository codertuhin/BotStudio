using MahApps.Metro.Controls;
using SmartifyBotStudio.RobotDesigner.TaskModel;
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

namespace SmartifyBotStudio.RobotDesigner.TaskView.Mouse_and_Keyboard
{
    /// <summary>
    /// Interaction logic for MouseClick.xaml
    /// </summary>
    public partial class MouseClick : UserControl
    {
        public RobotDesigner.TaskModel.Mouse_and_Keyboard.MouseClick MouseClickData { get; set; }

        public MouseClick()
        {
            InitializeComponent();
            Loaded += MouseClickData_loaded;


            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += (sender, e) =>
            {

                X_label.Content = System.Windows.Forms.Cursor.Position.X;
                Y_label.Content = System.Windows.Forms.Cursor.Position.Y;
            };

            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();

        }


        private void MouseClickData_loaded(object sender, RoutedEventArgs e)
        {
            X_textBox.Text = MouseClickData.X.ToString();
            Y_textBox.Text = MouseClickData.Y.ToString();

            speed_combo.SelectedIndex = MouseClickData.SpeedComboIndex;

            mouseEvent_combo.SelectedIndex = MouseClickData.ClickIndex;

            moveBefore_checkBox.IsChecked = MouseClickData.BeforeEventCheck;

        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            MouseClickData.X = Convert.ToInt32(X_textBox.Text.Trim());
            MouseClickData.Y = Convert.ToInt32(Y_textBox.Text.Trim());


            MouseClickData.ClickIndex = mouseEvent_combo.SelectedIndex;
            MouseClickData.SpeedComboIndex = speed_combo.SelectedIndex;

            MouseClickData.BeforeEventCheck = (bool)moveBefore_checkBox.IsChecked;


            if (speed_combo.SelectedIndex == 0)
            {
                MouseClickData.Speed = 0;
            }
            if (speed_combo.SelectedIndex == 1)
            {
                MouseClickData.Speed = 100;
            }
            if (speed_combo.SelectedIndex == 2)
            {
                MouseClickData.Speed = 75;
            }
            if (speed_combo.SelectedIndex == 3)
            {
                MouseClickData.Speed = 25;
            }


            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if ((!e.KeyboardDevice.IsKeyDown(Key.LeftShift)) && (!e.KeyboardDevice.IsKeyDown(Key.RightShift)) && e.KeyboardDevice.IsKeyDown(Key.Tab))
            // if ( e.KeyboardDevice.IsKeyDown(Key.LeftCtrl))
            {
                X_textBox.Text = System.Windows.Forms.Cursor.Position.X.ToString();
                Y_textBox.Text = System.Windows.Forms.Cursor.Position.Y.ToString();
            }
        }

    }
}

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
    /// Interaction logic for MoveMouse.xaml
    /// </summary>
    public partial class MoveMouse : UserControl
    {
        public RobotDesigner.TaskModel.Mouse_and_Keyboard.MoveMouse MoveMouseData { get; set; }
        public MoveMouse()
        {
            InitializeComponent();
            Loaded += MoveMouseData_loaded;


            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += (sender, e) =>
             {

                 X_label.Content = System.Windows.Forms.Cursor.Position.X;
                 Y_label.Content = System.Windows.Forms.Cursor.Position.Y;
             };

            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();
        }

        private void MoveMouseData_loaded(object sender, RoutedEventArgs e)
        {
            X_textBox.Text = MoveMouseData.X.ToString();
            Y_textBox.Text = MoveMouseData.Y.ToString();

            speed_combo.SelectedIndex = MoveMouseData.SpeedComboIndex;

        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            MoveMouseData.X = Convert.ToInt32(X_textBox.Text.Trim());
            MoveMouseData.Y = Convert.ToInt32(Y_textBox.Text.Trim());

            MoveMouseData.SpeedComboIndex=speed_combo.SelectedIndex;

            if (speed_combo.SelectedIndex==0)
            {
                MoveMouseData.Speed = 0;
            }
            if (speed_combo.SelectedIndex == 1)
            {
                MoveMouseData.Speed = 100;
            }
            if (speed_combo.SelectedIndex == 2)
            {
                MoveMouseData.Speed = 75;
            }
            if (speed_combo.SelectedIndex == 3)
            {
                MoveMouseData.Speed = 25;
            }

            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void moveMouse(object sender, MouseEventArgs e)
        {

        }





        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // code goes here
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

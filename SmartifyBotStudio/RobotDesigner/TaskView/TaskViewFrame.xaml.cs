using SmartifyBotStudio.RobotDesigner.Enums;
using SmartifyBotStudio.RobotDesigner.TaskView.File;
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
using System.Windows.Shapes;

namespace SmartifyBotStudio.RobotDesigner.TaskView
{
    /// <summary>
    /// Interaction logic for TaskViewFrame.xaml
    /// </summary>
    public partial class TaskViewFrame : System.Windows.Controls.UserControl
    {

        public RobotDesigner.Enums.RobotActionsEnum TaskType { get; set; }
        public TaskViewFrame()
        {
            InitializeComponent();

            
            
        }

        private void TaskViewFrame_Loaded(object sender, RoutedEventArgs e)
        {
            switch (TaskType)
            {
                case RobotActionsEnum.CopyFile:
                    panelMain.Children.Clear();
                    panelMain.Children.Add(new CopyFiles());
                    break;
                case RobotActionsEnum.MoveFile:

                    break;
                case RobotActionsEnum.DeleteFile:
                    
                    break;
            }
        }
    }
}

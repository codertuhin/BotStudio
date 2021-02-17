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

namespace SmartifyBotStudio.RobotDesigner.TaskView.Conditional
{
    /// <summary>
    /// Interaction logic for IfFileExists.xaml
    /// </summary>
    public partial class IfFileExists : UserControl
    {
        public RobotDesigner.TaskModel.Conditional.IfFileExists IfFileExistsData { get; set; }
        public IfFileExists()
        {
            InitializeComponent();
            Loaded += IfFileExists_loaded;
        }

        private void IfFileExists_loaded(object sender, RoutedEventArgs e)
        {
            filePath_text.Text=IfFileExistsData.FilePath;
            exitsOrNotExits_combo.SelectedIndex =IfFileExistsData.ExistsOrNotExists ==true ? 0 : 1;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            IfFileExistsData.FilePath = filePath_text.Text.Trim();
            IfFileExistsData.ExistsOrNotExists = exitsOrNotExits_combo.SelectedIndex == 0 ? true : false;

            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }
        private void btnOpenFiles_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                filePath_text.Text = openFileDialog.FileName;

            }
        }
    }
}

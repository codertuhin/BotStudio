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
    /// Interaction logic for IfFolderExists.xaml
    /// </summary>
    public partial class IfFolderExists : UserControl
    {

        public RobotDesigner.TaskModel.Conditional.IfFolderExists IfFolderExistsData { get; set; }
        public IfFolderExists()
        {
            InitializeComponent();
            Loaded += IfFolderExistsData_loaded;
        }

        private void IfFolderExistsData_loaded(object sender, RoutedEventArgs e)
        {

            folderPath_text.Text = IfFolderExistsData.FolderPath;
            exitsOrNotExists_combo.SelectedIndex = IfFolderExistsData.ExistsOrNotExists == false ? 0 : 1;
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            IfFolderExistsData.FolderPath = folderPath_text.Text.Trim();
            IfFolderExistsData.ExistsOrNotExists = exitsOrNotExists_combo.SelectedIndex == 0 ? true : false;


            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void btnOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog()
            {
                ShowNewFolderButton = true,
            };

            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                folderPath_text.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}

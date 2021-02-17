using MahApps.Metro.Controls;
using SmartifyBotStudio.RobotDesigner.TaskModel;
using SmartifyBotStudio.RobotDesigner.TaskModel.File;
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

namespace SmartifyBotStudio.RobotDesigner.TaskView.File
{
    /// <summary>
    /// Interaction logic for GetTemporaryFile.xaml
    /// </summary>
    public partial class GetTemporaryFile : UserControl
    {
        public GetTemporaryFile()
        {
            InitializeComponent();
        }
       

        private void more_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            //CopyFilesData.Destination = txtDestinationFolder.Text;
            List<SelectedFileInfo> files = new List<SelectedFileInfo>();

            //foreach (ListBoxItem item in FilesToDelete_text.Items)
            //{

            //    files.Add(new SelectedFileInfo() { FilePath = item.Tag.ToString(), FileName = item.Content.ToString() });

            //    DeleteFileData.Description = "Files(" + FilesToDelete_text.Items.Count + ") ";
            //}

            // DeleteFileData.FilesToDetele = files;
            //DeleteFileData.IsActive = (bool)this_checkbox.IsChecked;
            //DeleteFileData.Execute();

            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;

        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
    }
}

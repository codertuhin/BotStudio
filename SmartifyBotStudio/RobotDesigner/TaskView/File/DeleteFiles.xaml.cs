using MahApps.Metro.Controls;
using SmartifyBotStudio.RobotDesigner.TaskModel;
using SmartifyBotStudio.RobotDesigner.TaskModel.File;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace SmartifyBotStudio.RobotDesigner.TaskView.File
{
    /// <summary>
    /// Interaction logic for DeleteFiles.xaml
    /// </summary>
    public partial class DeleteFiles : System.Windows.Controls.UserControl
    {
        public RobotDesigner.TaskModel.File.DeleteFiles DeleteFileData { get; set; }
        public DeleteFiles()
        {
            InitializeComponent();
            Loaded += DeleteFile_Loaded;
        }
        private void DeleteFile_Loaded(object sender, RoutedEventArgs e)
        {
            if (DeleteFileData != null)
            {

                this_checkbox.IsChecked = DeleteFileData.IsActive;
                if (DeleteFileData.FilesToDetele != null)
                    foreach (var item in DeleteFileData.FilesToDetele)
                    {
                        FilesToDelete_text.Items.Add(new ListBoxItem() { Tag = item.FilePath, Content = item.FileName });
                    }
            }
        }
        private void btnOpenFiles_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {

                foreach (var item in openFileDialog.FileNames)
                {

                    FilesToDelete_text.Items.Add(new ListBoxItem() { Tag = item, Content = new FileInfo(item).Name });
                }

            }
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ok_button_Click(object sender, RoutedEventArgs e)
        {


            List<SelectedFileInfo> files = new List<SelectedFileInfo>();

            foreach (ListBoxItem item in FilesToDelete_text.Items)
            {

                files.Add(new SelectedFileInfo() { FilePath = item.Tag.ToString(), FileName = item.Content.ToString() });

                DeleteFileData.Description = "Files(" + FilesToDelete_text.Items.Count + ") ";
            }

            DeleteFileData.FilesToDetele = files;
            DeleteFileData.IsActive = (bool)this_checkbox.IsChecked;
            //DeleteFileData.Execute();

            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;

        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
    }
}

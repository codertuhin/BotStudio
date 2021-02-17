using MahApps.Metro.Controls;
using SmartifyBotStudio.RobotDesigner.TaskModel;
using SmartifyBotStudio.RobotDesigner.TaskModel.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartifyBotStudio.RobotDesigner.TaskView.File
{
    /// <summary>
    /// Interaction logic for MoveFiles.xaml
    /// </summary>
    public partial class MoveFiles : System.Windows.Controls.UserControl
    {
        public RobotDesigner.TaskModel.File.MoveFiles MoveFilesData { get; set; }
        public MoveFiles()
        {
            InitializeComponent();
            Loaded += MoveFiles_Loaded;
        }
        private void MoveFiles_Loaded(object sender, RoutedEventArgs e)
        {
            if (MoveFilesData != null)
            {
                destination_text.Text = MoveFilesData.DestinationFolder;
                this_checkbox.IsChecked = MoveFilesData.IsActive;

                if_combobox.SelectedIndex = MoveFilesData.OverWriteIfExists ? 1 : 0;

                if (MoveFilesData.FilesToMove != null)
                    foreach (var item in MoveFilesData.FilesToMove)
                    {
                        file_text.Items.Add(new ListBoxItem() { Tag = item.FilePath, Content = item.FileName });
                    }
            }
        }
        private void btnOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
            {
                ShowNewFolderButton = true,
            };

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                destination_text.Text = folderBrowserDialog.SelectedPath;
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

                    file_text.Items.Add(new ListBoxItem() { Tag = item, Content = new FileInfo(item).Name });
                }

            }
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            MoveFilesData.DestinationFolder = destination_text.Text;
            List<SelectedFileInfo> files = new List<SelectedFileInfo>();

            foreach (ListBoxItem item in file_text.Items)
            {

                files.Add(new SelectedFileInfo() { FilePath = item.Tag.ToString(), FileName = item.Content.ToString() });

                MoveFilesData.Description = "Files(" + file_text.Items.Count + ") Des:" + destination_text.Text;
            }

            MoveFilesData.FilesToMove = files;
            MoveFilesData.IsActive = (bool)this_checkbox.IsChecked;
            MoveFilesData.OverWriteIfExists = Convert.ToBoolean(if_combobox.SelectedIndex);
            MoveFilesData.Var_StoreMovedFilesInto = store_text.Text;
           // MoveFilesData.Execute();
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;

        }
    }
}

using MahApps.Metro.Controls;
using SmartifyBotStudio.RobotDesigner.TaskModel;
using SmartifyBotStudio.RobotDesigner.TaskModel.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace SmartifyBotStudio.RobotDesigner.TaskView.File
{
    /// <summary>
    /// Interaction logic for CopyFiles.xaml
    /// </summary>
    public partial class CopyFiles : System.Windows.Controls.UserControl
    {
        public RobotDesigner.TaskModel.File.CopyFiles CopyFilesData { get; set; }
        public CopyFiles()
        {
            InitializeComponent();

            Loaded += CopyFiles_Loaded;
        }

        private void CopyFiles_Loaded(object sender, RoutedEventArgs e)
        {
            if (CopyFilesData != null)
            {
                txtDestinationFolder.Text = CopyFilesData.Destination;
                this_checkbox.IsChecked = CopyFilesData.IsActive;

                if_dropdown.SelectedIndex = CopyFilesData.OverWriteIfFilesExixts ? 1 : 0;

                if (CopyFilesData.FilesToCopy != null)
                    foreach (var item in CopyFilesData.FilesToCopy)
                    {
                        txtFilesToCopy.Items.Add(new ListBoxItem() { Tag = item.FilePath, Content = item.FileName });
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

                    txtFilesToCopy.Items.Add(new ListBoxItem() {Tag = item, Content = new FileInfo(item).Name });
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
                txtDestinationFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            CopyFilesData.Destination = txtDestinationFolder.Text;
            List<SelectedFileInfo> files = new List<SelectedFileInfo>();

            foreach (ListBoxItem item in txtFilesToCopy.Items)
            {

                files.Add(new SelectedFileInfo() { FilePath = item.Tag.ToString(), FileName = item.Content.ToString() });

                CopyFilesData.Description = "Files(" + txtFilesToCopy.Items.Count + ") Des:" + txtDestinationFolder.Text;
            }

            CopyFilesData.FilesToCopy = files;
            CopyFilesData.IsActive = (bool)this_checkbox.IsChecked;
            CopyFilesData.OverWriteIfFilesExixts = Convert.ToBoolean(if_dropdown.SelectedIndex);
            CopyFilesData.Var_StoreCopiedFiles = store_text.Text;
            
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;

        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;

        }

        private void btnClearList_Click(object sender, RoutedEventArgs e)
        {
            txtFilesToCopy.Items.Clear();
        }

        private void btnRemoveSelected_Click(object sender, RoutedEventArgs e)
        {
            if (txtFilesToCopy.SelectedIndex < 0)
            {
                return;
            }
            txtFilesToCopy.Items.RemoveAt(txtFilesToCopy.SelectedIndex);
        }
    }
}

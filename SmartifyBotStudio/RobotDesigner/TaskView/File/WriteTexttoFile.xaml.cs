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

namespace SmartifyBotStudio.RobotDesigner.TaskView.File
{
    /// <summary>
    /// Interaction logic for WriteTexttoFile.xaml
    /// </summary>
    public partial class WriteTexttoFile : UserControl
    {
         public RobotDesigner.TaskModel.File.WriteTextToFile TextFiletoWrite { get; set; }
        public WriteTexttoFile()
        {
            InitializeComponent();
        }
        private void btnOpenFiles_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                file_text.Text = openFileDialog.FileName;
             }
        }
        private void more_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ok_button_Click(object sender, RoutedEventArgs e)
        {
            TextFiletoWrite.FilePath = file_text.Text;
            //MessageBox.Show(TextFiletoWrite.FilePath);

            //TextFiletoWrite.TextToWrite=rich_text_to_write.tex

            

            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;

        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
    }
}

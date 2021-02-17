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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace SmartifyBotStudio.RobotDesigner.TaskView.File
{
    /// <summary>
    /// Interaction logic for RenameFiles.xaml
    /// </summary>
    public partial class RenameFiles : UserControl
    {
        public RobotDesigner.TaskModel.File.RenameFiles RenameFileData { get; set; }





        public RenameFiles()
        {
            InitializeComponent();
            Loaded += RenameFiles_Loaded;
        }

        private void RenameFiles_Loaded(object sender, RoutedEventArgs e)
        {
            this_checkbox.IsChecked = RenameFileData.IsActive;

            ifExists_dropdown.SelectedIndex = RenameFileData.IfFileExists ? 1 : 0;

            if (RenameFileData.FilesToRename != null)
            {
                fileRename_text.Text = RenameFileData.FilesToRename.FileInfo;


                this_checkbox.IsChecked = RenameFileData.IsActive;
                ifExists_dropdown.SelectedIndex = Convert.ToInt32(RenameFileData.IfFileExists);
                store_text.Text = RenameFileData.Var_StoreRenamedFilesInto;
                renameSchema_dropdown.Text = RenameFileData.RenameScheme;
                newFileName_text.Text = RenameFileData.NewFileName;
                addDateTime_dropdown.Text = RenameFileData.After_Or_Before_Date;
                dateTimeAdd_dropdown.Text = RenameFileData.Date_time_to_Add;
                addText_dropdown.Text = RenameFileData.Ater_Or_Before_AddText;
                separator_dropdown.Text = RenameFileData.Separator_datetime;
                newExtension_text.Text = RenameFileData.New_Extension;
                keepExtensionCheck.IsChecked = RenameFileData.KeepExtension;
                add_text.Text = RenameFileData.Text_to_Add;
                replace_text.Text = RenameFileData.Text_to_Replace;
                replaceWith_text.Text = RenameFileData.Text_to_ReplaceWith;
                format_dropdown.Text = RenameFileData.Date_Time_Format;
                custom_text.Text = RenameFileData.Custom_Date;
                remove_text.Text = RenameFileData.Text_to_Remove;


                Add_dropdown.Text = RenameFileData.NewFileName_or_Existing_Sequentioal;
                newName_text.Text = RenameFileData.NewFileNameSequential;
                ddDateTime_dropdown.Text = RenameFileData.After_or_before_sequential;

                if (RenameFileData.Start_numbering_at.ToString() != "0")
                {
                    start_text.Text = RenameFileData.Start_numbering_at.ToString();
                }
                if (RenameFileData.Increment_by.ToString() != "0")
                {
                    ample_text.Text = RenameFileData.Increment_by.ToString();
                }

                //RenameFileData.Lenght_of_digit = Int32.Parse(sequnce_length.SelectedValue.ToString());
                sequnce_length.Text = RenameFileData.Lenght_of_digit.ToString();


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
                fileRename_text.Text = openFileDialog.FileName;

                string sourceFile = openFileDialog.FileName;


            }
        }



        private void more_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            SelectedFileInfo files = new SelectedFileInfo();

            if (fileRename_text.Text == "")
            {
                MessageBox.Show("Enter a valid File Path");
            }
            else
            {

                files.FileInfo = fileRename_text.Text;
                files.FileName = System.IO.Path.GetFileName(fileRename_text.Text);
                files.FilePath = System.IO.Path.GetDirectoryName(fileRename_text.Text);

                files.FileExtention = System.IO.Path.GetExtension(fileRename_text.Text);
                files.FileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fileRename_text.Text);
            }


            RenameFileData.FilesToRename = files;

            RenameFileData.IsActive = (bool)this_checkbox.IsChecked;
            RenameFileData.IfFileExists = Convert.ToBoolean(ifExists_dropdown.SelectedIndex);
            RenameFileData.Var_StoreRenamedFilesInto = store_text.Text;
            RenameFileData.RenameScheme = renameSchema_dropdown.Text.ToString();
            RenameFileData.NewFileName = newFileName_text.Text;

            RenameFileData.After_Or_Before_Date = addDateTime_dropdown.Text.ToString();
            RenameFileData.Date_time_to_Add = dateTimeAdd_dropdown.Text.ToString();
            RenameFileData.Ater_Or_Before_AddText = addText_dropdown.Text.ToString();

            RenameFileData.Separator_datetime = separator_dropdown.Text.ToString();
            RenameFileData.New_Extension = newExtension_text.Text.ToString();
            RenameFileData.KeepExtension = (bool)keepExtensionCheck.IsChecked;
            RenameFileData.Text_to_Add = add_text.Text.ToString();
            RenameFileData.Text_to_Remove = remove_text.Text;
            RenameFileData.Text_to_Replace = replace_text.Text.ToString();
            RenameFileData.Text_to_ReplaceWith = replaceWith_text.Text.ToString();
            RenameFileData.Date_Time_Format = format_dropdown.Text.ToString();
            RenameFileData.Custom_Date = custom_text.Text.ToString();

            //Make Sequential
            RenameFileData.NewFileName_or_Existing_Sequentioal = Add_dropdown.Text.ToString();
            RenameFileData.NewFileNameSequential = newName_text.Text.ToString();
            RenameFileData.Separator_sequential = serator_dropdown.Text.ToString();
            // if(start_text.Text.ToString()!="")
            {
                RenameFileData.After_or_before_sequential = ddDateTime_dropdown.Text.ToString();
            }
            if (start_text.Text.ToString() != "")
            {
                RenameFileData.Start_numbering_at = Convert.ToInt32((start_text.Text.ToString()));
            }
            if (ample_text.Text.ToString() != "")
            {
                RenameFileData.Increment_by = Convert.ToInt32(ample_text.Text.ToString());
            }
            //MessageBox.Show(sequnce_length.SelectedItem.ToString());
            if (sequnce_length.Text.ToString() == "")
            {
                RenameFileData.Lenght_of_digit = 1;
            }
            else
            {
                RenameFileData.Lenght_of_digit = Convert.ToInt32(sequnce_length.Text.ToString());
            }


            ////FileInfo f = new FileInfo("c:\test.txt");
            ////f.("test2.txt");
            //string newFileName = "Foo.txt";

            //// full pattern
            ////System.IO.FileInfo fileInfo = new System.IO.FileInfo(files.FileName);
            ////fileInfo.re(newFileName);

            //// or short form
            //new System.IO.FileInfo(fileRename_text.Text).Rename(newFileName);

            if (fileRename_text.Text != "")
            {
                ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;

            }
           


        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;

        }


        private void format_dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //if (format_dropdown.SelectedItem == "yyyy-MM-dd")
                //{
                //    example_text.Text = "cleared";
                //}
                //else {
                //    var a = "not cleared";
                //    example_text.Text =a.ToString() ;
                //}
                //if (format_dropdown.SelectedItem.Equals("yyyy-MM-dd"))
                //{
                //    example_text.Text = "cleared";
                //}
                //else { example_text.Text = "not cleared"; }


                var currentText = (sender as ComboBox).SelectedItem as string;

                //if (currentText.Equals("yyyy-MM-dd"))
                //{
                //    example_text.Text = "m";
                //}
                //if (currentText.Equals("yyMMdd"))
                //{
                //    example_text.Text = "f";
                //}
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void SetNewName_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void AddText_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveText_Selected(object sender, RoutedEventArgs e)
        {

        }


        private void renameSchema_dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /* string selected_Rename_Schema = renameSchema_dropdown.SelectionBoxItem.ToString();   //rafsan
             MessageBox.Show(selected_Rename_Schema);*/
        }
    }
}

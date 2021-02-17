using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.File
{
    [Serializable]
    public class RenameFiles : RobotActionBase, ITask
    {
        public SelectedFileInfo FilesToRename { get; set; }
        public string RenameScheme { get; set; }
        public string NewFileName { get; set; }
        public string NewFileName_or_Existing_Sequentioal { get; set; }
        public string NewFileNameSequential { get; set; }
        public bool KeepExtension { get; set; }
        public bool IfFileExists { get; set; }
        public string New_Extension { get; set; }
        public string After_Or_Before_Date { get; set; }
        public string Ater_Or_Before_AddText { get; set; }
        public string After_or_before_sequential { get; set; }
        public string Text_to_Add { get; set; }
        public string Text_to_Remove { get; set; }
        public string Text_to_Replace { get; set; }
        public string Text_to_ReplaceWith { get; set; }
        public string Custom_Date { get; set; }
        public string Separator_datetime { get; set; }
        public string Separator_sequential { get; set; }
        public string Date_Time_Format { get; set; }
        public string Date_time_to_Add { get; set; }
        public string Add_number_to { get; set; }
        public int Start_numbering_at { get; set; }
        public int Increment_by { get; set; }
        public int Lenght_of_digit { get; set; }
        public string Var_StoreRenamedFilesInto { get; set; }
        public int Execute()
        {
            try
            {
                string destinationFile="";

                //if (System.IO.File.Exists(FilesToRename.FilePath))
                //{
                    if (RenameScheme == "Set New Name")
                    {

                        if (KeepExtension == true)
                        {
                            destinationFile = FilesToRename.FilePath + "\\" + NewFileName + FilesToRename.FileExtention;
                            System.IO.File.Move(FilesToRename.FileInfo, destinationFile);

                        }
                        else
                        {
                            destinationFile = FilesToRename.FilePath + "\\" + NewFileName;
                            System.IO.File.Move(FilesToRename.FileInfo, destinationFile);
                        }

                    }
                    else if (RenameScheme == "Add Text")
                    {

                        if (Ater_Or_Before_AddText == "after name")
                        {
                            destinationFile = FilesToRename.FilePath + "\\" + FilesToRename.FileNameWithoutExtension + Text_to_Add + FilesToRename.FileExtention;
                            System.IO.File.Move(FilesToRename.FileInfo, destinationFile);
                        }
                        else if (Ater_Or_Before_AddText == "before name")
                        {
                            destinationFile = FilesToRename.FilePath + "\\" + Text_to_Add + FilesToRename.FileNameWithoutExtension + FilesToRename.FileExtention;
                            System.IO.File.Move(FilesToRename.FileInfo, destinationFile);
                        }

                    }
                    else if (RenameScheme == "Remove Text")
                    {
                        FilesToRename.FileNameWithoutExtension = FilesToRename.FileNameWithoutExtension.Replace(Text_to_Remove, "");

                        destinationFile = FilesToRename.FilePath + "\\" + FilesToRename.FileNameWithoutExtension + FilesToRename.FileExtention;
                        System.IO.File.Move(FilesToRename.FileInfo, destinationFile);
                    }
                    else if (RenameScheme == "Replace Text")
                    {
                        FilesToRename.FileNameWithoutExtension = FilesToRename.FileNameWithoutExtension.Replace(Text_to_Replace, Text_to_ReplaceWith);

                        destinationFile = FilesToRename.FilePath + "\\" + FilesToRename.FileNameWithoutExtension + FilesToRename.FileExtention;
                        System.IO.File.Move(FilesToRename.FileInfo, destinationFile);
                    }
                    else if (RenameScheme == "Change Extension")
                    {

                        destinationFile = FilesToRename.FilePath + "\\" + FilesToRename.FileNameWithoutExtension + "." + New_Extension;
                        System.IO.File.Move(FilesToRename.FileInfo, destinationFile);
                    }
                    else if (RenameScheme == "Add Date or Time")
                    {

                        // Time information
                        DateTime last_access_time = System.IO.File.GetLastAccessTime(FilesToRename.FileInfo);
                        DateTime last_modified_time = System.IO.File.GetLastWriteTime(FilesToRename.FileInfo);
                        DateTime creation_time = System.IO.File.GetCreationTime(FilesToRename.FileInfo);
                        DateTime current_date = DateTime.Now;



                        string final_formated_date = "";

                        if (Date_time_to_Add == "Current DateTime")
                        {
                            string str_current_date_time = current_date.ToString(Date_Time_Format, CultureInfo.InvariantCulture);
                            final_formated_date = str_current_date_time;
                        }
                        else if (Date_time_to_Add == "Creation DateTime")
                        {
                            string str_creation_time = creation_time.ToString(Date_Time_Format, CultureInfo.InvariantCulture);
                            final_formated_date = str_creation_time;
                        }
                        else if (Date_time_to_Add == "Last Accessed DateTime")
                        {
                            string str_last_access_time = last_access_time.ToString(Date_Time_Format, CultureInfo.InvariantCulture);
                            final_formated_date = str_last_access_time;
                        }
                        else if (Date_time_to_Add == "Last Modified DateTime")
                        {
                            string str_last_modified_time = last_modified_time.ToString(Date_Time_Format, CultureInfo.InvariantCulture);
                            final_formated_date = str_last_modified_time;
                        }
                        else if (Date_time_to_Add == "Custom DateTime")
                        {
                            string str_last_modified_time = last_modified_time.ToString(Custom_Date, CultureInfo.InvariantCulture);
                            final_formated_date = str_last_modified_time;
                        }



                        if (After_Or_Before_Date == "after name")
                        {
                            destinationFile = FilesToRename.FilePath + "\\" + FilesToRename.FileNameWithoutExtension + Separator_(Separator_datetime) + final_formated_date + FilesToRename.FileExtention;
                            System.IO.File.Move(FilesToRename.FileInfo, destinationFile);
                        }
                        else if (After_Or_Before_Date == "before name")
                        {
                            destinationFile = FilesToRename.FilePath + "\\" + final_formated_date + Separator_(Separator_datetime) + FilesToRename.FileNameWithoutExtension + FilesToRename.FileExtention;
                            System.IO.File.Move(FilesToRename.FileInfo, destinationFile);
                        }
                    }
                    else if (RenameScheme == "Make Sequential")
                    {
                        string digit_string_filled_with_0 = string.Concat(Enumerable.Repeat("0", Lenght_of_digit));
                        string digit_string = digit_string_filled_with_0.Remove(digit_string_filled_with_0.Length - Start_numbering_at.ToString().Length); ;

                        digit_string = digit_string + Start_numbering_at.ToString();


                        if (NewFileName_or_Existing_Sequentioal == "existing name")
                        {
                            if (After_or_before_sequential == "after name")
                            {
                                destinationFile = FilesToRename.FilePath + "\\" + FilesToRename.FileNameWithoutExtension + Separator_(Separator_sequential) + digit_string + FilesToRename.FileExtention;
                                System.IO.File.Move(FilesToRename.FileInfo, destinationFile);
                            }
                            else if (After_or_before_sequential == "before name")
                            {
                                destinationFile = FilesToRename.FilePath + "\\" + digit_string + Separator_(Separator_sequential) + FilesToRename.FileNameWithoutExtension + FilesToRename.FileExtention;
                                System.IO.File.Move(FilesToRename.FileInfo, destinationFile);
                            }
                        }
                        else if (NewFileName_or_Existing_Sequentioal == "new name")
                        {
                            if (After_or_before_sequential == "after name")
                            {
                                destinationFile = FilesToRename.FilePath + "\\" + NewFileNameSequential + Separator_(Separator_sequential) + digit_string + FilesToRename.FileExtention;
                                System.IO.File.Move(FilesToRename.FileInfo, destinationFile);
                            }
                            else if (After_or_before_sequential == "before name")
                            {
                                destinationFile = FilesToRename.FilePath + "\\" + digit_string + Separator_(Separator_sequential) + NewFileNameSequential + FilesToRename.FileExtention;
                                System.IO.File.Move(FilesToRename.FileInfo, destinationFile);
                            }
                        }
                    }

               // }
             
               


                return 1;
        }
            catch (Exception ex)
            {
                return 0;
            }
    }

        public string Separator_(string separator)
        {
            if (separator== "space")
            {
                return " ";
            }
            else if(separator == "dash")
            {
                return "-";
            }
            else if (separator == "period")
            {
                return ".";
            }
            else if (separator == "underscore")
            {
                return "_";
            }
            return "";
        }

    }

}

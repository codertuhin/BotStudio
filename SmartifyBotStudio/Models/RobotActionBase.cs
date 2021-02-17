using DevComponents.WPF.Controls;
using MaterialDesignThemes.Wpf;
using SmartifyBotStudio.RobotDesigner.Enums;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.TaskModel.File;
using SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation;
using SmartifyBotStudio.RobotDesigner.TaskModel.DataBase;
using SmartifyBotStudio.RobotDesigner.TaskModel.Excel;
using SmartifyBotStudio.RobotDesigner.TaskModel.Email;
using SmartifyBotStudio.RobotDesigner.TaskModel.Text;
using SmartifyBotStudio.RobotDesigner.TaskModel.DateTimeActions;
using SmartifyBotStudio.RobotDesigner.TaskModel.Mouse_and_Keyboard;
using SmartifyBotStudio.RobotDesigner.TaskModel.VariablesActions;
using SmartifyBotStudio.RobotDesigner.TaskModel.MessageBoxes;
using SmartifyBotStudio.RobotDesigner.TaskModel.Conditional;
using SmartifyBotStudio.RobotDesigner.TaskModel.Loops;
using SmartifyBotStudio.RobotDesigner.TaskModel.Flow_Control;
using SmartifyBotStudio.RobotDesigner.Variable;
using SmartifyBotStudio.TypeEditor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SmartifyBotStudio.RobotDesigner.TaskModel.Delay;

namespace SmartifyBotStudio.Models
{

    [Serializable]
    public class RobotActionBase
    {

        #region CREATE ACTION TASK

        public static ObservableCollection<RobotActionBase> CreateFileTasks()
        {
            ObservableCollection<RobotActionBase> list = new ObservableCollection<RobotActionBase>()
            {

                 new CopyFiles(){ ActionType = RobotActionsEnum.CopyFile, Name = "Copy File",   ID = "CopyFile", Description = "File Copy Description", Finished = false, Icon = PackIconKind.ContentCopy, IsExisting = false },
                 new DeleteFiles(){ ActionType = RobotActionsEnum.DeleteFile, Name = "Delete Files",   ID = "DeleteFiles", Description = "File Delete Description", Finished = false, Icon = PackIconKind.Delete, IsExisting = false },
                 new GetFilePathPart(){ ActionType = RobotActionsEnum.GetFilePathPart, Name = "Get File Path Part",   ID = "GetFilePathPart", Description = "Get File Path Part Description", Finished = false, Icon = PackIconKind.FileTree, IsExisting = false },
                 new GetFilesinFolder(){ ActionType = RobotActionsEnum.GetFilesinFolder, Name = "Get Files in Folder",   ID = "GetFilesInFolder", Description = "Get Files in Folder Description", Finished = false, Icon = PackIconKind.FileXml, IsExisting = false },
                 new MoveFiles(){ ActionType = RobotActionsEnum.MoveFiles, Name = "Move Files",   ID = "MoveFiles", Description = "Move Files Description", Finished = false, Icon = PackIconKind.FolderMove, IsExisting = false },
                 new ReadFromCSVFile(){ ActionType = RobotActionsEnum.ReadFromCSVFile, Name = "Read From CSV File",   ID = "ReadFromCSVFile", Description = "Read From CSV File Description", Finished = false, Icon = PackIconKind.TooltipText, IsExisting = false },
                 new ReadTextFromFile(){ ActionType = RobotActionsEnum.ReadTextFromFile, Name = "Read Text From File",   ID = "ReadTextFromFile", Description = "Read Text From File Description", Finished = false, Icon = PackIconKind.FileDocument, IsExisting = false },
                 new RenameFiles(){ ActionType = RobotActionsEnum.RenameFiles, Name = "Rename Files",   ID = "RenameFiles", Description = "Rename Files Description", Finished = false, Icon = PackIconKind.RenameBox, IsExisting = false },
                 new WriteTextToFile(){ ActionType = RobotActionsEnum.WriteTextToFile, Name = "Write Text To File",   ID = "WriteTextToFile", Description = "Write Text To File Description", Finished = false, Icon = PackIconKind.Pencil, IsExisting = false },
                 new WriteToCSVFile(){ ActionType = RobotActionsEnum.WriteToCSVFile, Name = "Write To CSV File",   ID = "WriteToCSVFile", Description = "Write To CSV File Description", Finished = false, Icon = PackIconKind.FileDelimited, IsExisting = false },

                  new ReadFromCSVFile(){ ActionType = RobotActionsEnum.ReadFromCSVFile, Name = "Read From CSV File",   ID = "ReadFromCSVFile", Description = "Read From CSV File", Finished = false, Icon = PackIconKind.FileDelimited, IsExisting = false },

                 new GetTemporaryFiles(){ActionType = RobotActionsEnum.GetTemporaryFile, Name = "Get Templorary Files", ID = "GetTemporaryFiles", Description = "Get Templorary Files", Finished = false, Icon = PackIconKind.FileMultiple, IsExisting = false},

                 new LaunchNewIE(){ ActionType = RobotActionsEnum.LaunchNewIE, Name = "Launch New IE",  ID="LaunchNewIE",  Description = "Launch New IE", Finished = false, Icon = PackIconKind.Web, IsExisting = false }


            };


            return list;
        }


        public static ObservableCollection<RobotActionBase> CreateWebTasks()
        {
            ObservableCollection<RobotActionBase> list = new ObservableCollection<RobotActionBase>()
            {


                 new LaunchNewIE(){ ActionType = RobotActionsEnum.LaunchNewIE, Name = "Launch New IE",  ID="LaunchNewIE",  Description = "Launch New IE", Finished = false, Icon = PackIconKind.Web, IsExisting = false },

                new CloseIE(){ ActionType = RobotActionsEnum.CloseIE, Name = "Close IE",  ID="CloseIE",  Description = "Close IE", Finished = false, Icon = PackIconKind.Web, IsExisting = false },

                new TakeScreenShotOfWebPage(){ ActionType = RobotActionsEnum.TakeScreenshotOfWebPage, Name = "Take Screenshot Of Web Page",  ID="TakeScreenshotOfWebPage",  Description = "Take Screenshot Of Web Page", Finished = false, Icon = PackIconKind.Web, IsExisting = false },

                 new ExtractPageDetails(){ ActionType = RobotActionsEnum.ExtractPageDetails, Name = "Extract Page Details",  ID="ExtractPageDetails",  Description = "Extract Page Details", Finished = false, Icon = PackIconKind.Web, IsExisting = false },

                 new GetObjectDetails(){ ActionType = RobotActionsEnum.GetObjectDetails, Name = "Get Object Details",  ID="ExtractPageDetails",  Description = "Get Object Details", Finished = false, Icon = PackIconKind.Web, IsExisting = false },

                 new HoverOnWebObject(){ ActionType = RobotActionsEnum.HoverOnWebObject, Name = "Hover On Web Object",  ID="ExtractPageDetails",  Description = "Hover On Web Object", Finished = false, Icon = PackIconKind.Web, IsExisting = false },

                 new PopulateTextFiedOnWeb(){ ActionType = RobotActionsEnum.PopulateTextFiedOnWeb, Name = "Populate Text Fied On Web",  ID="PopulateTextFiedOnWeb",  Description = "Populate Text Fied On Web", Finished = false, Icon = PackIconKind.Web, IsExisting = false },


                 new ClickOnWebPage(){ ActionType = RobotActionsEnum.ClickOnWebPage, Name = "Click on Web Page",  ID="ClickonWebPage",  Description = "Click on Web Page", Finished = false, Icon = PackIconKind.Web, IsExisting = false },

                 new SetDropDownList(){ ActionType = RobotActionsEnum.SetDropDownList, Name = "Set Drop Down List on Web Page",  ID="SetDropDownList",  Description = "Set Drop Down List on Web Page", Finished = false, Icon = PackIconKind.Web, IsExisting = false },

                  new SetRadioButton(){ ActionType = RobotActionsEnum.SetRadioButton, Name = "Set Radio Button on Web Page",  ID="SetRadioButton",  Description = "Set Radio Button on Web Page", Finished = false, Icon = PackIconKind.Web, IsExisting = false },

                  new SetCheckBox(){ ActionType = RobotActionsEnum.SetCheckBox, Name = "Set Check Box on Web Page",  ID="SetCheckBox",  Description = "Set Check Box on Web Page", Finished = false, Icon = PackIconKind.Web, IsExisting = false },

                  new ExtractDataFromWeb(){ ActionType = RobotActionsEnum.ExtractDataFromWeb, Name = "Extract Data From Web Page",  ID="ExtractDataFromWeb",  Description = "Extract Data From Web Page", Finished = false, Icon = PackIconKind.Web, IsExisting = false }




            };


            return list;
        }



        public static ObservableCollection<RobotActionBase> CreateExcelTasks()
        {
            ObservableCollection<RobotActionBase> list = new ObservableCollection<RobotActionBase>()
            {


                 new LaunchExcel(){ ActionType = RobotActionsEnum.LaunchExcel, Name = "Launch Excel",  ID="LaunchExcel",  Description = "Launch Excel", Finished = false, Icon = PackIconKind.FileExcel, IsExisting = false },
                 new CloseExcel(){ ActionType = RobotActionsEnum.CloseExcel, Name = "Close Excel",  ID="CloseExcel",  Description = "Close Excel", Finished = false, Icon = PackIconKind.FileExcel, IsExisting = false },
                 new WriteToExcel(){ ActionType = RobotActionsEnum.WriteToExcel, Name = "Write to  Excel",  ID="WtirteToExcel",  Description = "Write to  Excel", Finished = false, Icon = PackIconKind.FileExcel, IsExisting = false },
                 new AddNewWorkSheet(){ ActionType = RobotActionsEnum.AddNewWorkSheet, Name = "Add New Worksheet to  Excel",  ID="AddNewWorkSheet",  Description = "Add New Worksheet to  Excel", Finished = false, Icon = PackIconKind.FileExcel, IsExisting = false },
                 new SetActiveWorkSheet(){ ActionType = RobotActionsEnum.SetActiveWorkSheet, Name = "Set Active WorkSheet to  Excel",  ID="SetActiveWorkSheet",  Description = "Set Active Worksheet to  Excel", Finished = false, Icon = PackIconKind.FileExcel, IsExisting = false },
                 new ReadFromExcel(){ ActionType = RobotActionsEnum.ReadFromExcel, Name = "Read From Excel Worksheet",  ID="ReadFromExcel",  Description = "Read From Excel Worksheet", Finished = false, Icon = PackIconKind.FileExcel, IsExisting = false }




            };


            return list;
        }

        public static ObservableCollection<RobotActionBase> CreateVariableTasks()
        {
            ObservableCollection<RobotActionBase> list = new ObservableCollection<RobotActionBase>()
            {


                 new SetVariable(){ ActionType = RobotActionsEnum.SetVariable, Name = "Set Variable",  ID="SetVariable",  Description = "Set Variable", Finished = false, Icon = PackIconKind.File, IsExisting = false },

                 new IncreaseVariable(){ ActionType = RobotActionsEnum.IncreaseVariable, Name = "Increase Variable",  ID="IncreaseVariable",  Description = "Increase Variable", Finished = false, Icon = PackIconKind.File, IsExisting = false },

                 new DecreaseVariable(){ ActionType = RobotActionsEnum.DecreaseVariable, Name = "DecreaseVariable",  ID="DecreaseVariable",  Description = "Decrease Variable", Finished = false, Icon = PackIconKind.File, IsExisting = false },

                 new TruncateNumber(){ ActionType = RobotActionsEnum.TruncateNumber, Name = "Truncate Number",  ID="TruncateNumber",  Description = "Truncate Number", Finished = false, Icon = PackIconKind.File, IsExisting = false },

                 new CreateNewList(){ ActionType = RobotActionsEnum.CreateNewList, Name = "Create New List",  ID="CreateNewList",  Description = "Create New List", Finished = false, Icon = PackIconKind.File, IsExisting = false },

                  new DeleteList(){ ActionType = RobotActionsEnum.DeleteList, Name = "Delete List",  ID="DeleteList",  Description = "Delete List", Finished = false, Icon = PackIconKind.File, IsExisting = false },

                  new GetListLength(){ ActionType = RobotActionsEnum.GetListLength, Name = "Get List Length",  ID="GetListLength",  Description = "Get List Length", Finished = false, Icon = PackIconKind.File, IsExisting = false },

                 new ClearList(){ ActionType = RobotActionsEnum.ClearList, Name = "Clear List",  ID="ClearList",  Description = "Clear List", Finished = false, Icon = PackIconKind.File, IsExisting = false },

                 new  AddTtemToList(){ ActionType = RobotActionsEnum.AddTtemToList, Name = "Add Ttem To List",  ID="AddTtemToList",  Description = "AddTtem To List", Finished = false, Icon = PackIconKind.File, IsExisting = false },

                 new  RemoveItemFromList(){ ActionType = RobotActionsEnum.RemoveItemFromList, Name = "Remove Item From List",  ID="RemoveItemFromList",  Description = "Remove Item From List", Finished = false, Icon = PackIconKind.File, IsExisting = false },

                  new  ReverseList(){ ActionType = RobotActionsEnum.ReverseList, Name = "Reverse List",  ID="ReverseList",  Description = "ReverseList", Finished = false, Icon = PackIconKind.File, IsExisting = false },

                   new  RemoveDuplicateItemFromList(){ ActionType = RobotActionsEnum.RemoveDuplicateItemFromList, Name = "Remove Duplicate Item From List",  ID="ReverseList",  Description = "Remove Duplicate Item From List", Finished = false, Icon = PackIconKind.File, IsExisting = false },

                    new  SuffleList(){ ActionType = RobotActionsEnum.SuffleList, Name = "Suffle List",  ID="SuffleList",  Description = "Suffle List", Finished = false, Icon = PackIconKind.File, IsExisting = false },

                    new  MergeLists(){ ActionType = RobotActionsEnum.MergeList, Name = "Merge List",  ID="Merge List",  Description = "MergeList", Finished = false, Icon = PackIconKind.File, IsExisting = false }




            };


            return list;
        }

        public static ObservableCollection<RobotActionBase> CreateDataBaseTasks()
        {
            ObservableCollection<RobotActionBase> list = new ObservableCollection<RobotActionBase>()
            {



                 new OpenSQLConnection(){ ActionType = RobotActionsEnum.OpenSQLConnection, Name = "Open SQL Connection",  ID="OpenSQLConnection",  Description = "Open SQL Connection", Finished = false, Icon = PackIconKind.Database, IsExisting = false },
                 new CloseSQLConnection(){ ActionType = RobotActionsEnum.CloseSQLConnection, Name = "Close SQL Connection",  ID="CloseSQLConnection",  Description = "Close SQL Connection", Finished = false, Icon = PackIconKind.Database, IsExisting = false },
                 new ExecuteSQLStatement(){ ActionType = RobotActionsEnum.ExecuteSQLStatement, Name = "Execute SQL Connection",  ID="ExecuteSQLStatement",  Description = "Execute SQL Connection", Finished = false, Icon = PackIconKind.Database, IsExisting = false }


            };


            return list;
        }

        public static ObservableCollection<RobotActionBase> CreateEmailTasks()
        {
            ObservableCollection<RobotActionBase> list = new ObservableCollection<RobotActionBase>()
            {

                 new SendMessages(){ ActionType = RobotActionsEnum.SendMessages, Name = "Send Messages",  ID="SendMessages",  Description = "Send Messages", Finished = false, Icon = PackIconKind.Email, IsExisting = false },
                 new GetMessages(){ ActionType = RobotActionsEnum.GetMessages, Name = "Get Messages",  ID="GetMessages",  Description = "Get Messages", Finished = false, Icon = PackIconKind.Email, IsExisting = false },
                 new ProcessMessage(){ ActionType = RobotActionsEnum.ProcessMessage, Name = "Process Message",  ID="ProcessMessage",  Description = "Process Message", Finished = false, Icon = PackIconKind.Email, IsExisting = false }


            };


            return list;
        }


        public static ObservableCollection<RobotActionBase> CreateMouseAndKeyboardTasks()
        {
            ObservableCollection<RobotActionBase> list = new ObservableCollection<RobotActionBase>()
            {

                 new Block_Input(){ ActionType = RobotActionsEnum.BlockInput, Name = "Block Input",  ID="BlockInput",  Description = "Block Input", Finished = false, Icon = PackIconKind.BlockHelper, IsExisting = false },

                 new SetKeyState(){ ActionType = RobotActionsEnum.SetKeysState, Name = "Set Keys State",  ID="SetKeysState",  Description = "Set Keys State", Finished = false, Icon = PackIconKind.Keyboard, IsExisting = false },

                  new PressReleaseKey(){ ActionType = RobotActionsEnum.PressReleaseKey, Name = "Press Release Key(s)",  ID="PressReleaseKey",  Description = "Press Release Key(s)", Finished = false, Icon = PackIconKind.Keyboard, IsExisting = false },

                  new MouseScroll(){ ActionType = RobotActionsEnum.MouseScroll, Name = "Mouse Scroll",  ID="MouseScroll",  Description = "Mouse Scroll", Finished = false, Icon = PackIconKind.Mouse, IsExisting = false },

                   new MoveMouse(){ ActionType = RobotActionsEnum.MoveMouse, Name = "Move Mouse",  ID="MoveMouse",  Description = "Move Mouse", Finished = false, Icon = PackIconKind.Mouse, IsExisting = false },

                    new MouseClick(){ ActionType = RobotActionsEnum.MouseClick, Name = "Send Mouse Event",  ID="SendMouseEvent",  Description = "Send Mouse Event", Finished = false, Icon = PackIconKind.Mouse, IsExisting = false }


            };


            return list;
        }


        public static ObservableCollection<RobotActionBase> CreateDelayTasks()
        {
            ObservableCollection<RobotActionBase> list = new ObservableCollection<RobotActionBase>()
            {

                 new DelayByTiming(){ ActionType = RobotActionsEnum.DelayByTiming, Name = "Delay By Timing",  ID="DelayByTiming",  Description = "Delay By Timing", Finished = false, Icon = PackIconKind.TimerOff, IsExisting = false }




            };


            return list;
        }


        public static ObservableCollection<RobotActionBase> CreateConditionalTasks()
        {
            ObservableCollection<RobotActionBase> list = new ObservableCollection<RobotActionBase>()
            {

                 new If(){ ActionType = RobotActionsEnum.If, Name = "If",  ID="If",  Description = "If", Finished = false, Icon = PackIconKind.Alarm, IsExisting = false },

                 new ElseIf(){ ActionType = RobotActionsEnum.ElseIf, Name = "Else If",  ID="ElseIf",  Description = "Else If", Finished = false, Icon = PackIconKind.Alarm, IsExisting = false },

                 new Else(){ ActionType = RobotActionsEnum.Else, Name = "Else",  ID="Else",  Description = "Else", Finished = false, Icon = PackIconKind.Alarm, IsExisting = false },

                 new EndIf(){ ActionType = RobotActionsEnum.EndIf, Name = "EndIf",  ID="EndIf",  Description = "EndIf", Finished = false, Icon = PackIconKind.Alarm, IsExisting = false },

                 new IfFileExists(){ ActionType = RobotActionsEnum.IfFileExists, Name = "If File Exists",  ID="IfFileExists",  Description = "If File Exists", Finished = false, Icon = PackIconKind.Alarm, IsExisting = false },

                 new IfFolderExists(){ ActionType = RobotActionsEnum.IfFolderExists, Name = "If Folder Exists",  ID="IfFolderExists",  Description = "If Folder Exists", Finished = false, Icon = PackIconKind.Alarm, IsExisting = false },

                 new IfProcessExists(){ ActionType = RobotActionsEnum.IfProcessExists, Name = "If Process",  ID="IfProcessExists",  Description = "If Process", Finished = false, Icon = PackIconKind.Alarm, IsExisting = false },

                 new IfService(){ ActionType = RobotActionsEnum.IfService, Name = "If Service",  ID="IfService",  Description = "If Service", Finished = false, Icon = PackIconKind.Alarm, IsExisting = false },

                 new IfImageExists(){ ActionType = RobotActionsEnum.IfImageExists, Name = "If Image",  ID="IfImageExists",  Description = "If Image", Finished = false, Icon = PackIconKind.Alarm, IsExisting = false }




            };


            return list;
        }

        public static ObservableCollection<RobotActionBase> CreateLoopTasks()
        {
            ObservableCollection<RobotActionBase> list = new ObservableCollection<RobotActionBase>()
            {

                 new Loop(){ ActionType = RobotActionsEnum.Loop, Name = "Loop",  ID="Loop",  Description = "Loop", Finished = false, Icon = PackIconKind.AlertCircle, IsExisting = false } ,

                 new LoopCondition(){ ActionType = RobotActionsEnum.LoopCondition, Name = "Loop Condition",  ID="LoopCondition",  Description = "Loop Condition", Finished = false, Icon = PackIconKind.AlertCircle, IsExisting = false },

                new EndLoop(){ ActionType = RobotActionsEnum.EndLoop, Name = "End Loop",  ID="EndLoop",  Description = "End Loop", Finished = false, Icon = PackIconKind.AlertCircle, IsExisting = false },

                 new ExitLoop(){ ActionType = RobotActionsEnum.ExitLoop, Name = "Exit Loop",  ID="ExitLoop",  Description = "Exit Loop", Finished = false, Icon = PackIconKind.AlertCircle, IsExisting = false },

                 new ForEach(){ ActionType = RobotActionsEnum.ForEach, Name = "For Each",  ID="ForEach",  Description = "For Each", Finished = false, Icon = PackIconKind.AlertCircle, IsExisting = false },

                 new LoopContinue(){ ActionType = RobotActionsEnum.LoopContinue, Name = "Loop Continue",  ID="LoopContinue",  Description = "Loop Continue", Finished = false, Icon = PackIconKind.AlertCircle, IsExisting = false },

                  new LoopConditionEnd(){ ActionType = RobotActionsEnum.LoopConditionEnd, Name = "Loop Condition End",  ID="LoopConditionEnd",  Description = "Loop Condition End", Finished = false, Icon = PackIconKind.AlertCircle, IsExisting = false }







            };


            return list;
        }


        public static ObservableCollection<RobotActionBase> CreateMessageBoxTasks()
        {
            ObservableCollection<RobotActionBase> list = new ObservableCollection<RobotActionBase>()
            {

                 new DisplayMessage(){ ActionType = RobotActionsEnum.DisplayMessage, Name = "Display Message",  ID="DisplayMessage",  Description = "Display Message", Finished = false, Icon = PackIconKind.MessageText, IsExisting = false }


            };


            return list;
        }


        public static ObservableCollection<RobotActionBase> CreateFlowTasks()
        {
            ObservableCollection<RobotActionBase> list = new ObservableCollection<RobotActionBase>()
            {

                 new Label_(){ ActionType = RobotActionsEnum.Label_, Name = "Label",  ID="Label",  Description = "Label", Finished = false, Icon = PackIconKind.Close, IsExisting = false },

                 new GoTo(){ ActionType = RobotActionsEnum.GoTo, Name = "Go To",  ID="GoTo",  Description = "Go To", Finished = false, Icon = PackIconKind.Close, IsExisting = false }



            };


            return list;
        }



        public static ObservableCollection<RobotActionBase> CreateDateTimeTasks()
        {
            ObservableCollection<RobotActionBase> list = new ObservableCollection<RobotActionBase>()
            {

                  new GetCurrentDateTime(){ ActionType = RobotActionsEnum.GetCurrentDateTime, Name = "Get Current Date and Time",  ID="GetCurrentDateTime",  Description = "Get Current Date and Time", Finished = false, Icon = PackIconKind.Timer, IsExisting = false },

                  new AddToDateTime(){ ActionType = RobotActionsEnum.AddToDateTime, Name = "Add To DateTime",  ID="AddToDateTime",  Description = "Add To DateTime", Finished = false, Icon = PackIconKind.Timer, IsExisting = false },

                  new SubtractDates(){ ActionType = RobotActionsEnum.SubtractDates, Name = "Subtract Dates",  ID="SubtractDates",  Description = "Subtract Dates", Finished = false, Icon = PackIconKind.Timer, IsExisting = false }


            };


            return list;
        }


        public static ObservableCollection<RobotActionBase> CreateTextTasks()
        {
            ObservableCollection<RobotActionBase> list = new ObservableCollection<RobotActionBase>()
            {


                new ChangeTextFormat(){ ActionType = RobotActionsEnum.ChangeTextFormat, Name = "Change Text Format",  ID="ChangeTextFormat",  Description = "Change Text Format", Finished = false, Icon = PackIconKind.FormatText, IsExisting = false },

                new CompareText(){ ActionType = RobotActionsEnum.CompareText, Name = "Compare Text",  ID="CompareText",  Description = "Compare Text", Finished = false, Icon = PackIconKind.FormatText, IsExisting = false },

                new FindText(){ ActionType = RobotActionsEnum.FindText, Name = "Find Text",  ID="FindText",  Description = "Find Text", Finished = false, Icon = PackIconKind.FormatText, IsExisting = false },

                new GetSubText(){ ActionType = RobotActionsEnum.GetSubText, Name = "Get SubText",  ID="GetSubText",  Description = "Get SubText", Finished = false, Icon = PackIconKind.FormatText, IsExisting = false },

                new GetTextLength(){ ActionType = RobotActionsEnum.GetTextLength, Name = "Get Text Length",  ID="GetTextLength",  Description = "Get Text Length", Finished = false, Icon = PackIconKind.FormatText, IsExisting = false },

                new JoinText(){ ActionType = RobotActionsEnum.JoinText, Name = "Join Text",  ID="JoinText",  Description = "Join Text", Finished = false, Icon = PackIconKind.FormatText, IsExisting = false },

                new ReplaceText(){ ActionType = RobotActionsEnum.ReplaceText, Name = "Replace Text",  ID="ReplaceText",  Description = "Replace Text", Finished = false, Icon = PackIconKind.FormatText, IsExisting = false },

                new ReverseText(){ ActionType = RobotActionsEnum.ReverseText, Name = "Reverse Text",  ID="ReverseText",  Description = "Reverse Text", Finished = false, Icon = PackIconKind.FormatText, IsExisting = false },

                new SplitText(){ ActionType = RobotActionsEnum.SplitText, Name = "Split Text",  ID="SplitText",  Description = "Split Text", Finished = false, Icon = PackIconKind.FormatText, IsExisting = false },

                new TrimText(){ ActionType = RobotActionsEnum.TrimText, Name = "Trim Text",  ID="TrimText",  Description = "Trim Text", Finished = false, Icon = PackIconKind.FormatText, IsExisting = false },

                new ChangeTextCase(){ ActionType = RobotActionsEnum.ChangeTextCase, Name = "Change Text Case",  ID="ChangeTextCase",  Description = "Change Text Case", Finished = false, Icon = PackIconKind.FormatText, IsExisting = false },

                new ChangeTextToNumber(){ ActionType = RobotActionsEnum.ChangeTextToNumber, Name = "Change Text To Number",  ID="ChangeTextToNumber",  Description = "Change Text To Number", Finished = false, Icon = PackIconKind.FormatText, IsExisting = false },

                new ConverNumberToText(){ ActionType = RobotActionsEnum.ConverNumberToText, Name = "Conver Number To Text",  ID="ConverNumberToText",  Description = "Conver Number To Text", Finished = false, Icon = PackIconKind.FormatText, IsExisting = false },

                new ConvertTextToDateTime(){ ActionType = RobotActionsEnum.ConvertTextToDateTime, Name = "Convert Text To DateTime",  ID="ConvertTextToDateTime",  Description = "Convert Text To DateTime", Finished = false, Icon = PackIconKind.FormatText, IsExisting = false },

                new ConvertDateTimeToText(){ ActionType = RobotActionsEnum.ConvertDateTimeToText, Name = "Convert DateTime To Text",  ID="ConvertDateTimeToText",  Description = "Convert DateTime To Text", Finished = false, Icon = PackIconKind.FormatText, IsExisting = false }





            };


            return list;
        }


        #endregion



        #region ACTION PROPERTIES


        #region BASE PROPERTIES


        string name;
        string description;
        bool finished;


        public RobotActionBase() { }

        public RobotActionBase(PackIconKind icon, string id, string name, string description, bool finished, bool isExisting)
        {
            this.Icon = icon;
            this.ID = id;
            this.name = name;
            this.description = description;
            this.finished = finished;
            this.IsExisting = isExisting;
        }

        [Browsable(false)]
        public bool Finished
        {
            get { return this.finished; }
            set { this.finished = value; }
        }

        [Browsable(false)]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        //[EditorAttribute(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        //[Editor(typeof(FileSelectorTypeEditor), typeof(UITypeEditor))]
        [Browsable(false)]
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        [ReadOnly(true)]
        [Browsable(false)]
        public PackIconKind Icon { get; set; }

        [ReadOnly(true)]
        public string ID { get; set; }
        [Browsable(false)]
        public bool IsExisting { get; set; }

        [Browsable(false)]
        public int Index { get; set; }
        /// <summary>
        /// 
        [Browsable(false)]
        public RobotActionsEnum ActionType { get; set; }                                  /// Create object for serialization

        //[Category("Boolean Editor")]

        [DisplayName("Is Active")]
        [Description("When inactive the action will not be executed.")]
        [BooleanEditorDescriptor(Type = BooleanEditorDescriptor.EditorType.DropDown, EnableInCellEditing = true, AutoComplete = AutoCompleteOptions.ReadOnly)]
        public bool IsActive { get; set; }



        [Browsable(false)]
        public bool IsChild { get; set; }


        [Browsable(false)]
        public string LabelName { get; set; }

        [Browsable(false)]
        public string LabelToGo { get; set; }


        [NonSerialized]
        public List<VariableBase> Variables = new List<VariableBase>();
        //public List<VariableBase> Variables
        //{
        //    get
        //    {
        //        return _Variables;
        //    }
        //    set
        //    {
        //        _Variables = value;
        //    }
        //}

        [NonSerialized]
        RobotDesignerProjectModel _RobotDesignerProjectModel;
        public RobotDesignerProjectModel RobotDesignerProject
        {
            get { return _RobotDesignerProjectModel; }
            set { _RobotDesignerProjectModel = value; }
        }

        #endregion


        #region CONDITION PROPERTIES

        public bool ConditionResult { get; set; }

        #endregion

        #region LOOP PROPERTIES


        [Browsable(false)]
        public int LoopStart { get; set; }

        [Browsable(false)]
        public int LoopEnd { get; set; }

        [Browsable(false)]
        public int IncreaseBy { get; set; }

        #endregion



        #region VARIABLE COLLECTION



        public static TestVar testVar = new TestVar();

        public static Dictionary<string, Tuple<string, string>> testVariable = new Dictionary<string, Tuple<string, string>>();



        #endregion



        #endregion


    }
}

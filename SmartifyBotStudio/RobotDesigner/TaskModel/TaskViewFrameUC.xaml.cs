using MahApps.Metro.Controls;
using SmartifyBotStudio.RobotDesigner.Enums;
using SmartifyBotStudio.RobotDesigner.TaskView.File;
using SmartifyBotStudio.RobotDesigner.TaskView.Web_Automation;
using SmartifyBotStudio.RobotDesigner.TaskView.DataBase;
using SmartifyBotStudio.RobotDesigner.TaskView.Excel;
using SmartifyBotStudio.RobotDesigner.TaskView.Email;
using SmartifyBotStudio.RobotDesigner.TaskView.Text;
using SmartifyBotStudio.RobotDesigner.TaskView.DateTimeActions;
using SmartifyBotStudio.RobotDesigner.TaskView.MessageBoxes;
using SmartifyBotStudio.RobotDesigner.TaskView.Conditional;
using SmartifyBotStudio.RobotDesigner.TaskView.Flow_Control;
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
using SmartifyBotStudio.RobotDesigner.TaskView.Mouse_and_Keyboard;
using SmartifyBotStudio.RobotDesigner.TaskView.VariablesActions;
using SmartifyBotStudio.RobotDesigner.TaskView.Delay;
using SmartifyBotStudio.RobotDesigner.TaskView.Loops;

namespace SmartifyBotStudio.RobotDesigner.TaskModel
{
    /// <summary>
    /// Interaction logic for TaskViewFrameUC.xaml
    /// </summary>
    public partial class TaskViewFrameUC : UserControl
    {

        public Object TaskData { get; set; }
        public RobotDesigner.Enums.RobotActionsEnum TaskType { get; set; }

        public TaskViewFrameUC()
        {
            InitializeComponent();

            Loaded += TaskViewFrameUC_Loaded;
        }

        private void TaskViewFrameUC_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTaskUI(TaskType);

        }

        void LoadTaskUI(RobotActionsEnum taskType)
        {
            panelMain.Children.Clear();
            var window = this.Parent as MetroWindow;

            switch (taskType)
            {
                case RobotActionsEnum.CopyFile:

                    panelMain.Children.Add(new CopyFiles() { CopyFilesData = TaskData != null ? TaskData as RobotDesigner.TaskModel.File.CopyFiles : null });
                    break;
                case RobotActionsEnum.DeleteFile:
                    panelMain.Children.Add(new DeleteFiles() { DeleteFileData = TaskData != null ? TaskData as RobotDesigner.TaskModel.File.DeleteFiles : null });
                    break;

                case RobotActionsEnum.MoveFiles:
                    panelMain.Children.Add(new MoveFiles() { MoveFilesData = TaskData != null ? TaskData as RobotDesigner.TaskModel.File.MoveFiles : null });
                    break;
                case RobotActionsEnum.GetFilePathPart:

                    panelMain.Children.Add(new GetFilePathPart());
                    break;
                case RobotActionsEnum.GetFilesinFolder:
                    panelMain.Children.Add(new GetFilesinFolder());
                    break;

                case RobotActionsEnum.GetTemporaryFile:
                    panelMain.Children.Add(new GetTemporaryFile());
                    break;
                case RobotActionsEnum.ReadFromCSVFile:
                    panelMain.Children.Add(new ReadFromCSVFile() { ReadFromCSVFileData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.File.ReadFromCSVFile : null });
                    break;

                case RobotActionsEnum.ReadTextFromFile:
                    panelMain.Children.Add(new ReadTextfromFile());
                    break;


                case RobotActionsEnum.RenameFiles:
                    panelMain.Children.Add(new RenameFiles() { RenameFileData = TaskData != null ? TaskData as RobotDesigner.TaskModel.File.RenameFiles : null });
                    break;

                case RobotActionsEnum.WriteTextToFile:
                    panelMain.Children.Add(new WriteTexttoFile() { TextFiletoWrite = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.File.WriteTextToFile : null });
                    break;
                case RobotActionsEnum.WriteToCSVFile:
                    panelMain.Children.Add(new WriteToCSVFile() { WriteToCSVFileData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.File.WriteToCSVFile : null });
                    break;
               

                case RobotActionsEnum.LaunchNewIE:
                    panelMain.Children.Add(new LaunchNewIE() { LaunchNewIEData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation.LaunchNewIE : null });
                    break;
                case RobotActionsEnum.CloseIE:
                    panelMain.Children.Add(new CloseIE() { CloseIEData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation.CloseIE : null });
                    break;
                case RobotActionsEnum.TakeScreenshotOfWebPage:
                    panelMain.Children.Add(new TakeScreenShotOfWebPage() { TakeScreenShotOfWebPageData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation.TakeScreenShotOfWebPage : null });
                    break;
                case RobotActionsEnum.ExtractPageDetails:
                    panelMain.Children.Add(new ExtractPageDetails() { ExtractPageDetailsData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation.ExtractPageDetails : null });
                    break;
                case RobotActionsEnum.GetObjectDetails:
                    panelMain.Children.Add(new GetObjectDetails() { GetObjectDetailsData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation.GetObjectDetails : null });
                    break;
                case RobotActionsEnum.HoverOnWebObject:
                    panelMain.Children.Add(new HoverOnWebObject() { HoverOnWebObjectData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation.HoverOnWebObject : null });
                    break;
                case RobotActionsEnum.PopulateTextFiedOnWeb:
                    panelMain.Children.Add(new PopulateTextFiedOnWeb() { PopulateTextData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation.PopulateTextFiedOnWeb : null });
                    break;
                case RobotActionsEnum.ClickOnWebPage:
                    panelMain.Children.Add(new ClickOnWebPage() { ClickOnWebPageTextData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation.ClickOnWebPage : null });
                    break;
                case RobotActionsEnum.SetDropDownList:
                    panelMain.Children.Add(new SetDropDownList() { SetDropDownListData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation.SetDropDownList : null });
                    break;
                case RobotActionsEnum.SetRadioButton:
                    panelMain.Children.Add(new SetRadioButton() { SetRadioButtonData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation.SetRadioButton : null });
                    break;
                case RobotActionsEnum.SetCheckBox:
                    panelMain.Children.Add(new SetCheckBox() { SetCheckBoxData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation.SetCheckBox : null });
                    break;

                case RobotActionsEnum.ExtractDataFromWeb:
                    panelMain.Children.Add(new ExtractDataFromWeb() { ExtractDataFromWebTextData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation.ExtractDataFromWeb : null });
                    break;

                case RobotActionsEnum.LaunchExcel:
                    panelMain.Children.Add(new LaunchExcel() { LaunchExcelData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Excel.LaunchExcel : null });
                    break;
                case RobotActionsEnum.WriteToExcel:
                    panelMain.Children.Add(new WriteToExcel() { WriteToExcelData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Excel.WriteToExcel : null });
                    break;
                case RobotActionsEnum.AddNewWorkSheet:
                    panelMain.Children.Add(new AddNewWorkSheet() { AddNewWorkSheetData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Excel.AddNewWorkSheet : null });
                    break;
                case RobotActionsEnum.SetActiveWorkSheet:
                    panelMain.Children.Add(new SetActiveWorkSheet() { SetActiveWorkSheetData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Excel.SetActiveWorkSheet : null });
                    break;
                case RobotActionsEnum.ReadFromExcel:
                    panelMain.Children.Add(new ReadFromExcel() { ReadFromExcelData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Excel.ReadFromExcel : null });
                    break;

                case RobotActionsEnum.CloseExcel:
                    panelMain.Children.Add(new CloseExcel() { CloseExcelData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Excel.CloseExcel : null });
                    break;
                case RobotActionsEnum.OpenSQLConnection:
                    panelMain.Children.Add(new OpenSQLConnection() { OpenSQLConnectionTaskData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.DataBase.OpenSQLConnection : null });
                    break;
                case RobotActionsEnum.CloseSQLConnection:
                    panelMain.Children.Add(new CloseSQLConnection() { CloseSQLConnectionTaskData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.DataBase.CloseSQLConnection : null });
                    break;
                case RobotActionsEnum.ExecuteSQLStatement:
                    panelMain.Children.Add(new ExecuteSQLStatement() { ExecuteSQLStatementData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.DataBase.ExecuteSQLStatement : null });
                    break;
                case RobotActionsEnum.SendMessages:
                    panelMain.Children.Add(new SendMessages() { SendMessagesData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Email.SendMessages : null });
                    break;
                case RobotActionsEnum.GetMessages:
                    panelMain.Children.Add(new GetMessages() { GetMessagesData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Email.GetMessages : null });
                    break;
                case RobotActionsEnum.ProcessMessage:
                    panelMain.Children.Add(new ProcessMessage() { ProcessMessageData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Email.ProcessMessage : null });
                    break;
                #region Text Actions
                case RobotActionsEnum.ChangeTextFormat:
                    panelMain.Children.Add(new ChangeTextFormat() { ChangeTextFormatData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Text.ChangeTextFormat : null });
                    break;
                case RobotActionsEnum.CompareText:
                    panelMain.Children.Add(new CompareText() { CompareTextData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Text.CompareText : null });
                    break;
                case RobotActionsEnum.FindText:
                    panelMain.Children.Add(new FindText() { FindTextData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Text.FindText : null });
                    break;
                case RobotActionsEnum.GetSubText:
                    panelMain.Children.Add(new GetSubText() { GetSubTextData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Text.GetSubText : null });
                    break;
                case RobotActionsEnum.GetTextLength:
                    panelMain.Children.Add(new GetTextLength() { GetTextLengthData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Text.GetTextLength : null });
                    break;
                case RobotActionsEnum.JoinText:
                    panelMain.Children.Add(new JoinText() { JoinTextData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Text.JoinText : null });
                    break;
                case RobotActionsEnum.ReplaceText:
                    panelMain.Children.Add(new ReplaceText() { ReplaceTextData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Text.ReplaceText : null });
                    break;
                case RobotActionsEnum.ReverseText:
                    panelMain.Children.Add(new ReverseText() { ReverseTextData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Text.ReverseText : null });
                    break;
                case RobotActionsEnum.SplitText:
                    panelMain.Children.Add(new SplitText() { SplitTextData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Text.SplitText : null });
                    break;
                case RobotActionsEnum.TrimText:
                    panelMain.Children.Add(new TrimText() { TrimTextData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Text.TrimText : null });
                    break;
                case RobotActionsEnum.ChangeTextCase:
                    panelMain.Children.Add(new ChangeTextCase() { ChangeTextCaseData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Text.ChangeTextCase : null });
                    break;
                case RobotActionsEnum.ChangeTextToNumber:
                    panelMain.Children.Add(new ChangeTextToNumber() { ChangeTextToNumberData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Text.ChangeTextToNumber : null });
                    break;
                case RobotActionsEnum.ConverNumberToText:
                    panelMain.Children.Add(new ConverNumberToText() { ConverNumberToTextData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Text.ConverNumberToText : null });
                    break;
                case RobotActionsEnum.ConvertDateTimeToText:
                    panelMain.Children.Add(new ConvertDateTimeToText() { ConvertDateTimeToTextData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Text.ConvertDateTimeToText : null });
                    break;
                case RobotActionsEnum.ConvertTextToDateTime:
                    panelMain.Children.Add(new ConvertTextToDateTime() { ConvertTextToDateTimeData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Text.ConvertTextToDateTime : null });
                    break;
                #endregion
                #region DateTimeAction
                case RobotActionsEnum.GetCurrentDateTime:
                    panelMain.Children.Add(new GetCurrentDateTime() { GetCurrentDateTimeData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.DateTimeActions.GetCurrentDateTime : null });
                    break;
                case RobotActionsEnum.AddToDateTime:
                    panelMain.Children.Add(new AddToDateTime() { AddToDateTimeData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.DateTimeActions.AddToDateTime : null });
                    break;
                case RobotActionsEnum.SubtractDates:
                    panelMain.Children.Add(new SubtractDates() { SubtractDatesData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.DateTimeActions.SubtractDates : null });
                    break;
                #endregion
                #region Mouse and Keyboard
                case RobotActionsEnum.SetKeysState:
                    panelMain.Children.Add(new SetKeyState() { SetKeyStateData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Mouse_and_Keyboard.SetKeyState : null });
                    break;
                case RobotActionsEnum.PressReleaseKey:
                    panelMain.Children.Add(new PressReleaseKey() { PressReleaseKeyData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Mouse_and_Keyboard.PressReleaseKey : null });
                    break;
                case RobotActionsEnum.MouseScroll:
                    panelMain.Children.Add(new MouseScroll() { MouseScrollData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Mouse_and_Keyboard.MouseScroll : null });
                    break;
                case RobotActionsEnum.BlockInput:
                    panelMain.Children.Add(new BlockInput() { BlockInputData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Mouse_and_Keyboard.Block_Input : null });
                    break;
                case RobotActionsEnum.MoveMouse:
                    panelMain.Children.Add(new MoveMouse() { MoveMouseData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Mouse_and_Keyboard.MoveMouse : null });
                    break;
                case RobotActionsEnum.MouseClick:
                    panelMain.Children.Add(new MouseClick() { MouseClickData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Mouse_and_Keyboard.MouseClick : null });
                    break;
                #endregion
                #region Variable Action
                case RobotActionsEnum.AddTtemToList:
                    panelMain.Children.Add(new AddTtemToList() { AddTtemToListData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.VariablesActions.AddTtemToList : null });
                    break;
                case RobotActionsEnum.ClearList:
                    panelMain.Children.Add(new ClearList() { ClearListData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.VariablesActions.ClearList : null });
                    break;
                case RobotActionsEnum.CreateNewList:
                    panelMain.Children.Add(new CreateNewList() { CreateNewListData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.VariablesActions.CreateNewList : null });
                    break;
                case RobotActionsEnum.DecreaseVariable:
                    panelMain.Children.Add(new DecreaseVariable() { DecreaseVariableData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.VariablesActions.DecreaseVariable : null });
                    break;
                case RobotActionsEnum.IncreaseVariable:
                    panelMain.Children.Add(new IncreaseVariable() { IncreaseVariableData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.VariablesActions.IncreaseVariable : null });
                    break;
                case RobotActionsEnum.RemoveItemFromList:
                    panelMain.Children.Add(new RemoveItemFromList() { RemoveItemFromListData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.VariablesActions.RemoveItemFromList : null });
                    break;
                case RobotActionsEnum.SetVariable:
                    panelMain.Children.Add(new SetVariable() { SetVariableData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.VariablesActions.SetVariable : null });
                    break;
                case RobotActionsEnum.TruncateNumber:
                    panelMain.Children.Add(new TruncateNumber() { TruncateNumberData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.VariablesActions.TruncateNumber : null });
                    break;
                case RobotActionsEnum.ReverseList:
                    panelMain.Children.Add(new ReverseList() { ReverseListData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.VariablesActions.ReverseList : null });
                    break;
                case RobotActionsEnum.RemoveDuplicateItemFromList:
                    panelMain.Children.Add(new RemoveDuplicateItemFromList() { RemoveDuplicateItemFromListData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.VariablesActions.RemoveDuplicateItemFromList : null });
                    break;
                case RobotActionsEnum.SuffleList:
                    panelMain.Children.Add(new SuffleList() { SuffleListData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.VariablesActions.SuffleList : null });
                    break;
                case RobotActionsEnum.MergeList:
                    panelMain.Children.Add(new MergeList() { MergeListsData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.VariablesActions.MergeLists : null });
                    break;
                case RobotActionsEnum.DeleteList:
                    panelMain.Children.Add(new DeleteList() { DeleteListData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.VariablesActions.DeleteList : null });
                    break;
                case RobotActionsEnum.GetListLength:
                    panelMain.Children.Add(new GetListLength() { GetListLengthData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.VariablesActions.GetListLength : null });
                    break;
                #endregion
                #region MessageBox Action
                case RobotActionsEnum.DisplayMessage:
                    panelMain.Children.Add(new DisplayMessage() { DisplayMessageData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.MessageBoxes.DisplayMessage : null });
                    break;
                #endregion
                #region Delay Action
                case RobotActionsEnum.DelayByTiming:
                    panelMain.Children.Add(new DelayByTiming() { DelayByTimingData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Delay.DelayByTiming : null });
                    break;
                #endregion
                #region Conditional
                case RobotActionsEnum.If:
                    panelMain.Children.Add(new If() { IfData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Conditional.If : null });
                    break;
                case RobotActionsEnum.EndIf:
                    panelMain.Children.Add(new EndIf() { EndIfData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Conditional.EndIf : null });
                    break;
                case RobotActionsEnum.IfFileExists:
                    panelMain.Children.Add(new IfFileExists() { IfFileExistsData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Conditional.IfFileExists : null });
                    break;
                case RobotActionsEnum.IfFolderExists:
                    panelMain.Children.Add(new IfFolderExists() { IfFolderExistsData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Conditional.IfFolderExists : null });
                    break;
                case RobotActionsEnum.IfProcessExists:
                    panelMain.Children.Add(new IfProcessExists() { IfProcessExistsData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Conditional.IfProcessExists : null });
                    break;
                case RobotActionsEnum.IfService:
                    panelMain.Children.Add(new IfService() { IfServiceData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Conditional.IfService : null });
                    break;
                case RobotActionsEnum.IfImageExists:
                    panelMain.Children.Add(new IfImageExists() { IfImageExistsData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Conditional.IfImageExists : null });
                    break;
                case RobotActionsEnum.ElseIf:
                    panelMain.Children.Add(new ElseIf() { ElseIfData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Conditional.ElseIf : null });
                    break;
                case RobotActionsEnum.Else:
                    panelMain.Children.Add(new Else() { ElseData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Conditional.Else : null });
                    break;
                #endregion
                #region Loops
                case RobotActionsEnum.Loop:
                    panelMain.Children.Add(new Loop() { LoopData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Loops.Loop : null });
                    break;
                case RobotActionsEnum.EndLoop:
                    panelMain.Children.Add(new EndLoop() { EndLoopData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Loops.EndLoop : null });
                    break;
                case RobotActionsEnum.ExitLoop:
                    panelMain.Children.Add(new ExitLoop() { ExitLoopData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Loops.ExitLoop : null });
                    break;
                case RobotActionsEnum.LoopCondition:
                    panelMain.Children.Add(new LoopCondition() { LoopConditionData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Loops.LoopCondition : null });
                    break;
                case RobotActionsEnum.ForEach:
                    panelMain.Children.Add(new ForEach() { ForEachData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Loops.ForEach : null });
                    break;
                case RobotActionsEnum.LoopContinue:
                    panelMain.Children.Add(new LoopContinue() { LoopContinueData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Loops.LoopContinue : null });
                    break;
                case RobotActionsEnum.LoopConditionEnd:
                    panelMain.Children.Add(new LoopConditionEnd() { LoopConditionEndData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Loops.LoopConditionEnd : null });
                    break;
                #endregion
                #region Flow Control
                case RobotActionsEnum.Label_:
                    panelMain.Children.Add(new Label_() { LabelData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Flow_Control.Label_ : null });
                    break;
                case RobotActionsEnum.GoTo:
                    panelMain.Children.Add(new GoTo() { GoToData = TaskData != null ? TaskData as SmartifyBotStudio.RobotDesigner.TaskModel.Flow_Control.GoTo : null });
                    break;
                    #endregion

            }

            window.Title = taskType.ToString();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            panelMain.Children.Clear();
            var window = this.Parent as MetroWindow;

            switch ((sender as MenuItem).Tag.ToString())
            {
                case "CopyFile":

                    LoadTaskUI(RobotActionsEnum.CopyFile);
                    break;
                case "DeleteFile":
                    LoadTaskUI(RobotActionsEnum.DeleteFile);
                    break;

                case "MoveFiles":
                    LoadTaskUI(RobotActionsEnum.MoveFiles);
                    break;
                case "GetFilePathPart":

                    LoadTaskUI(RobotActionsEnum.GetFilePathPart);
                    break;
                case "GetFilesInFolder":
                    LoadTaskUI(RobotActionsEnum.GetFilesinFolder);
                    break;

                case "GetTemporaryFiles":
                    LoadTaskUI(RobotActionsEnum.GetTemporaryFile);
                    break;
                case "ReadfromCSVFile":
                    LoadTaskUI(RobotActionsEnum.ReadFromCSVFile);
                    break;

                case "ReadTextfromFile":
                    LoadTaskUI(RobotActionsEnum.ReadTextFromFile);
                    break;


                case "RenameFiles":
                    LoadTaskUI(RobotActionsEnum.RenameFiles);
                    break;

                case "WriteTexttoFile":
                    LoadTaskUI(RobotActionsEnum.WriteTextToFile);
                    break;
                case "WritetoCSVFile":
                    LoadTaskUI(RobotActionsEnum.WriteToCSVFile);
                    break;
                case "LaunchNewIE":
                    LoadTaskUI(RobotActionsEnum.LaunchNewIE);
                    break;
                case "CloseIE":
                    LoadTaskUI(RobotActionsEnum.CloseIE);
                    break;
                case "TakeScreenshotOfWebPage":
                    LoadTaskUI(RobotActionsEnum.TakeScreenshotOfWebPage);
                    break;
                case "ExtractPageDetails":
                    LoadTaskUI(RobotActionsEnum.ExtractPageDetails);
                    break;
                case "GetObjectDetails":
                    LoadTaskUI(RobotActionsEnum.GetObjectDetails);
                    break;
                case "HoverOnWebObject":
                    LoadTaskUI(RobotActionsEnum.HoverOnWebObject);
                    break;
                case "PopulateTextFiedOnWeb":
                    LoadTaskUI(RobotActionsEnum.PopulateTextFiedOnWeb);
                    break;
                case "ClickOnWebPage":
                    LoadTaskUI(RobotActionsEnum.ClickOnWebPage);
                    break;
                case "SetDropDownList":
                    LoadTaskUI(RobotActionsEnum.SetDropDownList);
                    break;
                case "SetRadioButton":
                    LoadTaskUI(RobotActionsEnum.SetRadioButton);
                    break;
                case "SetCheckBox":
                    LoadTaskUI(RobotActionsEnum.SetCheckBox);
                    break;
                case "ExtractDataFromWeb":
                    LoadTaskUI(RobotActionsEnum.ExtractDataFromWeb);
                    break;
                case "LaunchExcel":
                    LoadTaskUI(RobotActionsEnum.LaunchExcel);
                    break;
                case "WriteToExcel":
                    LoadTaskUI(RobotActionsEnum.WriteToExcel);
                    break;
                case "AddNewWorkSheet":
                    LoadTaskUI(RobotActionsEnum.AddNewWorkSheet);
                    break;
                case "SetActiveWorkSheet":
                    LoadTaskUI(RobotActionsEnum.SetActiveWorkSheet);
                    break;
                case "ReadFromExcel":
                    LoadTaskUI(RobotActionsEnum.ReadFromExcel);
                    break;
                case "CloseExcel":
                    LoadTaskUI(RobotActionsEnum.CloseExcel);
                    break;
                case "OpenSQLConnection":
                    LoadTaskUI(RobotActionsEnum.OpenSQLConnection);
                    break;
                case "CloseSQLConnection":
                    LoadTaskUI(RobotActionsEnum.CloseSQLConnection);
                    break;
                case "ExecuteSQLStatement":
                    LoadTaskUI(RobotActionsEnum.ExecuteSQLStatement);
                    break;
                case "GetMessages":
                    LoadTaskUI(RobotActionsEnum.GetMessages);
                    break;
                case "SendMessages":
                    LoadTaskUI(RobotActionsEnum.SendMessages);
                    break;
                case "ProcessMessage":
                    LoadTaskUI(RobotActionsEnum.ProcessMessage);
                    break;
                #region Text Actoins
                case "ChangeTextFormat":
                    LoadTaskUI(RobotActionsEnum.ChangeTextFormat);
                    break;
                case "CompareText":
                    LoadTaskUI(RobotActionsEnum.CompareText);
                    break;
                case "FindText":
                    LoadTaskUI(RobotActionsEnum.FindText);
                    break;
                case "GetSubText":
                    LoadTaskUI(RobotActionsEnum.GetSubText);
                    break;
                case "GetTextLength":
                    LoadTaskUI(RobotActionsEnum.GetTextLength);
                    break;
                case "JoinText":
                    LoadTaskUI(RobotActionsEnum.JoinText);
                    break;
                case "ReplaceText":
                    LoadTaskUI(RobotActionsEnum.ReplaceText);
                    break;
                case "ReverseText":
                    LoadTaskUI(RobotActionsEnum.ReverseText);
                    break;
                case "SplitText":
                    LoadTaskUI(RobotActionsEnum.SplitText);
                    break;
                case "TrimText":
                    LoadTaskUI(RobotActionsEnum.TrimText);
                    break;
                case "ChangeTextCase":
                    LoadTaskUI(RobotActionsEnum.ChangeTextCase);
                    break;
                case "ChangeTextToNumber":
                    LoadTaskUI(RobotActionsEnum.ChangeTextToNumber);
                    break;
                case "ConverNumberToText":
                    LoadTaskUI(RobotActionsEnum.ConverNumberToText);
                    break;
                case "ConvertDateTimeToText":
                    LoadTaskUI(RobotActionsEnum.ConvertDateTimeToText);
                    break;
                case "ConvertTextToDateTime":
                    LoadTaskUI(RobotActionsEnum.ConvertTextToDateTime);
                    break;
                #endregion
                #region DateTimeAction
                case "GetCurrentDateTime":
                    LoadTaskUI(RobotActionsEnum.GetCurrentDateTime);
                    break;
                #endregion
                #region Mouse and Keyboard
                case "SetKeysState":
                    LoadTaskUI(RobotActionsEnum.SetKeysState);
                    break;
                case "PressReleaseKey":
                    LoadTaskUI(RobotActionsEnum.PressReleaseKey);
                    break;
                case "MouseScroll":
                    LoadTaskUI(RobotActionsEnum.MouseScroll);
                    break;
                case "BlockInput":
                    LoadTaskUI(RobotActionsEnum.BlockInput);
                    break;
                case "MoveMouse":
                    LoadTaskUI(RobotActionsEnum.MoveMouse);
                    break;
                case "MouseClick":
                    LoadTaskUI(RobotActionsEnum.MouseClick);
                    break;
                #endregion
                #region Variable Action
                case "AddTtemToList":
                    LoadTaskUI(RobotActionsEnum.AddTtemToList);
                    break;
                case "ClearList":
                    LoadTaskUI(RobotActionsEnum.ClearList);
                    break;
                case "CreateNewList":
                    LoadTaskUI(RobotActionsEnum.CreateNewList);
                    break;
                case "DecreaseVariable":
                    LoadTaskUI(RobotActionsEnum.DecreaseVariable);
                    break;
                case "IncreaseVariable":
                    LoadTaskUI(RobotActionsEnum.IncreaseVariable);
                    break;
                case "RemoveItemFromList":
                    LoadTaskUI(RobotActionsEnum.RemoveItemFromList);
                    break;
                case "SetVariable":
                    LoadTaskUI(RobotActionsEnum.SetVariable);
                    break;
                case "TruncateNumber":
                    LoadTaskUI(RobotActionsEnum.TruncateNumber);
                    break;
                case "ReverseList":
                    LoadTaskUI(RobotActionsEnum.ReverseList);
                    break;
                case "RemoveDuplicateItemFromList":
                    LoadTaskUI(RobotActionsEnum.RemoveDuplicateItemFromList);
                    break;
                case "SuffleList":
                    LoadTaskUI(RobotActionsEnum.SuffleList);
                    break;
                case "MergeList":
                    LoadTaskUI(RobotActionsEnum.MergeList);
                    break;
                case "DeleteList":
                    LoadTaskUI(RobotActionsEnum.DeleteList);
                    break;
                case "GetListLength":
                    LoadTaskUI(RobotActionsEnum.GetListLength);
                    break;
                #endregion
                #region MessageBox Action
                case "DisplayMessage":
                    LoadTaskUI(RobotActionsEnum.DisplayMessage);
                    break;
                #endregion
                #region Delay Action
                case "DelayByTiming":
                    LoadTaskUI(RobotActionsEnum.DelayByTiming);
                    break;
                #endregion
                #region Conditional
                case "If":
                    LoadTaskUI(RobotActionsEnum.If);
                    break;
                case "EndIf":
                    LoadTaskUI(RobotActionsEnum.EndIf);
                    break;
                case "IfFileExists":
                    LoadTaskUI(RobotActionsEnum.IfFileExists);
                    break;
                case "IfFolderExists":
                    LoadTaskUI(RobotActionsEnum.IfFolderExists);
                    break;
                case "IfProcessExists":
                    LoadTaskUI(RobotActionsEnum.IfProcessExists);
                    break;
                case "IfService":
                    LoadTaskUI(RobotActionsEnum.IfService);
                    break;
                case "IfImageExists":
                    LoadTaskUI(RobotActionsEnum.IfImageExists);
                    break;
                case "ElseIf":
                    LoadTaskUI(RobotActionsEnum.ElseIf);
                    break;
                case "Else":
                    LoadTaskUI(RobotActionsEnum.Else);
                    break;
                #endregion
                #region Loops
                case "Loop":
                    LoadTaskUI(RobotActionsEnum.Loop);
                    break;
                case "EndLoop":
                    LoadTaskUI(RobotActionsEnum.EndLoop);
                    break;
                case "ExitLoop":
                    LoadTaskUI(RobotActionsEnum.ExitLoop);
                    break;
                case "LoopContinue":
                    LoadTaskUI(RobotActionsEnum.LoopContinue);
                    break;
                case "LoopCondition":
                    LoadTaskUI(RobotActionsEnum.LoopCondition);
                    break;
                case "LoopConditionEnd":
                    LoadTaskUI(RobotActionsEnum.LoopConditionEnd);
                    break;
                #endregion
                #region Flow Control
                case "Label_":
                    LoadTaskUI(RobotActionsEnum.Label_);
                    break;
                case "GoTo":
                    LoadTaskUI(RobotActionsEnum.GoTo);
                    break;
                    #endregion
            }

            window.Title = (sender as MenuItem).Tag.ToString();
        }
    }
}

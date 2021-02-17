using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.Enums
{
    public enum RobotActionsEnum
    {
        // File  Actions
        CopyFile,
        DeleteFile,
        GetFilePathPart,
        GetFilesinFolder,
        MoveFiles,
        ReadFromCSVFile,
        ReadTextFromFile,
        RenameFiles,
        WriteTextToFile,
        WriteToCSVFile,
        GetTemporaryFile,

        // Web Actions
        LaunchNewIE,
        CloseIE,
        PopulateTextFiedOnWeb,
        ClickOnWebPage,
        SetDropDownList,
        SetRadioButton,
        SetCheckBox,
        ExtractDataFromWeb,
        ExtractPageDetails,
        TakeScreenshotOfWebPage,
        GetObjectDetails,
        HoverOnWebObject,

        // Excel Actions
        LaunchExcel,
        CloseExcel,
        WriteToExcel,
        AddNewWorkSheet,
        SetActiveWorkSheet,
        ReadFromExcel,

        // DataBae Actions
        OpenSQLConnection,
        CloseSQLConnection,
        ExecuteSQLStatement,

        // Email Actions
        GetMessages,
        SendMessages,
        ProcessMessage,

        // Text Actions
        ChangeTextFormat,
        CompareText,
        FindText,
        GetSubText,
        GetTextLength,
        JoinText,
        ReplaceText,
        ReverseText,
        SplitText,
        TrimText,
        ChangeTextCase,
        ChangeTextToNumber,
        ConverNumberToText,
        ConvertDateTimeToText,
        ConvertTextToDateTime,

        // DateTime Actions
        GetCurrentDateTime,
        AddToDateTime,
        SubtractDates,


        // Mouse and Keyboard Action
        SetKeysState,
        PressReleaseKey,
        MouseScroll,
        MoveMouse,
        MouseClick,
        BlockInput,

        // Variable Action
        AddTtemToList,
        ClearList,
        CreateNewList,
        DecreaseVariable,
        IncreaseVariable,
        RemoveItemFromList,
        SetVariable,
        TruncateNumber,
        ReverseList,
        RemoveDuplicateItemFromList,
        SuffleList,
        MergeList,
        DeleteList,
        GetListLength,

        //MessageBox Action
        DisplayMessage,


        //Delay
        DelayByTiming,

        //Conditionals
        If,
        IfFileExists,
        IfImageExists,
        IfFolderExists,
        IfProcessExists,
        IfService,
        IfWindowExists,
        IfWindowContains,
        IfWebPageContains,
        Else,
        ElseIf,
        EndIf,


        //Loops
        Loop,
        LoopCondition,
        LoopConditionEnd,
        ForEach,
        ExitLoop,
        EndLoop,
        LoopContinue,

        //Flow Control
        Label_,
        GoTo







    }
}

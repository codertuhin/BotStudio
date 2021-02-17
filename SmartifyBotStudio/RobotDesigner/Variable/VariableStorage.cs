using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MExcel = Microsoft.Office.Interop.Excel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.IE;
using SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation;

using MimeKit;
using MailKit.Net.Smtp;

using MailKit.Search;
using MailKit.Net.Imap;
using MailKit.Security;
using MailKit;
using SmartifyBotStudio.RobotDesigner.TaskView.VariableView;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Forms;

namespace SmartifyBotStudio.RobotDesigner.Variable
{

    public class VariableStorage
    {


        #region FILE

        public static Dictionary<string, Tuple<string, DataTable>> CSVVar = new Dictionary<string, Tuple<string, DataTable>>();

        #endregion
        #region EXCEL

        public static Dictionary<string, Tuple<string, Microsoft.Office.Interop.Excel.Application>> ExcelVar = new Dictionary<string, Tuple<string, Microsoft.Office.Interop.Excel.Application>>();

        public static Dictionary<string, Tuple<string, DataTable>> ExcelDataVar = new Dictionary<string, Tuple<string, DataTable>>();

        #endregion
        #region EMAIL

        public static Dictionary<string, Tuple<string, ImapClient, string, object>> EmailVar = new Dictionary<string, Tuple<string, ImapClient, string, object>>();

        public static Dictionary<string, Tuple<string, int, string, string>> IMAPCredentialVar = new Dictionary<string, Tuple<string, int, string, string>>();


        #endregion
        #region WEB

        public static Dictionary<string, Tuple<string, object>> WebDriverVar = new Dictionary<string, Tuple<string, object>>();

        public static Dictionary<string, Tuple<string, string>> WebDetailsVar = new Dictionary<string, Tuple<string, string>>();

        public static Dictionary<string, Tuple<string, string>> WebElementDetailsVar = new Dictionary<string, Tuple<string, string>>();



        #endregion
        #region DATABASE

        public static Dictionary<string, Tuple<string, OleDbConnection>> DataBaseConnStrVar = new Dictionary<string, Tuple<string, OleDbConnection>>();

        public static Dictionary<string, Tuple<string, DataSet>> DataBaseQueryResultlVar = new Dictionary<string, Tuple<string, DataSet>>();


        #endregion
        #region TEXT
        public static Dictionary<string, Tuple<string, string>> SubTextVar = new Dictionary<string, Tuple<string, string>>();

        public static Dictionary<string, Tuple<string, string>> TrimmedTextVar = new Dictionary<string, Tuple<string, string>>();

        public static Dictionary<string, Tuple<string, string>> ReversedTextVar = new Dictionary<string, Tuple<string, string>>();

        public static Dictionary<string, Tuple<string, int>> TextLengthVar = new Dictionary<string, Tuple<string, int>>();

        public static Dictionary<string, Tuple<string, string[]>> SplitTextVar = new Dictionary<string, Tuple<string, string[]>>();

        public static Dictionary<string, Tuple<string, string>> ReplaceTextVar = new Dictionary<string, Tuple<string, string>>();

        public static Dictionary<string, Tuple<string, string>> CaseConvetedTextVar = new Dictionary<string, Tuple<string, string>>();

        public static Dictionary<string, Tuple<string, int>> TextToNumberVar = new Dictionary<string, Tuple<string, int>>();

        public static Dictionary<string, Tuple<string, string>> NumberToTextVar = new Dictionary<string, Tuple<string, string>>();

        public static Dictionary<string, Tuple<string, DateTime>> TextToDateTimeVar = new Dictionary<string, Tuple<string, DateTime>>();

        public static Dictionary<string, Tuple<string, List<int>>> FindTextVar = new Dictionary<string, Tuple<string, List<int>>>();

        public static Dictionary<string, Tuple<string, string>> DateToTextVar = new Dictionary<string, Tuple<string, string>>();
        #endregion
        #region MOUSE AND KEYBOARD

        #endregion
        #region DATA TIME ACTION

        public static Dictionary<string, Tuple<string, DateTime>> GetCurrentDateTimeVar = new Dictionary<string, Tuple<string, DateTime>>();

        public static Dictionary<string, Tuple<string, DateTime>> AddToDateTimeVar = new Dictionary<string, Tuple<string, DateTime>>();

        #endregion
        #region VARIABLE ACTION

        public static Dictionary<string, Tuple<string, object, Type>> SetVariableVar = new Dictionary<string, Tuple<string, object, Type>>();
        public static Dictionary<string, Tuple<string, List<object>, Type>> CreateNewListVar = new Dictionary<string, Tuple<string, List<object>, Type>>();
        public static Dictionary<string, Tuple<string, double>> TruncateNuberVar = new Dictionary<string, Tuple<string, double>>();

        #endregion
        #region FLOW CONTROL
        public static Dictionary<string, Tuple<string, string>> LabelVar = new Dictionary<string, Tuple<string, string>>();

        #endregion

        #region Other Variable

        //public static List<VariableCollectionModel> VariableCollection = new List<VariableCollectionModel>();

        public static Dictionary<string, string> Url_tracker = new Dictionary<string, string>();

        public static string InitialVariableName_forVariableAction = "NewVar";

        public static int VariableCounter_forVariableAction = 0;

        public static char CommonDelimiter = '%';

        #endregion
        #region DUMMY DATA
        public static string DUMMYString = "";
        public static string DUMMYInteger = "";
        #endregion


        public static List<VariableCollectionModel> AllVariableCollection = GetAllVariables();

        /// <summary>
        /// 
        /// </summary>
        /// <returns> a list of all variables </returns>
        public static List<VariableCollectionModel> GetAllVariables()
        {
            // Collection of all variables
            List<VariableCollectionModel> VariableCollection = new List<VariableCollectionModel>();


            #region FILE

            #endregion
            #region EXCEL

            foreach (string str in ExcelVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = ExcelVar[str].Item1, ObjectValue = ExcelVar[str].Item2, Type = ExcelVar[str].Item2.GetType(), TypeString = ExcelVar[str].Item2.ToString() });
            }

            foreach (string str in ExcelDataVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = ExcelDataVar[str].Item1, ObjectValue = ExcelDataVar[str].Item2, Type = ExcelDataVar[str].Item2.GetType(), TypeString = ExcelDataVar[str].Item2.ToString() });
            }

            #endregion
            #region EMAIL

            foreach (string str in EmailVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = EmailVar[str].Item1, ObjectValue = EmailVar[str].Item2, Type = EmailVar[str].Item2.GetType(), TypeString = EmailVar[str].Item2.ToString(), AdditionalInfo_1 = EmailVar[str].Item3, AdditionalInfo_2 = EmailVar[str].Item4 });
            }

            foreach (string str in IMAPCredentialVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = IMAPCredentialVar[str].Item1, ObjectValue = IMAPCredentialVar[str].Item2, Type = IMAPCredentialVar[str].Item2.GetType(), TypeString = IMAPCredentialVar[str].Item2.ToString(), AdditionalInfo_1 = IMAPCredentialVar[str].Item3, AdditionalInfo_2 = IMAPCredentialVar[str].Item4 });
            }

            #endregion
            #region WEB

            //foreach (string str in WebDriverVar.Keys)
            //{
            //    VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = WebDriverVar[str].Item1, ObjectValue = WebDriverVar[str].Item2, Type = WebDriverVar[str].Item2.GetType(), TypeString = WebDriverVar[str].Item2.ToString() });
            //}

            foreach (string str in WebDetailsVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = WebDetailsVar[str].Item1, ObjectValue = WebDetailsVar[str].Item2, Type = WebDetailsVar[str].Item2.GetType(), TypeString = WebDetailsVar[str].Item2.ToString() });
            }

            foreach (string str in WebElementDetailsVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = WebElementDetailsVar[str].Item1, ObjectValue = WebElementDetailsVar[str].Item2, Type = WebElementDetailsVar[str].Item2.GetType(), TypeString = WebElementDetailsVar[str].Item2.ToString() });
            }

            #endregion
            #region DATABASE

            foreach (string str in DataBaseConnStrVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = DataBaseConnStrVar[str].Item1, ObjectValue = DataBaseConnStrVar[str].Item2, Type = DataBaseConnStrVar[str].Item2.GetType(), TypeString = DataBaseConnStrVar[str].Item2.ToString() });
            }


            foreach (string str in DataBaseQueryResultlVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = DataBaseQueryResultlVar[str].Item1, ObjectValue = DataBaseQueryResultlVar[str].Item2, Type = DataBaseQueryResultlVar[str].Item2.GetType(), TypeString = DataBaseQueryResultlVar[str].Item2.ToString() });
            }

            #endregion
            #region TEXT

            foreach (string str in SplitTextVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = SplitTextVar[str].Item1, ObjectValue = SplitTextVar[str].Item2, Type = SplitTextVar[str].Item2.GetType(), TypeString = SplitTextVar[str].Item2.ToString() });
            }


            foreach (string str in SubTextVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = SubTextVar[str].Item1, ObjectValue = SubTextVar[str].Item2, Type = SubTextVar[str].Item2.GetType(), TypeString = SubTextVar[str].Item2.ToString() });
            }


            foreach (string str in TrimmedTextVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = TrimmedTextVar[str].Item1, ObjectValue = TrimmedTextVar[str].Item2, Type = TrimmedTextVar[str].Item2.GetType(), TypeString = TrimmedTextVar[str].Item2.ToString() });
            }

            foreach (string str in ReversedTextVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = ReversedTextVar[str].Item1, ObjectValue = ReversedTextVar[str].Item2, Type = ReversedTextVar[str].Item2.GetType(), TypeString = ReversedTextVar[str].Item2.ToString() });
            }

            foreach (string str in TextLengthVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = TextLengthVar[str].Item1, ObjectValue = TextLengthVar[str].Item2, Type = TextLengthVar[str].Item2.GetType(), TypeString = TextLengthVar[str].Item2.ToString() });
            }

            foreach (string str in ReplaceTextVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = ReplaceTextVar[str].Item1, ObjectValue = ReplaceTextVar[str].Item2, Type = ReplaceTextVar[str].Item2.GetType(), TypeString = ReplaceTextVar[str].Item2.ToString() });
            }

            foreach (string str in CaseConvetedTextVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = CaseConvetedTextVar[str].Item1, ObjectValue = CaseConvetedTextVar[str].Item2, Type = CaseConvetedTextVar[str].Item2.GetType(), TypeString = CaseConvetedTextVar[str].Item2.ToString() });
            }

            foreach (string str in TextToNumberVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = TextToNumberVar[str].Item1, ObjectValue = TextToNumberVar[str].Item2, Type = TextToNumberVar[str].Item2.GetType(), TypeString = TextToNumberVar[str].Item2.ToString() });
            }

            foreach (string str in NumberToTextVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = NumberToTextVar[str].Item1, ObjectValue = NumberToTextVar[str].Item2, Type = NumberToTextVar[str].Item2.GetType(), TypeString = NumberToTextVar[str].Item2.ToString() });
            }

            foreach (string str in TextToDateTimeVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = TextToDateTimeVar[str].Item1, ObjectValue = TextToDateTimeVar[str].Item2, Type = TextToDateTimeVar[str].Item2.GetType(), TypeString = TextToDateTimeVar[str].Item2.ToString() });
            }

            foreach (string str in FindTextVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = FindTextVar[str].Item1, ObjectValue = FindTextVar[str].Item2, Type = FindTextVar[str].Item2.GetType(), TypeString = FindTextVar[str].Item2.ToString() });
            }

            foreach (string str in DateToTextVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = DateToTextVar[str].Item1, ObjectValue = DateToTextVar[str].Item2, Type = DateToTextVar[str].Item2.GetType(), TypeString = DateToTextVar[str].Item2.ToString() });
            }


            #endregion
            #region MOUSE AND KEYBOARD

            #endregion
            #region DATE TIME ACTION

            foreach (string str in GetCurrentDateTimeVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = GetCurrentDateTimeVar[str].Item1, ObjectValue = GetCurrentDateTimeVar[str].Item2, Type = GetCurrentDateTimeVar[str].Item2.GetType(), TypeString = GetCurrentDateTimeVar[str].Item2.ToString() });
            }

            foreach (string str in AddToDateTimeVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = AddToDateTimeVar[str].Item1, ObjectValue = AddToDateTimeVar[str].Item2, Type = AddToDateTimeVar[str].Item2.GetType(), TypeString = AddToDateTimeVar[str].Item2.ToString() });
            }

            #endregion
            #region VARIABLE ACTION
            foreach (string str in SetVariableVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = SetVariableVar[str].Item1, ObjectValue = SetVariableVar[str].Item2, Type = SetVariableVar[str].Item3, TypeString = SetVariableVar[str].Item2.ToString() });
            }

            foreach (string str in CreateNewListVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = CreateNewListVar[str].Item1, ObjectValue = CreateNewListVar[str].Item2, Type = CreateNewListVar[str].Item3, TypeString = CreateNewListVar[str].Item2.ToString() });
            }

            foreach (string str in TruncateNuberVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = TruncateNuberVar[str].Item1, ObjectValue = TruncateNuberVar[str].Item2, Type = TruncateNuberVar[str].Item2.GetType(), TypeString = TruncateNuberVar[str].Item2.ToString() });
            }
            #endregion
            #region FLOW CONTROL
            foreach (string str in LabelVar.Keys)
            {
                VariableCollection.Add(new VariableCollectionModel() { VariableName = str, ActionID = LabelVar[str].Item1, ObjectValue = LabelVar[str].Item2, Type = LabelVar[str].Item2.GetType(), TypeString = LabelVar[str].Item2.ToString() });
            }
            #endregion


            return VariableCollection;

        }



        /// <summary>
        /// Format user defined varibale name 
        /// </summary>
        /// <param name="varName">formated variable name</param>
        /// <returns></returns>
        public static string VariableNameFormator(string varName)
        {
            if (string.IsNullOrEmpty(varName))
            {
                return string.Empty;
            }

            if (varName[0] == CommonDelimiter && varName[varName.Length - 1] == CommonDelimiter)
            {
                return varName;
            }



            if (varName[0] != CommonDelimiter)
            {
                varName = CommonDelimiter + varName;
            }

            if (varName[varName.Length - 1] != CommonDelimiter)
            {
                varName = varName + CommonDelimiter;
            }


            return varName;

        }

        /// <summary>
        /// Helps to pick a variable
        /// </summary>
        /// <returns>returns a string that contains a variable name</returns>
        public static VariableCollectionModel Pick()
        {
            VariablePicker variablePicker = new VariablePicker();

            new MetroWindow()
            {
                //Owner=;
                Content = variablePicker,
                Height = 350,
                Width = 300,
                ShowMaxRestoreButton = false,
                ShowMinButton = false,
                ShowCloseButton = false,
                //Top = Cursor.Position.X,
                //Left = Cursor.Position.Y
            }
            .ShowDialog();

            if (variablePicker.selectedVariable != null)
            {
                return variablePicker.selectedVariable;

            }

            return null;
        }

        public static void InitializeWithNull()

        {
            #region FILE

            #endregion
            #region EXCEL

            ExcelVar = new Dictionary<string, Tuple<string, Microsoft.Office.Interop.Excel.Application>>(); ;

            ExcelDataVar = new Dictionary<string, Tuple<string, DataTable>>();

            #endregion
            #region EMAIL

            EmailVar = new Dictionary<string, Tuple<string, ImapClient, string, object>>();

            IMAPCredentialVar = new Dictionary<string, Tuple<string, int, string, string>>();


            #endregion
            #region WEB

            WebDriverVar = new Dictionary<string, Tuple<string, object>>();

            WebDetailsVar = new Dictionary<string, Tuple<string, string>>();

            WebElementDetailsVar = new Dictionary<string, Tuple<string, string>>();



            #endregion
            #region DATABASE

            DataBaseConnStrVar = new Dictionary<string, Tuple<string, OleDbConnection>>();

            DataBaseQueryResultlVar = new Dictionary<string, Tuple<string, DataSet>>();


            #endregion
            #region TEXT
            SubTextVar = new Dictionary<string, Tuple<string, string>>();

            TrimmedTextVar = new Dictionary<string, Tuple<string, string>>();

            ReversedTextVar = new Dictionary<string, Tuple<string, string>>();

            TextLengthVar = new Dictionary<string, Tuple<string, int>>();

            SplitTextVar = new Dictionary<string, Tuple<string, string[]>>();

            ReplaceTextVar = new Dictionary<string, Tuple<string, string>>();

            CaseConvetedTextVar = new Dictionary<string, Tuple<string, string>>();

            TextToNumberVar = new Dictionary<string, Tuple<string, int>>();

            NumberToTextVar = new Dictionary<string, Tuple<string, string>>();

            TextToDateTimeVar = new Dictionary<string, Tuple<string, DateTime>>();

            FindTextVar = new Dictionary<string, Tuple<string, List<int>>>();

            DateToTextVar = new Dictionary<string, Tuple<string, string>>();
            #endregion
            #region MOUSE AND KEYBOARD

            #endregion
            #region DATA TIME ACTION

            GetCurrentDateTimeVar = new Dictionary<string, Tuple<string, DateTime>>();

            AddToDateTimeVar = new Dictionary<string, Tuple<string, DateTime>>();

            #endregion
            #region VARIABLE ACTION

            SetVariableVar = new Dictionary<string, Tuple<string, object, Type>>();
            CreateNewListVar = new Dictionary<string, Tuple<string, List<object>, Type>>();
            TruncateNuberVar = new Dictionary<string, Tuple<string, double>>();

            #endregion

            #region Other Variable

            //public static List<VariableCollectionModel> VariableCollection = new List<VariableCollectionModel>();

            Url_tracker = new Dictionary<string, string>();

            InitialVariableName_forVariableAction = "NewVar";

            VariableCounter_forVariableAction = 0;

            CommonDelimiter = '%';

            #endregion
            #region DUMMY DATA
            DUMMYString = "";
            DUMMYInteger = "";
            #endregion
        }


    }



}

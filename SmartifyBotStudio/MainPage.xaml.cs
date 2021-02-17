using MahApps.Metro.Controls;
using Microsoft.Win32;
using SmartifyBotStudio.Helpers.DragDropListView;
using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.TaskModel;
using SmartifyBotStudio.RobotDesigner.TaskModel.File;
using SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation;
using SmartifyBotStudio.RobotDesigner.TaskModel.Excel;
using SmartifyBotStudio.RobotDesigner.TaskModel.Email;
using SmartifyBotStudio.RobotDesigner.TaskModel.Text;
using SmartifyBotStudio.RobotDesigner.TaskModel.MessageBoxes;
using SmartifyBotStudio.RobotDesigner.TaskModel.Delay;
using SmartifyBotStudio.RobotDesigner.TaskModel.Conditional;
using SmartifyBotStudio.RobotDesigner.TaskModel.Flow_Control;
using SmartifyBotStudio.RobotDesigner.TaskView;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Collections.Generic;
using SmartifyBotStudio.RobotDesigner.TaskModel.DataBase;
using SmartifyBotStudio.RobotDesigner.Variable;
using System.Data.OleDb;
using System.Data;
using SmartifyBotStudio.RobotDesigner.TaskView.VariableView;
using System.Windows.Media;

using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using SmartifyBotStudio.Serialization;
using System.Xml;
using System.Linq.Expressions;
using MailKit.Net.Imap;
using MailKit.Security;
using SmartifyBotStudio.RobotDesigner.TaskModel.DateTimeActions;
using SmartifyBotStudio.RobotDesigner.TaskModel.Mouse_and_Keyboard;
using SmartifyBotStudio.RobotDesigner.TaskModel.VariablesActions;
using SmartifyBotStudio.RobotDesigner.TaskModel.Loops;

namespace SmartifyBotStudio
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Serializable]
    public partial class MainPage : UserControl
    {
        RobotDesignerProjectModel projectModel;


        Dictionary<int, Tuple<int, List<int>, int>> condition_map = new Dictionary<int, Tuple<int, List<int>, int>>();

        Dictionary<int, Tuple<int, List<int>>> loop_map = new Dictionary<int, Tuple<int, List<int>>>();

        Dictionary<int, Tuple<int, List<int>>> loopCondition_map = new Dictionary<int, Tuple<int, List<int>>>();



        int index = 0;


        ListViewDragDropManager<RobotActionBase> dragMgr;
        ListViewDragDropManager<RobotActionBase> dragMgr2;
        ListViewDragDropManager<RobotActionBase> dragMgrWebActions;
        ListViewDragDropManager<RobotActionBase> dragMgrExcelActions;
        ListViewDragDropManager<RobotActionBase> dragMgrDataBaseActions;
        ListViewDragDropManager<RobotActionBase> dragMgrEmailActions;
        ListViewDragDropManager<RobotActionBase> dragMgrTextActions;
        ListViewDragDropManager<RobotActionBase> dragMgrDateTimeActions;
        ListViewDragDropManager<RobotActionBase> dragMgrMouseAndKeyboardActions;
        ListViewDragDropManager<RobotActionBase> dragMgrVariabledActions;
        ListViewDragDropManager<RobotActionBase> dragMgrMessageBoxActions;
        ListViewDragDropManager<RobotActionBase> dragMgrDelayActions;
        ListViewDragDropManager<RobotActionBase> dragMgrConditionalsActions;
        ListViewDragDropManager<RobotActionBase> dragMgrLoopActions;
        ListViewDragDropManager<RobotActionBase> dragMgrFlowActions;

        ObservableCollection<RobotActionBase> actionCollection;
        List<VariableCollectionModel> VariableCollectionLst;
        string XmlFileFullPath;

        public SerializationTest serializationTest = new SerializationTest(@"Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=SEH-HBW;Data Source=\sqlexpress");



        #region Constructor
        public MainPage()
        {
            InitializeComponent();

            this.Loaded += MainPage_Loaded;


            projectModel = new RobotDesignerProjectModel()
            {
                Actions = actionCollection,
                Description = "This is test p description",
                Owner = Environment.UserName,
                ProjectName = "Test Projet",


            };


        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {

            docManager.ChangeColorScheme(DevComponents.WpfDock.eDockVisualStyle.Office2010Black, System.Windows.Media.Color.FromRgb(0, 0, 0));




            actionCollection = new ObservableCollection<RobotActionBase>();
            actionCollection.CollectionChanged += ActionCollection_CollectionChanged;

            this.listView.ItemsSource = actionCollection;
            listView.SelectionChanged += (o, oo) =>
            {

                propertyGrid.SelectedObject = listView.SelectedItem as RobotActionBase;
            };

            this.lstFileActions.ItemsSource = RobotActionBase.CreateFileTasks();
            this.lstWebActions.ItemsSource = RobotActionBase.CreateWebTasks();
            this.lstExcelActions.ItemsSource = RobotActionBase.CreateExcelTasks();
            this.lstDataBaseActions.ItemsSource = RobotActionBase.CreateDataBaseTasks();
            this.lstEmailActions.ItemsSource = RobotActionBase.CreateEmailTasks();
            this.lstTextActions.ItemsSource = RobotActionBase.CreateTextTasks();
            this.lstDateTimeActions.ItemsSource = RobotActionBase.CreateDateTimeTasks();
            this.lstMouseAndKeyboardActions.ItemsSource = RobotActionBase.CreateMouseAndKeyboardTasks();
            this.lstVariableActions.ItemsSource = RobotActionBase.CreateVariableTasks();
            this.lstMessageBoxActions.ItemsSource = RobotActionBase.CreateMessageBoxTasks();
            this.lstDelayActions.ItemsSource = RobotActionBase.CreateDelayTasks();
            this.lstConditionalActions.ItemsSource = RobotActionBase.CreateConditionalTasks();
            this.lstLoopActions.ItemsSource = RobotActionBase.CreateLoopTasks();
            this.lstFlowActions.ItemsSource = RobotActionBase.CreateFlowTasks();

            //this.listView2.ItemsSource = new ObservableCollection<IRobotActionBase>()
            //{
            //     new FileCopy(){ Icon = MaterialDesignThemes.Wpf.PackIconKind.AccessPoint, Name = "A", Description =  "AAA", ID  = "AAA", Finished =  false, IsExisting =  false },
            //     new FileCopy(){ Icon = MaterialDesignThemes.Wpf.PackIconKind.XboxControllerOff, Name = "B", Description =  "BBB", ID  = "AAA", Finished =  false, IsExisting =  false },
            //     new FileCopy(){ Icon = MaterialDesignThemes.Wpf.PackIconKind.XingBox, Name = "C", Description =  "AAA", ID  = "BBB", Finished =  false, IsExisting =  false },
            //     new FileCopy(){ Icon = MaterialDesignThemes.Wpf.PackIconKind.Yelp, Name = "D", Description =  "AAA", ID  = "CCC", Finished =  false, IsExisting =  false },


            //};

            this.dragMgr = new ListViewDragDropManager<RobotActionBase>(this.listView);
            this.dragMgr2 = new ListViewDragDropManager<RobotActionBase>(this.lstFileActions);
            this.dragMgrWebActions = new ListViewDragDropManager<RobotActionBase>(this.lstWebActions);
            this.dragMgrExcelActions = new ListViewDragDropManager<RobotActionBase>(this.lstExcelActions);
            this.dragMgrDataBaseActions = new ListViewDragDropManager<RobotActionBase>(this.lstDataBaseActions);
            this.dragMgrEmailActions = new ListViewDragDropManager<RobotActionBase>(this.lstEmailActions);
            this.dragMgrTextActions = new ListViewDragDropManager<RobotActionBase>(this.lstTextActions);
            this.dragMgrDateTimeActions = new ListViewDragDropManager<RobotActionBase>(this.lstDateTimeActions);
            this.dragMgrMouseAndKeyboardActions = new ListViewDragDropManager<RobotActionBase>(this.lstMouseAndKeyboardActions);
            this.dragMgrVariabledActions = new ListViewDragDropManager<RobotActionBase>(this.lstVariableActions);
            this.dragMgrMessageBoxActions = new ListViewDragDropManager<RobotActionBase>(this.lstMessageBoxActions);
            this.dragMgrDelayActions = new ListViewDragDropManager<RobotActionBase>(this.lstDelayActions);
            this.dragMgrConditionalsActions = new ListViewDragDropManager<RobotActionBase>(this.lstConditionalActions);
            this.dragMgrLoopActions = new ListViewDragDropManager<RobotActionBase>(this.lstLoopActions);
            this.dragMgrFlowActions = new ListViewDragDropManager<RobotActionBase>(this.lstFlowActions);


            this.listView.DragEnter += OnListViewDragEnter;
            this.listView.Drop += OnListViewDrop;

            this.lstFileActions.DragEnter += OnListViewDragEnter;
            this.lstFileActions.Drop += OnListViewDrop;


            this.lstDataBaseActions.DragEnter += OnListViewDragEnter;
            this.lstDataBaseActions.Drop += OnListViewDrop;

            this.lstDateTimeActions.DragEnter += OnListViewDragEnter;
            this.lstDateTimeActions.Drop += OnListViewDrop;


        }

        private void ActionCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(this.listView.ItemsSource);
            view.Refresh();
        }

        #endregion

        #region OnListViewDragEnter

        // Handles the DragEnter event for both ListViews.
        void OnListViewDragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
        }

        #endregion // OnListViewDragEnter

        #region OnListViewDrop

        // Handles the Drop event for both ListViews.
        void OnListViewDrop(object sender, DragEventArgs e)
        {
            if (e.Effects == DragDropEffects.None)
                return;

            RobotActionBase task = e.Data.GetData(typeof(RobotActionBase)) as RobotActionBase;
            if (sender == this.listView)
            {
                if (this.dragMgr.IsDragInProgress)
                    return;

                // An item was dragged from the bottom ListView into the top ListView
                // so remove that item from the bottom ListView.
                //(this.listView2.ItemsSource as ObservableCollection<Task>).Remove(task);
            }
            else
            {
                if (this.dragMgr2.IsDragInProgress)
                    return;
                if (this.dragMgrWebActions.IsDragInProgress)
                    return;
                if (this.dragMgrExcelActions.IsDragInProgress)
                    return;

                // An item was dragged from the top ListView into the bottom ListView
                // so remove that item from the top ListView.
                (this.listView.ItemsSource as ObservableCollection<RobotActionBase>).Remove(task);
            }
        }

        #endregion // OnListViewDrop

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }




        private void listView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var s = listView.SelectedItem as RobotActionBase;
            //if (s != null)
            //    MessageBox.Show(s.Description);
            this.Cursor = Cursors.Wait;
            if (s != null)
            {
                var m = new MetroWindow()
                {
                    //copyFilesAction,
                    SizeToContent = SizeToContent.WidthAndHeight,
                    WindowStyle = WindowStyle.SingleBorderWindow,
                    WindowState = WindowState.Normal,
                    //Title = "Copy File",
                    ShowMaxRestoreButton = false,
                    ShowMinButton = false,
                    ResizeMode = ResizeMode.NoResize,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };


                m.Content = new TaskViewFrameUC()
                {
                    TaskType = s.ActionType,
                    TaskData = listView.SelectedItem
                };


                if (m.ShowDialog() == true)
                {
                    //if (copyFilesAction.CopyFilesData != null)
                    //{
                    //    ICollectionView view = CollectionViewSource.GetDefaultView(listView.ItemsSource);
                    //    view.Refresh();
                    //}
                }

            }


            //switch (s.actiontype)
            //{
            //    case robotdesigner.enums.robotactionsenum.copyfile:
            //        m.content = new taskviewframeuc()
            //        {
            //            tasktype = robotdesigner.enums.robotactionsenum.copyfile,
            //            taskdata = listview.selecteditem
            //        };

            //        break;
            //    case robotdesigner.enums.robotactionsenum.deletefile:
            //        m.content = new taskviewframeuc()
            //        {
            //            tasktype = robotdesigner.enums.robotactionsenum.deletefile,
            //            taskdata = listview.selecteditem
            //        };

            //        break;
            //}

            // if (m.ShowDialog() == true)
            {
                //if (copyFilesAction.CopyFilesData != null)
                //{
                //    ICollectionView view = CollectionViewSource.GetDefaultView(listView.ItemsSource);
                //    view.Refresh();
                //}
            }
            this.Cursor = Cursors.Arrow;
        }

        private void mnuSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog opnFileDialog = new SaveFileDialog()
            {
                FileName = "Project1",

                AddExtension = true,
                //CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "sbs",
                Title = "Save Robot Designer File",
                //Filter = "Smartify Bot files (*.bin)|*.bin|All files (*.*)|*.*"
                Filter = "Smartify Bot files (*.sbs)|*.sbs|All files (*.sbs*)|*.sbs*"

            };

            if (opnFileDialog.ShowDialog() == true)
            {
                SaveProject(opnFileDialog.FileName);
            }


        }


        /// <summary>
        /// Save graphics to XML file.
        /// Throws: DrawingCanvasException.
        /// </summary>

        public void XMLWriter(List<VariableCollectionModel> VariableCollectionLst)
        {

            string myVariable = string.Empty;
            Expression<Func<object>> expression = () => VariableStorage.DataBaseConnStrVar;

            string name = (expression.Body as MemberExpression).Member.Name;


            XmlWriter writer = XmlWriter.Create(@"E:\botprojectXML.xml");

            foreach (VariableCollectionModel varCollection in VariableCollectionLst)
            {

            }

            writer.WriteStartElement("book");
            writer.WriteElementString("title", "Graphics Programming using GDI+");
            writer.WriteElementString("author", "Mahesh Chand");
            writer.WriteElementString("publisher", "Addison-Wesley");
            writer.WriteElementString("price", "64.95");
            writer.WriteEndElement();
            writer.Flush();
        }

        public void ReadVariableFromXML(string filename)
        {
            this.Cursor = Cursors.Wait;

            XmlDocument xml = new XmlDocument();
            xml.Load(filename);



            #region DataBaseConnStrVar

            VariableStorage.DataBaseConnStrVar.Clear();

            XmlNodeList nodes_DataBaseConnStrVar = xml.SelectNodes("//DataBaseConnStrVar/*");

            foreach (XmlNode node in nodes_DataBaseConnStrVar)
            {
                VariableStorage.DataBaseConnStrVar.Add(node.Name, Tuple.Create(node.Attributes["GUID"].Value, new OleDbConnection(node.Attributes["ConnectionString"].Value)));
            }


            #endregion
            #region DataBaseQueryResultlVar


            #endregion
            #region ExcelVar

            VariableStorage.ExcelVar.Clear();

            XmlNodeList nodes_ExcelVar = xml.SelectNodes("//ExcelVar/*");

            foreach (XmlNode node in nodes_ExcelVar)
            {
                VariableStorage.ExcelVar.Add(node.Name, Tuple.Create(node.Attributes["GUID"].Value, new Microsoft.Office.Interop.Excel.Application()));
            }


            #endregion
            #region ExcelDataVar

            #endregion
            #region WebDriverVar

            VariableStorage.WebDriverVar.Clear();

            XmlNodeList nodes_WebDriverVar = xml.SelectNodes("//WebDriverVar/*");

            foreach (XmlNode node in nodes_WebDriverVar)
            {
                VariableStorage.WebDriverVar.Add(node.Name, Tuple.Create(node.Attributes["GUID"].Value, (object)null));
            }


            #endregion
            #region IMAPCredentialVar
            VariableStorage.IMAPCredentialVar.Clear();

            XmlNodeList nodes_IMAPCredentialVar = xml.SelectNodes("//IMAPCredentialVar/*");

            foreach (XmlNode node in nodes_IMAPCredentialVar)
            {
                VariableStorage.IMAPCredentialVar.Add(node.Name, Tuple.Create(node.Attributes["imapservername"].Value, Convert.ToInt32(node.Attributes["portno"].Value), node.Attributes["username"].Value, node.Attributes["password"].Value));
            }

            #endregion
            #region EmailVar
            VariableStorage.EmailVar.Clear();

            XmlNodeList nodes_EmailVar = xml.SelectNodes("//EmailVar/*");

            foreach (XmlNode node in nodes_EmailVar)
            {
                ImapClient client = new ImapClient();

                client.Connect(VariableStorage.IMAPCredentialVar[node.Name].Item1, VariableStorage.IMAPCredentialVar[node.Name].Item2, SecureSocketOptions.SslOnConnect);


                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(VariableStorage.IMAPCredentialVar[node.Name].Item3, VariableStorage.IMAPCredentialVar[node.Name].Item4);

                VariableStorage.EmailVar.Add(node.Name, Tuple.Create(node.Attributes["GUID"].Value, client, node.Attributes["FolderName"].Value, (object)null));
            }
            #endregion

            this.Cursor = Cursors.Arrow;

        }
        public void btnUndo_click(object sender, RoutedEventArgs e)
        {

            MetroWindow window = new MetroWindow()
            {
                Content = new Admin.Admin1(),
            };

            window.SizeToContent = SizeToContent.WidthAndHeight;
            //window.WindowState = WindowState.Normal;
            window.WindowState = WindowState.Maximized;
            window.Title = "Admin Panel";
            window.Show();

            //XmlDocument xml = new XmlDocument();
            //xml.Load(@"E:\botprojectXML.xml");



            //#region DataBaseConnStrVar

            //VariableStorage.DataBaseConnStrVar.Clear();

            //XmlNodeList nodes_DataBaseConnStrVar = xml.SelectNodes("//DataBaseConnStrVar/*");

            //foreach (XmlNode node in nodes_DataBaseConnStrVar)
            //{
            //    VariableStorage.DataBaseConnStrVar.Add(node.Name, Tuple.Create(node.Attributes["GUID"].Value, new OleDbConnection(node.Attributes["ConnectionString"].Value)));
            //}


            //#endregion
            //#region DataBaseQueryResultlVar


            //#endregion
            //#region ExcelVar

            //VariableStorage.ExcelVar.Clear();

            //XmlNodeList nodes_ExcelVar = xml.SelectNodes("//ExcelVar/*");

            //foreach (XmlNode node in nodes_ExcelVar)
            //{
            //    VariableStorage.ExcelVar.Add(node.Name, Tuple.Create(node.Attributes["GUID"].Value, new Microsoft.Office.Interop.Excel.Application()));
            //}


            //#endregion
            //#region ExcelDataVar

            //#endregion



        }

        public void WriteVariableinXML(string filename)

        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.NewLineOnAttributes = true;
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.ConformanceLevel = ConformanceLevel.Auto;

            using (XmlWriter writer = XmlWriter.Create(filename, xmlWriterSettings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Variables"); // root 


                #region DataBaseConnStrVar



                Expression<Func<object>> expression = () => VariableStorage.DataBaseConnStrVar;

                string varName = (expression.Body as MemberExpression).Member.Name;

                writer.WriteStartElement(varName);

                foreach (var tempVar in VariableStorage.DataBaseConnStrVar)
                {
                    writer.WriteStartElement(tempVar.Key);
                    writer.WriteAttributeString("GUID", tempVar.Value.Item1);
                    writer.WriteAttributeString("Type", tempVar.Value.Item2.ToString());
                    writer.WriteAttributeString("ConnectionString", tempVar.Value.Item2.ConnectionString);


                    writer.WriteEndElement();
                }
                // writer.WriteStartElement(varName);
                writer.WriteFullEndElement();

                #endregion
                #region DataBaseQueryResultlVar



                Expression<Func<object>> expression_DataBaseQueryResultlVar = () => VariableStorage.DataBaseQueryResultlVar;

                string varName_DataBaseQueryResultlVar = (expression_DataBaseQueryResultlVar.Body as MemberExpression).Member.Name;

                writer.WriteStartElement(varName_DataBaseQueryResultlVar);

                foreach (var tempVar in VariableStorage.DataBaseQueryResultlVar)
                {
                    writer.WriteStartElement(tempVar.Key);

                    writer.WriteAttributeString("GUID", tempVar.Value.Item1);
                    writer.WriteAttributeString("Type", tempVar.Value.Item2.ToString());



                    writer.WriteEndElement();
                }

                writer.WriteFullEndElement();
                #endregion
                #region ExcelVar

                Expression<Func<object>> expression_ExcelVar = () => VariableStorage.ExcelVar;

                string varName_ExcelVar = (expression_ExcelVar.Body as MemberExpression).Member.Name;

                writer.WriteStartElement(varName_ExcelVar);

                foreach (var tempVar in VariableStorage.ExcelVar)
                {
                    writer.WriteStartElement(tempVar.Key);

                    writer.WriteAttributeString("GUID", tempVar.Value.Item1);
                    writer.WriteAttributeString("Type", tempVar.Value.Item2.ToString());
                    //writer.WriteAttributeString("FullFilePath", tempVar.Value.Item2.ActiveWorkbook.FullName);



                    writer.WriteEndElement();
                }

                writer.WriteFullEndElement();

                #endregion
                #region ExcelDataVar


                Expression<Func<object>> expression_ExcelDataVar = () => VariableStorage.ExcelDataVar;

                string varName_ExcelDataVar = (expression_ExcelDataVar.Body as MemberExpression).Member.Name;

                writer.WriteStartElement(varName_ExcelDataVar);

                foreach (var tempVar in VariableStorage.ExcelDataVar)
                {
                    writer.WriteStartElement(tempVar.Key);

                    writer.WriteAttributeString("GUID", tempVar.Value.Item1);
                    writer.WriteAttributeString("Type", tempVar.Value.Item2.ToString());



                    writer.WriteEndElement();
                }

                writer.WriteFullEndElement();

                #endregion
                #region WebDriverVar
                Expression<Func<object>> expression_WebDriverVar = () => VariableStorage.WebDriverVar;

                string varName_WebDriverVar = (expression_WebDriverVar.Body as MemberExpression).Member.Name;

                writer.WriteStartElement(varName_WebDriverVar);

                foreach (var tempVar in VariableStorage.WebDriverVar)
                {
                    writer.WriteStartElement(tempVar.Key);
                    writer.WriteAttributeString("GUID", tempVar.Value.Item1);
                    writer.WriteAttributeString("Type", tempVar.Value.Item2.ToString());



                    writer.WriteEndElement();
                }
                // writer.WriteStartElement(varName);
                writer.WriteFullEndElement();
                #endregion
                #region IMAPCredentialVar
                Expression<Func<object>> expression_IMAPCredentialVar = () => VariableStorage.IMAPCredentialVar;

                string varName_IMAPCredentialVar = (expression_IMAPCredentialVar.Body as MemberExpression).Member.Name;

                writer.WriteStartElement(varName_IMAPCredentialVar);

                foreach (var tempVar in VariableStorage.IMAPCredentialVar)
                {
                    writer.WriteStartElement(tempVar.Key);
                    writer.WriteAttributeString("imapservername", tempVar.Value.Item1);
                    writer.WriteAttributeString("portno", tempVar.Value.Item2.ToString());
                    writer.WriteAttributeString("username", tempVar.Value.Item3);
                    writer.WriteAttributeString("password", tempVar.Value.Item4);


                    writer.WriteEndElement();
                }
                // writer.WriteStartElement(varName);
                writer.WriteFullEndElement();
                #endregion
                #region EmailVar
                Expression<Func<object>> expression_EmailVar = () => VariableStorage.EmailVar;

                string varName_EmailVar = (expression_EmailVar.Body as MemberExpression).Member.Name;

                writer.WriteStartElement(varName_EmailVar);

                foreach (var tempVar in VariableStorage.EmailVar)
                {
                    writer.WriteStartElement(tempVar.Key);
                    writer.WriteAttributeString("GUID", tempVar.Value.Item1);
                    writer.WriteAttributeString("Type", tempVar.Value.Item2.ToString());
                    writer.WriteAttributeString("FolderName", tempVar.Value.Item3);



                    writer.WriteEndElement();
                }
                writer.WriteFullEndElement();
                #endregion



                writer.WriteEndDocument();
            }



        }
        public void btnCopy_click(object sender, RoutedEventArgs e)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.NewLineOnAttributes = true;
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.ConformanceLevel = ConformanceLevel.Auto;

            using (XmlWriter writer = XmlWriter.Create(@"E:\botprojectXML.xml", xmlWriterSettings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Variables"); // root 


                #region DataBaseConnStrVar



                Expression<Func<object>> expression = () => VariableStorage.DataBaseConnStrVar;

                string varName = (expression.Body as MemberExpression).Member.Name;

                writer.WriteStartElement(varName);

                foreach (var tempVar in VariableStorage.DataBaseConnStrVar)
                {
                    writer.WriteStartElement(tempVar.Key);
                    writer.WriteAttributeString("GUID", tempVar.Value.Item1);
                    writer.WriteAttributeString("Type", tempVar.Value.Item2.ToString());
                    writer.WriteAttributeString("ConnectionString", tempVar.Value.Item2.ConnectionString);


                    writer.WriteEndElement();
                }
                // writer.WriteStartElement(varName);
                writer.WriteFullEndElement();

                #endregion
                #region DataBaseQueryResultlVar



                Expression<Func<object>> expression_DataBaseQueryResultlVar = () => VariableStorage.DataBaseQueryResultlVar;

                string varName_DataBaseQueryResultlVar = (expression_DataBaseQueryResultlVar.Body as MemberExpression).Member.Name;

                writer.WriteStartElement(varName_DataBaseQueryResultlVar);

                foreach (var tempVar in VariableStorage.DataBaseQueryResultlVar)
                {
                    writer.WriteStartElement(tempVar.Key);

                    writer.WriteAttributeString("GUID", tempVar.Value.Item1);
                    writer.WriteAttributeString("Type", tempVar.Value.Item2.ToString());



                    writer.WriteEndElement();
                }

                writer.WriteFullEndElement();
                #endregion
                #region ExcelVar

                Expression<Func<object>> expression_ExcelVar = () => VariableStorage.ExcelVar;

                string varName_ExcelVar = (expression_ExcelVar.Body as MemberExpression).Member.Name;

                writer.WriteStartElement(varName_ExcelVar);

                foreach (var tempVar in VariableStorage.ExcelVar)
                {
                    writer.WriteStartElement(tempVar.Key);

                    writer.WriteAttributeString("GUID", tempVar.Value.Item1);
                    writer.WriteAttributeString("Type", tempVar.Value.Item2.ToString());
                    //writer.WriteAttributeString("FullFilePath", tempVar.Value.Item2.ActiveWorkbook.FullName);



                    writer.WriteEndElement();
                }

                writer.WriteFullEndElement();

                #endregion
                #region ExcelDataVar


                Expression<Func<object>> expression_ExcelDataVar = () => VariableStorage.ExcelDataVar;

                string varName_ExcelDataVar = (expression_ExcelDataVar.Body as MemberExpression).Member.Name;

                writer.WriteStartElement(varName_ExcelDataVar);

                foreach (var tempVar in VariableStorage.ExcelDataVar)
                {
                    writer.WriteStartElement(tempVar.Key);

                    writer.WriteAttributeString("GUID", tempVar.Value.Item1);
                    writer.WriteAttributeString("Type", tempVar.Value.Item2.ToString());



                    writer.WriteEndElement();
                }

                writer.WriteFullEndElement();

                #endregion


                writer.WriteEndDocument();
            }



        }
        public void btnSaveProject_inBinaryFormat_click(object sender, RoutedEventArgs e)
        {

            BinarySerialization.WriteToBinaryFile<ObservableCollection<RobotActionBase>>(@"E:\botproject.bin", actionCollection);
            //BinarySerialization.WriteToBinaryFile<List<VariableCollectionModel>>(@"E:\variable.bin", VariableCollectionLst);

            //BinarySerialization.WriteToBinaryFile<SerializationTest>(@"E:\serializationTest.bin", serializationTest);
            //serializationTest.Open();


        }
        public void btnOpenProject_inBinaryFormat_click(object sender, RoutedEventArgs e)
        {
            //serializationTest.connStr = "";
            //serializationTest.conn = null;

            //serializationTest = BinarySerialization.ReadFromBinaryFile<SerializationTest>(@"E:\serializationTest.bin");
            //serializationTest.Open();

            actionCollection = BinarySerialization.ReadFromBinaryFile<ObservableCollection<RobotActionBase>>(@"E:\botproject.bin");
            listView.ItemsSource = actionCollection;
            actionCollection.CollectionChanged += ActionCollection_CollectionChanged;

            //VariableCollectionLst = BinarySerialization.ReadFromBinaryFile<List<VariableCollectionModel>>(@"E:\variable.bin");
            //variableGridUpdate();






        }
        public void SaveProject(string fileName)
        {
            BinarySerialization.WriteToBinaryFile<ObservableCollection<RobotActionBase>>(fileName, actionCollection);

            string tempXmlFileName = System.IO.Path.GetFileNameWithoutExtension(fileName);
            string tempXmlFilePath = System.IO.Path.GetDirectoryName(fileName);
            //string[] chunk = tempXmlFileName.Split(new Char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            XmlFileFullPath = tempXmlFilePath + tempXmlFileName + ".xml";

            WriteVariableinXML(XmlFileFullPath);
        }


        void OpenProject(string fileName)
        {
            try
            {

                actionCollection = BinarySerialization.ReadFromBinaryFile<ObservableCollection<RobotActionBase>>(fileName);
                listView.ItemsSource = actionCollection;
                actionCollection.CollectionChanged += ActionCollection_CollectionChanged;

                string tempXmlFileName = System.IO.Path.GetFileNameWithoutExtension(fileName);
                string tempXmlFilePath = System.IO.Path.GetDirectoryName(fileName);
                //string[] chunk = tempXmlFileName.Split(new Char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                XmlFileFullPath = tempXmlFilePath + tempXmlFileName + ".xml";


                ReadVariableFromXML(XmlFileFullPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Smartify Bot file.", "Open File", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //private void launceNewIE(object sender, RoutedEventArgs e)
        //{
        //    this.NavigationService.Navigate("Second.xaml")
        //}



        private void mnuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {

                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "sbs",
                Title = "Open Robot Designer File",
                //Filter = "Smartify Bot files (*.bin)|*.bin|All files (*.*)|*.*"
                Filter = "Smartify Bot files (*.sbs)|*.sbs"
            };


            if (openFileDialog.ShowDialog() == true)
            {
                OpenProject(openFileDialog.FileName);
            }


        }
        //  btnCut_Click


        private async void btnCut_Click(object sender, RoutedEventArgs e)
        {
            MetroWindow window = new MetroWindow()
            {
                Content = new Admin.Admin() ,
            };

            window.SizeToContent = SizeToContent.WidthAndHeight;
            //window.WindowState = WindowState.Normal;
            window.WindowState = WindowState.Maximized;
            window.Title = "Admin Panel";
            window.Show();
        }




        private void DataGrid_MouseDoubleClick(object sender, RoutedEventArgs e)
        {

            if (variableGrid.SelectedItem == null) return;
            var selectedItem = variableGrid.SelectedItem as VariableCollectionModel;






            var obj = selectedItem.ObjectValue;

            if (obj.GetType() == typeof(OleDbConnection))
            {
                var con = obj as OleDbConnection;

                DataSetView dataview = new DataSetView();       // DataSetView is a  UserControl 
                //dataview.VariableViewString.Text = "Connection String : "+con.ConnectionString;


                TextBlock vartxtblk = new TextBlock();
                vartxtblk.Text = "Connection String : " + con.ConnectionString;
                vartxtblk.Foreground = Brushes.White;

                vartxtblk.FontWeight = FontWeights.Bold;
                vartxtblk.FontSize = 15;
                //vartxtblk.TextWrapping = TextWrapping.Wrap;
                vartxtblk.Margin = new Thickness(0, 63, 0, 176);
                dataview.VariableViewGrid.Children.Add(vartxtblk);

                new MetroWindow() { Content = dataview, Title = "Connenctin String", Background = Brushes.DimGray }.ShowDialog();


            }
            else if (obj.GetType() == typeof(String))
            {
                var con = obj as String;

                DataSetView dataview = new DataSetView();       // DataSetView is a  UserControl 
                //dataview.VariableViewString.Text = "Connection String : "+con.ConnectionString;


                TextBlock vartxtblk = new TextBlock();
                vartxtblk.Text = "Value : " + con;
                vartxtblk.Foreground = Brushes.White;

                vartxtblk.FontWeight = FontWeights.Bold;
                vartxtblk.FontSize = 15;
                //vartxtblk.TextWrapping = TextWrapping.Wrap;
                vartxtblk.Margin = new Thickness(0, 63, 0, 176);
                dataview.VariableViewGrid.Children.Add(vartxtblk);

                new MetroWindow() { Content = dataview, Title = "", Background = Brushes.DimGray }.ShowDialog();


            }
            else if (obj.GetType() == typeof(DataSet))
            {
                var dataset = obj as DataSet;

                DataSetView dataview = new DataSetView();       // DataSetView is a  UserControl 


                DataGrid varGrid = new DataGrid();
                varGrid.IsReadOnly = true;

                varGrid.ItemsSource = dataset.Tables[0].DefaultView;
                dataview.VariableViewGrid.Children.Add(varGrid);
                new MetroWindow() { Content = dataview, Title = "Query Result" }.ShowDialog();



            }
            else if (obj.GetType() == typeof(DataTable))
            {
                var dataTable = obj as DataTable;

                DataSetView dataview = new DataSetView();      // DataSetView is a  UserControl 


                DataGrid varGrid = new DataGrid();
                varGrid.IsReadOnly = true;

                varGrid.ItemsSource = dataTable.DefaultView;
                dataview.VariableViewGrid.Children.Add(varGrid);
                new MetroWindow() { Content = dataview, Title = "Result" }.ShowDialog();



            }
            else if (obj.GetType() == typeof(double))
            {
                var con = obj.ToString();

                DataSetView dataview = new DataSetView();       // DataSetView is a  UserControl 
                //dataview.VariableViewString.Text = "Connection String : "+con.ConnectionString;


                TextBlock vartxtblk = new TextBlock();
                vartxtblk.Text = "Value : " + con;
                vartxtblk.Foreground = Brushes.White;

                vartxtblk.FontWeight = FontWeights.Bold;
                vartxtblk.FontSize = 15;
                //vartxtblk.TextWrapping = TextWrapping.Wrap;
                vartxtblk.Margin = new Thickness(0, 63, 0, 176);
                dataview.VariableViewGrid.Children.Add(vartxtblk);

                new MetroWindow() { Content = dataview, Title = "", Background = Brushes.DimGray }.ShowDialog();


            }
            else if (obj.GetType() == typeof(List<object>))
            {
                //var dataset = obj as DataSet;

                DataSetView dataview = new DataSetView();       // DataSetView is a  UserControl 

                List<object> tmp = (List<object>)obj;

                DataGrid varGrid = new DataGrid();
                varGrid.IsReadOnly = true;

                varGrid.ItemsSource = from t in tmp
                                      select new { Value = t };

                dataview.VariableViewGrid.Children.Add(varGrid);
                new MetroWindow() { Content = dataview, Title = "List" }.ShowDialog();



            }


        }


        private async void variableGridUpdate()
        {
            VariableCollectionLst = VariableStorage.GetAllVariables();
            variableGrid.AutoGeneratedColumns += (sender, e) =>
            {


            };

            variableGrid.AutoGeneratingColumn += (sender, e) =>
            {
                if (e.PropertyName == "AdditionalInfo_1")
                {
                    e.Column.Visibility = Visibility.Collapsed;
                }
                if (e.PropertyName == "AdditionalInfo_2")
                {
                    e.Column.Visibility = Visibility.Collapsed;
                }
                if (e.PropertyName == "ActionID")
                {
                    e.Column.Visibility = Visibility.Collapsed;
                }
                if (e.PropertyName == "VariableName")
                {
                    e.Column.Header = "Variable Name";
                }

                if (e.PropertyName == "ObjectValue")
                {
                    e.Column.Header = "Object Value";
                }
            };


            variableGrid.ItemsSource = VariableCollectionLst;
            // variableGrid.UpdateLayout();

            //variableGrid.Columns[0].Header = "Variable Name";
            // variableGrid.Columns[2].Visibility = Visibility.Collapsed;
            //  MessageBox.Show(variableGrid.Columns.Count.ToString());
            foreach (VariableCollectionModel item in VariableCollectionLst)
            {

                var obj = item.ObjectValue;

                if (obj.GetType() == typeof(OleDbConnection))
                {
                    var con = obj as OleDbConnection;

                    //MessageBox.Show(con.ConnectionString);
                }
                else if (obj.GetType() == typeof(DataSet))
                {
                    var dataset = obj as DataSet;
                    // dataset.WriteXml(@"E:\queryresultInMain.xml");
                }


            }
        }


        private async void Enable_DisableContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                if ((listView.SelectedItem as RobotActionBase).IsActive == true)
                {
                    (listView.SelectedItem as RobotActionBase).IsActive = false;
                }
                else if ((listView.SelectedItem as RobotActionBase).IsActive == false)
                {
                    (listView.SelectedItem as RobotActionBase).IsActive = true;
                }

                ICollectionView view = CollectionViewSource.GetDefaultView(listView.ItemsSource);
                view.Refresh();

            }

        }
        private async void DeleteContextMenu_Click(object sender, RoutedEventArgs e)
        {

            if (listView.SelectedItem != null)
            {
                (this.listView.ItemsSource as ObservableCollection<RobotActionBase>).RemoveAt(this.listView.SelectedIndex);
                ICollectionView view = CollectionViewSource.GetDefaultView(listView.ItemsSource);
                view.Refresh();
            }





        }

        void GenerateConditionalMap()
        {
            Stack<int> if_stack = new Stack<int>();

            List<int> exists = new List<int>();

            int ifpos;
            int endifpos;


            condition_map.Clear();


            for (int i = 0; i < actionCollection.Count; i++)
            {
                if (actionCollection[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.If
                    || actionCollection[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.Else
                    || actionCollection[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.ElseIf
                    || actionCollection[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfFileExists
                    || actionCollection[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfImageExists
                    || actionCollection[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfFolderExists
                    || actionCollection[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfProcessExists
                    || actionCollection[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfWindowExists
                    || actionCollection[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfWindowContains
                    || actionCollection[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfWebPageContains
                    || actionCollection[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfService
                    || actionCollection[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.EndIf)
                {
                    if_stack.Push(i);
                }
                if (actionCollection[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.EndIf)
                {
                    endifpos = if_stack.Pop();
                    ifpos = if_stack.Pop();

                    List<int> tmp = new List<int>();
                    for (int j = ifpos + 1; j < endifpos; j++)
                    {
                        if (!exists.Contains(j))
                        {
                            exists.Add(j);
                            tmp.Add(j);
                        }

                    }

                    if (endifpos + 1 < actionCollection.Count)
                    {


                        if (actionCollection[endifpos + 1].ActionType == RobotDesigner.Enums.RobotActionsEnum.ElseIf
                            || actionCollection[endifpos + 1].ActionType == RobotDesigner.Enums.RobotActionsEnum.Else)
                        {
                            condition_map.Add(ifpos, Tuple.Create(endifpos, tmp, endifpos + 1));

                        }
                        else
                        {
                            condition_map.Add(ifpos, Tuple.Create(endifpos, tmp, -1));

                        }
                    }
                    else
                    {
                        condition_map.Add(ifpos, Tuple.Create(endifpos, tmp, -1));

                    }


                }

            }

        }

        void GenerateLoopConditionMap()
        {
            Stack<int> loop_stack = new Stack<int>();

            List<int> exists = new List<int>();

            int loopStartpos;
            int loopEndpos;


            loopCondition_map.Clear();


            for (int i = 0; i < actionCollection.Count; i++)
            {
                if (actionCollection[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.LoopCondition
                    || actionCollection[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.LoopConditionEnd)
                {
                    loop_stack.Push(i);
                }
                if (actionCollection[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.LoopConditionEnd)
                {
                    loopEndpos = loop_stack.Pop();
                    loopStartpos = loop_stack.Pop();

                    List<int> tmp = new List<int>();
                    for (int j = loopStartpos + 1; j < loopEndpos; j++)
                    {
                        if (!exists.Contains(j))
                        {
                            exists.Add(j);
                            tmp.Add(j);
                        }

                    }

                    loopCondition_map.Add(loopStartpos, Tuple.Create(loopEndpos, tmp));

                }

            }
        }
        void GenerateLoopMap()
        {
            Stack<int> loop_stack = new Stack<int>();

            List<int> exists = new List<int>();

            int loopStartpos;
            int loopEndpos;


            loop_map.Clear();


            for (int i = 0; i < actionCollection.Count; i++)
            {
                if (actionCollection[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.Loop
                    || actionCollection[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.EndLoop)
                {
                    loop_stack.Push(i);
                }
                if (actionCollection[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.EndLoop)
                {
                    loopEndpos = loop_stack.Pop();
                    loopStartpos = loop_stack.Pop();

                    List<int> tmp = new List<int>();
                    for (int j = loopStartpos + 1; j < loopEndpos; j++)
                    {
                        if (!exists.Contains(j))
                        {
                            exists.Add(j);
                            tmp.Add(j);
                        }

                    }

                    loop_map.Add(loopStartpos, Tuple.Create(loopEndpos, tmp));

                }

            }
        }

        private async void btnExecute_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            GenerateConditionalMap();

            GenerateLoopConditionMap();

            GenerateLoopMap();



            this.IsEnabled = false;


            //   int actionIndex = -1;

            for (int Index = 0; Index < actionCollection.Count; Index++)
            {
                //actionIndex++;



                TextBlock outputText = new TextBlock();

                actionCollection[Index].RobotDesignerProject = projectModel;

                listView.SelectedIndex = index;




                int tmpActionIndex = actionCollection[Index].Index;

                #region Condition


                if (Index >= 1)
                    if (actionCollection[Index - 1].ActionType == RobotDesigner.Enums.RobotActionsEnum.If
                        || actionCollection[Index - 1].ActionType == RobotDesigner.Enums.RobotActionsEnum.Else
                        || actionCollection[Index - 1].ActionType == RobotDesigner.Enums.RobotActionsEnum.ElseIf
                        || actionCollection[Index - 1].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfFileExists
                        || actionCollection[Index - 1].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfFolderExists
                        || actionCollection[Index - 1].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfProcessExists
                        || actionCollection[Index - 1].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfService
                        || actionCollection[Index - 1].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfWindowExists
                        || actionCollection[Index - 1].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfImageExists
                        || actionCollection[Index - 1].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfWebPageContains
                        || actionCollection[Index - 1].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfWindowContains)
                    {
                        if (actionCollection[Index - 1].ConditionResult == false && actionCollection[Index - 1].IsActive)                 // If condition is false
                        {

                            for (int i = Index; i < condition_map[Index - 1].Item1; i++)
                            {
                                actionCollection[i].IsActive = false;
                            }

                            int posElseIfOrElse = condition_map[Index - 1].Item3;

                            if (posElseIfOrElse != -1)                                           // If condition has ElseIf or Else
                            {
                                //if (actionCollection[posElseIfOrElse].ActionType == RobotDesigner.Enums.RobotActionsEnum.Else)
                                {
                                    actionCollection[posElseIfOrElse].ConditionResult = true;   // as ConditionResult of Else Action can not be set in Excecute function
                                }

                                foreach (var idx in condition_map[posElseIfOrElse].Item2)
                                {
                                    actionCollection[idx].IsActive = true;
                                }
                            }
                        }
                        else if(actionCollection[Index - 1].IsActive)                                                                   // If condition is true
                        {

                            foreach (var idx in condition_map[Index - 1].Item2)
                            {
                                actionCollection[idx].IsActive = true;
                            }

                            int posElseIfOrElse = condition_map[Index - 1].Item3;

                            if (posElseIfOrElse != -1)                                             // If condition has ElseIf or Else
                            {
                                Set_IsActive_False_To_The_Rest_Of_Condition_Block(Index - 1);




                                //actionCollection[posElseIfOrElse].IsActive = false;
                                ////if (actionCollection[posElseIfOrElse].ActionType == RobotDesigner.Enums.RobotActionsEnum.Else)
                                //{
                                //    actionCollection[posElseIfOrElse].ConditionResult = false;   // as ConditionResult of Else Action can not be set in Excecute function
                                //}



                                foreach (var idx in condition_map[posElseIfOrElse].Item2)
                                {
                                    actionCollection[idx].IsActive = false;
                                }
                            }

                        }
                    }

                #endregion

                if (actionCollection[Index].IsActive == false)
                {
                    continue;
                }


                #region Loop Condition


                if (Index >= 1)
                    if (actionCollection[Index - 1].ActionType == RobotDesigner.Enums.RobotActionsEnum.LoopCondition)
                    {
                        if (actionCollection[Index - 1].ConditionResult == false)
                        {

                            for (int i = Index; i < loopCondition_map[Index - 1].Item1; i++)
                            {
                                actionCollection[i].IsActive = false;
                            }
                        }
                        else
                        {

                            foreach (var idx in loopCondition_map[Index - 1].Item2)
                            {
                                actionCollection[idx].IsActive = true;
                            }

                        }
                    }

                #endregion


                if (actionCollection[Index].IsActive == false)
                {
                    continue;
                }

                #region Loop
                if (Index == 0)
                {


                    if (actionCollection[Index].ActionType == RobotDesigner.Enums.RobotActionsEnum.Loop)
                    {
                        TextBlock outputText2 = new TextBlock();
                        outputText2.Text = Execute(Index);
                        OutputStackPanel.Children.Add(outputText2);

                        for (int i = actionCollection[Index].LoopStart; i < actionCollection[Index].LoopEnd; i += actionCollection[Index].IncreaseBy)
                        {
                            bool exitFlag = false;

                            foreach (var item in loop_map[Index].Item2)
                            {

                                if (actionCollection[item].ActionType == RobotDesigner.Enums.RobotActionsEnum.ExitLoop)
                                {
                                    exitFlag = true;
                                    break;
                                }
                                if (actionCollection[item].ActionType == RobotDesigner.Enums.RobotActionsEnum.LoopContinue)
                                {
                                    continue;
                                }
                                await PutTaskDelay();
                                TextBlock outputText3 = new TextBlock()
                                {
                                    Text = Execute(item)
                                };
                                // outputText3.Text = Execute(item);
                                OutputStackPanel.Children.Add(outputText3);

                            }
                            if (exitFlag)
                            {
                                break;
                            }
                        }

                        Index += loop_map[Index].Item2.Count + 1;

                    }
                }
                else
                {
                    if (actionCollection[Index - 1].ActionType == RobotDesigner.Enums.RobotActionsEnum.Loop)
                    {

                        for (int i = actionCollection[Index - 1].LoopStart; i < actionCollection[Index - 1].LoopEnd; i += actionCollection[Index - 1].IncreaseBy)
                        {
                            bool exitFlag = false;

                            foreach (var item in loop_map[Index - 1].Item2)
                            {
                                if (actionCollection[item].ActionType == RobotDesigner.Enums.RobotActionsEnum.ExitLoop)
                                {
                                    exitFlag = true;
                                    break;
                                }
                                await PutTaskDelay();
                                TextBlock outputText4 = new TextBlock()
                                {
                                    Text = Execute(item)
                                };
                                //outputText4.Text = Execute(item);
                                OutputStackPanel.Children.Add(outputText4);

                            }
                            if (exitFlag)
                            {
                                break;
                            }
                        }

                        Index += loop_map[Index - 1].Item2.Count + 1;

                    }
                }
                #endregion

                if (actionCollection[Index].IsActive == false)
                {
                    continue;
                }

                bool labelFlag = false;

                if (!string.IsNullOrEmpty(actionCollection[Index].LabelToGo))
                {
                    for (int i = 0; i < actionCollection.Count; i++)
                    {
                        if (actionCollection[Index].LabelToGo != null && actionCollection[i].LabelName != null)
                        {
                            if (actionCollection[Index].LabelToGo == actionCollection[i].LabelName)
                            {
                                Index = i;
                                labelFlag = true;
                                break;
                            }
                        }
                    }
                }

                if (labelFlag)
                {
                    continue;
                }




                await PutTaskDelay();

                outputText.Text = Execute(Index);

                OutputStackPanel.Children.Add(outputText);

                variableGridUpdate();



            }

            for (int Index = 0; Index < actionCollection.Count; Index++)    // set IsActive true of all actions after execution of actionCollection list
            {
                actionCollection[Index].IsActive = true;
            }
            // WebDriverGenerator.working_driver.Quit();
            this.Cursor = Cursors.Arrow;
            this.IsEnabled = true;
        }

        void Set_IsActive_False_To_The_Rest_Of_Condition_Block(int ifStart)
        {

            int elseOrElseIfpos = condition_map[ifStart].Item3;

            if (elseOrElseIfpos != -1)
            {
                actionCollection[elseOrElseIfpos].IsActive = false;
                foreach (var item in condition_map[elseOrElseIfpos].Item2)
                {
                    actionCollection[item].IsActive = false;
                }

                if (condition_map[elseOrElseIfpos].Item3 != -1)
                    Set_IsActive_False_To_The_Rest_Of_Condition_Block(condition_map[elseOrElseIfpos].Item3);
            }
            else
            {
                actionCollection[ifStart].IsActive = false;
                foreach (var item in condition_map[ifStart].Item2)
                {
                    actionCollection[item].IsActive = false;
                }
            }
        }
        string Execute(int Index)
        {
            string output = "";

            switch (actionCollection[Index].ActionType)//(item.ActionType)
            {
                #region File Actions

                case RobotDesigner.Enums.RobotActionsEnum.CopyFile:
                    if ((actionCollection[Index] as CopyFiles).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";

                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.DeleteFile:
                    if ((actionCollection[Index] as DeleteFiles).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.MoveFiles:
                    if ((actionCollection[Index] as MoveFiles).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.RenameFiles:
                    if ((actionCollection[Index] as RenameFiles).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.WriteTextToFile:
                    if ((actionCollection[Index] as WriteTextToFile).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                #endregion
                #region Web Actions


                case RobotDesigner.Enums.RobotActionsEnum.LaunchNewIE:
                    if ((actionCollection[Index] as LaunchNewIE).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.CloseIE:
                    if ((actionCollection[Index] as CloseIE).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.TakeScreenshotOfWebPage:
                    if ((actionCollection[Index] as TakeScreenShotOfWebPage).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.ExtractPageDetails:
                    if ((actionCollection[Index] as ExtractPageDetails).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.GetObjectDetails:
                    if ((actionCollection[Index] as GetObjectDetails).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.HoverOnWebObject:
                    if ((actionCollection[Index] as HoverOnWebObject).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;

                case RobotDesigner.Enums.RobotActionsEnum.PopulateTextFiedOnWeb:
                    if ((actionCollection[Index] as PopulateTextFiedOnWeb).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.ClickOnWebPage:
                    if ((actionCollection[Index] as ClickOnWebPage).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.SetDropDownList:
                    if ((actionCollection[Index] as SetDropDownList).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.SetRadioButton:
                    if ((actionCollection[Index] as SetRadioButton).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.SetCheckBox:
                    if ((actionCollection[Index] as SetCheckBox).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.ExtractDataFromWeb:
                    if ((actionCollection[Index] as ExtractDataFromWeb).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                #endregion
                #region Excel Actions


                case RobotDesigner.Enums.RobotActionsEnum.LaunchExcel:
                    if ((actionCollection[Index] as LaunchExcel).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.WriteToExcel:
                    if ((actionCollection[Index] as WriteToExcel).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.AddNewWorkSheet:
                    if ((actionCollection[Index] as AddNewWorkSheet).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.SetActiveWorkSheet:
                    if ((actionCollection[Index] as SetActiveWorkSheet).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.ReadFromExcel:
                    if ((actionCollection[Index] as ReadFromExcel).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.CloseExcel:
                    if ((actionCollection[Index] as CloseExcel).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                #endregion
                #region Email Actions


                case RobotDesigner.Enums.RobotActionsEnum.SendMessages:
                    if ((actionCollection[Index] as SendMessages).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.ProcessMessage:
                    if ((actionCollection[Index] as ProcessMessage).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.GetMessages:
                    if ((actionCollection[Index] as GetMessages).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                #endregion
                #region DataBase Actons


                case RobotDesigner.Enums.RobotActionsEnum.OpenSQLConnection:
                    if ((actionCollection[Index] as OpenSQLConnection).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.CloseSQLConnection:
                    if ((actionCollection[Index] as CloseSQLConnection).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.ExecuteSQLStatement:
                    if ((actionCollection[Index] as ExecuteSQLStatement).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                #endregion
                #region Text Actions



                case RobotDesigner.Enums.RobotActionsEnum.ChangeTextFormat:
                    if ((actionCollection[Index] as ChangeTextFormat).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.CompareText:
                    if ((actionCollection[Index] as CompareText).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.FindText:
                    if ((actionCollection[Index] as FindText).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.GetSubText:
                    if ((actionCollection[Index] as GetSubText).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.GetTextLength:
                    if ((actionCollection[Index] as GetTextLength).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.JoinText:
                    if ((actionCollection[Index] as JoinText).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.ReplaceText:
                    if ((actionCollection[Index] as ReplaceText).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.ReverseText:
                    if ((actionCollection[Index] as ReverseText).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.SplitText:
                    if ((actionCollection[Index] as SplitText).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.TrimText:
                    if ((actionCollection[Index] as TrimText).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.ChangeTextCase:
                    if ((actionCollection[Index] as ChangeTextCase).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.ChangeTextToNumber:
                    if ((actionCollection[Index] as ChangeTextToNumber).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.ConverNumberToText:
                    if ((actionCollection[Index] as ConverNumberToText).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.ConvertTextToDateTime:
                    if ((actionCollection[Index] as ConvertTextToDateTime).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.ConvertDateTimeToText:
                    if ((actionCollection[Index] as ConvertDateTimeToText).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;


                #endregion
                #region DateTime Action
                case RobotDesigner.Enums.RobotActionsEnum.GetCurrentDateTime:
                    if ((actionCollection[Index] as GetCurrentDateTime).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.AddToDateTime:
                    if ((actionCollection[Index] as AddToDateTime).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.SubtractDates:
                    if ((actionCollection[Index] as SubtractDates).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                #endregion
                #region Mouse and Keyboard
                case RobotDesigner.Enums.RobotActionsEnum.SetKeysState:
                    if ((actionCollection[Index] as SetKeyState).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.PressReleaseKey:
                    if ((actionCollection[Index] as PressReleaseKey).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.MouseScroll:
                    if ((actionCollection[Index] as MouseScroll).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.BlockInput:
                    if ((actionCollection[Index] as Block_Input).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.MoveMouse:
                    if ((actionCollection[Index] as MoveMouse).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.MouseClick:
                    if ((actionCollection[Index] as MouseClick).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                #endregion
                #region Variable Action
                case RobotDesigner.Enums.RobotActionsEnum.AddTtemToList:
                    if ((actionCollection[Index] as AddTtemToList).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.ClearList:
                    if ((actionCollection[Index] as ClearList).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.CreateNewList:
                    if ((actionCollection[Index] as CreateNewList).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.DecreaseVariable:
                    if ((actionCollection[Index] as DecreaseVariable).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.IncreaseVariable:
                    if ((actionCollection[Index] as IncreaseVariable).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.RemoveItemFromList:
                    if ((actionCollection[Index] as RemoveItemFromList).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.SetVariable:
                    if ((actionCollection[Index] as SetVariable).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.TruncateNumber:
                    if ((actionCollection[Index] as TruncateNumber).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.ReverseList:
                    if ((actionCollection[Index] as ReverseList).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.RemoveDuplicateItemFromList:
                    if ((actionCollection[Index] as RemoveDuplicateItemFromList).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.SuffleList:
                    if ((actionCollection[Index] as SuffleList).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.MergeList:
                    if ((actionCollection[Index] as MergeLists).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.DeleteList:
                    if ((actionCollection[Index] as DeleteList).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.GetListLength:
                    if ((actionCollection[Index] as GetListLength).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                #endregion
                #region Message Action
                case RobotDesigner.Enums.RobotActionsEnum.DisplayMessage:
                    if ((actionCollection[Index] as DisplayMessage).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                #endregion
                #region Delay Action
                case RobotDesigner.Enums.RobotActionsEnum.DelayByTiming:
                    if ((actionCollection[Index] as DelayByTiming).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                #endregion
                #region Conditional
                case RobotDesigner.Enums.RobotActionsEnum.If:
                    if ((actionCollection[Index] as If).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.EndIf:
                    if ((actionCollection[Index] as EndIf).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.IfFileExists:
                    if ((actionCollection[Index] as IfFileExists).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.IfFolderExists:
                    if ((actionCollection[Index] as IfFolderExists).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.IfProcessExists:
                    if ((actionCollection[Index] as IfProcessExists).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.IfService:
                    if ((actionCollection[Index] as IfService).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.IfImageExists:
                    if ((actionCollection[Index] as IfImageExists).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.Else:
                    if ((actionCollection[Index] as Else).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.ElseIf:
                    if ((actionCollection[Index] as ElseIf).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                #endregion
                #region Loops
                case RobotDesigner.Enums.RobotActionsEnum.Loop:
                    if ((actionCollection[Index] as Loop).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.EndLoop:
                    if ((actionCollection[Index] as EndLoop).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.ExitLoop:
                    if ((actionCollection[Index] as ExitLoop).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.LoopCondition:
                    if ((actionCollection[Index] as LoopCondition).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.ForEach:
                    if ((actionCollection[Index] as ForEach).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.LoopContinue:
                    if ((actionCollection[Index] as ForEach).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.LoopConditionEnd:
                    if ((actionCollection[Index] as LoopConditionEnd).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                #endregion
                #region Flow Control
                case RobotDesigner.Enums.RobotActionsEnum.Label_:
                    if ((actionCollection[Index] as Label_).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                case RobotDesigner.Enums.RobotActionsEnum.GoTo:
                    if ((actionCollection[Index] as GoTo).Execute() > 0)
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Successful";
                        index++;
                    }
                    else
                    {
                        output = "Task: " + actionCollection[Index].ActionType.ToString() + " Failed";
                    }
                    break;
                    #endregion


            }

            return output;

        }


        async Task PutTaskDelay()
        {
            await Task.Delay(1000);
        }

        private void launchnewie(object sender, RoutedEventArgs e)
        {
            //NavigationService nav = NavigationService.GetNavigationService(this);
            //nav.Navigate(new Uri(@"\web automation\launchnewie.xaml", UriKind.RelativeOrAbsolute));

            //MessageBox.Show("adfasdf");
        }

        private void TabablzControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DockWindowGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

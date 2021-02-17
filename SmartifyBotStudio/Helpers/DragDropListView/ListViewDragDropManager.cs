using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.TaskModel.File;
using SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation;
using SmartifyBotStudio.RobotDesigner.TaskModel.Excel;
using SmartifyBotStudio.RobotDesigner.TaskModel.Email;
using SmartifyBotStudio.RobotDesigner.TaskModel.DateTimeActions;
using SmartifyBotStudio.RobotDesigner.TaskModel.Mouse_and_Keyboard;
using SmartifyBotStudio.RobotDesigner.TaskModel.MessageBoxes;
using SmartifyBotStudio.RobotDesigner.TaskModel.Conditional;
using SmartifyBotStudio.RobotDesigner.TaskModel.Loops;
using SmartifyBotStudio.RobotDesigner.TaskModel.Flow_Control;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using SmartifyBotStudio.RobotDesigner.TaskModel.DataBase;
using SmartifyBotStudio.RobotDesigner.TaskModel.Text;
using SmartifyBotStudio.RobotDesigner.TaskModel.VariablesActions;
using SmartifyBotStudio.RobotDesigner.TaskModel.Delay;
using System.Collections.Generic;

namespace SmartifyBotStudio.Helpers.DragDropListView
{
    #region ListViewDragDropManager

    /// <summary>
    /// Manages the dragging and dropping of ListViewItems in a ListView.
    /// The ItemType type parameter indicates the type of the objects in
    /// the ListView's items source.  The ListView's ItemsSource must be 
    /// set to an instance of ObservableCollection of ItemType, or an 
    /// Exception will be thrown.
    /// </summary>
    /// <typeparam name="ItemType">The type of the ListView's items.</typeparam>
    public class ListViewDragDropManager<ItemType> where ItemType : class
    {
        #region Data

        bool canInitiateDrag;
        DragAdorner dragAdorner;
        double dragAdornerOpacity;
        int indexToSelect;
        bool isDragInProgress;
        ItemType itemUnderDragCursor;
        ListView listView;
        Point ptMouseDown;
        bool showDragAdorner;


        Dictionary<int, Tuple<int, List<int>>> condition_map = new Dictionary<int, Tuple<int, List<int>>>();

        Dictionary<int, Tuple<int, List<int>>> loop_map = new Dictionary<int, Tuple<int, List<int>>>();

        Dictionary<int, Tuple<int, List<int>>> loopCondition_map = new Dictionary<int, Tuple<int, List<int>>>();


        #endregion // Data

        #region Constructors

        /// <summary>
        /// Initializes a new instance of ListViewDragManager.
        /// </summary>
        public ListViewDragDropManager()
        {
            this.canInitiateDrag = false;
            this.dragAdornerOpacity = 0.7;
            this.indexToSelect = -1;
            this.showDragAdorner = true;
        }

        /// <summary>
        /// Initializes a new instance of ListViewDragManager.
        /// </summary>
        /// <param name="listView"></param>
        public ListViewDragDropManager(ListView listView)
            : this()
        {
            this.ListView = listView;
        }

        /// <summary>
        /// Initializes a new instance of ListViewDragManager.
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="dragAdornerOpacity"></param>
        public ListViewDragDropManager(ListView listView, double dragAdornerOpacity)
            : this(listView)
        {
            this.DragAdornerOpacity = dragAdornerOpacity;
        }

        /// <summary>
        /// Initializes a new instance of ListViewDragManager.
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="showDragAdorner"></param>
        public ListViewDragDropManager(ListView listView, bool showDragAdorner)
            : this(listView)
        {
            this.ShowDragAdorner = showDragAdorner;
        }

        #endregion // Constructors

        #region Public Interface

        #region DragAdornerOpacity

        /// <summary>
        /// Gets/sets the opacity of the drag adorner.  This property has no
        /// effect if ShowDragAdorner is false. The default value is 0.7
        /// </summary>
        public double DragAdornerOpacity
        {
            get { return this.dragAdornerOpacity; }
            set
            {
                if (this.IsDragInProgress)
                    throw new InvalidOperationException("Cannot set the DragAdornerOpacity property during a drag operation.");

                if (value < 0.0 || value > 1.0)
                    throw new ArgumentOutOfRangeException("DragAdornerOpacity", value, "Must be between 0 and 1.");

                this.dragAdornerOpacity = value;
            }
        }

        #endregion // DragAdornerOpacity

        #region IsDragInProgress

        /// <summary>
        /// Returns true if there is currently a drag operation being managed.
        /// </summary>
        public bool IsDragInProgress
        {
            get { return this.isDragInProgress; }
            private set { this.isDragInProgress = value; }
        }

        #endregion // IsDragInProgress

        #region ListView

        /// <summary>
        /// Gets/sets the ListView whose dragging is managed.  This property
        /// can be set to null, to prevent drag management from occuring.  If
        /// the ListView's AllowDrop property is false, it will be set to true.
        /// </summary>
        public ListView ListView
        {
            get { return listView; }
            set
            {
                if (this.IsDragInProgress)
                    throw new InvalidOperationException("Cannot set the ListView property during a drag operation.");

                if (this.listView != null)
                {
                    #region Unhook Events

                    this.listView.PreviewMouseLeftButtonDown -= listView_PreviewMouseLeftButtonDown;
                    this.listView.PreviewMouseMove -= listView_PreviewMouseMove;
                    this.listView.DragOver -= listView_DragOver;
                    this.listView.DragLeave -= listView_DragLeave;
                    this.listView.DragEnter -= listView_DragEnter;
                    this.listView.Drop -= listView_Drop;

                    #endregion // Unhook Events
                }

                this.listView = value;

                if (this.listView != null)
                {
                    if (!this.listView.AllowDrop)
                        this.listView.AllowDrop = true;

                    #region Hook Events

                    this.listView.PreviewMouseLeftButtonDown += listView_PreviewMouseLeftButtonDown;
                    this.listView.PreviewMouseMove += listView_PreviewMouseMove;
                    this.listView.DragOver += listView_DragOver;
                    this.listView.DragLeave += listView_DragLeave;
                    this.listView.DragEnter += listView_DragEnter;
                    this.listView.Drop += listView_Drop;

                    #endregion // Hook Events
                }
            }
        }

        #endregion // ListView

        #region ProcessDrop [event]

        /// <summary>
        /// Raised when a drop occurs.  By default the dropped item will be moved
        /// to the target index.  Handle this event if relocating the dropped item
        /// requires custom behavior.  Note, if this event is handled the default
        /// item dropping logic will not occur.
        /// </summary>
        public event EventHandler<ProcessDropEventArgs<RobotActionBase>> ProcessDrop;

        #endregion // ProcessDrop [event]

        #region ShowDragAdorner

        /// <summary>
        /// Gets/sets whether a visual representation of the ListViewItem being dragged
        /// follows the mouse cursor during a drag operation.  The default value is true.
        /// </summary>
        public bool ShowDragAdorner
        {
            get { return this.showDragAdorner; }
            set
            {
                if (this.IsDragInProgress)
                    throw new InvalidOperationException("Cannot set the ShowDragAdorner property during a drag operation.");

                this.showDragAdorner = value;
            }
        }

        #endregion // ShowDragAdorner

        #endregion // Public Interface

        #region Event Handling Methods

        #region listView_PreviewMouseLeftButtonDown

        void listView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.IsMouseOverScrollbar)
            {
                // 4/13/2007 - Set the flag to false when cursor is over scrollbar.
                this.canInitiateDrag = false;
                return;
            }

            int index = this.IndexUnderDragCursor;
            //MessageBox.Show(index.ToString());
            this.canInitiateDrag = index > -1;

            if (this.canInitiateDrag)
            {
                // Remember the location and index of the ListViewItem the user clicked on for later.
                this.ptMouseDown = MouseUtilities.GetMousePosition(this.listView);
                this.indexToSelect = index;
            }
            else
            {
                this.ptMouseDown = new Point(-10000, -10000);
                this.indexToSelect = -1;
            }
        }

        #endregion // listView_PreviewMouseLeftButtonDown

        #region listView_PreviewMouseMove

        void listView_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!this.CanStartDragOperation)
                return;

            // Select the item the user clicked on.
            if (this.listView.SelectedIndex != this.indexToSelect)
                this.listView.SelectedIndex = this.indexToSelect;

            // If the item at the selected index is null, there's nothing
            // we can do, so just return;
            if (this.listView.SelectedItem == null)
                return;

            ListViewItem itemToDrag = this.GetListViewItem(this.listView.SelectedIndex);
            if (itemToDrag == null)
                return;

            AdornerLayer adornerLayer = this.ShowDragAdornerResolved ? this.InitializeAdornerLayer(itemToDrag) : null;

            this.InitializeDragOperation(itemToDrag);
            this.PerformDragOperation();
            this.FinishDragOperation(itemToDrag, adornerLayer);
        }

        #endregion // listView_PreviewMouseMove

        #region listView_DragOver

        void listView_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;

            if (this.ShowDragAdornerResolved)
                this.UpdateDragAdornerLocation();

            // Update the item which is known to be currently under the drag cursor.
            int index = this.IndexUnderDragCursor;
            this.ItemUnderDragCursor = index < 0 ? null : this.ListView.Items[index] as ItemType;

            //MessageBox.Show(this.IndexUnderDragCursor.ToString());

        }

        #endregion // listView_DragOver

        #region listView_DragLeave

        void listView_DragLeave(object sender, DragEventArgs e)
        {
            if (!this.IsMouseOver(this.listView))
            {
                if (this.ItemUnderDragCursor != null)
                    this.ItemUnderDragCursor = null;

                if (this.dragAdorner != null)
                    this.dragAdorner.Visibility = Visibility.Collapsed;
            }
        }

        #endregion // listView_DragLeave

        #region listView_DragEnter

        void listView_DragEnter(object sender, DragEventArgs e)
        {
            if (this.dragAdorner != null && this.dragAdorner.Visibility != Visibility.Visible)
            {
                // Update the location of the adorner and then show it.				
                this.UpdateDragAdornerLocation();
                this.dragAdorner.Visibility = Visibility.Visible;
            }
        }

        #endregion // listView_DragEnter

        #region listView_Drop

        void listView_Drop(object sender, DragEventArgs e)
        {
            if (this.ItemUnderDragCursor != null)
                this.ItemUnderDragCursor = null;

            e.Effects = DragDropEffects.None;

            //if (!e.Data.GetDataPresent(typeof(ItemType)))
            //    return;
            var dataFromat = e.Data.GetFormats()[0];



            // Get the data object which was dropped.
            RobotActionBase data = e.Data.GetData(dataFromat) as RobotActionBase;
            if (data == null)
                return;

            // Get the ObservableCollection<ItemType> which contains the dropped data object.
            ObservableCollection<RobotActionBase> itemsSource = this.listView.ItemsSource as ObservableCollection<RobotActionBase>;
            if (itemsSource == null)
                throw new Exception(
                    "A ListView managed by ListViewDragManager must have its ItemsSource set to an ObservableCollection<ItemType>.");

            int oldIndex = itemsSource.IndexOf(data);
            int newIndex = this.IndexUnderDragCursor;

            //var myData = data as Task;

            //if (myData.IsExisting == false)
            //{

            //    var d = new Task(myData.Icon, myData.ID, myData.Name, myData.Description, false, true);
            //    if (newIndex < 0)
            //        newIndex = 0;

            //    itemsSource.Insert(newIndex, d as ItemType);
            //    return;
            //}


            if (newIndex < 0)
            {
                // The drag started somewhere else, and our ListView is empty
                // so make the new item the first in the list.
                if (itemsSource.Count == 0)
                    newIndex = 0;

                // The drag started somewhere else, but our ListView has items
                // so make the new item the last in the list.
                else if (oldIndex < 0)
                    newIndex = itemsSource.Count;

                // The user is trying to drop an item from our ListView into
                // our ListView, but the mouse is not over an item, so don't
                // let them drop it.
                else
                    return;
            }

            // Dropping an item back onto itself is not considered an actual 'drop'.
            if (oldIndex == newIndex)
                return;

            if (this.ProcessDrop != null)
            {
                // Let the client code process the drop.
                ProcessDropEventArgs<RobotActionBase> args = new ProcessDropEventArgs<RobotActionBase>(itemsSource, data, oldIndex, newIndex, e.AllowedEffects);
                this.ProcessDrop(this, args);
                e.Effects = args.Effects;
            }
            else
            {
                // Move the dragged data object from it's original index to the
                // new index (according to where the mouse cursor is).  If it was
                // not previously in the ListBox, then insert the item.
                if (oldIndex > -1)
                {


                    itemsSource.Move(oldIndex, newIndex);



                }
                else
                {
                    //(data as Task).IsExisting = true;
                    var myData = data as RobotActionBase;

                    #region File Actions 



                    if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.CopyFile)
                    {
                        var d = new CopyFiles()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Destination = @"C:\",
                            Finished = false,
                            OverWriteIfFilesExixts = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Copy Files",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, d as RobotActionBase);

                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.DeleteFile)
                    {
                        var del = new DeleteFiles()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Delete Files",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, del as RobotActionBase);

                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.GetFilePathPart)
                    {
                        var getFilePath = new GetFilePathPart()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Get File Path Part",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, getFilePath as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.GetFilesinFolder)
                    {
                        var getFile = new GetFilesinFolder()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Get Files in Folder",
                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, getFile as RobotActionBase);
                    }

                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.MoveFiles)
                    {
                        var moveFile = new MoveFiles()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Move Files",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, moveFile as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.ReadFromCSVFile)
                    {
                        var readCsv = new ReadFromCSVFile()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Read From CSV File",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, readCsv as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.ReadTextFromFile)
                    {
                        var readText = new ReadTextFromFile()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Read Text From File",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, readText as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.RenameFiles)
                    {
                        var renameFile = new RenameFiles()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Rename Files",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, renameFile as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.WriteTextToFile)
                    {
                        var writeText = new WriteTextToFile()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Write Text To File",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeText as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.WriteToCSVFile)
                    {
                        var writeCsv = new WriteToCSVFile()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Write To CSV File",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.ReadFromCSVFile)
                    {
                        var writeCsv = new ReadFromCSVFile()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Read From CSV File",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }
                    #endregion
                    #region Web Actions


                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.LaunchNewIE)
                    {
                        var writeCsv = new LaunchNewIE()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Launch New IE",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.CloseIE)
                    {
                        var writeCsv = new CloseIE()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Close IE",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.TakeScreenshotOfWebPage)
                    {
                        var writeCsv = new TakeScreenShotOfWebPage()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Take Screenshot of Web Page",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.ExtractPageDetails)
                    {
                        var writeCsv = new ExtractPageDetails()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Extract Page Details",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.GetObjectDetails)
                    {
                        var writeCsv = new GetObjectDetails()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Get Object Details",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.HoverOnWebObject)
                    {
                        var writeCsv = new HoverOnWebObject()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Hover On Web Object",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }

                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.PopulateTextFiedOnWeb)
                    {
                        var writeCsv = new PopulateTextFiedOnWeb()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Populate Text Field On Web",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.ClickOnWebPage)
                    {
                        var writeCsv = new ClickOnWebPage()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Click On Web Page",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.SetDropDownList)
                    {
                        var writeCsv = new SetDropDownList()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Set Drop Down List On Web Page",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.SetRadioButton)
                    {
                        var writeCsv = new SetRadioButton()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Set Radio Button On Web Page",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.SetCheckBox)
                    {
                        var writeCsv = new SetCheckBox()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Set Check Box",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.ExtractDataFromWeb)
                    {
                        var writeCsv = new ExtractDataFromWeb()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Extract Data From Web Page",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }

                    #endregion
                    #region Email Actions


                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.GetMessages)
                    {
                        var writeCsv = new GetMessages()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Get Messages",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.SendMessages)
                    {
                        var writeCsv = new SendMessages()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Send Messages",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.ProcessMessage)
                    {
                        var writeCsv = new ProcessMessage()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Process Message",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }
                    #endregion
                    #region Excel Actions


                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.LaunchExcel)
                    {
                        var writeCsv = new LaunchExcel()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Launch Excel",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.CloseExcel)
                    {
                        var writeCsv = new CloseExcel()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Close Excel",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.WriteToExcel)
                    {
                        var writeCsv = new WriteToExcel()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Write to  Excel",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.AddNewWorkSheet)
                    {
                        var writeCsv = new AddNewWorkSheet()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Add New Worksheet to  Excel",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.SetActiveWorkSheet)
                    {
                        var writeCsv = new SetActiveWorkSheet()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Set Active Worksheet to  Excel",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.ReadFromExcel)
                    {
                        var writeCsv = new ReadFromExcel()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Read from  Excel Worksheet",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                    }
                    #endregion                    
                    #region DataBase Actions


                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.OpenSQLConnection)
                    {
                        var writeCsv = new OpenSQLConnection()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Open SQL Connection",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.CloseSQLConnection)
                    {
                        var writeCsv = new CloseSQLConnection()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Close SQL Connection",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.ExecuteSQLStatement)
                    {
                        var writeCsv = new ExecuteSQLStatement()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Execute SQL Connection",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    #endregion
                    #region Text Actons

                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.ChangeTextFormat)
                    {
                        var writeCsv = new ChangeTextFormat()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Change Text Format",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.CompareText)
                    {
                        var writeCsv = new CompareText()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Compare Text",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.FindText)
                    {
                        var writeCsv = new FindText()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "FindText",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.GetSubText)
                    {
                        var writeCsv = new GetSubText()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Get Sub Text",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.GetTextLength)
                    {
                        var writeCsv = new GetTextLength()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Get Text Length",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.JoinText)
                    {
                        var writeCsv = new JoinText()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Join Text",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.ReplaceText)
                    {
                        var writeCsv = new ReplaceText()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Replace Text",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.ReverseText)
                    {
                        var writeCsv = new ReverseText()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Reverse Text",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.SplitText)
                    {
                        var writeCsv = new SplitText()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Split Text",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.TrimText)
                    {
                        var writeCsv = new TrimText()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Trim Text",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.ChangeTextCase)
                    {
                        var writeCsv = new ChangeTextCase()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Change Text Case",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.ChangeTextToNumber)
                    {
                        var writeCsv = new ChangeTextToNumber()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Convert Text To Number",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.ConverNumberToText)
                    {
                        var writeCsv = new ConverNumberToText()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Conver Number To Text",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.ConvertDateTimeToText)
                    {
                        var writeCsv = new ConvertDateTimeToText()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Convert DateTime To Text",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.ConvertTextToDateTime)
                    {
                        var writeCsv = new ConvertTextToDateTime()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Convert Text To DateTime",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }


                    #endregion
                    #region DateTime Actions
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.GetCurrentDateTime)
                    {
                        var writeCsv = new GetCurrentDateTime()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Get Current Date and Time",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.AddToDateTime)
                    {
                        var writeCsv = new AddToDateTime()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Add To DateTime",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.SubtractDates)
                    {
                        var writeCsv = new SubtractDates()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Subtract Dates",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    #endregion
                    #region Mouse and Keyboard
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.SetKeysState)
                    {
                        var writeCsv = new SetKeyState()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Set Keys State",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.PressReleaseKey)
                    {
                        var writeCsv = new PressReleaseKey()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Press/Release Keys",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.MouseScroll)
                    {
                        var writeCsv = new MouseScroll()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Mouse Scroll",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.BlockInput)
                    {
                        var writeCsv = new Block_Input()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Block Input",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.MoveMouse)
                    {
                        var writeCsv = new MoveMouse()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Move Mouse",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.MouseClick)
                    {
                        var writeCsv = new MouseClick()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Send Mouse Event",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    #endregion
                    #region Variable Action
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.AddTtemToList)
                    {
                        var writeCsv = new AddTtemToList()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Add Ttem To List",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.ClearList)
                    {
                        var writeCsv = new ClearList()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Clear List",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.DecreaseVariable)
                    {
                        var writeCsv = new DecreaseVariable()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Decrease Variable",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.CreateNewList)
                    {
                        var writeCsv = new CreateNewList()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Create New List",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.IncreaseVariable)
                    {
                        var writeCsv = new IncreaseVariable()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Increase Variable",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.RemoveItemFromList)
                    {
                        var writeCsv = new RemoveItemFromList()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Remove Item From List",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.SetVariable)
                    {
                        var writeCsv = new SetVariable()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Set Variable",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.TruncateNumber)
                    {
                        var writeCsv = new TruncateNumber()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Truncate Number",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.ReverseList)
                    {
                        var writeCsv = new ReverseList()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Reverse List",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.RemoveDuplicateItemFromList)
                    {
                        var writeCsv = new RemoveDuplicateItemFromList()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Remove Duplicate Item From List",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.SuffleList)
                    {
                        var writeCsv = new SuffleList()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Suffle List",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.MergeList)
                    {
                        var writeCsv = new MergeLists()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Merge List",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.DeleteList)
                    {
                        var writeCsv = new DeleteList()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Delete List",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.GetListLength)
                    {
                        var writeCsv = new GetListLength()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Get List Length",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    #endregion
                    #region MessageBox Action
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.DisplayMessage)
                    {
                        var writeCsv = new DisplayMessage()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Display Message",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    #endregion
                    #region Delay Action
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.DelayByTiming)
                    {
                        var writeCsv = new DelayByTiming()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Delay By Timing",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    #endregion
                    #region Conditional
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.If)
                    {
                        var writeCsv = new If()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "If",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.EndIf)
                    {
                        var writeCsv = new EndIf()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "EndIf",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.IfFileExists)
                    {
                        var writeCsv = new IfFileExists()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "If File Exists",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.IfFolderExists)
                    {
                        var writeCsv = new IfFolderExists()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "If Folder Exists",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.IfProcessExists)
                    {
                        var writeCsv = new IfProcessExists()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "If Process",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.IfService)
                    {
                        var writeCsv = new IfService()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "If Service",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.IfImageExists)
                    {
                        var writeCsv = new IfImageExists()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "If Image",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.Else)
                    {
                        var writeCsv = new Else()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Else",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.ElseIf)
                    {
                        var writeCsv = new ElseIf()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Else If",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    #endregion
                    #region Loops
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.Loop)
                    {
                        var writeCsv = new Loop()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Loop",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);

                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.EndLoop)
                    {
                        var endLoopCsv = new EndLoop()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "End Loop",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, endLoopCsv as RobotActionBase);




                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.ExitLoop)
                    {
                        var writeCsv = new ExitLoop()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Exit Loop",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.LoopCondition)
                    {
                        var writeCsv = new LoopCondition()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Loop Condition",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.LoopContinue)
                    {
                        var writeCsv = new LoopContinue()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Loop Continue",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.LoopConditionEnd)
                    {
                        var writeCsv = new LoopConditionEnd()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Loop Condition End",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    #endregion
                    #region Flow Control
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.Label_)
                    {
                        var writeCsv = new Label_()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Label",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    else if (myData.ActionType == RobotDesigner.Enums.RobotActionsEnum.GoTo)
                    {
                        var writeCsv = new GoTo()
                        {
                            ActionType = myData.ActionType,
                            Description = "Description",
                            Finished = false,
                            IsExisting = data.IsExisting,
                            ID = Guid.NewGuid().ToString(),
                            Icon = data.Icon,
                            Name = "Go To",

                            IsActive = true
                        };

                        itemsSource.Insert(newIndex, writeCsv as RobotActionBase);
                        //MessageBox.Show(writeCsv.ID);
                    }
                    #endregion

                }

                // Set the Effects property so that the call to DoDragDrop will return 'Move'.
                e.Effects = DragDropEffects.Move;
            }


            ArrangeActionIndex(itemsSource, data);
        }

        void ArrangeActionIndex(ObservableCollection<RobotActionBase> itemsSource, RobotActionBase currentAction)
        {
            int index = 1;
            foreach (var item in itemsSource)
            {
                item.Index = index;
                index++;

            }

            List<int> childCollection = new List<int>();

            GenerateLoopMap(itemsSource);

            foreach (var item in loop_map)
            {
                foreach (var item2 in item.Value.Item2)
                {
                    itemsSource[item2].IsChild = true;
                    childCollection.Add(item2);
                }
            }

            GenerateConditionalMap(itemsSource);

            foreach (var item in condition_map)
            {
                foreach (var item2 in item.Value.Item2)
                {
                    itemsSource[item2].IsChild = true;
                    childCollection.Add(item2);
                }
            }

            GenerateLoopConditionMap(itemsSource);

            foreach (var item in loopCondition_map)
            {
                foreach (var item2 in item.Value.Item2)
                {
                    itemsSource[item2].IsChild = true;
                    childCollection.Add(item2);
                }
            }

            for (int i = 0; i < itemsSource.Count; i++)
            {
                if (!childCollection.Contains(i))
                {
                    itemsSource[i].IsChild = false;
                }
            }

        }


        void GenerateConditionalMap(ObservableCollection<RobotActionBase> itemsSource)
        {
            Stack<int> if_stack = new Stack<int>();

            List<int> exists = new List<int>();

            int ifpos = 0;
            int endifpos = 0;


            condition_map.Clear();


            for (int i = 0; i < itemsSource.Count; i++)
            {
                if (itemsSource[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.If
                    || itemsSource[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.Else
                    || itemsSource[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.ElseIf
                    || itemsSource[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfFileExists
                    || itemsSource[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfImageExists
                    || itemsSource[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfFolderExists
                    || itemsSource[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfProcessExists
                    || itemsSource[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfWindowExists
                    || itemsSource[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfWindowContains
                    || itemsSource[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfWebPageContains
                    || itemsSource[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.IfService
                    || itemsSource[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.EndIf)
                {
                    if_stack.Push(i);
                }
                if (itemsSource[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.EndIf)
                {
                    if (if_stack != null)
                    {
                        endifpos = if_stack.Pop();
                        ifpos = if_stack.Pop();
                    }


                    List<int> tmp = new List<int>();
                    for (int j = ifpos + 1; j < endifpos; j++)
                    {
                        if (!exists.Contains(j))
                        {
                            exists.Add(j);
                            tmp.Add(j);
                        }

                    }

                    condition_map.Add(ifpos, Tuple.Create(endifpos, tmp));

                }

            }

        }


        void GenerateLoopMap(ObservableCollection<RobotActionBase> itemsSource)
        {
            Stack<int> loop_stack = new Stack<int>();

            List<int> exists = new List<int>();

            int loopStartpos = 0;
            int loopEndpos = 0;


            loop_map.Clear();


            for (int i = 0; i < itemsSource.Count; i++)
            {
                if (itemsSource[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.Loop
                    || itemsSource[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.EndLoop)
                {
                    loop_stack.Push(i);
                }
                if (itemsSource[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.EndLoop)
                {
                    if (loop_stack != null)
                    {
                        if (loop_stack.Count >= 2)
                        {
                            loopEndpos = loop_stack.Pop();
                            loopStartpos = loop_stack.Pop();
                        }


                    }

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

        void GenerateLoopConditionMap(ObservableCollection<RobotActionBase> itemsSource)
        {
            Stack<int> loop_stack = new Stack<int>();

            List<int> exists = new List<int>();

            int loopStartpos = 0;
            int loopEndpos = 0;


            loopCondition_map.Clear();


            for (int i = 0; i < itemsSource.Count; i++)
            {
                if (itemsSource[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.LoopCondition
                    || itemsSource[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.LoopConditionEnd)
                {
                    loop_stack.Push(i);
                }
                if (itemsSource[i].ActionType == RobotDesigner.Enums.RobotActionsEnum.LoopConditionEnd)
                {
                    if (loop_stack.Count >= 2)
                    {
                        loopEndpos = loop_stack.Pop();
                        loopStartpos = loop_stack.Pop();
                    }


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



        #endregion // listView_Drop

        #endregion // Event Handling Methods

        #region Private Helpers

        #region CanStartDragOperation

        bool CanStartDragOperation
        {
            get
            {
                if (Mouse.LeftButton != MouseButtonState.Pressed)
                    return false;

                if (!this.canInitiateDrag)
                    return false;

                if (this.indexToSelect == -1)
                    return false;

                if (!this.HasCursorLeftDragThreshold)
                    return false;

                return true;
            }
        }

        #endregion // CanStartDragOperation

        #region FinishDragOperation

        void FinishDragOperation(ListViewItem draggedItem, AdornerLayer adornerLayer)
        {
            // Let the ListViewItem know that it is not being dragged anymore.
            ListViewItemDragState.SetIsBeingDragged(draggedItem, false);

            this.IsDragInProgress = false;

            if (this.ItemUnderDragCursor != null)
                this.ItemUnderDragCursor = null;

            // Remove the drag adorner from the adorner layer.
            if (adornerLayer != null)
            {
                adornerLayer.Remove(this.dragAdorner);
                this.dragAdorner = null;
            }
        }

        #endregion // FinishDragOperation

        #region GetListViewItem

        ListViewItem GetListViewItem(int index)
        {
            if (this.listView.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                return null;

            return this.listView.ItemContainerGenerator.ContainerFromIndex(index) as ListViewItem;
        }

        ListViewItem GetListViewItem(ItemType dataItem)
        {
            if (this.listView.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                return null;

            return this.listView.ItemContainerGenerator.ContainerFromItem(dataItem) as ListViewItem;
        }

        #endregion // GetListViewItem

        #region HasCursorLeftDragThreshold

        bool HasCursorLeftDragThreshold
        {
            get
            {
                if (this.indexToSelect < 0)
                    return false;

                ListViewItem item = this.GetListViewItem(this.indexToSelect);
                Rect bounds = VisualTreeHelper.GetDescendantBounds(item);
                Point ptInItem = this.listView.TranslatePoint(this.ptMouseDown, item);

                // In case the cursor is at the very top or bottom of the ListViewItem
                // we want to make the vertical threshold very small so that dragging
                // over an adjacent item does not select it.
                double topOffset = Math.Abs(ptInItem.Y);
                double btmOffset = Math.Abs(bounds.Height - ptInItem.Y);
                double vertOffset = Math.Min(topOffset, btmOffset);

                double width = SystemParameters.MinimumHorizontalDragDistance * 2;
                double height = Math.Min(SystemParameters.MinimumVerticalDragDistance, vertOffset) * 2;
                Size szThreshold = new Size(width, height);

                Rect rect = new Rect(this.ptMouseDown, szThreshold);
                rect.Offset(szThreshold.Width / -2, szThreshold.Height / -2);
                Point ptInListView = MouseUtilities.GetMousePosition(this.listView);
                return !rect.Contains(ptInListView);
            }
        }

        #endregion // HasCursorLeftDragThreshold

        #region IndexUnderDragCursor

        /// <summary>
        /// Returns the index of the ListViewItem underneath the
        /// drag cursor, or -1 if the cursor is not over an item.
        /// </summary>
        int IndexUnderDragCursor
        {
            get
            {
                int index = -1;
                for (int i = 0; i < this.listView.Items.Count; ++i)
                {
                    ListViewItem item = this.GetListViewItem(i);
                    if (this.IsMouseOver(item))
                    {
                        index = i;
                        break;
                    }
                }
                return index;
            }
        }

        #endregion // IndexUnderDragCursor

        #region InitializeAdornerLayer

        AdornerLayer InitializeAdornerLayer(ListViewItem itemToDrag)
        {
            // Create a brush which will paint the ListViewItem onto
            // a visual in the adorner layer.
            VisualBrush brush = new VisualBrush(itemToDrag);

            // Create an element which displays the source item while it is dragged.
            this.dragAdorner = new DragAdorner(this.listView, itemToDrag.RenderSize, brush);

            // Set the drag adorner's opacity.		
            this.dragAdorner.Opacity = this.DragAdornerOpacity;

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(this.listView);
            layer.Add(dragAdorner);

            // Save the location of the cursor when the left mouse button was pressed.
            this.ptMouseDown = MouseUtilities.GetMousePosition(this.listView);

            return layer;
        }

        #endregion // InitializeAdornerLayer

        #region InitializeDragOperation

        void InitializeDragOperation(ListViewItem itemToDrag)
        {
            // Set some flags used during the drag operation.
            this.IsDragInProgress = true;
            this.canInitiateDrag = false;

            // Let the ListViewItem know that it is being dragged.
            ListViewItemDragState.SetIsBeingDragged(itemToDrag, true);
        }

        #endregion // InitializeDragOperation

        #region IsMouseOver

        bool IsMouseOver(Visual target)
        {
            // We need to use MouseUtilities to figure out the cursor
            // coordinates because, during a drag-drop operation, the WPF
            // mechanisms for getting the coordinates behave strangely.

            Rect bounds = new Rect();
            Point mousePos = new Point();

            //if (target != null)
            {
                bounds = VisualTreeHelper.GetDescendantBounds(target);
                mousePos = MouseUtilities.GetMousePosition(target);
            }

            return bounds.Contains(mousePos);
        }

        #endregion // IsMouseOver

        #region IsMouseOverScrollbar

        /// <summary>
        /// Returns true if the mouse cursor is over a scrollbar in the ListView.
        /// </summary>
        bool IsMouseOverScrollbar
        {
            get
            {
                Point ptMouse = MouseUtilities.GetMousePosition(this.listView);
                HitTestResult res = VisualTreeHelper.HitTest(this.listView, ptMouse);
                if (res == null)
                    return false;

                DependencyObject depObj = res.VisualHit;
                while (depObj != null)
                {
                    if (depObj is ScrollBar)
                        return true;

                    // VisualTreeHelper works with objects of type Visual or Visual3D.
                    // If the current object is not derived from Visual or Visual3D,
                    // then use the LogicalTreeHelper to find the parent element.
                    if (depObj is Visual || depObj is System.Windows.Media.Media3D.Visual3D)
                        depObj = VisualTreeHelper.GetParent(depObj);
                    else
                        depObj = LogicalTreeHelper.GetParent(depObj);
                }

                return false;
            }
        }

        #endregion // IsMouseOverScrollbar

        #region ItemUnderDragCursor

        ItemType ItemUnderDragCursor
        {
            get { return this.itemUnderDragCursor; }
            set
            {
                if (this.itemUnderDragCursor == value)
                    return;

                // The first pass handles the previous item under the cursor.
                // The second pass handles the new one.
                for (int i = 0; i < 2; ++i)
                {
                    if (i == 1)
                        this.itemUnderDragCursor = value;

                    if (this.itemUnderDragCursor != null)
                    {
                        ListViewItem listViewItem = this.GetListViewItem(this.itemUnderDragCursor);
                        if (listViewItem != null)
                            ListViewItemDragState.SetIsUnderDragCursor(listViewItem, i == 1);
                    }
                }
            }
        }

        #endregion // ItemUnderDragCursor

        #region PerformDragOperation

        void PerformDragOperation()
        {
            try
            {


                ItemType selectedItem = this.listView.SelectedItem as ItemType;
                DragDropEffects allowedEffects = DragDropEffects.Move | DragDropEffects.Move | DragDropEffects.Link;
                if (DragDrop.DoDragDrop(this.listView, selectedItem, allowedEffects) != DragDropEffects.None)
                {
                    // The item was dropped into a new location,
                    // so make it the new selected item.
                    this.listView.SelectedItem = selectedItem;
                }

            }
            catch (Exception)
            {


            }
        }

        #endregion // PerformDragOperation

        #region ShowDragAdornerResolved

        bool ShowDragAdornerResolved
        {
            get { return this.ShowDragAdorner && this.DragAdornerOpacity > 0.0; }
        }

        #endregion // ShowDragAdornerResolved

        #region UpdateDragAdornerLocation

        void UpdateDragAdornerLocation()
        {
            if (this.dragAdorner != null)
            {
                Point ptCursor = MouseUtilities.GetMousePosition(this.ListView);

                double left = ptCursor.X - this.ptMouseDown.X;

                // 4/13/2007 - Made the top offset relative to the item being dragged.
                ListViewItem itemBeingDragged = this.GetListViewItem(this.indexToSelect);
                Point itemLoc = itemBeingDragged.TranslatePoint(new Point(0, 0), this.ListView);
                double top = itemLoc.Y + ptCursor.Y - this.ptMouseDown.Y;

                this.dragAdorner.SetOffsets(left, top);
            }
        }

        #endregion // UpdateDragAdornerLocation

        #endregion // Private Helpers
    }

    #endregion // ListViewDragDropManager

    #region ListViewItemDragState

    /// <summary>
    /// Exposes attached properties used in conjunction with the ListViewDragDropManager class.
    /// Those properties can be used to allow triggers to modify the appearance of ListViewItems
    /// in a ListView during a drag-drop operation.
    /// </summary>
    public static class ListViewItemDragState
    {
        #region IsBeingDragged

        /// <summary>
        /// Identifies the ListViewItemDragState's IsBeingDragged attached property.  
        /// This field is read-only.
        /// </summary>
        public static readonly DependencyProperty IsBeingDraggedProperty =
            DependencyProperty.RegisterAttached(
                "IsBeingDragged",
                typeof(bool),
                typeof(ListViewItemDragState),
                new UIPropertyMetadata(false));

        /// <summary>
        /// Returns true if the specified ListViewItem is being dragged, else false.
        /// </summary>
        /// <param name="item">The ListViewItem to check.</param>
        public static bool GetIsBeingDragged(ListViewItem item)
        {
            return (bool)item.GetValue(IsBeingDraggedProperty);
        }

        /// <summary>
        /// Sets the IsBeingDragged attached property for the specified ListViewItem.
        /// </summary>
        /// <param name="item">The ListViewItem to set the property on.</param>
        /// <param name="value">Pass true if the element is being dragged, else false.</param>
        internal static void SetIsBeingDragged(ListViewItem item, bool value)
        {
            item.SetValue(IsBeingDraggedProperty, value);
        }

        #endregion // IsBeingDragged

        #region IsUnderDragCursor

        /// <summary>
        /// Identifies the ListViewItemDragState's IsUnderDragCursor attached property.  
        /// This field is read-only.
        /// </summary>
        public static readonly DependencyProperty IsUnderDragCursorProperty =
            DependencyProperty.RegisterAttached(
                "IsUnderDragCursor",
                typeof(bool),
                typeof(ListViewItemDragState),
                new UIPropertyMetadata(false));

        /// <summary>
        /// Returns true if the specified ListViewItem is currently underneath the cursor 
        /// during a drag-drop operation, else false.
        /// </summary>
        /// <param name="item">The ListViewItem to check.</param>
        public static bool GetIsUnderDragCursor(ListViewItem item)
        {
            return (bool)item.GetValue(IsUnderDragCursorProperty);
        }

        /// <summary>
        /// Sets the IsUnderDragCursor attached property for the specified ListViewItem.
        /// </summary>
        /// <param name="item">The ListViewItem to set the property on.</param>
        /// <param name="value">Pass true if the element is underneath the drag cursor, else false.</param>
        internal static void SetIsUnderDragCursor(ListViewItem item, bool value)
        {
            item.SetValue(IsUnderDragCursorProperty, value);
        }

        #endregion // IsUnderDragCursor
    }

    #endregion // ListViewItemDragState

    #region ProcessDropEventArgs

    /// <summary>
    /// Event arguments used by the ListViewDragDropManager.ProcessDrop event.
    /// </summary>
    /// <typeparam name="ItemType">The type of data object being dropped.</typeparam>
    public class ProcessDropEventArgs<ItemType> : EventArgs where ItemType : class
    {
        #region Data

        ObservableCollection<ItemType> itemsSource;
        ItemType dataItem;
        int oldIndex;
        int newIndex;
        DragDropEffects allowedEffects = DragDropEffects.None;
        DragDropEffects effects = DragDropEffects.None;

        #endregion // Data

        #region Constructor

        internal ProcessDropEventArgs(
            ObservableCollection<ItemType> itemsSource,
            ItemType dataItem,
            int oldIndex,
            int newIndex,
            DragDropEffects allowedEffects)
        {
            this.itemsSource = itemsSource;
            this.dataItem = dataItem;
            this.oldIndex = oldIndex;
            this.newIndex = newIndex;
            this.allowedEffects = allowedEffects;
        }

        #endregion // Constructor

        #region Public Properties

        /// <summary>
        /// The items source of the ListView where the drop occurred.
        /// </summary>
        public ObservableCollection<ItemType> ItemsSource
        {
            get { return this.itemsSource; }
        }

        /// <summary>
        /// The data object which was dropped.
        /// </summary>
        public ItemType DataItem
        {
            get { return this.dataItem; }
        }

        /// <summary>
        /// The current index of the data item being dropped, in the ItemsSource collection.
        /// </summary>
        public int OldIndex
        {
            get { return this.oldIndex; }
        }

        /// <summary>
        /// The target index of the data item being dropped, in the ItemsSource collection.
        /// </summary>
        public int NewIndex
        {
            get { return this.newIndex; }
        }

        /// <summary>
        /// The drag drop effects allowed to be performed.
        /// </summary>
        public DragDropEffects AllowedEffects
        {
            get { return allowedEffects; }
        }

        /// <summary>
        /// The drag drop effect(s) performed on the dropped item.
        /// </summary>
        public DragDropEffects Effects
        {
            get { return effects; }
            set { effects = value; }
        }

        #endregion // Public Properties
    }

    #endregion // ProcessDropEventArgs
}

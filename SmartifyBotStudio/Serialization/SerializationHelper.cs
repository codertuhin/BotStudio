using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.TaskModel.File;
using System.Collections.ObjectModel;

namespace SmartifyBotStudio.Serialization
{


    /// <summary>
    /// Helper class used for XML serialization.
    /// Contains array of SerializedGraphicsBase instances.
    /// </summary>

    [XmlRoot("RobotDesignerActions")]
    public class SerializationHelper
    {
        RobotActionBase[] robotActions;

        /// <summary>
        /// Default constructor is XML serialization requirement.
        /// </summary>
        public SerializationHelper()
        {

        }

        /// <summary>
        /// This constructor is used for serialization.
        /// VisualCollection contains Graphics* instances.
        /// Every Graphics instance creates SerializedGraphics*
        /// instance which is added to graphics array.
        /// </summary>
        public SerializationHelper(ObservableCollection<RobotActionBase> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            robotActions = new RobotActionBase[collection.Count];

            int i = 0;

            foreach (RobotActionBase g in collection)
            {
                robotActions[i++] = g;
            }
        }

        /// <summary>
        /// When class is serialized, robotActions array is filled in the constructor
        /// and saved to XML file.
        /// When class is deserialized, robotActions array is loaded from XML file
        /// </summary>

        [XmlArrayItem(typeof(CopyFiles))]
        public RobotActionBase[] RobotActions
        {
            get { return robotActions; }
            set { robotActions = value; }
        }
    }
}



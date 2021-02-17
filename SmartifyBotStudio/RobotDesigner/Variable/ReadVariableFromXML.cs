using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SmartifyBotStudio.RobotDesigner.Variable
{
    public static class ReadVariableFromXML
    {
        public static void Read(string filename)
        {

            filename = GetXMLFileName(filename);

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



            
        }

        public static string GetXMLFileName(string filename)
        {
            string tempXmlFileName = System.IO.Path.GetFileNameWithoutExtension(filename);
            string tempXmlFilePath = System.IO.Path.GetDirectoryName(filename);
            string XmlFileFullPath = tempXmlFilePath + tempXmlFileName + ".xml";

            return XmlFileFullPath;

        }

    }
}

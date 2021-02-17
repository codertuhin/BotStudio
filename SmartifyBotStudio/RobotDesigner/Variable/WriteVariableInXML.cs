using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SmartifyBotStudio.RobotDesigner.Variable
{
    public static class WriteVariableInXML
    {
        public static void Write(string filename)
        {
            filename = GetXMLFileName(filename);

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


                writer.WriteEndDocument();
            }



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

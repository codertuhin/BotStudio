using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SmartifyBotStudio.RobotDesigner.Variable
{
    [Serializable]
    public class HtmlElementInfo
    {
        public string Html_element_id { get; set; }
        public string Html_element_name { get; set; }
        public string Html_element_tag { get; set; }
        public string Html_element_input_text { get; set; }
        public int Html_drop_down_index { get; set; }
        public string Html_element_class { get; set; }
        public string Html_element_XPATH { get; set; }

    }
}

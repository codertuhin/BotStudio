
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Ofir_Shtainfeld
{
    public class UI_Channel_Display : I_UI_Channel_Display
    {
        public string Description
        {
            get; set;
        }

        public int ID
        {
            get; set;
        }

        public byte[] Image
        {
            get; set;
        }

        public bool IsConnected
        {
            get; set;
        }

        public bool IsInDB
        {
            get; set;
        }

        public bool IsRecording
        {
            get; set;
        }

        public List<Status> LastAlert
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }
    }

}
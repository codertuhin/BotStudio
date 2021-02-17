
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ofir_Shtainfeld
{
    public class Status : I_Status
    {
        public AlertTypes AlertType
        {
            get; set;
        }

        public DateTime Date
        {
            get; set;
        }

        public string OriginOfStatus
        {
            get; set;
        }
    }

}
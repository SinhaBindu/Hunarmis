using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hunarmis.Models
{
    public class JsonResponseData
    {
        public string StatusType { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
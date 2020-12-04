using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PGproject.ViewModels
{
    public class POQResp
    {
        public Int64 token { get; set; }
        public string online_url { get; set; }
        public string mobile_url { get; set; }
        public int code { get; set; }
        public string message { get; set; }

        public POQResp()
        {
            code = 0;
            message = string.Empty;
        }
    }
}
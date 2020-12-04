using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PGproject.ViewModels
{
    public class POQSuccessResp
    {
        public string pgcode { get; set; }
        public string cid { get; set; }
        public string uid { get; set; }
        public int amount { get; set; }
        public string product { get; set; }
        public string returnUrl { get; set; }
        public string callbackUrl { get; set; }
        public string product_name { get; set; }
    }
}
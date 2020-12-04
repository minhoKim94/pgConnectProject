using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PGproject.ViewModels
{
    public class PayResult
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public long order_no { get; set; }
        public string service_name { get; set; }
        public string product_name { get; set; }

        public string custom_parameter { get; set; }
        public string tid { get; set; }
        public string cid { get; set; }
        public decimal amount { get; set; }
        public string pay_info { get; set; }

        public string pgcode { get; set; }
        public string billkey { get; set; }
        public string domestic_flag { get; set; }
        public string transaction_date { get; set; }
        public string card_info { get; set; }

        public string payhash { get; set; }
        public string installmonth { get; set; }
        public string nonsettleamt { get; set; }
    }
}
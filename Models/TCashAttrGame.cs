
namespace PGproject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TCashAttrGame
    {
        public string CashAttrCode { get; set; }
        public string GameCode { get; set; }
        public string UseFlag { get; set; }
        public string AdminID { get; set; }
        public System.DateTime RegDate { get; set; }
        public Nullable<System.DateTime> UpdDate { get; set; }
    }
}

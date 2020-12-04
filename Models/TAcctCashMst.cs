namespace PGproject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TAcctCashMst
    {
        public byte SiteCode { get; set; }
        public int UserNo { get; set; }
        public string CashAttrCode { get; set; }
        public decimal CashAmt { get; set; }
        public decimal TotalInCashAmt { get; set; }
        public decimal TotalOutCashAmt { get; set; }
        public System.DateTime RegDate { get; set; }
        public Nullable<System.DateTime> UpdDate { get; set; }
    }
}

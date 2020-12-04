
namespace PGproject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TCashMst
    {
        public long CashNo { get; set; }
        public int SeqNo { get; set; }
        public byte SiteCode { get; set; }
        public int UserNo { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string CashAttrCode { get; set; }
        public byte PayToolCode { get; set; }
        public string PGCode { get; set; }
        public string MallID { get; set; }
        public string CurrencyCode { get; set; }
        public decimal PayAmt { get; set; }
        public decimal VATAmt { get; set; }
        public decimal CashAmt { get; set; }
        public decimal RemainCashAmt { get; set; }
        public string PayToolName { get; set; }
        public Nullable<long> OrderNo { get; set; }
        public string TID { get; set; }
        public string CID { get; set; }
        public System.DateTime PayYMD { get; set; }
        public System.DateTime PGPayYMD { get; set; }
        public Nullable<System.DateTime> CnlYMD { get; set; }
        public Nullable<System.DateTime> PGCnlYMD { get; set; }
        public System.DateTime CashExpireYMD { get; set; }
        public byte PayTypeCode { get; set; }
        public string PayTypeVal { get; set; }
        public Nullable<long> OrgCashNo { get; set; }
        public string CnlFlag { get; set; }
        public string IPAddr { get; set; }
        public string AccessCountryCode { get; set; }
        public System.DateTime RegDate { get; set; }
        public Nullable<System.DateTime> UpdDate { get; set; }
    }
}

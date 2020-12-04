
namespace PGproject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TCashAttrMgmt
    {
        public string CashAttrCode { get; set; }
        public byte CashTypeCode { get; set; }
        public string CashAttrDesc { get; set; }
        public byte UseSort { get; set; }
        public string SalesCalcFlag { get; set; }
        public string CPSettleFlag { get; set; }
        public string AllCashFlag { get; set; }
        public string CashPresentAvailFlag { get; set; }
        public string RefundAvailFlag { get; set; }
        public string PayLimitSetFlag { get; set; }
        public string CashExpireSetFlag { get; set; }
        public Nullable<byte> CashExpireTypeCode { get; set; }
        public int CashExpirePeriod { get; set; }
        public string UseFlag { get; set; }
        public string AdminID { get; set; }
        public System.DateTime RegDate { get; set; }
        public Nullable<System.DateTime> UpdDate { get; set; }
    }
}

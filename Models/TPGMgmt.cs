//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 템플릿에서 생성되었습니다.
//
//     이 파일을 수동으로 변경하면 응용 프로그램에서 예기치 않은 동작이 발생할 수 있습니다.
//     이 파일을 수동으로 변경하면 코드가 다시 생성될 때 변경 내용을 덮어씁니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PGproject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TPGMgmt
    {
        public byte PayToolCode { get; set; }
        public string PGCode { get; set; }
        public string MallID { get; set; }
        public string PGDesc { get; set; }
        public string MICName { get; set; }
        public string MICPhoneNo { get; set; }
        public string MICEmailAddr { get; set; }
        public string PGURL { get; set; }
        public string DupTIDAllowFlag { get; set; }
        public string CashReceiptPubFlag { get; set; }
        public string PayCnlAvailFlag { get; set; }
        public Nullable<byte> PayCnlAvailTypeCode { get; set; }
        public Nullable<short> PayCnlAvailDay { get; set; }
        public string DisplayFlag { get; set; }
        public Nullable<byte> DisplaySort { get; set; }
        public string PGImageURL { get; set; }
        public string UseFlag { get; set; }
        public string AdminID { get; set; }
        public System.DateTime RegDate { get; set; }
        public Nullable<System.DateTime> UpdDate { get; set; }
    }
}

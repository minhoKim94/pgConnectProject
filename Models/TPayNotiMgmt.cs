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
    
    public partial class TPayNotiMgmt
    {
        public long SeqNo { get; set; }
        public long CashNo { get; set; }
        public string NotiMsg { get; set; }
        public byte NotiTypeCode { get; set; }
        public int TryCnt { get; set; }
        public byte TryStateCode { get; set; }
        public Nullable<int> NotiRsltCode { get; set; }
        public string NotiRsltMsg { get; set; }
        public string EtcMsg { get; set; }
        public System.DateTime RegDate { get; set; }
        public Nullable<System.DateTime> UpdDate { get; set; }
    }
}

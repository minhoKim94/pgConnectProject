namespace PGproject.ViewModels
{
    public class POQCashLog
    {
        public long order_no { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string pgcode { get; set; }
        public string client_id { get; set; }

        public decimal amount { get; set; }
        public decimal tax_amount { get; set; }
    }
}
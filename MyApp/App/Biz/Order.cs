namespace MyApp.App.Biz
{
    public class Order
    {
        public int orderId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string addrLine1 { get; set; }
        public string addrLine2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public DateTime orderDate { get; set; }
    }
}

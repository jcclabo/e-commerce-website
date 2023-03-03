namespace MyApp.App.Biz
{
    public class OrderLine
    {
        public int orderLineId { get; set; }
        public int orderId { get; set; }
        public int productId { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public decimal unitPrice { get; set; }
        public DateTime orderDate { get; set; }
    }
}

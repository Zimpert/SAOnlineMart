namespace SAOnlineMart.Models
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public required Order Order { get; set; }
        public int ProductID { get; set; }
        public required Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}

namespace SAOnlineMart.Models
{
    public class SellerOrderItemViewModel
    {
        public int OrderID { get; set; }
        public required string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public required string BuyerAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public required string Status { get; set; }
    }
}

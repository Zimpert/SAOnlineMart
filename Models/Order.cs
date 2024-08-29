namespace SAOnlineMart.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public required User User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public required string Status { get; set; }
        public required ICollection<OrderItem> OrderItems { get; set; }
    }
}

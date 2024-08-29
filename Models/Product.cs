namespace SAOnlineMart.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public required string ImageURL { get; set; }
    }
}

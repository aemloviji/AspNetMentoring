namespace Module4.Client.Console.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public Category Category { get; set; }
    }
}


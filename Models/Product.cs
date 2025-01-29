namespace StripeBlazorApp.Models
{
    public class Product
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; } // Ensure decimal for currency
    }
}

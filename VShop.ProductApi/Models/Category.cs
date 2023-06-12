namespace VShop.ProductApi.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string? ExternalId { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ICollection<Product>? Products { get; set; }
}
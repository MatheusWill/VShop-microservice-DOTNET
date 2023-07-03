using System.ComponentModel.DataAnnotations;

namespace VShop.Web.Models;

public class ProductViewModel
{
    public int Id { get; set; }

    public string? ExternalId { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public long Stock { get; set; }

    [Required]
    public string? ImageURL { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CategoryName { get; set; }

    [Display(Name = "Categorias")]
    public int CategoryId { get; set; }
}

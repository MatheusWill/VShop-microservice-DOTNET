using System.ComponentModel.DataAnnotations;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.DTOs;

public class ProductDTO
{
    public int Id { get; set; }

    public string? ExternalId { get; set; }

    [Required(ErrorMessage = "THE_NAME_IS_REQUIRED")]
    [MinLength(3)]
    [MaxLength(100)]
    public string? Name { get; set; }

    [Required(ErrorMessage = "THE_PRICE_IS_REQUIRED")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "THE_DESCRIPTION_IS_REQUIRED")]
    [MinLength(5)]
    [MaxLength(200)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "THE_STOCK_IS_REQUIRED")]
    [Range(1, 9999)]
    public long Stock { get; set; }

    public string? ImageURL { get; set; }

    public DateTime CreatedAt { get; set; }

    public Category? Category { get; set; }

    public int CategoryId { get; set; }
}

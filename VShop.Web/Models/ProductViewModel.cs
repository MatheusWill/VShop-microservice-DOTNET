﻿namespace VShop.Web.Models;

public class ProductViewModel
{
    public int Id { get; set; }

    public string? ExternalId { get; set; }

    public string? Name { get; set; }

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public long Stock { get; set; }

    public string? ImageURL { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CategoryName { get; set; }

    public int CategoryId { get; set; }
}

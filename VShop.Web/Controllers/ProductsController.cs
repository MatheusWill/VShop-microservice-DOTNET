﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
    {
        var result = await _productService.GetAllProducts();

        if (result is null) return View("Error");

        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        ViewBag.CategoryId = new SelectList(await
             _categoryService.GetAllCategories(), "CategoryId", "Name");

        return View();
    }
}

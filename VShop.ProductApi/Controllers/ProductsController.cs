using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Services;

namespace VShop.ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
    {
        var productsDto = await _productService.GetProducts();

        if (productsDto == null) return NotFound("PRODUCTS_NOT_FOUND");

        return Ok(productsDto);
    }

    [HttpGet("{id}", Name = "GetProducts")]
    public async Task<ActionResult<ProductDTO>> Get(int id)
    {
        var productDto = await _productService.GetProductById(id);

        if (productDto == null) return NotFound("PRODUCTS_NOT_FOUND");

        return Ok(productDto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductDTO productDto)
    {
        if (productDto is null) return BadRequest("INVALID_DATA");

        await _productService.AddProduct(productDto);
        return new CreatedAtRouteResult("GetProducts", new { id = productDto.CategoryId }, productDto);
    }

    [HttpPut("{ id : int }")]
    public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDto)
    {
        if (id != productDto.CategoryId) return BadRequest("INVALID_ID");

        if (productDto is null) return BadRequest("INVALID_DATA");

        await _productService.UpdateProduct(productDto);

        return Ok(productDto);
    }

    [HttpDelete("{ id : int }")]
    public async Task<ActionResult<ProductDTO>> Delete(int id)
    {
        var productDto = await _productService.GetProductById(id);

        if (productDto is null) return NotFound("PRODUCT_NOT_FOUND");

        await _productService.RemoveProduct(id);

        return Ok(productDto);
    }
}

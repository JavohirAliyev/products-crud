using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAllProducts()
    {
        var products = await productService.GetAllProductsAsync();
        return Ok(products.Select(p => new ProductReadDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            CreatedAt = p.CreatedAt,
            ModifiedAt = p.ModifiedAt,
            Status = p.Status
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductReadDto>> GetProductById(Guid id)
    {
        var product = await productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();

        return Ok(new ProductReadDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            CreatedAt = product.CreatedAt,
            ModifiedAt = product.ModifiedAt,
            Status = product.Status
        });
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(ProductCreateDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Status = dto.Status,
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now,
            ProductDetail = new ProductDetail()
        };

        var createdProduct = await productService.CreateProductAsync(product);

        return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id } );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductUpdateDto>> UpdateProduct(Guid id, ProductUpdateDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Status = dto.Status
        };

        var updatedProduct = await productService.UpdateProductAsync(id, product);
        if (updatedProduct == null) return NotFound();

        return Ok(new ProductReadDto
        {
            Id = updatedProduct.Id,
            Name = updatedProduct.Name,
            Price = updatedProduct.Price,
            CreatedAt = updatedProduct.CreatedAt,
            ModifiedAt = updatedProduct.ModifiedAt,
            Status = updatedProduct.Status
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        var deleted = await productService.DeleteProductAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}
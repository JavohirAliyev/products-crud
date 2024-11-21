using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductDetailController(IProductDetailService productDetailService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductReadDto>> GetProductDetailById(Guid id)
    {
        var detail = await productDetailService.GetDetailsByProductIdAsync(id);
        if (detail == null) return NotFound();

        return Ok(new ProductDetailReadDto
        {
            Id = detail.Id,
            Color = detail.Color,
            CountryOfOrigin = detail.CountryOfOrigin,
            Description = detail.Description,
            ExpiryDate = detail.ExpiryDate,
            ManufactureDate = detail.ManufactureDate,
            Manufacturer = detail.Manufacturer,
            Material = detail.Material,
            QuantityInStock = detail.QuantityInStock,
            Size = detail.Size,
            Weight = detail.Weight
        });
    }
    
    [HttpPost]
    public async Task<ActionResult<ProductDetailCreateDto>> CreateProductDetail(ProductDetailCreateDto dto)
    {
        var detail = new ProductDetail
        {
            Color = dto.Color!,
            CountryOfOrigin = dto.CountryOfOrigin!,
            Description = dto.Description!,
            ExpiryDate = dto.ExpiryDate,
            ManufactureDate = dto.ManufactureDate,
            Manufacturer = dto.Manufacturer!,
            Material = dto.Material!,
            Weight = dto.Weight,
            Size = dto.Size,
            QuantityInStock = dto.QuantityInStock
        };

        var createdProduct = await productDetailService.CreateProductDetailAsync(dto.ProductId, detail);

        return CreatedAtAction(nameof(GetProductDetailById), new { id = createdProduct.Id } );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductReadDto>> UpdateProduct(Guid id, ProductDetailUpdateDto dto)
    {
        var detail = new ProductDetail
        {
            Color = dto.Color!,
            CountryOfOrigin = dto.CountryOfOrigin!,
            Description = dto.Description!,
            ExpiryDate = dto.ExpiryDate,
            ManufactureDate = dto.ManufactureDate,
            Manufacturer = dto.Manufacturer!,
            Material = dto.Material!,
            Weight = dto.Weight,
            Size = dto.Size,
            QuantityInStock = dto.QuantityInStock
        };

        var updatedProduct = await productDetailService.UpdateProductDetailAsync(id, detail);
        if (updatedProduct == null) return NotFound();

        return Ok(GetProductDetailById(id));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProductDetail(Guid id)
    {
        var deleted = await productDetailService.DeleteProductDetailAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}
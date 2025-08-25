using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository repo) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var result = await repo.GetProductsAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")] //api/products/1
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.AddProduct(product);

        if (await repo.SaveChangesAsync())
        {
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }
        return BadRequest("Unable to create product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !ProductExists(id))
            return BadRequest("Cannot update this Product");

        repo.UpdateProduct(product);
        if (await repo.SaveChangesAsync())
        {
            return NoContent();
        }
        return BadRequest("Unable to update the product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repo.GetProductByIdAsync(id);
        if (product == null)
            return NotFound();
            
        repo.DeleteProduct(product);
        if (await repo.SaveChangesAsync())
        {
            return NoContent();
        }
        return BadRequest("Unable to delete the product");
    }

    private bool ProductExists(int id)
    {
        return repo.ProductExists(id);
    }

}

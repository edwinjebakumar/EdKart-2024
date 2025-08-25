using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext storeContext) : IProductRepository
{
    public void AddProduct(Product product)
    {
        storeContext.Products.Add(product);
    }

    public void DeleteProduct(Product product)
    {
        storeContext.Products.Remove(product);
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await storeContext.Products.FindAsync(id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await storeContext.Products.ToListAsync();
    }

    public bool ProductExists(int id)
    {
        return storeContext.Products.Any(x => x.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await storeContext.SaveChangesAsync() > 0;
    }

    public void UpdateProduct(Product product)
    {
        storeContext.Entry(product).State = EntityState.Modified;
    }
}

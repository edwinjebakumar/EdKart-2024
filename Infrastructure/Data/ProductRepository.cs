using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext storeContext) : IProductRepository
{
    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort)
    {
        var query = storeContext.Products.AsQueryable();
        if (!string.IsNullOrEmpty(brand))
            query = query.Where(p => p.Brand == brand);
        if (!string.IsNullOrEmpty(type))
            query = query.Where(p => p.Type == type);

        query = sort switch
        {
            "priceAsc" => query.OrderBy(p => p.Price),
            "priceDesc" => query.OrderByDescending(p => p.Price),
            _ => query.OrderBy(x => x.Name)
        };


        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        return await storeContext.Products.Select(p => p.Brand).Distinct().ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        return await storeContext.Products.Select(p => p.Type).Distinct().ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await storeContext.Products.FindAsync(id);
    }

    public void AddProduct(Product product)
    {
        storeContext.Products.Add(product);
    }

    public void UpdateProduct(Product product)
    {
        storeContext.Entry(product).State = EntityState.Modified;
    }

    public void DeleteProduct(Product product)
    {
        storeContext.Products.Remove(product);
    }

    public bool ProductExists(int id)
    {
        return storeContext.Products.Any(x => x.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await storeContext.SaveChangesAsync() > 0;
    }
}

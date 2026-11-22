using LearningUnitTest.Examples._10_Moq.Models;
using LearningUnitTest.Examples.Moq10.Data;
using Microsoft.EntityFrameworkCore;

namespace LearningUnitTest.Examples.Moq10.Services;

public class ProductService(ProductDbContext context)
{
    public async Task<Product?> GetProductByIdAsync(int id) => await context.Products.FirstOrDefaultAsync(x => x.Id == id);
    

    public async Task<Product> AddProductAsync(Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();

        return product;
    }
}
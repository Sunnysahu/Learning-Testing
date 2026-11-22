using LearningUnitTest.Examples.Moq09.Data;
using LearningUnitTest.Examples.Moq09.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningUnitTest.Examples.Moq09.Services;

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
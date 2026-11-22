using LearningUnitTest.Examples.Moq09.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningUnitTest.Examples.Moq09.Data;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    public virtual DbSet<Product> Products => Set<Product>();
}
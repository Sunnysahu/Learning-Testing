using LearningUnitTest.Examples._10_Moq.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningUnitTest.Examples.Moq10.Data;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    public virtual DbSet<Product> Products => Set<Product>();
}
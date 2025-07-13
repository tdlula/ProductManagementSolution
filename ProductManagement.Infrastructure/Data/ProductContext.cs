using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Infrastructure.Data
{
    /// <summary>
    /// EF Core database context for the Product Management system.
    /// </summary>
    public class ProductContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by a DbContext.</param>
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options) { }

        /// <summary>
        /// Gets or sets the Products table.
        /// </summary>
        public DbSet<Product> Products { get; set; }
    }
}

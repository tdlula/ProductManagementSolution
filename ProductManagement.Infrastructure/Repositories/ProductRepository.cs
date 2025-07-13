using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Data;
using ProductManagement.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for Product entity using Entity Framework Core.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="context">Injected EF Core database context.</param>
        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.ToListAsync();

        /// <inheritdoc/>
        public async Task<Product> GetByIdAsync(int id) => await _context.Products.FindAsync(id);

        /// <inheritdoc/>
        public async Task<Product> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}

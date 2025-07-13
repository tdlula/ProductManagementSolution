using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Interfaces;
using ProductManager.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManager.Application.Services
{
    /// <summary>
    /// Business logic implementation for products.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="productRepository">Injected product repository.</param>
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Product>> GetAllAsync() => await _productRepository.GetAllAsync();

        /// <inheritdoc/>
        public async Task<Product> GetByIdAsync(int id) => await _productRepository.GetByIdAsync(id);

        /// <inheritdoc/>
        public async Task<Product> AddAsync(Product product) => await _productRepository.AddAsync(product);

        /// <inheritdoc/>
        public async Task UpdateAsync(Product product) => await _productRepository.UpdateAsync(product);

        /// <inheritdoc/>
        public async Task DeleteAsync(int id) => await _productRepository.DeleteAsync(id);
    }
}

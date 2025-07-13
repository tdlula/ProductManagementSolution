using ProductManagement.Domain.Entities;

namespace ProductManager.Application.Interfaces
{
    /// <summary>
    /// Contract for product business operations.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>Enumerable of Product entities.</returns>
        Task<IEnumerable<Product>> GetAllAsync();

        /// <summary>
        /// Gets a product by its ID.
        /// </summary>
        /// <param name="id">Product ID.</param>
        /// <returns>Product entity if found, otherwise null.</returns>
        Task<Product> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="product">Product entity to add.</param>
        /// <returns>Added Product entity.</returns>
        Task<Product> AddAsync(Product product);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="product">Product entity to update.</param>
        Task UpdateAsync(Product product);

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">Product ID.</param>
        Task DeleteAsync(int id);
    }
}

using ProductManagement.Domain.Entities;

namespace ProductManagement.Infrastructure.Interfaces
{
    /// <summary>
    /// Contract for product data operations (repository pattern).
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Gets all products from the data store.
        /// </summary>
        /// <returns>Enumerable of Product entities.</returns>
        Task<IEnumerable<Product>> GetAllAsync();

        /// <summary>
        /// Gets a product by its ID from the data store.
        /// </summary>
        /// <param name="id">Product ID.</param>
        /// <returns>Product entity if found, otherwise null.</returns>
        Task<Product> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new product to the data store.
        /// </summary>
        /// <param name="product">Product entity to add.</param>
        /// <returns>Added Product entity.</returns>
        Task<Product> AddAsync(Product product);

        /// <summary>
        /// Updates an existing product in the data store.
        /// </summary>
        /// <param name="product">Product entity to update.</param>
        Task UpdateAsync(Product product);

        /// <summary>
        /// Deletes a product by its ID from the data store.
        /// </summary>
        /// <param name="id">Product ID.</param>
        Task DeleteAsync(int id);
    }
}

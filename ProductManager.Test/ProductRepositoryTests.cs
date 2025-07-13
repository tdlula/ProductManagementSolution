using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Data;
using ProductManagement.Infrastructure.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.Test
{
    /// <summary>
    /// Integration tests for ProductRepository using in-memory EF Core context.
    /// </summary>
    public class ProductRepositoryTests
    {
        private ProductContext _context;
        private ProductRepository _repository;

        /// <summary>
        /// Sets up a new in-memory database and repository before each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _context = new ProductContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _repository = new ProductRepository(_context);
        }

        /// <summary>
        /// Disposes the in-memory database after each test.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
        }

        /// <summary>
        /// Tests that a product can be added and retrieved by ID.
        /// </summary>
        [Test]
        public async Task AddAndGetByIdAsync_Works()
        {
            var product = new Product { Name = "RepoTest", Description = "Desc", Price = 5 };
            var added = await _repository.AddAsync(product);
            var fetched = await _repository.GetByIdAsync(added.ProductID);
            Assert.That(fetched.Name, Is.EqualTo("RepoTest"));
        }

        /// <summary>
        /// Tests that all products can be retrieved.
        /// </summary>
        [Test]
        public async Task GetAllAsync_ReturnsAll()
        {
            await _repository.AddAsync(new Product { Name = "A", Description = "D1", Price = 1 });
            await _repository.AddAsync(new Product { Name = "B", Description = "D2", Price = 2 });
            var all = await _repository.GetAllAsync();
            Assert.That(all.Count(), Is.EqualTo(2));
        }

        /// <summary>
        /// Tests that a product can be updated.
        /// </summary>
        [Test]
        public async Task UpdateAsync_UpdatesProduct()
        {
            var product = await _repository.AddAsync(new Product { Name = "Old", Description = "D", Price = 1 });
            product.Name = "New";
            await _repository.UpdateAsync(product);
            var updated = await _repository.GetByIdAsync(product.ProductID);
            Assert.That(updated.Name, Is.EqualTo("New"));
        }

        /// <summary>
        /// Tests that a product can be deleted.
        /// </summary>
        [Test]
        public async Task DeleteAsync_RemovesProduct()
        {
            var product = await _repository.AddAsync(new Product { Name = "Del", Description = "D", Price = 1 });
            await _repository.DeleteAsync(product.ProductID);
            var deleted = await _repository.GetByIdAsync(product.ProductID);
            Assert.That(deleted, Is.Null);
        }
    }
}

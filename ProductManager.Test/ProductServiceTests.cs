using Moq;
using NUnit.Framework;
using ProductManager.Application.Services;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManager.Test
{
    /// <summary>
    /// Unit tests for ProductService business logic using a mocked repository.
    /// </summary>
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _mockRepo;
        private ProductService _service;

        /// <summary>
        /// Sets up a new ProductService with a mocked repository before each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IProductRepository>();
            _service = new ProductService(_mockRepo.Object);
        }

        /// <summary>
        /// Tests that GetAllAsync returns all products.
        /// </summary>
        [Test]
        public async Task GetAllAsync_ReturnsAllProducts()
        {
            // Arrange
            var products = new List<Product> {
                new Product { ProductID = 1, Name = "A", Description = "D1", Price = 10 },
                new Product { ProductID = 2, Name = "B", Description = "D2", Price = 20 }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.That(result, Is.EquivalentTo(products));
        }

        /// <summary>
        /// Tests that GetAllAsync returns an empty list when there are no products.
        /// </summary>
        [Test]
        public async Task GetAllAsync_ReturnsEmpty_WhenNoProducts()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Product>());

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.That(result, Is.Empty);
        }

        /// <summary>
        /// Tests that GetByIdAsync returns a product when it exists.
        /// </summary>
        [Test]
        public async Task GetByIdAsync_ReturnsProduct_WhenExists()
        {
            // Arrange
            var product = new Product { ProductID = 1, Name = "A", Description = "D1", Price = 10 };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.That(result, Is.EqualTo(product));
        }

        /// <summary>
        /// Tests that GetByIdAsync returns null when the product does not exist.
        /// </summary>
        [Test]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Product)null);

            // Act
            var result = await _service.GetByIdAsync(99);

            // Assert
            Assert.That(result, Is.Null);
        }

        /// <summary>
        /// Tests that AddAsync calls the repository and returns the product.
        /// </summary>
        [Test]
        public async Task AddAsync_CallsRepositoryAndReturnsProduct()
        {
            // Arrange
            var product = new Product { Name = "A", Description = "D1", Price = 10 };
            _mockRepo.Setup(r => r.AddAsync(product)).ReturnsAsync(product);

            // Act
            var result = await _service.AddAsync(product);

            // Assert
            Assert.That(result, Is.EqualTo(product));
        }

        /// <summary>
        /// Tests that AddAsync throws an exception when the repository fails.
        /// </summary>
        [Test]
        public void AddAsync_ThrowsException_WhenRepositoryFails()
        {
            // Arrange
            var product = new Product { Name = "A", Description = "D1", Price = 10 };
            _mockRepo.Setup(r => r.AddAsync(product)).ThrowsAsync(new System.Exception("DB error"));

            // Act & Assert
            Assert.ThrowsAsync<System.Exception>(async () => await _service.AddAsync(product));
        }

        /// <summary>
        /// Tests that UpdateAsync calls the repository.
        /// </summary>
        [Test]
        public async Task UpdateAsync_CallsRepository()
        {
            // Arrange
            var product = new Product { ProductID = 1, Name = "A", Description = "D1", Price = 10 };
            _mockRepo.Setup(r => r.UpdateAsync(product)).Returns(Task.CompletedTask).Verifiable();

            // Act
            await _service.UpdateAsync(product);

            // Assert
            _mockRepo.Verify(r => r.UpdateAsync(product), Times.Once);
        }

        /// <summary>
        /// Tests that UpdateAsync throws an exception when the repository fails.
        /// </summary>
        [Test]
        public void UpdateAsync_ThrowsException_WhenRepositoryFails()
        {
            // Arrange
            var product = new Product { ProductID = 1, Name = "A", Description = "D1", Price = 10 };
            _mockRepo.Setup(r => r.UpdateAsync(product)).ThrowsAsync(new System.Exception("Update failed"));

            // Act & Assert
            Assert.ThrowsAsync<System.Exception>(async () => await _service.UpdateAsync(product));
        }

        /// <summary>
        /// Tests that DeleteAsync calls the repository.
        /// </summary>
        [Test]
        public async Task DeleteAsync_CallsRepository()
        {
            // Arrange
            _mockRepo.Setup(r => r.DeleteAsync(1)).Returns(Task.CompletedTask).Verifiable();

            // Act
            await _service.DeleteAsync(1);

            // Assert
            _mockRepo.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        /// <summary>
        /// Tests that DeleteAsync throws an exception when the repository fails.
        /// </summary>
        [Test]
        public void DeleteAsync_ThrowsException_WhenRepositoryFails()
        {
            // Arrange
            _mockRepo.Setup(r => r.DeleteAsync(1)).ThrowsAsync(new System.Exception("Delete failed"));

            // Act & Assert
            Assert.ThrowsAsync<System.Exception>(async () => await _service.DeleteAsync(1));
        }
    }
}

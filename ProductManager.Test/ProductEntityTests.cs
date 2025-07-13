using NUnit.Framework;
using ProductManagement.Domain.Entities;

namespace ProductManager.Test
{
    /// <summary>
    /// Unit tests for the Product entity.
    /// </summary>
    public class ProductEntityTests
    {
        /// <summary>
        /// Verifies that Product properties can be set and retrieved.
        /// </summary>
        [Test]
        public void Can_Create_And_Modify_Product_Properties()
        {
            var product = new Product();
            product.ProductID = 1;
            product.Name = "Test";
            product.Description = "Desc";
            product.Price = 9.99m;

            Assert.That(product.ProductID, Is.EqualTo(1));
            Assert.That(product.Name, Is.EqualTo("Test"));
            Assert.That(product.Description, Is.EqualTo("Desc"));
            Assert.That(product.Price, Is.EqualTo(9.99m));
        }
    }
}

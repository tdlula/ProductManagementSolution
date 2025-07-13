namespace ProductManagement.Domain.Entities
{
    /// <summary>
    /// Represents a product in the system.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int ProductID { get; set; }  // Primary Key

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }    // Product Name

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; } // Product Description

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }  // Product Price
    }
}

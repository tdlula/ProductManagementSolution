namespace ProductManagement.API.DTOs
{
    /// <summary>
    /// Data Transfer Object for Product (for API requests/responses)
    /// </summary>
    public class ProductDto
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    /// <summary>
    /// DTO for creating a product (no ProductID)
    /// </summary>
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    /// <summary>
    /// DTO for updating a product
    /// </summary>
    public class UpdateProductDto
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using ProductManager.Application.Interfaces;
using ProductManagement.API.DTOs;
using ProductManagement.Domain.Entities;

namespace ProductManagement.API.Controllers
{
    /// <summary>
    /// Handles HTTP requests for Products.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Constructor for ProductsController.
        /// </summary>
        /// <param name="productService">Injected product service.</param>
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>List of ProductDto.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _productService.GetAllAsync();
            // Map domain entities to DTOs
            var dtos = products.Select(p => new ProductDto
            {
                ProductID = p.ProductID,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            });
            return Ok(dtos);
        }

        /// <summary>
        /// Gets a product by its ID.
        /// </summary>
        /// <param name="id">Product ID.</param>
        /// <returns>ProductDto if found, otherwise 404.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            var dto = new ProductDto
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
            return Ok(dto);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="createDto">DTO for product creation.</param>
        /// <returns>Created ProductDto.</returns>
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create(CreateProductDto createDto)
        {
            var product = new Product
            {
                Name = createDto.Name,
                Description = createDto.Description,
                Price = createDto.Price
            };
            var created = await _productService.AddAsync(product);
            var dto = new ProductDto
            {
                ProductID = created.ProductID,
                Name = created.Name,
                Description = created.Description,
                Price = created.Price
            };
            return CreatedAtAction(nameof(GetById), new { id = dto.ProductID }, dto);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">Product ID.</param>
        /// <param name="updateDto">DTO for product update.</param>
        /// <returns>No content if successful, otherwise 400 if ID mismatch.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductDto updateDto)
        {
            if (id != updateDto.ProductID) return BadRequest();
            var product = new Product
            {
                ProductID = updateDto.ProductID,
                Name = updateDto.Name,
                Description = updateDto.Description,
                Price = updateDto.Price
            };
            await _productService.UpdateAsync(product);
            return NoContent();
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">Product ID.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}

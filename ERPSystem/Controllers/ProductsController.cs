using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ERPSystem.Models;
using ERPSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly InventoryService _inventoryService; 
        public ProductsController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _inventoryService.GetProductsAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _inventoryService.GetProductAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet]
        [Route("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(int categoryId)
        {
            var products = await _inventoryService.GetProductsByCategoryAsync(categoryId);

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            product = await _inventoryService.CreateProductAsync(product);
            if (product == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var isSuccessStatusCode = await _inventoryService.UpdateProductAsync(id, product);

            if (isSuccessStatusCode)
            {
                return NoContent();
            }
            return BadRequest();
        }

        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _inventoryService.DeleteProductAsync(id);
            if (product == null)
            {
                return BadRequest();
            }

            return product;
        }

    }
}

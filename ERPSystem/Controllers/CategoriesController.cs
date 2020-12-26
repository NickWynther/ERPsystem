using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CategoriesController : ControllerBase
    {
        private readonly InventoryService _inventoryService;
        public CategoriesController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _inventoryService.GetCategoriesAsync();
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _inventoryService.GetCategoryAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            category = await _inventoryService.CreateCategoryAsync(category);
            if (category == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            var isSuccessStatusCode = await _inventoryService.UpdateCategoryAsync(id, category);

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
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            var category = await _inventoryService.DeleteCategoryAsync(id);
            if (category == null)
            {
                return BadRequest();
            }

            return category;
        }
    }
}

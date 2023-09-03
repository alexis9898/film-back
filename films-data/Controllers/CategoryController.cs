using BLL.Model;
using BLL.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoriesService;

        public CategoryController(ICategoryService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _categoriesService.GetCategories();
                return Ok(categories);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneCategory([FromRoute]int id)
        {
            try
            {
                var catedory=await _categoriesService.GetOneCategory(id);
                if(catedory==null)
                    return NotFound();
                return Ok(catedory);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("categories-by-filmId/{id}")]
        public async Task<IActionResult> GetCategoriesByFilmId([FromRoute] int id)
        {
            try
            {
                var catedories = await _categoriesService.GetCategoriesByFilmId(id);
                if (catedories == null)
                    return NotFound();
                return Ok(catedories);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST 
        [HttpPost("")]
        public async Task<IActionResult> addNewCategory([FromBody] CategoryModel categoryModel)
        {
            try
            {
                var newCategory= await _categoriesService.addCategory(categoryModel);
                return Ok(newCategory);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT 
        [HttpPut("{id}")]
        public async Task<IActionResult> updateCategory([FromRoute]int id, [FromBody] CategoryModel categoryModel)
        {
            try
            {
               var updateCategory= await _categoriesService.UpdateCategoryAsync(id, categoryModel);
                if (updateCategory == null)
                    return NotFound();
                return Ok(updateCategory);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //patch
        [HttpPatch("{id}")]
        public async Task<IActionResult> updateCategoryPatch([FromRoute]int id, [FromBody] JsonPatchDocument categoryPatch)
        {
            try
            {
                var updateCategory = await _categoriesService.UpdateCategoryPatchAsync(id, categoryPatch);
                if (updateCategory == null)
                    return NotFound();
                return Ok(updateCategory);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            try
            {
                var prsDelete= await _categoriesService.deleteCategoryAsync(id);
                if(prsDelete == false)
                    return NoContent();
                return NotFound();
            }
            catch (Exception)
            {

                return BadRequest();
            }
            
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using eCommerce.Backoffice.Shared.Model.Products;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Backoffice.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class CategoriesController : ControllerBase
    {
        private readonly IDataService<eCommerce.Storefront.Model.Products.Category, int> _dataService;

        public CategoriesController(IDataService<eCommerce.Storefront.Model.Products.Category, int> dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IEnumerable<Category> GetCategories()
        {
            return _dataService.Get().Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        [HttpGet("{id}")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult<Category> GetCategory(int id)
        {
            var category = _dataService.Get(id);

            if (category == null)
            { 
                return NotFound();
            }

            return new Category { Id = category.Id, Name = category.Name };
        }

        [HttpPost]
        public ActionResult<Category> CreateCategory(Category category)
        {
            try
            {
                var c = _dataService.Create(new eCommerce.Storefront.Model.Products.Category { Id = category.Id, Name = category.Name });
                category.Id = c.Id;
            }
            catch (DbUpdateException ex)
            {
                if (ex?.InnerException?.Message != null)
                {
                    return BadRequest(ex?.InnerException?.Message);
                }
                else
                {
                    return BadRequest(ex?.Message);
                }
            }   

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            try
            {
                _dataService.Modify(new eCommerce.Storefront.Model.Products.Category { Id = category.Id, Name = category.Name });
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            catch (DbUpdateException ex)
            {
                if (ex?.InnerException?.Message != null)
                {
                    return BadRequest(ex?.InnerException?.Message);
                }
                else
                {
                    return BadRequest(ex?.Message);
                }
            }   

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                _dataService.Delete(id);
            }
            catch (DbUpdateException ex)
            {
                if (ex?.InnerException?.Message != null)
                {
                    return BadRequest(ex?.InnerException?.Message);
                }
                else
                {
                    return BadRequest(ex?.Message);
                }
            }   

            return NoContent();
        }
    }
}
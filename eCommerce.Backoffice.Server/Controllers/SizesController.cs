using System.Collections.Generic;
using System.Linq;
using eCommerce.Backoffice.Shared.Model.Products;
using eCommerce.Storefront.Model.Products;
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
    [IgnoreAntiforgeryToken]
    public class SizesController : ControllerBase
    {
        private readonly IDataService<ProductSize, int> _dataService;

        public SizesController(IDataService<ProductSize, int> dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IEnumerable<ProductSizeDto> GetSizes()
        {
            return _dataService.Get().Select(p => new ProductSizeDto
            {
                Id = p.Id,
                Name = p.Name
            });
        }

        [HttpGet("{id}")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult<ProductSizeDto> GetSize(int id)
        {
            var productSize = _dataService.Get(id);

            if (productSize == null)
            { 
                return NotFound();
            }

            return new ProductSizeDto { Id = productSize.Id, Name = productSize.Name };
        }

        [HttpPost]
        public ActionResult<ProductSizeDto> CreateSize(ProductSizeDto size)
        {
            try
            {
                var productSize = _dataService.Create(new ProductSize { Id = size.Id, Name = size.Name });
                size.Id = productSize.Id;
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

            return CreatedAtAction(nameof(GetSize), new { id = size.Id }, size);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSize(int id, ProductSizeDto size)
        {
            if (id != size.Id)
            {
                return BadRequest();
            }

            try
            {
                _dataService.Modify(new ProductSize { Id = size.Id, Name = size.Name });
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
        public IActionResult DeleteSize(int id)
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
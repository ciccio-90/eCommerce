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
    public class SizesController : ControllerBase
    {
        private readonly IDataService<eCommerce.Storefront.Model.Products.ProductSize, int> _dataService;

        public SizesController(IDataService<eCommerce.Storefront.Model.Products.ProductSize, int> dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IEnumerable<ProductSize> GetSizes()
        {
            return _dataService.Get().Select(p => new ProductSize
            {
                Id = p.Id,
                Name = p.Name
            });
        }

        [HttpGet("{id}")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult<ProductSize> GetSize(int id)
        {
            var productSize = _dataService.Get(id);

            if (productSize == null)
            { 
                return NotFound();
            }

            return new ProductSize { Id = productSize.Id, Name = productSize.Name };
        }

        [HttpPost]
        public ActionResult<ProductSize> CreateSize(ProductSize size)
        {
            try
            {
                var productSize = _dataService.Create(new eCommerce.Storefront.Model.Products.ProductSize { Id = size.Id, Name = size.Name });
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
        public IActionResult UpdateSize(int id, ProductSize size)
        {
            if (id != size.Id)
            {
                return BadRequest();
            }

            try
            {
                _dataService.Modify(new eCommerce.Storefront.Model.Products.ProductSize { Id = size.Id, Name = size.Name });
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
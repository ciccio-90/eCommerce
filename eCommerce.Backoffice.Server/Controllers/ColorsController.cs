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
    public class ColorsController : ControllerBase
    {
        private readonly IDataService<eCommerce.Storefront.Model.Products.ProductColor, int> _dataService;

        public ColorsController(IDataService<eCommerce.Storefront.Model.Products.ProductColor, int> dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IEnumerable<ProductColor> GetColors()
        {
            return _dataService.Get().Select(p => new ProductColor
            {
                Id = p.Id,
                Name = p.Name
            });
        }

        [HttpGet("{id}")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult<ProductColor> GetColor(int id)
        {
            var productColor = _dataService.Get(id);

            if (productColor == null)
            { 
                return NotFound();
            }

            return new ProductColor { Id = productColor.Id, Name = productColor.Name };
        }

        [HttpPost]
        public ActionResult<ProductColor> CreateColor(ProductColor color)
        {
            try
            {
                var productColor = _dataService.Create(new eCommerce.Storefront.Model.Products.ProductColor { Id = color.Id, Name = color.Name });
                color.Id = productColor.Id;
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

            return CreatedAtAction(nameof(GetColor), new { id = color.Id }, color);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateColor(int id, ProductColor color)
        {
            if (id != color.Id)
            {
                return BadRequest();
            }

            try
            {
                _dataService.Modify(new eCommerce.Storefront.Model.Products.ProductColor { Id = color.Id, Name = color.Name });
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
        public IActionResult DeleteColor(int id)
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
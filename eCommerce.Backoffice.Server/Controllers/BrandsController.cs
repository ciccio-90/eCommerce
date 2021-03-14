using System.Collections.Generic;
using System.Linq;
using eCommerce.Backoffice.Shared.Model.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Infrastructure.Services.Interfaces;

namespace eCommerce.Backoffice.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class BrandsController : ControllerBase
    {
        private readonly IDataService<eCommerce.Storefront.Model.Products.Brand, int> _dataService;

        public BrandsController(IDataService<eCommerce.Storefront.Model.Products.Brand, int> dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IEnumerable<Brand> GetBrands()
        {
            return _dataService.Get().Select(b => new Brand
            {
                Id = b.Id,
                Name = b.Name
            });
        }

        [HttpGet("{id}")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult<Brand> GetBrand(int id)
        {
            var brand = _dataService.Get(id);

            if (brand == null)
            { 
                return NotFound();
            }

            return new Brand { Id = brand.Id, Name = brand.Name };
        }

        [HttpPost]
        public ActionResult<Brand> CreateBrand(Brand brand)
        {
            try
            {
                var b = _dataService.Create(new eCommerce.Storefront.Model.Products.Brand { Id = brand.Id, Name = brand.Name });
                brand.Id = b.Id;
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

            return CreatedAtAction(nameof(GetBrand), new { id = brand.Id }, brand);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBrand(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }

            try
            {
                _dataService.Modify(new eCommerce.Storefront.Model.Products.Brand { Id = brand.Id, Name = brand.Name });
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
        public IActionResult DeleteBrand(int id)
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
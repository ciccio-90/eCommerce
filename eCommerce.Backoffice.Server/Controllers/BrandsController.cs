using System.Collections.Generic;
using System.Linq;
using eCommerce.Backoffice.Shared.Model.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Infrastructure.Services.Interfaces;
using eCommerce.Storefront.Model.Products;

namespace eCommerce.Backoffice.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [IgnoreAntiforgeryToken]
    public class BrandsController : ControllerBase
    {
        private readonly IDataService<Brand, long> _dataService;

        public BrandsController(IDataService<Brand, long> dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IEnumerable<BrandDto> GetBrands()
        {
            return _dataService.Get().Select(b => new BrandDto
            {
                Id = b.Id,
                Name = b.Name
            });
        }

        [HttpGet("{id}")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult<BrandDto> GetBrand(int id)
        {
            var brand = _dataService.Get(id);

            if (brand == null)
            { 
                return NotFound();
            }

            return new BrandDto { Id = brand.Id, Name = brand.Name };
        }

        [HttpPost]
        public ActionResult<BrandDto> CreateBrand(BrandDto brand)
        {
            try
            {
                var b = _dataService.Create(new Brand { Id = brand.Id, Name = brand.Name });
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
        public IActionResult UpdateBrand(int id, BrandDto brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }

            try
            {
                _dataService.Modify(new Brand { Id = brand.Id, Name = brand.Name });
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
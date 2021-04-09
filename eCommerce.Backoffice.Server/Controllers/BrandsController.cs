using System.Collections.Generic;
using System.Linq;
using eCommerce.Backoffice.Shared.Model.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using eCommerce.Storefront.Model.Products;
using Infrastructure.Domain;

namespace eCommerce.Backoffice.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [IgnoreAntiforgeryToken]
    public class BrandsController : ControllerBase
    {
        private readonly IEntityService<Brand, long> _brandService;

        public BrandsController(IEntityService<Brand, long> brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IEnumerable<BrandDto> GetBrands()
        {
            return _brandService.Get().Select(b => new BrandDto
            {
                Id = b.Id,
                Name = b.Name
            });
        }

        [HttpGet("{id}")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult<BrandDto> GetBrand(int id)
        {
            var brand = _brandService.Get(id);

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
                var b = _brandService.Create(new Brand { Id = brand.Id, Name = brand.Name });
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
                _brandService.Modify(new Brand { Id = brand.Id, Name = brand.Name });
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
                _brandService.Delete(id);
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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Backoffice.Shared.Model.Products;
using Infrastructure.Cqrs.Queries.Requests;
using Infrastructure.Cqrs.Commands.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Backoffice.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IEnumerable<Brand>> GetBrands()
        {
            return (await _mediator.Send(new GetRequest<eCommerce.Storefront.Model.Products.Brand, int>(null, null, null))).Select(b => new Brand
            {
                Id = b.Id,
                Name = b.Name
            });
        }

        [HttpGet("{id}")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            var brand = await _mediator.Send(new GetByIdRequest<eCommerce.Storefront.Model.Products.Brand, int>(id));

            if (brand == null)
            { 
                return NotFound();
            }

            return new Brand { Id = brand.Id, Name = brand.Name };
        }

        [HttpPost]
        public async Task<ActionResult<Brand>> CreateBrand(Brand brand)
        {
            try
            {
                var b = await _mediator.Send(new CreateRequest<eCommerce.Storefront.Model.Products.Brand, int>(new eCommerce.Storefront.Model.Products.Brand { Id = brand.Id, Name = brand.Name }));
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
        public async Task<IActionResult> UpdateBrand(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }

            try
            {
                await _mediator.Send(new ModifyRequest<eCommerce.Storefront.Model.Products.Brand, int>(new eCommerce.Storefront.Model.Products.Brand { Id = brand.Id, Name = brand.Name }));
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
        public async Task<IActionResult> DeleteBrand(int id)
        {            
            try
            {
                await _mediator.Send(new DeleteRequest<eCommerce.Storefront.Model.Products.Brand, int>(id));
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
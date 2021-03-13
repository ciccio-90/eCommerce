using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Backoffice.Shared.Model.Products;
using Infrastructure.Cqrs.Commands.Requests;
using Infrastructure.Cqrs.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Backoffice.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class SizesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SizesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IEnumerable<ProductSize>> GetSizes()
        {
            return (await _mediator.Send(new GetRequest<eCommerce.Storefront.Model.Products.ProductSize, int>(null, null, null))).Select(p => new ProductSize
            {
                Id = p.Id,
                Name = p.Name
            });
        }

        [HttpGet("{id}")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<ActionResult<ProductSize>> GetSize(int id)
        {
            var productSize = await _mediator.Send(new GetByIdRequest<eCommerce.Storefront.Model.Products.ProductSize, int>(id));

            if (productSize == null)
            { 
                return NotFound();
            }

            return new ProductSize { Id = productSize.Id, Name = productSize.Name };
        }

        [HttpPost]
        public async Task<ActionResult<ProductSize>> CreateSize(ProductSize size)
        {
            try
            {
                var productSize = await _mediator.Send(new CreateRequest<eCommerce.Storefront.Model.Products.ProductSize, int>(new eCommerce.Storefront.Model.Products.ProductSize { Id = size.Id, Name = size.Name }));
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
        public async Task<IActionResult> UpdateSize(int id, ProductSize size)
        {
            if (id != size.Id)
            {
                return BadRequest();
            }

            try
            {
                await _mediator.Send(new ModifyRequest<eCommerce.Storefront.Model.Products.ProductSize, int>(new eCommerce.Storefront.Model.Products.ProductSize { Id = size.Id, Name = size.Name }));
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
        public async Task<IActionResult> DeleteSize(int id)
        {
            try
            {
                await _mediator.Send(new DeleteRequest<eCommerce.Storefront.Model.Products.ProductSize, int>(id));
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
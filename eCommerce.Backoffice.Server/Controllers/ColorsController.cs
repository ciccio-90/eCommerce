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
    public class ColorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ColorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IEnumerable<ProductColor>> GetColors()
        {
            return (await _mediator.Send(new GetRequest<eCommerce.Storefront.Model.Products.ProductColor, int>(null, null, null))).Select(p => new ProductColor
            {
                Id = p.Id,
                Name = p.Name
            });
        }

        [HttpGet("{id}")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<ActionResult<ProductColor>> GetColor(int id)
        {
            var productColor = await _mediator.Send(new GetByIdRequest<eCommerce.Storefront.Model.Products.ProductColor, int>(id));

            if (productColor == null)
            { 
                return NotFound();
            }

            return new ProductColor { Id = productColor.Id, Name = productColor.Name };
        }

        [HttpPost]
        public async Task<ActionResult<ProductColor>> CreateColor(ProductColor color)
        {
            try
            {
                var productColor = await _mediator.Send(new CreateRequest<eCommerce.Storefront.Model.Products.ProductColor, int>(new eCommerce.Storefront.Model.Products.ProductColor { Id = color.Id, Name = color.Name }));
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
        public async Task<IActionResult> UpdateColor(int id, ProductColor color)
        {
            if (id != color.Id)
            {
                return BadRequest();
            }

            try
            {
                await _mediator.Send(new ModifyRequest<eCommerce.Storefront.Model.Products.ProductColor, int>(new eCommerce.Storefront.Model.Products.ProductColor { Id = color.Id, Name = color.Name }));
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
        public async Task<IActionResult> DeleteColor(int id)
        {
            try
            {
                await _mediator.Send(new DeleteRequest<eCommerce.Storefront.Model.Products.ProductColor, int>(id));
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
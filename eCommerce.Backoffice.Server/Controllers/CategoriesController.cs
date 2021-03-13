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
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return (await _mediator.Send(new GetRequest<eCommerce.Storefront.Model.Products.Category, int>(null, null, null))).Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        [HttpGet("{id}")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _mediator.Send(new GetByIdRequest<eCommerce.Storefront.Model.Products.Category, int>(id));

            if (category == null)
            { 
                return NotFound();
            }

            return new Category { Id = category.Id, Name = category.Name };
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            try
            {
                var c = await _mediator.Send(new CreateRequest<eCommerce.Storefront.Model.Products.Category, int>(new eCommerce.Storefront.Model.Products.Category { Id = category.Id, Name = category.Name }));
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
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            try
            {
                await _mediator.Send(new ModifyRequest<eCommerce.Storefront.Model.Products.Category, int>(new eCommerce.Storefront.Model.Products.Category { Id = category.Id, Name = category.Name }));
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
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _mediator.Send(new DeleteRequest<eCommerce.Storefront.Model.Products.Category, int>(id));
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
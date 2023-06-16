
using AspNetCoreHero.Boilerplate.API.Controllers;
using AspNetCoreHero.Boilerplate.Application.Features.Categories.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.Categories.Commands.Delete;
using AspNetCoreHero.Boilerplate.Application.Features.Categories.Commands.Update;
using AspNetCoreHero.Boilerplate.Application.Features.Categories.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Categories.Queries.GetById;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Api.Controllers.v1
{
    public class CategoryController : BaseApiController<CategoryController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Categories = await _mediator.Send(new GetAllCategoriesCachedQuery());
            return Ok(Categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Category = await _mediator.Send(new GetCategoryByIdQuery() { Id = id });
            return Ok(Category);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateCategoryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateCategoryCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteCategoryCommand { Id = id }));
        }
    }
}
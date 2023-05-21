using Core.DTOs.CategoryDTOs;
using Core.DTOs.UserDTOs;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddCategoryDTO model)
        {
            await categoryService.Add(model);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateCategoryDTO model)
        {
            await categoryService.Update(model);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await categoryService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await categoryService.GetById(id));
        }
    }
}

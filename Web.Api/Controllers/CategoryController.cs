using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetCategoriesById(Guid categoryId)
    {
        var category = await _categoryService.GetCategoryByIdAsync(categoryId);
        return Ok(category);
    }
}
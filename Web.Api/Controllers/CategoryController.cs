using Application.Dto.Category;
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

    [HttpGet]
    public async Task<IActionResult> GetCategoryByCategoryName(string categoryName)
    {
        var category = await _categoryService.GetCategoryByCategoryNameAsync(categoryName);

        return Ok(category);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoryNames()
    {
        var categoryNames = await _categoryService.GetCategoryNamesAsync();

        return Ok(categoryNames);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(CategoryDto categoryDto)
    {
        var newCategory = await _categoryService.AddCategoryAsync(categoryDto);

        return Ok(newCategory);
    }
}
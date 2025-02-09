using Application.IServices;
using Domain.CustomExceptions;
using Domain.Entity;
using Domain.IRepositories;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class CategoryService : ICategoryService

{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid categoryId)
    {
        if(categoryId == Guid.Empty) 
            throw new CategoryException($"category with {categoryId} not found",StatusCodes.Status404NotFound);
        
        var category = await _unitOfWork.CategoryRepository.GetCategoryById(categoryId)
         ?? throw new CategoryException($"category with {categoryId} not found",StatusCodes.Status404NotFound);
        
        return category;
    }
}
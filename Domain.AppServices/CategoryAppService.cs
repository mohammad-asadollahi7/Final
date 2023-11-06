
using Domain.Core.Contracts.AppServices;
using Domain.Core.Contracts.Services;
using Domain.Core.Dtos.Categories;
using Domain.Core.Dtos.Products;

namespace Domain.AppServices;


public class CategoryAppService : ICategoryAppService
{
    private readonly ICategoryService _categoryService;

    public CategoryAppService(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<CategoryDto> GetById(int categoryId,
                                           CancellationToken cancellationToken)
    {
        await _categoryService.EnsureExistById(categoryId, cancellationToken);
        return await _categoryService.GetById(categoryId, cancellationToken);
    }

    public async Task<List<CustomAttributeDto>> GetCustomAttributeTitlesByCategoryId(int categoryId,
                                                                                     CancellationToken cancellationToken)
    {
        return await _categoryService.GetCustomAttributeTitlesByCategoryId(categoryId, cancellationToken);
    }

    public async Task<List<CategoryTitleDto>> GetLeafCategoryTitles(CancellationToken cancellationToken)
    {
        return await _categoryService.GetLeafCategoryTitles(cancellationToken);
    }
}

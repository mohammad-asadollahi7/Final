
using Domain.Core.Dtos.Categories;
using Domain.Core.Dtos.Products;

namespace Domain.Core.Contracts.Services;

public interface ICategoryService
{
    Task<List<int>> GetSubcategoriesIdsByCategoryId(int categoryId,
                                                    CancellationToken cancellationToken);
    Task EnsureCategoryIsLeaf(int categoryId,
                              CancellationToken cancellationToken);
    Task<CategoryDto?> GetById(int categoryId,
                               CancellationToken cancellationToken);
    Task EnsureExistById(int categoryId,
                         CancellationToken cancellationToken);
    Task<List<CustomAttributeDto>> GetCustomAttributeTitlesByCategoryId(int categoryId,
                                                                        CancellationToken cancellationToken);

    Task<List<CategoryTitleDto>> GetLeafCategoryTitles(CancellationToken cancellationToken);
}
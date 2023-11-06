using Domain.Core.Dtos.Categories;
using Domain.Core.Dtos.Products;

namespace Domain.Core.Contracts.Repos;

public interface ICategoryRepository
{
    Task<List<int>> GetSubcategoryIdsByParentId(int categoryId,
                                                CancellationToken cancellationToken);

    Task<CategoryDto?> GetById(int categoryId,
                               CancellationToken cancellationToken);


    Task<List<CategoryTitleDto>> GetLeafCategoryTitles(CancellationToken cancellationToken);


    Task<bool> IsExistById(int categoryId,
                           CancellationToken cancellationToken);

    Task<List<CustomAttributeDto>> GetCustomAttributeTitlesByCategoryId(int categoryId,
                                                                        CancellationToken cancellationToken);
}

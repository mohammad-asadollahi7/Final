
using Domain.Core.Dtos.Categories;
using Domain.Core.Dtos.Products;

namespace Domain.Core.Contracts.AppServices;

public interface ICategoryAppService
{
    Task<CategoryDto> GetById(int categoryId,
                              CancellationToken cancellationToken);

    Task<List<CustomAttributeDto>> GetCustomAttributeTitlesByCategoryId(int categoryId,
                                                                        CancellationToken cancellationToken);

    Task<List<CategoryTitleDto>> GetLeafCategoryTitles(CancellationToken cancellationToken);
}

using Domain.Core.Contracts.Repos;
using Domain.Core.Contracts.Services;
using Domain.Core.Dtos.Categories;
using Domain.Core.Dtos.Products;
using Domain.Core.Exceptions;

namespace Domain.Services;


public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository) => _categoryRepository
                                                                          = categoryRepository;



    public async Task<List<int>> GetSubcategoriesIdsByCategoryId(int categoryId,
                                                                 CancellationToken cancellationToken)
    {
        var categoriesIds = await _categoryRepository.GetSubcategoryIdsByParentId(categoryId,
                                                                                  cancellationToken);
        if (categoriesIds.Count() == 0)
        {
            List<int> list = new List<int>() { categoryId };
            return list;
        }
        return categoriesIds;
    }

    public async Task EnsureCategoryIsLeaf(int categoryId,
                                           CancellationToken cancellationToken)
    {
        var categoriesIds = await _categoryRepository.GetSubcategoryIdsByParentId(categoryId,
                                                                                  cancellationToken);
        if (categoriesIds.Count() != 0)
            throw new AppException(ExpMessage.NotAllowAddProduct,
                                   ExpStatusCode.BadRequest);
    }


    public async Task EnsureExistById(int categoryId,
                                      CancellationToken cancellationToken)
    {
        var isExist = await _categoryRepository.IsExistById(categoryId, cancellationToken);

        if (!isExist)
            throw new AppException(ExpMessage.NotFoundCategory,
                                   ExpStatusCode.NotFound);
    }


    public async Task<CategoryDto?> GetById(int categoryId,
                                            CancellationToken cancellationToken)
    {
        return await _categoryRepository.GetById(categoryId, cancellationToken);
    }

    public async Task<List<CustomAttributeDto>> GetCustomAttributeTitlesByCategoryId(int categoryId,
                                                                                     CancellationToken cancellationToken)

    {
        var attributeTitles = await _categoryRepository.GetCustomAttributeTitlesByCategoryId
                                                        (categoryId, cancellationToken);

        if (attributeTitles.Count() == 0)
            throw new AppException(ExpMessage.HaveNotCustomAttribute,
                                   ExpStatusCode.NotFound);

        return attributeTitles;
    }

    public async Task<List<CategoryTitleDto>> GetLeafCategoryTitles(CancellationToken cancellationToken)
    {
        return await _categoryRepository.GetLeafCategoryTitles(cancellationToken);
    }
}
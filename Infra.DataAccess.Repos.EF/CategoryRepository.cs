
using Domain.Core.Contracts.Repos;
using Domain.Core.Dtos.Categories;
using Domain.Core.Dtos.Products;
using Domain.Core.Entities;
using Infra.Db.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infra.DataAccess.Repos.EF;

public class CategoryRepository : ICategoryRepository
{
    private readonly FinalContext _context;
    private readonly IMemoryCache _memoryCache;

    public CategoryRepository(FinalContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }



    public async Task<CategoryDto?> GetById(int categoryId,
                                            CancellationToken cancellationToken)
    {
        return await _context.Categories.Where(c => c.Id == categoryId)
                                 .Select(c => new CategoryDto()
                                 {
                                     Id = c.Id,
                                     Title = c.Title,
                                     ParentId = c.ParentId,
                                     PictureName = c.CategoryPicture.Picture.Name,
                                     SubCategories = _context.Categories
                                                     .Where(c => c.ParentId == categoryId)
                                                     .Select(sc => new SubCategoryDto()
                                                     {
                                                         Id = sc.Id,
                                                         Title = sc.Title

                                                     }).ToList(),
                                     ProductIds = c.Products.Select(p => p.Id),
                                     AttributeTitles = c.CustomAttributesTemplates
                                                       .Select(c => c.AttributeTitle),

                                 }).FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);
    }

    public async Task<bool> IsExistById(int categoryId,
                                        CancellationToken cancellationToken)
    {
        return await _context.Categories.AnyAsync(c => c.Id == categoryId, cancellationToken);
    }


    public async Task<List<CustomAttributeDto>> GetCustomAttributeTitlesByCategoryId(int categoryId,
                                                                                      CancellationToken cancellationToken)
    {

        List<CustomAttributeDto> attributeList = new List<CustomAttributeDto>();

        await GetCustomAttributeTitlesByCategoryId(categoryId,
                                                   attributeList,
                                                   cancellationToken);
        return attributeList;
    }




    public async Task<List<int>> GetSubcategoryIdsByParentId(int categoryId,
                                                             CancellationToken cancellationToken)
    {
        List<int> categoryIds = new List<int>();
        await GetSubcategoryIdsByParentId(categoryId, categoryIds, cancellationToken);
        return categoryIds;

    }


    public async Task<List<CategoryTitleDto>> GetLeafCategoryTitles(CancellationToken cancellationToken)
    {
        var allCategories = await GetAllCategories(cancellationToken);

        var leafCategories = allCategories.Except(from c1 in allCategories
                                                  join c2 in allCategories
                                                  on c1.Id equals c2.ParentId
                                                  select c1)
                                           .Select(a => new CategoryTitleDto
                                           {
                                               Id = a.Id,
                                               Title = a.Title
                                           });
        return leafCategories.ToList();
    }


    private async Task GetSubcategoryIdsByParentId(int categoryId,
                                                   List<int> categoryIds,
                                                   CancellationToken cancellationToken)
    {
        var categories = await GetAllCategories(cancellationToken);

        var ids = categories.Where(c => c.ParentId == categoryId)
                            .Select(c => c.Id).ToList();

        categoryIds.AddRange(ids);
        for (int i = 0; i < ids.Count(); i++)
        {
            await GetSubcategoryIdsByParentId(ids[i], categoryIds, cancellationToken);
        }
    }


    private async Task GetCustomAttributeTitlesByCategoryId(int categoryId,
                                                            List<CustomAttributeDto> attributeList,
                                                            CancellationToken cancellationToken)
    {
        var allCategories = await GetAllCategories(cancellationToken);
        var allAttributes = await GetAllCustomAttributes(cancellationToken);

        var attributes = allAttributes.Where(a => a.CategoryId == categoryId)
                            .Select(c => new CustomAttributeDto
                            {
                                Id = c.Id,
                                Title = c.AttributeTitle
                            });
        attributeList.AddRange(attributes);


        var parentIds = allCategories.Where(c => c.Id == categoryId).Select(c => c.ParentId).ToList();
        if (parentIds is not null)
        {
            foreach (var parentId in parentIds)
            {
                await GetCustomAttributeTitlesByCategoryId(parentId ?? 0, attributeList, cancellationToken);
            }
        }

    }

    private async Task<List<Category>> GetAllCategories(CancellationToken cancellationToken)
    {
        var categories = _memoryCache.Get<List<Category>>("categories");
        if (categories is null)
        {
            categories = await _context.Categories.ToListAsync(cancellationToken);
            _memoryCache.Set("categories", categories, DateTimeOffset.Now.AddDays(10));
        }
        return categories;
    }


    private async Task<List<CustomAttributesTemplate>> GetAllCustomAttributes(CancellationToken cancellationToken)
    {
        var attributes = _memoryCache.Get<List<CustomAttributesTemplate>>("attributes");
        if (attributes is null)
        {
            attributes = await _context.CustomAttributesTemplates.ToListAsync(cancellationToken);
            _memoryCache.Set("attributes", attributes, DateTimeOffset.Now.AddDays(10));
        }
        return attributes;
    }
}

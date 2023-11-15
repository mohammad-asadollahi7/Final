using Domain.Core.Contracts.AppServices;
using Domain.Core.Dtos.Categories;
using Domain.Core.Enums;
using Endpoint.API.CustomAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.API.Controllers;
public class CategoryController : BaseController
{
    private readonly ICategoryAppService _categoryAppService;

    public CategoryController(ICategoryAppService categoryAppService)
    {
        _categoryAppService = categoryAppService;
    }


    [HttpGet("GetById")]
    //[HaveAccess(Role.Admin, Role.Seller)]
    public async Task<IActionResult> GetById(int categoryId,
                                             CancellationToken cancellationToken)
    {
        var category = await _categoryAppService.GetById(categoryId, cancellationToken);
        return Ok(category);
    }


    [HttpGet("GetCustomAttributeTitles/{categoryId}")]
    //[HaveAccess(Role.Admin, Role.Seller)]
    public async Task<IActionResult> GetCustomAttributeTitlesByCategoryId(int categoryId,
                                                                          CancellationToken cancellationToken)
    {
        var attributes = await _categoryAppService.GetCustomAttributeTitlesByCategoryId(categoryId, cancellationToken);
        return Ok(attributes);
    }


    [HttpGet("GetLeafCategories")]
    //[AllowAnonymous]
    public async Task<List<CategoryTitleDto>> GetLeafCategoryTitles(CancellationToken cancellationToken)
    {
        return await _categoryAppService.GetLeafCategoryTitles(cancellationToken);
    }

}

using Endpoint.MVC.Controllers;
using Endpoint.MVC.Dtos.Categories;
using Endpoint.MVC.Dtos.Pictures;
using Endpoint.MVC.Dtos.Products;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;


namespace Endpoint.MVC.Areas.Admin.Controllers;


[Area("Admin")]
public class ProductController : BaseController
{
    private readonly IHostingEnvironment _hostingEnvironment;

    public ProductController(IHttpClientFactory httpClientFactory,
                             IHostingEnvironment hostingEnvironment) : 
                                                base(httpClientFactory)
    { 
        _hostingEnvironment = hostingEnvironment;
    }


    public async Task<IActionResult> GetAllByCategoryId(CancellationToken cancellationToken,
                                                        int id = 3)
    {
        var httpResponseMessage = await SendGetRequest($"Product/GetAllByCategoryId/{id}",
                                                                    cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var products = await httpResponseMessage.Content.ReadFromJsonAsync<List<ProductOutputDto>>();
        foreach (var product in products)
        {
            if (product.DiscountPercent != 0)
                product.Price *= Convert.ToDecimal((100 - product.DiscountPercent) * 0.01);
        }

        return View(products);
    }


    [HttpGet("Create")]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest("category/GetLeafCategories",
                                                        cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        var categoryTitles = await httpResponseMessage.Content.ReadFromJsonAsync<List<CategoryTitleDto>>();
        return View(categoryTitles);
    }



    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] CreateProductViewModel model,
                                            CancellationToken cancellationToken)
    {
        string uniqueFileName = string.Empty;
        string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "pictures");
        uniqueFileName = Guid.NewGuid().ToString() + model.Picture.FileName;
        string filePath = Path.Combine(uploadFolder, uniqueFileName);
        model.Picture.CopyTo(new FileStream(filePath, FileMode.Create));

        var createProductDto = new CreateProductDto()
        {
            PersianTitle = model.PersianTitle,
            EnglishTitle = model.EnglishTitle,
            Description = model.Description,
            FirstDiscount = model.FirstDiscount,
            FirstPrice = model.FirstPrice,
            CategoryId = model.CategoryId,
            FirstQuantity = model.FirstQuantity,
            CustomAttributes = model.CustomAttributes,
            PictureDto = new PictureDto() { PictureName = uniqueFileName }
        };

        var httpResponseMessage = await SendPostRequest("product/Create",
                                                        JsonConvert.SerializeObject(createProductDto),
                                                        cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction("GetAllByCategoryId");
    }




    [HttpGet("Update/{productId}")]
    public async Task<IActionResult> Update(int productId,
                                            CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest($"Product/GetById/{productId}",
                                                        cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        var product = await httpResponseMessage.Content.ReadFromJsonAsync<ProductDetailsDto>();
        var updateProductDto = new UpdateProductDto()
        {
            Id = product.Id,
            Description = product.Description,
            PersianTitle = product.PersianTitle,
            EnglishTitle = product.EnglishTitle,
            CustomAttributes = product.CustomAttributes
        };


        return View(updateProductDto);
    }


    [HttpPost("Update/{productId}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int productId,
                                            UpdateProductDto productDto,
                                            CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendPutRequest($"Product/Update/{productId}",
                                                       JsonConvert.SerializeObject(productDto),
                                                       cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction("GetAllByCategoryId");
    }


    public async Task<IActionResult> Delete(int productId,
                                            CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendDeleteRequest($"product/remove/{productId}",
                                                          cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction("GetAllByCategoryId");
    }

}


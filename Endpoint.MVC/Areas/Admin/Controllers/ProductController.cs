using Endpoint.MVC.Controllers;
using Endpoint.MVC.Dtos;
using Endpoint.MVC.Dtos.Categories;
using Endpoint.MVC.Dtos.Comment;
using Endpoint.MVC.Dtos.Enums;
using Endpoint.MVC.Dtos.Pictures;
using Endpoint.MVC.Dtos.Products;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Net.Http;
using System.Threading;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;


namespace Endpoint.MVC.Areas.Admin.Controllers;


[Area("Admin")]
public class ProductController : AdminBaseController
{
    private readonly IHostingEnvironment _hostingEnvironment;

    public ProductController(IHttpClientFactory httpClientFactory,
                             IHostingEnvironment hostingEnvironment) :
                                                base(httpClientFactory)
    {
        _hostingEnvironment = hostingEnvironment;
    }

    public IActionResult Index()
    {
        return View();
    }

    #region Get
    public async Task<IActionResult> GetWages(CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest($"Product/GetWages",
                                                        cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var wages = await httpResponseMessage.Content
                                              .ReadFromJsonAsync<List<WageDto>>();
        return View(wages);
    }

    public async Task<IActionResult> GetProductsForApprove(CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest($"Product/GetProductsForApprove",
                                                                    cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var products = await httpResponseMessage.Content
                                              .ReadFromJsonAsync<List<ProductOutputApprove>>();
        return View(products);
    }

    public async Task<IActionResult> GetProductById(int productId, SellType sellType,
                                                CancellationToken cancellationToken,
                                                bool? isApproved = null)
    {
        string url;
        if (sellType == SellType.NonAuction)
            url = $"Product/GetNonAuctionProductById/{productId}";
        else
            url = $"Product/GetAuctionProductById/{productId}";


        var httpResponseMessage = await SendGetRequest(url, cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var product = await httpResponseMessage.Content
                                         .ReadFromJsonAsync<ProductDetailsDto>();
        return View(product);
    }

    public async Task<IActionResult> GetAuctionById(int id, CancellationToken cancellationToken)
    {

        var httpResponseMessage = await SendGetRequest($"Product/GetAuctionProductById/{id}?isApproved=true",
                                                                            cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var product = await httpResponseMessage.Content
                                         .ReadFromJsonAsync<AuctionDetailsDto>();
        return View(product);
    }

    public async Task<IActionResult> GetAuctions(CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest($"Product/GetAuctions?isApproved=true",
                                                                    cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var products = await httpResponseMessage.Content.ReadFromJsonAsync<List<ProductOutputDto>>();
        return View(products);
    }

    public async Task<IActionResult> GetCommentsForApprove(CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest("Product/GetAllComments",
                                                            cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var comments = await httpResponseMessage.Content
                                         .ReadFromJsonAsync<List<CommentDto>>();
        return View(comments);

    }

    public async Task<IActionResult> GetNonAuctionsByCategoryId(CancellationToken cancellationToken,
                                                               int id = 1)
    {
        var httpResponseMessage = await SendGetRequest($"Product/GetNonAuctionsByCategoryId/{id}",
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
    public async Task<IActionResult> GetNonAuctionById(int id, CancellationToken cancellationToken)
    {

        var httpResponseMessage = await SendGetRequest($"Product/GetNonAuctionProductById/{id}?isApproved=true",
                                                                            cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var product = await httpResponseMessage.Content
                                         .ReadFromJsonAsync<ProductDetailsDto>();
        return View(product);
    }
    #endregion


    #region Update
  [HttpGet]
    public async Task<IActionResult> UpdateNonAuction(int id, CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest($"Product/GetNonAuctionProductById/{id}?isApproved=true",
                                                       cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var productDetails = await httpResponseMessage.Content
                                             .ReadFromJsonAsync<ProductDetailsDto>();
        var updateProductDto = new UpdateProductDto()
        {
            Id = productDetails.Id,
            CustomAttributes = productDetails.CustomAttributes,
            Description = productDetails.Description,
            EnglishTitle = productDetails.EnglishTitle,
            PersianTitle = productDetails.PersianTitle,
            Price = productDetails.Price ?? 0,
            Discount = productDetails.DiscountPercent ?? 0,
            BoothTitle = productDetails.BoothTitle,
            CategoryId = productDetails.CategoryId
        };
        return View(updateProductDto);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateNonAuction(UpdateProductDto updateProductDto,
                                            CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(updateProductDto);
        var httpResponseMessage = await SendPutRequest($"Product/UpdateNonAuction/{updateProductDto.Id}",
                                                      JsonConvert.SerializeObject(updateProductDto),
                                                      cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction(nameof(GetNonAuctionsByCategoryId),
                                new { id = updateProductDto.CategoryId });
    }

    [HttpGet]
    public async Task<IActionResult> UpdateAuction(int id, CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest($"Product/GetAuctionProductById/{id}?isApproved=true",
                                                       cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var productDetails = await httpResponseMessage.Content
                                             .ReadFromJsonAsync<AuctionDetailsDto>();

        return View(productDetails);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateAuction(AuctionDetailsDto updateProductDto,
                                            CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(updateProductDto);
        var httpResponseMessage = await SendPutRequest($"Product/UpdateAuction/{updateProductDto.Id}",
                                                      JsonConvert.SerializeObject(updateProductDto),
                                                      cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction(nameof(GetAuctions));
    }

    #endregion


    public async Task<IActionResult> ApproveProduct(int id, bool isApproved,
                                                    CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendPatchRequest($"Product/ApproveProduct/{id}/{isApproved}",
                                                            cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction(nameof(GetProductsForApprove));
    }


    public async Task<IActionResult> ApproveComment(int id, bool isApproved,
                                                  CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendPatchRequest($"Product/ApproveComment/{id}?isApproved={isApproved}",
                                                            cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction(nameof(GetCommentsForApprove));
    }




    public async Task<IActionResult> Delete(int productId, int categoryId,
                                           CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendDeleteRequest($"product/remove/{productId}",
                                                          cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction(nameof(GetNonAuctionsByCategoryId), new { id = categoryId });
    }





    public async Task<IActionResult> DeleteAuction(int productId,
                                                  CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendDeleteRequest($"product/remove/{productId}",
                                                          cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction(nameof(GetAuctions));
    }



    [HttpGet("CreateNonAuction")]
    public async Task<IActionResult> CreateNonAuction(CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest("CreateNonAuction/GetLeafCategories",
                                                        cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        var categoryTitles = await httpResponseMessage.Content
                                             .ReadFromJsonAsync<List<CategoryTitleDto>>();
        return View(categoryTitles);
    }

}


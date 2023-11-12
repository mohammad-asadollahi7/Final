using Endpoint.MVC.Controllers;
using Endpoint.MVC.Dtos.Booth;
using Endpoint.MVC.Dtos.Products;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Endpoint.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class BoothController : BaseController
{
    public BoothController(IHttpClientFactory httpClientFactory) 
                            : base(httpClientFactory){ }

    public async Task<IActionResult> GetByTitle(string title,
                                               CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest($"Booth/GetByTitle/{title}",
                                                                cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var booth = await httpResponseMessage.Content.ReadFromJsonAsync<BoothDto>();
        return View(booth);
    }


    public async Task<IActionResult> GetNonAuctionProducts(string title, 
                                                        CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest($"Booth/GetNonAuctionsByBoothTitle/{title}",
                                                                             cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var products = await httpResponseMessage.Content
                                         .ReadFromJsonAsync<List<ProductOutputDto>>();
        return View(products);
    }


    public async Task<IActionResult> GetNonAuctionById(int productId,
                                                       CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest($"Product/GetNonAuctionProductById/{productId}?isApproved=true",
                                                    cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var product = await httpResponseMessage.Content
                                             .ReadFromJsonAsync<ProductDetailsDto>();
        return View(product);
    }


    public async Task<IActionResult> Delete(int productId, string title,
                                           CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendDeleteRequest($"product/remove/{productId}",
                                                          cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction(nameof(GetNonAuctionProducts), new { title = title });
    }



    [HttpGet("UpdateNonAuction")]
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
            Price = productDetails.Price,
            Discount = productDetails.DiscountPercent??0,
            BoothTitle = productDetails.BoothTitle,
        };
        return View(updateProductDto);
    }



    [HttpPost("Update")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(UpdateProductDto updateProductDto, 
                                                     CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendPutRequest($"Product/UpdateNonAuction/{updateProductDto.Id}",
                                                      JsonConvert.SerializeObject(updateProductDto),
                                                      cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction(nameof(GetNonAuctionProducts), 
                                new { title = updateProductDto.BoothTitle });
    }

}



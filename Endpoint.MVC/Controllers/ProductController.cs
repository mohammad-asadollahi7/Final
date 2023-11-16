using Endpoint.MVC.Dtos.Products;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.MVC.Controllers;

public class ProductController : BaseController
{
    public ProductController(IHttpClientFactory httpClientFactory) 
                                         : base(httpClientFactory)
    {  }

    public IActionResult Index()
    {
        return View();
    }


    public async Task<IActionResult> GetNonAuctions(CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest("product/GetNonAuctionsByCategoryId/1", 
                                                        cancellationToken);

        if(!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        var products = await httpResponseMessage.Content
                                            .ReadFromJsonAsync<List<ProductOutputDto>>();

        return View(products);
    }



    public async Task<IActionResult> GetNonAuction(int id, 
                                            CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest($"product/GetNonAuctionProductById/{id}?isApproved=true",
                                                        cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        var product = await httpResponseMessage.Content
                                            .ReadFromJsonAsync<ProductDetailsDto>();

        return View(product);
    }



    //public async Task<IActionResult> GetAuctions(CancellationToken cancellationToken)
    //{

    //}




    //public async Task<IActionResult> GetAuction(int id, CancellationToken cancellationToken)
    //{

    //}


}

using Endpoint.MVC.Dtos.Comment;
using Endpoint.MVC.Dtos.Products;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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


    public async Task<IActionResult> GetNonAuctions(CancellationToken cancellationToken,
                                                    int categoryId = 1)
    {
        var httpResponseMessage = await SendGetRequest($"product/GetNonAuctionsByCategoryId/{categoryId}", 
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

    [HttpGet] 
    public IActionResult CreateComment(int productId)
    {
        return View(productId);
    }


    [HttpPost]
    public async Task<IActionResult> CreateComment(CreateCommentDto createCommentDto, 
                                                    CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendPostRequest($"product/CreateComment/",
                                                        JsonConvert.SerializeObject(createCommentDto),
                                                        cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        return RedirectToAction(nameof(GetNonAuctions));
    }

    
    //public async Task<IActionResult> GetAuctions(CancellationToken cancellationToken)
    //{

    //}




    //public async Task<IActionResult> GetAuction(int id, CancellationToken cancellationToken)
    //{

    //}


}
 
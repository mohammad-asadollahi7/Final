using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;
using Endpoint.MVC.Controllers;
using Microsoft.AspNetCore.Mvc;
using Endpoint.MVC.Dtos.Products;
using Newtonsoft.Json;

namespace Endpoint.MVC.Areas.Seller.Controllers;


[Area("Seller")]
public class ProductController : SellerBaseController
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


    public async Task<IActionResult> GetNonAuctions(CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest("Booth/GetNonAuctionsBySellerId",
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
            Price = productDetails.Price,
            Discount = productDetails.DiscountPercent ?? 0,
            BoothTitle = productDetails.BoothTitle,
            CategoryId = productDetails.CategoryId,

        };
        return View(updateProductDto);
    }


    [HttpPost]
    public async Task<IActionResult> UpdateNonAuction(UpdateProductDto updateProductDto,
                                                      CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendPutRequest($"Product/UpdateNonAuction/{updateProductDto.Id}",
                                                      JsonConvert.SerializeObject(updateProductDto),
                                                      cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction(nameof(GetNonAuctions));
    }



    //public async Task<IActionResult> GetAuctions(CancellationToken cancellationToken)
    //{

    //}


    //[HttpGet]
    //public async Task<IActionResult> CreateAuction(CancellationToken cancellationToken)
    //{
    //    return View();
    //}


    //[HttpPost]
    //public async Task<IActionResult> CreateAuction(CancellationToken cancellationToken)
    //{

    //}

    //[HttpGet]
    //public async Task<IActionResult> CreateNonAuction(CancellationToken cancellationToken)
    //{
    //    return View();
    //}


    //[HttpPost]
    //public async Task<IActionResult> CreateNonAuction(CancellationToken cancellationToken)
    //{

    //}






    //public async Task<IActionResult> GetAuctionById(int id, CancellationToken cancellationToken)
    //{

    //}



    //public async Task<IActionResult> GetNonAuctionById(int id, CancellationToken cancellationToken)
    //{

    //}


    //[HttpGet]
    //public async Task<IActionResult> UpdateAuction(int id, CancellationToken cancellationToken)
    //{

    //}

    //[HttpGet]
    //public async Task<IActionResult> UpdateAuction(CancellationToken cancellationToken)
    //{

    //}


    //[HttpGet]
    //public async Task<IActionResult> UpdateNonAuction(int id, CancellationToken cancellationToken)
    //{

    //}

    //[HttpGet]
    //public async Task<IActionResult> UpdateNonAuction(CancellationToken cancellationToken)
    //{

    //}

    //public async Task<IActionResult> DeleteAuction(int id, CancellationToken cancellationToken)
    //{

    //}

    //public async Task<IActionResult> DeleteNonAuction(int id, CancellationToken cancellationToken)
    //{

    //}

}

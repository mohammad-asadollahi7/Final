using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;
using Endpoint.MVC.Controllers;
using Microsoft.AspNetCore.Mvc;
using Endpoint.MVC.Dtos.Products;

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

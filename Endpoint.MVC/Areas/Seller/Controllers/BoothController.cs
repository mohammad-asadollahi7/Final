using Microsoft.AspNetCore.Mvc;

namespace Endpoint.MVC.Areas.Seller.Controllers;

[Area("Seller")]
public class BoothController : SellerBaseController
{
    public BoothController(IHttpClientFactory httpClientFactory)
                                    : base(httpClientFactory)
    { }

    public IActionResult Index()
    {
        return View();
    }

    //public async Task<IActionResult> Get(CancellationToken cancellationToken)
    //{

    //}

    //public async Task<IActionResult> Update(CancellationToken cancellationToken)
    //{

    //}

    //public async Task<IActionResult> Create(CancellationToken cancellationToken)
    //{

    //}


}

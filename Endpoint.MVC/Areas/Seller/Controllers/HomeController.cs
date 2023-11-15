using Microsoft.AspNetCore.Mvc;

namespace Endpoint.MVC.Areas.Seller.Controllers;

public class HomeController : SellerBaseController
{
    public HomeController(IHttpClientFactory httpClientFactory)
        : base(httpClientFactory)
    {  }


    public IActionResult Error()
    {
        ViewData["ErrorMessage"] = TempData["ErrorMessage"];
        return View();
    }
}

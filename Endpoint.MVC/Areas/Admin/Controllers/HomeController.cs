using Endpoint.MVC.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.MVC.Areas.Admin.Controllers;

public class HomeController : AdminBaseController
{
    public HomeController(IHttpClientFactory httpClientFactory)
        : base(httpClientFactory)
    { }

    [HttpGet("Error")]
    public IActionResult Error()
    {
        ViewData["ErrorMessage"] = TempData["ErrorMessage"];
        return View();
    }
}

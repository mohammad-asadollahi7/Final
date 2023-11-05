using Microsoft.AspNetCore.Mvc;

namespace Endpoint.MVC.Areas.Customer.Controllers;

public class HomeController : Controller
{

    [HttpGet("Error")]
    public IActionResult Error()
    {
        ViewData["ErrorMessage"] = TempData["ErrorMessage"];
        return View();
    }
}

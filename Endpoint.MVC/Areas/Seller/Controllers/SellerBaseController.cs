using Endpoint.MVC.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Endpoint.MVC.Areas.Seller.Controllers;

[Area("Seller")]
public class SellerBaseController : BaseController
{
    public SellerBaseController(IHttpClientFactory httpClientFactory)
                                : base(httpClientFactory)
    { }
  
    public override IActionResult RedirectToErrorPage(HttpResponseMessage httpResponseMessage)
    {
        if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
            TempData["ErrorMessage"] = "عدم دسترسی به محتوای جاری";

        else
        {
            var errorMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
            TempData["ErrorMessage"] = errorMessage;
        }
        return RedirectToAction("Error", "Home", new { Area = "Seller" });
    }

}

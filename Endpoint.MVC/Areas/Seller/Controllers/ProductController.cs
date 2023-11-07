using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;
using Endpoint.MVC.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.MVC.Areas.Seller.Controllers;


[Area("Seller")]
public class ProductController : BaseController
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
}

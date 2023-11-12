//using Endpoint.MVC.Controllers;
//using Endpoint.MVC.Dtos.Products;
//using Microsoft.AspNetCore.Mvc;

//namespace Endpoint.MVC.Areas.Customer.Controllers;

//[Area("Customer")]
//public class ProductController : BaseController
//{
//    public ProductController(IHttpClientFactory httpClientFactory) :
//                                                  base(httpClientFactory)
//    { }


//    public async Task<IActionResult> GetNonAuctionsByCategoryId(int categoryId,
//                                                                CancellationToken cancellationToken)
//    {
//        var httpResponseMessage = await SendGetRequest($"Product/GetNonAuctionsByCategoryId/" +
//                                                          $"{categoryId}", cancellationToken);

//        if (!httpResponseMessage.IsSuccessStatusCode)
//            return RedirectToErrorPage(httpResponseMessage);

//        var products = await httpResponseMessage.Content.ReadFromJsonAsync<List<ProductOutputDto>>();
//        foreach (var product in products)
//        {
//            if (product.DiscountPercent != 0)
//                product.Price *= Convert.ToDecimal((100 - product.DiscountPercent) * 0.01);
//        }

//        return View(products);
//    }



//    public async Task<IActionResult> GetAllByCategoryId(CancellationToken cancellationToken, int id = 3)
//    {
//        var httpResponseMessage = await SendGetRequest($"Product/GetAllByCategoryId/{id}",
//                                                                    cancellationToken);
//        if (!httpResponseMessage.IsSuccessStatusCode)
//            return RedirectToErrorPage(httpResponseMessage);

//        var products = await httpResponseMessage.Content.ReadFromJsonAsync<List<ProductOutputDto>>();
//        foreach (var product in products)
//        {
//            if (product.DiscountPercent != 0)
//                product.Price *= Convert.ToDecimal((100 - product.DiscountPercent) * 0.01);
//        }

//        return View(products);
//    }

//    public async Task<IActionResult> GetById(int id,
//                                             CancellationToken cancellationToken)
//    {

//        var httpResponseMessage = await SendGetRequest($"Product/GetById/{id}",
//                                                       cancellationToken);

//        if (!httpResponseMessage.IsSuccessStatusCode)
//            return RedirectToErrorPage(httpResponseMessage);

//        var product = await httpResponseMessage.Content.ReadFromJsonAsync<ProductDetailsDto>();
//        return View(product);
//    }
//}

using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;
using Endpoint.MVC.Controllers;
using Microsoft.AspNetCore.Mvc;
using Endpoint.MVC.Dtos.Products;
using Newtonsoft.Json;
using Endpoint.MVC.Dtos.Pictures;
using Endpoint.MVC.Dtos.Categories;

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
            Price = productDetails.Price ?? 0,
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
        if (!ModelState.IsValid)
            return View(updateProductDto);

        var httpResponseMessage = await SendPutRequest($"Product/UpdateNonAuction/{updateProductDto.Id}",
                                                      JsonConvert.SerializeObject(updateProductDto),
                                                      cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction(nameof(GetNonAuctions));
    }


    public async Task<IActionResult> DeleteNonAuction(int productId, 
                                                 CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendDeleteRequest($"product/remove/{productId}",
                                                          cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction(nameof(GetNonAuctions));
    }



    [HttpGet]
    public async Task<IActionResult> CreateNonAuction(CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest("category/GetLeafCategories",
                                                        cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        var categoryTitles = await httpResponseMessage.Content.ReadFromJsonAsync<List<CategoryTitleDto>>();
        return View(categoryTitles);
    }


    [HttpPost]
    public async Task<IActionResult> CreateNonAuction(CreateProductViewModel model,
                                                CancellationToken cancellationToken)
    {
        string uniqueFileName = string.Empty;
        string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
        uniqueFileName = Guid.NewGuid().ToString() + model.Picture.FileName;
        string filePath = Path.Combine(uploadFolder, uniqueFileName);
        model.Picture.CopyTo(new FileStream(filePath, FileMode.Create));

        var createProductDto = new CreateNonAuctionProductDto()
        {
            PersianTitle = model.PersianTitle,
            EnglishTitle = model.EnglishTitle,
            Description = model.Description,
            FirstDiscount = model.FirstDiscount,
            FirstPrice = model.FirstPrice,
            CategoryId = model.CategoryId,
            FirstQuantity = model.FirstQuantity,
            CustomAttributes = model.CustomAttributes,
            PictureDto = new PictureDto() { PictureName = uniqueFileName },
        };

    var httpResponseMessage = await SendPostRequest("product/CreateNonAuction",
                                                        JsonConvert.SerializeObject(createProductDto),
                                                        cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction(nameof(Index));
    }



    public async Task<IActionResult> GetAuctions(CancellationToken cancellationToken)
    {

        var httpResponseMessage = await SendGetRequest("Booth/GetAuctionsBySellerId",
                                                                cancellationToken);

       if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var products = await httpResponseMessage.Content
                                            .ReadFromJsonAsync<List<ProductOutputDto>>();
        return View(products);
    }



    public async Task<IActionResult> GetAuctionById(int id, 
                                                    CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest($"Product/GetAuctionProductById/{id}?isApproved=true",
                                                                            cancellationToken);

        
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var product = await httpResponseMessage.Content
                                         .ReadFromJsonAsync<AuctionDetailsDto>();
        return View(product);
    }


    [HttpGet]
    public async Task<IActionResult> UpdateAuction(int id, 
                                                   CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest($"Product/GetAuctionProductById/{id}?isApproved=true",
                                                       cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var productDetails = await httpResponseMessage.Content
                                             .ReadFromJsonAsync<AuctionDetailsDto>();

        return View(productDetails);
    }



    [HttpPost]
    public async Task<IActionResult> UpdateAuction(AuctionDetailsDto updateProductDto, 
                                                   CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(updateProductDto);

        var httpResponseMessage = await SendPutRequest($"Product/UpdateAuction/{updateProductDto.Id}",
                                                     JsonConvert.SerializeObject(updateProductDto),
                                                     cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction(nameof(GetAuctions));
    }



    public async Task<IActionResult> DeleteAuction(int id, CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendDeleteRequest($"product/remove/{id}",
                                                          cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction(nameof(GetAuctions));
    }


    [HttpGet]
    public async Task<IActionResult> CreateAuction(CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest("category/GetLeafCategories",
                                                        cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        var categoryTitles = await httpResponseMessage.Content.ReadFromJsonAsync<List<CategoryTitleDto>>();
        return View(categoryTitles);
    }


    [HttpPost]
    public async Task<IActionResult> CreateAuction(CreateAuctionViewModel model, 
                                              CancellationToken cancellationToken)
    {

        string uniqueFileName = string.Empty;
        string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
        uniqueFileName = Guid.NewGuid().ToString() + model.Picture.FileName;
        string filePath = Path.Combine(uploadFolder, uniqueFileName);
        model.Picture.CopyTo(new FileStream(filePath, FileMode.Create));

        var createProductDto = new CreateAuctionProductDto()
        {
            PersianTitle = model.PersianTitle,
            EnglishTitle = model.EnglishTitle,
            Description = model.Description,
            MinPrice = model.MinPrice,
            CategoryId = model.CategoryId,
            FirstQuantity = model.FirstQuantity,
            FromDate = model.FromDate,
            ToDate = model.ToDate,
            CustomAttributes = model.CustomAttributes.ToList(),
            PictureDto = new PictureDto() { PictureName = uniqueFileName }
        };

        var httpResponseMessage = await SendPostRequest("product/CreateAuction",
                                                            JsonConvert.SerializeObject(createProductDto),
                                                            cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction(nameof(Index));
    }





}

using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;
using Endpoint.MVC.Dtos.Booth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;



namespace Endpoint.MVC.Areas.Seller.Controllers;

[Area("Seller")]
public class BoothController : SellerBaseController
{
    private readonly IHostingEnvironment _hostingEnvironment;

    public BoothController(IHttpClientFactory httpClientFactory,
                            IHostingEnvironment hostingEnvironment)
                                    : base(httpClientFactory)
    {
        _hostingEnvironment = hostingEnvironment;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest("Booth/GetBySellerId",
                                                        cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var booth = await httpResponseMessage.Content
                                             .ReadFromJsonAsync<BoothDto>();

        return View(booth);
    }


    [HttpGet]
    public async Task<IActionResult> Update(CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest("Booth/GetBySellerId",
                                                        cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var booth = await httpResponseMessage.Content
                                             .ReadFromJsonAsync<UpdateBoothViewModel>();

        return View(booth);
    }


    [HttpPost]
    public async Task<IActionResult> Update(UpdateBoothViewModel booth, 
                                        CancellationToken cancellationToken)
    {
        string uniqueFileName = string.Empty;
        string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
        uniqueFileName = Guid.NewGuid().ToString() + booth.Picture.FileName;
        string filePath = Path.Combine(uploadFolder, uniqueFileName);
        booth.Picture.CopyTo(new FileStream(filePath, FileMode.Create));
        var updateDto = new UpdateBoothDto()
        {
            Description = booth.Description,
            Id = booth.Id,
            PictureName = uniqueFileName,
            Title = booth.Title,
        };
        var httpResponseMessage = await SendPutRequest($"Booth/Update/{updateDto.Id}",
                                                      JsonConvert.SerializeObject(updateDto),
                                                       cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction(nameof(Get));
    }
}

    //public async Task<IActionResult> Create(CancellationToken cancellationToken)
    //{

    //}




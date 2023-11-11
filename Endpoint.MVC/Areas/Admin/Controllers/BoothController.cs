using Endpoint.MVC.Controllers;
using Endpoint.MVC.Dtos.Booth;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class BoothController : BaseController
{
    public BoothController(IHttpClientFactory httpClientFactory) 
                            : base(httpClientFactory){ }

    public async Task<IActionResult> GetByTitle(string title,
                                               CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest($"Booth/GetByTitle/{title}",
                                                                cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var booth = await httpResponseMessage.Content.ReadFromJsonAsync<BoothDto>();
        return View(booth);
    }
}

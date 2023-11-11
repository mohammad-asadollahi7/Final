using Endpoint.MVC.Controllers;
using Endpoint.MVC.Dtos;
using Endpoint.MVC.Dtos.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class AccountController : BaseController
{
    public AccountController(IHttpClientFactory httpClientFactory) 
                                            : base(httpClientFactory)
    {
    }

    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest("Account/GetUsers",
                                                       cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var users = await httpResponseMessage.Content
                                         .ReadFromJsonAsync<List<UserOutputDto>>();
        return View(users);

    }


    public async Task<IActionResult> DeleteUser(int id, Role role,
                                        CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendDeleteRequest($"Account/DeleteUser/{id}?role={role}",
                                                          cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction(nameof(GetUsers));

    }
}

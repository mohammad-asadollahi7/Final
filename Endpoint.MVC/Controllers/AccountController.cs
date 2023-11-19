using Endpoint.MVC.Dtos;
using Endpoint.MVC.Dtos.Account;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Endpoint.MVC.Controllers;

public class AccountController : BaseController
{
    public AccountController(IHttpClientFactory httpClientFactory) :
                                                    base(httpClientFactory)
    { }


    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterDto registerDto,
                                              CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(registerDto);

        var httpResponseMessage = await SendPostRequest("Account/Register",
                                                        JsonConvert.SerializeObject(registerDto),
                                                        cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction(nameof(Login));
    }


    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginDto loginDto,
                                           CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(loginDto);

        var httpResponseMessage = await SendPostRequest("Account/Login",
                                                        JsonConvert.SerializeObject(loginDto),
                                                        cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var loginOutput = await httpResponseMessage.Content
                                                .ReadFromJsonAsync<LoginOutputDto>();

        Response.Cookies.Append("authorize", loginOutput.Token);
        Response.Cookies.Append("fullName", loginOutput.FullName);


        if (loginDto.Role.ToString().ToLower() == "admin")
            return RedirectToAction("Index", "Product", new { area = "admin" });


        else if (loginDto.Role.ToString().ToLower() == "seller")
            return RedirectToAction("Index", "Product", new { area = "seller" });

        else
            return RedirectToAction("getnonauctions", "Product");



    }


    [HttpGet]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("authorize");
        return RedirectToAction("getnonauctions", "Product");
    }



    [HttpGet]
    public async Task<IActionResult> Update(CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest("Account/Get",
                                                        cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        var user = await httpResponseMessage.Content
                                                .ReadFromJsonAsync<UserOutputDto>();

        return View(user);
    }


    [HttpPost]
    public async Task<IActionResult> Update(UpdateUserDto updateDto,
                                    CancellationToken cancellationToken)
    {
        if(!ModelState.IsValid)
            return View(updateDto);

        var httpResponseMessage = await SendPostRequest("Account/update",
                                                        JsonConvert.SerializeObject(updateDto),
                                                        cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);


        return RedirectToAction("logout");
    }
}

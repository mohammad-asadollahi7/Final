using Endpoint.MVC.Dtos;
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


        var token = await httpResponseMessage.Content.ReadAsStringAsync();

        Response.Cookies.Append("authorize", token);
        // HttpContext.Session.SetString("authorize", token);

        TempData.Peek("Role");
        TempData["Role"] = loginDto.Role;
        if (loginDto.Role.ToString().ToLower() == "customer")
            return RedirectToAction("GetAllByCategoryId", "Product", new { area = "customer" });

        else if(loginDto.Role.ToString().ToLower() == "admin")
            return RedirectToAction("Index", "Product", new { area = "admin" });

        else
           return RedirectToAction("Index", "Product");

            // else if (loginDto.Role.ToString().ToLower() == "seller")
            //     return RedirectToAction("Index", "Product", new { area = "seller" });
            //  else
            //     return RedirectToErrorPage(httpResponseMessage);


    }


    [HttpGet]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("authorize");
        return RedirectToAction("GetAllByCategoryId", "Product", new { area = "customer" });
    }
}

using Domain.Core.Contracts.AppServices;
using Domain.Core.Entities;
using Endpoint.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.API.Controllers;
public class AccountController : BaseController
{
    private readonly IAccountAppService _accountAppService;

    public AccountController(IAccountAppService accountAppService)
    {
        _accountAppService = accountAppService;
    }


    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterDto model,
                                              CancellationToken cancellationToken)
    {
        var newUser = new ApplicationUser()
        {
            UserName = model.Username,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
        };

        await _accountAppService.Register(newUser, model.Password,
                                          model.Role, cancellationToken);

        return Ok();
    }


    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto model,
                                           CancellationToken cancellationToken)
    {

        var token = await _accountAppService.Login(model.Username,
                                                   model.Password,
                                                   model.Role,
                                                   cancellationToken);

        return Ok(token);
    }

}
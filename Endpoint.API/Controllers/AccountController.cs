using Domain.Core.Contracts.AppServices;
using Domain.Core.Dtos;
using Domain.Core.Dtos.Account;
using Domain.Core.Entities;
using Domain.Core.Enums;
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
            FullName = model.FName + " " + model.LName,
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

        var output = await _accountAppService.Login(model.Username,
                                                   model.Password,
                                                   model.Role,
                                                   cancellationToken);
        
        return Ok(output);
    }


    [HttpGet("GetUsers")]
    //[HaveAccess(Role.Admin)]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var users = await _accountAppService.GetUsers(cancellationToken);
        return Ok(users);
    }

    [HttpDelete("DeleteUser/{userId}")]
    //[HaveAccess(Role.Admin)]
    public async Task<IActionResult> DeleteUser(int userId, Role role,
                                            CancellationToken cancellationToken)
    {
         await _accountAppService.DeleteUser(userId, role, cancellationToken);
        return Ok();
    }


    [HttpGet("GetUserNumbers")]
    //[HaveAccess(Role.Admin)]
    public async Task<IActionResult> GetUserNumbers(CancellationToken cancellationToken)
    {
        var numberOfUsers = await _accountAppService.GetUserNumbers(cancellationToken);
        return Ok(numberOfUsers);
    }
}
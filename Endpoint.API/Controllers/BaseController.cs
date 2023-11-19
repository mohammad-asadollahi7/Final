using Domain.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Endpoint.API.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize(AuthenticationSchemes = "Bearer")]
[EnableCors("AllowMyFrontEnd")]
public class BaseController : ControllerBase
{
    public int CurrentUserId => Convert.ToInt32(User.Claims.SingleOrDefault
                                               (c => c.Type == ClaimTypes.NameIdentifier)?.Value);

    public string CurrentUserRole =>  User.Claims.FirstOrDefault
                                                   (c => c.Type == ClaimTypes.Role)?.Value;
}

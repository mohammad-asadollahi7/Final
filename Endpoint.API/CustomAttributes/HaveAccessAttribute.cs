using Domain.Core.Entities;
using Domain.Core.Enums;
using Domain.Core.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Endpoint.API.CustomAttributes;


[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class,
                         Inherited = true, AllowMultiple = true)]
public class HaveAccessAttribute : Attribute, IAuthorizationFilter
{
    private Role[] _permissionRoles;
    private UserManager<ApplicationUser>? _userManager;

    public HaveAccessAttribute(params Role[] roles) => _permissionRoles = roles;


    public void OnAuthorization(AuthorizationFilterContext filterContext)
    {
        var user = filterContext.HttpContext.User;
        _userManager = filterContext.HttpContext.RequestServices
                                    .GetService<UserManager<ApplicationUser>>();

        if (!user.Identity.IsAuthenticated)
            throw new AppException(ExpMessage.Unauthorized, ExpStatusCode.Unauthorized);


        var username = user.Claims.Where(c => c.Type == ClaimTypes.Name)
                                     .Select(c => c.Value).SingleOrDefault();

        var currentUser = _userManager.FindByNameAsync(username).Result;
        var currentUserRoles = _userManager.GetRolesAsync(currentUser).Result.ToList();
        int number = 0;
        int currentUserRolesCount = currentUserRoles.Count();
        int permissionRolesCount = _permissionRoles.Count();


        for (int i = 0; i < currentUserRolesCount; i++)
        {
            for (int j = 0; j < permissionRolesCount; j++)
            {
                if (currentUserRoles[i].ToLower() != _permissionRoles[j].ToString().ToLower())
                    number++;
            }
        }
        if (number == (currentUserRolesCount * permissionRolesCount))
        {
            throw new AppException(ExpMessage.Unauthorized, ExpStatusCode.Unauthorized);
        }

    }

}
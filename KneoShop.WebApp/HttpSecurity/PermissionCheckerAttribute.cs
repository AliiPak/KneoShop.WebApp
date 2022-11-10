using System.Net;
using KenoShop.WebApp.Context;
using KenoShop.WebApp.HttpExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KenoShop.WebApp.HttpSecurity;

public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly string _permission;

    public PermissionCheckerAttribute(string permissionName)
    {
        _permission = permissionName;
    }


    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var _context = context.HttpContext.RequestServices.GetService<KenoShopDbContext>();
        var userId = context.HttpContext.User.GetCurrentUserId();
        var user = _context.Users.Single(u => u.UserID == userId);

        /*var hasUserPermission = _context.UserRoles
            .Include(a => a.Role)
            .ThenInclude(a => a.RolePermissions)
            .ThenInclude(a => a.Permission)
            .Any(ur => ur.UserId == userId &&
                       ur.Role.RolePermissions.Any(rp => rp.Permission.SystemTitle == _permission));

        if (user.IsAdmin || hasUserPermission)
        {
            // log
        }
        else
        {
            context.Result = new RedirectResult("/");
        }*/
    }
}
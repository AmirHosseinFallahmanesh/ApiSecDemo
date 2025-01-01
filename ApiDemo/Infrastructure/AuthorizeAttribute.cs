using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;



[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public string Roles { get; set; }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string idText = context.HttpContext.Items["Id"]?.ToString();

        string role = context.HttpContext.Items["Role"]?.ToString();

        if (idText == "0" | string.IsNullOrEmpty(idText))
        {
            context.Result = new JsonResult
             (
           new { message = "401 nauthorized !!!!" })
            { StatusCode = StatusCodes.Status401Unauthorized };

        }
        if (role!= Roles)
        {
            context.Result = new JsonResult
             (
           new { message = "403 Forbidden !!!!!" })
            { StatusCode = StatusCodes.Status403Forbidden };

        }


    }


}



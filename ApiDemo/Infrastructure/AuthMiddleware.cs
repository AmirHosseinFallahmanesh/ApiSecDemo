using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDemo.Infrastructure
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate next;

        public AuthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ")[1];
            if (token!=null)
            {
                try
                {
                    var tokenHadnler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes("THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING");

                    tokenHadnler.ValidateToken(token, new TokenValidationParameters
                    {
                        IssuerSigningKey=new SymmetricSecurityKey(key),
                        ValidateAudience=false,
                        ValidateIssuer=false,
                        ValidateIssuerSigningKey=true,
                        ClockSkew=TimeSpan.Zero
                    },out SecurityToken tokenvalidateToken);

                    var jwt = tokenvalidateToken as JwtSecurityToken;

                    int id = int.Parse(jwt.Claims.First(a => a.Type == "id").Value);
                    string role = jwt.Claims.First(a => a.Type == "role").Value;
                    context.Items["Id"] = id;
                    context.Items["Role"] = role;

            }
                catch (Exception)
                {


                }
            }

            await next(context);
        }
    }
}

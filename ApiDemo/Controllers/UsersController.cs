using ApiDemo.Application;
using ApiDemo.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Authentiacate(AuthenticateRequestDTO request)
        {

            try
            {
                if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                {
                    throw new ArgumentException();
                }
                var user = userService.Login(request);
                if (user==null)
                {
                    throw new Exception();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
          
        }
    }
}

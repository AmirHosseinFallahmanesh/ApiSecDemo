using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDemo.Domain.DTOs
{
    public class AuthenticationResposeDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }

        public string Token { get; set; }
    }
}

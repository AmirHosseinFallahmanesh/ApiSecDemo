using ApiDemo.Domain.DTOs;
using ApiDemo.Domain.Entites;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiDemo.Application
{
    public interface IUserService
    {
        AuthenticationResposeDTO Login(AuthenticateRequestDTO authenticateRequestDTO);
        User Get(int id);
    }
    public class UserService : IUserService
    {
        List<User> users = new List<User>()
        {
            new User()
            {
                Id=1,FirstName="test",Lastname="test",Username="a@a",Password="123"
            }
        };
        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public AuthenticationResposeDTO Login(AuthenticateRequestDTO authenticateRequestDTO)
        {
            var user = users.SingleOrDefault(a => a.Username == authenticateRequestDTO.Username);
            if (user==null)
            {
                throw new NullReferenceException();
            }
            if (user.Password!=authenticateRequestDTO.Password)
            {
                throw new Exception();
            }
            AuthenticationResposeDTO resposeDTO = new AuthenticationResposeDTO();
            resposeDTO.Id = user.Id;
            resposeDTO.FirstName = user.FirstName;
            resposeDTO.Lastname = user.Lastname;
            resposeDTO.Username = user.Username;
            resposeDTO.Token = generateToken(user);
            return resposeDTO;



        }

        public string generateToken(User user)
        {
            var tokenHadnler= new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING");
            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(10),
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), new Claim("role", "admin") }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHadnler.CreateToken(tokenDiscriptor);
            return tokenHadnler.WriteToken(token);
        }
    }
}

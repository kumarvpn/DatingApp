using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _Tservice;
        public AccountController(DataContext context, ITokenService Tservice)
        {
            _Tservice = Tservice;
            _context = context;
        }


        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDTO Rdto)
        {
            if (await IsUserExists(Rdto.UserName)) return BadRequest("UserName already taken");

            using var hmac = new HMACSHA512();

            var user = new AppUser()
            {

                UserName = Rdto.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Rdto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

           return new UserDto()
            {
                UserName = user.UserName,
                Token = _Tservice.CreateToken(user)
            };
        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDTO login)
        {
            var user = await _context.Users.SingleOrDefaultAsync(p => p.UserName == login.Username);
            if (user == null) return Unauthorized("Invalid Username !");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var ComputedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
            for (int i = 0; i < ComputedHash.Length; i++)
            {
                if (ComputedHash[i] != user.PasswordHash[i]) return Unauthorized("In Valid Password");
            }

            return new UserDto()
            {
                UserName = user.UserName,
                Token = _Tservice.CreateToken(user)
            };
        }

        private async Task<bool> IsUserExists(string Username)
        {
            return await _context.Users.AnyAsync(u => u.UserName.ToLower() == Username.ToLower());
        }

    }
}
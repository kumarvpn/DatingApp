using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsersController : ControllerBase
    {
        private readonly DataContext _Context;
        public UsersController(DataContext Context)
        {
            _Context=Context;

        }

        [HttpGet]
        public async Task<ActionResult<List<AppUser>>> GetUsers()
        {
            return await _Context.Users.ToListAsync();
        }

          [HttpGet("{Id}")]
        public  async Task<ActionResult<AppUser>> GetUser(int id)
        {
            return  await _Context.Users.FindAsync(id);
        }
    }
}
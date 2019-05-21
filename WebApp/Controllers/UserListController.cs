using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NFCSystem.Models;
using NFCSystem.Data;
using Microsoft.AspNetCore.Mvc;

namespace NFCSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserListController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserListController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Todo
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<ApplicationUser>> GetUser(string id)
        {
        var todoItem = await _context.Users.FindAsync(id);

        if (todoItem == null)
        {
            return NotFound();
        }

        return todoItem;
        }

        [HttpGet("[action]/{studentCode}")]
        public ActionResult<string> GetUserId(string studentCode)
        {
            var id = _context.Users.FirstOrDefault(x => x.StudentCode == studentCode).Id;
            return id;
        }

        // POST: api/Todo
        [HttpPost("[action]")]
        public async Task<ActionResult<ApplicationUser>> PostUser(ApplicationUser item)
        {
            _context.Users.Update(item);
            await _context.SaveChangesAsync();
            //System.Diagnostics.Debug.WriteLine(item.ToString());

            return CreatedAtAction(nameof(GetUser), new { id = item.StudentCode }, item);
        }   
    }
}
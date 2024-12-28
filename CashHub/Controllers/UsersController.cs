using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CashHub.Data;
using CashHub.Models;
using CashHub.DTO;

namespace CashHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CashHubContext _context;

        public UsersController(CashHubContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Getusers()
        {
            return await _context.users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.users.Any(e => e.Id == id);
        }
        //GET: api/Users/search?username=
        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers([FromQuery] string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username query parameter is required!");
            }
            var users = await _context.users.Where(u => u.UserName.Contains(username)).ToListAsync();
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found with the given username");
            }
            return Ok(users);
        }
        [HttpPost("add-completed-task")]
        public IActionResult AddCompletedTask(int UserId, int TaskId)
        {
            var user = _context.users.FirstOrDefault(u => u.Id == UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            if (user.CompletedTask == null)
            {
                user.CompletedTask = new int[] { };
            }
            if (user.CompletedTask.Contains(TaskId))
            {
                return BadRequest("Task is already completed");
            }
            var ad = _context.ads.FirstOrDefault(u => u.Id == TaskId);
            if (ad == null)
            {
                return NotFound("task not found");
            }
            user.CompletedTask = user.CompletedTask.Append(TaskId).ToArray();
            user.TaskBal += ad.Amount;
            //_context.users.Add(user);
            try
            {
                _context.SaveChanges();
                return Ok($"Task id {TaskId} succesfully completed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"failed to complete task: {ex.Message}");
            }
        }
        [HttpPost("signin")]
        public IActionResult Signin([FromQuery] SigninDto signindto)
        {
            if (string.IsNullOrEmpty(signindto.UserName) || string.IsNullOrEmpty(signindto.Password))
            {
                return BadRequest("Username and Password are required");
            }
            var users = _context.users.Where(u => u.UserName.ToLower() == signindto.UserName.ToLower()).ToList();

            if (users == null)
            {
                return Unauthorized("Inavlid username");
            }
            var user = users.FirstOrDefault(u => u.Password == signindto.Password);
            if (user == null)
            {
                return Unauthorized("Invalid Password");
            }
            return Ok(new
            {
                Message = "Signin Successful",
            });
        }
        private readonly List<User> _users = new();
        [HttpPost("signup")]
        public IActionResult signup(SignupDto signupdto)
        {
            if (_users.Any(u => u.UserName == signupdto.UserName))
            {
                return BadRequest("UserName already in use.");
            }
            var user = new User
            {

                Fullname = signupdto.Fullname,
                UserName = signupdto.UserName,
                Email = signupdto.Email,
                Password = signupdto.Password,
                PhoneNumber = signupdto.PhoneNumber,
                ReferredBy = signupdto.ReferredBy,
                RegTime = signupdto.RegTime,
            };
            _context.Add(user);
            var res = _context.SaveChanges();
            if (res > 0)
            {
                return Ok("User registered succesfully.");

            }
            //_users.Add(user);
            return StatusCode(500, "failed to save user.");
        }
    }
}

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
    public class AdsController : ControllerBase
    {
        private readonly CashHubContext _context;

        public AdsController(CashHubContext context)
        {
            _context = context;
        }

        // GET: api/Ads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ads>>> Getads()
        {
            return await _context.ads.ToListAsync();
        }

        // GET: api/Ads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ads>> GetAds(int id)
        {
            var ads = await _context.ads.FindAsync(id);

            if (ads == null)
            {
                return NotFound();
            }

            return ads;
        }

        // PUT: api/Ads/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAds(int id, Ads ads)
        {
            if (id != ads.Id)
            {
                return BadRequest();
            }

            _context.Entry(ads).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdsExists(id))
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

        // POST: api/Ads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ads>> PostAds(Ads ads)
        {
            _context.ads.Add(ads);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAds", new { id = ads.Id }, ads);
        }

        //POST api/ads
        //[HttpPost]
        //public IActionResult createtask([FromForm] TaskDto taskdto)
        //{
        //    if(taskdto == null)
        //    {
        //        return BadRequest("Invalid task data");
        //    }
        //    string imagepath = null;
        //    if(taskdto.TaskImage != null && taskdto.TaskImage.Length > 0)
        //    {
        //        var filename = Guid.NewGuid().ToString() + Path.GetExtension(taskdto.TaskImage.FileName);
        //        var uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", filename);
        //        using (var stream = new FileStream(uploadpath, FileMode.Create))
        //        {
        //            taskdto.TaskImage.CopyTo(stream);
        //        }
        //        imagepath = $"/images/{filename}";
        //    }
        //    var newTask = new Ads
        //    {
        //        Title = taskdto.Title,
        //        Description = taskdto.Description,
        //        Amount = taskdto.Amount,
        //        TaskImage = imagepath
        //    };
        //}
        // DELETE: api/Ads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAds(int id)
        {
            var ads = await _context.ads.FindAsync(id);
            if (ads == null)
            {
                return NotFound();
            }

            _context.ads.Remove(ads);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdsExists(int id)
        {
            return _context.ads.Any(e => e.Id == id);
        }
    }
}

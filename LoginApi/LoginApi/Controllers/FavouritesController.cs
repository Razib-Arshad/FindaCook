using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoginApi.Models;
using Azure;

namespace LoginApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouritesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FavouritesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Favourites
        [HttpGet("favourites/get")]
        public async Task<ActionResult<IEnumerable<Favourite>>> GetFavourites()
        {
          if (_context.Favourites == null)
          {
              return NotFound();
          }
          var favourites = await _context.Favourites.ToListAsync();
            var response = new
                {
                    StatusCode = 200,
                    Message = "post order successfully",
                    Data = favourites
                };
            return Ok(response);
        }

        // GET: api/Favourites/5
        [HttpGet("favourites/get/{id}")]
        public async Task<ActionResult<Favourite>> GetFavourite(int id)
        {
          if (_context.Favourites == null)
          {
              return NotFound(new { StatusCode = 400, Message = "Entity set 'AppDbContext.Favourite'  is null." });
          }
            var favourite = await _context.Favourites.FindAsync(id);

            if (favourite == null)
            {
                return NotFound(new { StatusCode = 400, Message = "not found favourite in db with this id " + id });
            }
            var response = new
            {
                StatusCode = 200,
                Message = "post order successfully",
                Data = favourite
            };
            return Ok(response);
        }

        // PUT: api/Favourites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("favourite/update/{id}")]
        public async Task<IActionResult> PutFavourite(int id, Favourite favourite)
        {
            if (id != favourite.FavouriteId)
            {
                return BadRequest(new { StatusCode = 400, Message = "Invalid Request" });
            }

            _context.Entry(favourite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                var response = new
                {
                    StatusCode = 200,
                    Message = "update favourite successfully"
                };
                return Ok(response);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavouriteExists(id))
                {
                    return NotFound(new { StatusCode = 400, Message = "udpate of Favourite'  is unsuccessfull." });
                }
                else
                {
                    throw;
                }
            }

        }

        // POST: api/Favourites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("favourite/post")]
        public async Task<ActionResult<Favourite>> PostFavourite(Favourite favourite)
        {
          if (_context.Favourites == null)
          {
                return NotFound(new { StatusCode = 400, Message = "Entity set 'AppDbContext.Favourite'  is null." });
          }
            _context.Favourites.Add(favourite);
            await _context.SaveChangesAsync();

            var response = new
            {
                StatusCode = 200,
                Message = "add favourite successfully"
            };
            return Ok(response);
        }

        // DELETE: api/Favourites/5
        [HttpDelete("favourite/delete/{id}")]
        public async Task<IActionResult> DeleteFavourite(int id)
        {
            if (_context.Favourites == null)
            {
                return NotFound(new { StatusCode = 400, Message = "Entity set 'AppDbContext.Favourite'  is null." });
            }
            var favourite = await _context.Favourites.FindAsync(id);
            if (favourite == null)
            {
                return NotFound();
            }

            _context.Favourites.Remove(favourite);
            await _context.SaveChangesAsync();

            var response = new
            {
                StatusCode = 200,
                Message = "delete favourite successfully"
            };
            return Ok(response);
        }

        private bool FavouriteExists(int id)
        {
            return (_context.Favourites?.Any(e => e.FavouriteId == id)).GetValueOrDefault();
        }
    }
}

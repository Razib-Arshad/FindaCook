using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoginApi.Models;
using Azure;
using Microsoft.Extensions.Configuration.UserSecrets;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

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
            try
            {
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
            catch(Exception ex)
            {
                // Log the exception or handle it as needed
                var errorResponse = new
                {
                    StatusCode = 500,
                    ErrorDetails = ex.Message
                };

                return StatusCode(500, errorResponse);
            }
        }

        [HttpGet("favourites/user/{userId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetUserFavourites(string userId)
        {
            try
            {
                // Retrieve favorites for the given user ID
                var favourites = await _context.Favourites
                    .Include(f => f.CookInfo)
                    .Where(f => f.UserId == userId)
                    .ToListAsync();

                if (favourites == null || !favourites.Any())
                {
                    return NotFound(new { StatusCode = 404, Message = "No favorites found for the user." });
                }

                // Extract necessary details for each favorite cook
                var cooksDetails = favourites.Select(favourite => new
                {
                    favourite.CookInfo.FirstName,
                    favourite.CookInfo.LastName,
                    favourite.CookInfo.SkillsAndSpecialties,
                    favourite.CookInfo.ServicesProvided,
                    favourite.CookInfo.SignatureDishes,
                    favourite.CookInfo.ExperienceYears
                }).ToList();

                // Construct and return the response object
                var response = new
                {
                    StatusCode = 200,
                    Message = "Favourites retrieved successfully",
                    Data = cooksDetails
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                var errorResponse = new
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving favourites",
                    ErrorDetails = ex.Message
                };

                return StatusCode(500, errorResponse);
            }
        }


        // PUT: api/Favourites/5
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
        [HttpPost("favourite/post")]
        public async Task<ActionResult<Favourite>> PostFavourite(FavouriteModel favourite)
        {
          if (_context.Favourites == null)
          {
                return NotFound(new { StatusCode = 400, Message = "Entity set 'AppDbContext.Favourite'  is null." });
          }
          try
            {
                var favourites = await _context.Favourites
                      .Where(f => f.UserId == favourite.UserId)
                      .ToListAsync();
                if (favourites != null)
                {
                    var fav = new Favourite
                    {
                        UserId = favourite.UserId,
                        CookInfoId = favourite.CookInfoId,

                    };
                    _context.Favourites.Add(fav);
                    await _context.SaveChangesAsync();

                    var response = new
                    {
                        StatusCode = 200,
                        Message = "add favourite successfully"
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new
                    {
                        StatusCode = 404,
                        Message = "cook already exist in user's favourites list"
                    };
                    return Ok(response);
                }
            }
            catch(Exception ex)
            {
                // Log the exception or handle it as needed
                var errorResponse = new
                {
                    StatusCode = 500,
                    ErrorDetails = ex.Message
                };

                return StatusCode(500, errorResponse);
            }
            
            
        }

        // DELETE: api/Favourites/5
        [HttpDelete("favourite/delete/{id}")]
        public async Task<IActionResult> DeleteFavourite(int id)
        {
            if (_context.Favourites == null)
            {
                return NotFound(new { StatusCode = 400, Message = "Entity set 'AppDbContext.Favourite'  is null." });
            }

            try
            {
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
            catch(Exception ex)
            {
                // Log the exception or handle it as needed
                var errorResponse = new
                {
                    StatusCode = 500,
                    ErrorDetails = ex.Message
                };

                return StatusCode(500, errorResponse);
            }
        }

        // DELETE: api/Favourites_User/UserId
        [HttpDelete("favourite_user/delete/{userId}/{cookId}")]
        public async Task<IActionResult> DeleteUserFavourite(string userId, string cookId)
        {
            try
            {
                var favourite = await _context.Favourites.FirstOrDefaultAsync(f => f.UserId == userId && f.CookInfoId == cookId);

                if (favourite == null)
                {
                    return NotFound(new { StatusCode = 404, Message = "Favorite not found for the given user and cook." });
                }

                _context.Favourites.Remove(favourite);
                await _context.SaveChangesAsync();

                var response = new
                {
                    StatusCode = 200,
                    Message = "Favorite deleted successfully"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = ex.Message });
            }
        }


        private bool FavouriteExists(int id)
        {
            return (_context.Favourites?.Any(e => e.FavouriteId == id)).GetValueOrDefault();
        }
    }
}

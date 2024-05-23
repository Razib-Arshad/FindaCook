using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoginApi.Models;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace LoginApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderRequestsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderRequestsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/OrderRequests
        [HttpGet("request/get")]
        public async Task<ActionResult<IEnumerable<OrderRequest>>> GetOrderRequest()
        {
            if (_context.OrderRequest == null)
            {
                return NotFound(new { StatusCode = 400, Message = "Entity set 'AppDbContext.OrderRequest'  is null." });
            }
            var orderRequest = await _context.OrderRequest.ToListAsync();
            var response = new
            {
                StatusCode = 200,
                Message = "post order successfully",
                Data = orderRequest.ToList()
            };
            return Ok(response);

        }

        // GET: api/OrderRequests/user/5 
        [HttpGet("request/getUserRequest/{id}")]
        public async Task<ActionResult<OrderRequest>> GetOrderRequest(string id)
        {
            if (_context.OrderRequest == null)
            {
                return NotFound(new { StatusCode = 400, Message = "Entity set 'AppDbContext.OrderRequest'  is null." });
            }
            try
            {
                // Query the database for order requests associated with the specified user ID
                var orderRequests = await _context.OrderRequest
                    .Where(o => o.UserId == id)
                    .ToListAsync();
                var cookInfo = await _context.OrderRequest
                    .Include(f => f.CookInfo)
                    .Where(o => o.UserId == id)
                    .ToListAsync();

                var request = new
                {
                    Order_Requests = new
                    { orderRequestID = orderRequests[0].RqID, 
                    orderRequestDesc = orderRequests[0].Desc, 
                    orderRequestDate = orderRequests[0].Date, 
                    orderRequestPrice = orderRequests[0].Price, 
                    orderRequestService = orderRequests[0].selectedService, 
                    orderRequestTime = orderRequests[0].Time
                    }
                    ,
                    CookInfo = new
                    {
                        cookId = cookInfo[0].CookInfo.Id,
                        cookFirstName = cookInfo[0].CookInfo.FirstName,
                        cookLastName = cookInfo[0].CookInfo.LastName,
                        cookSkillsAndSpecialties = cookInfo[0].CookInfo.SkillsAndSpecialties,
                        cookSignatureDishes = cookInfo[0].CookInfo.SignatureDishes,
                        cookExperience = cookInfo[0].CookInfo.ExperienceYears,
                    }
                };


                if (orderRequests == null || !orderRequests.Any())
                {
                    return NotFound(new { StatusCode = 404, Message = "No order requests found for the specified user." });
                }

                var response = new
                {
                    StatusCode = 200,
                    Message = "Order requests retrieved successfully",
                    Data = request
                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = ex.Message });
            }
            
        }

        //get accepted orderRequest of users
        [HttpGet("request_user/accept/{id}")]
        public async Task<ActionResult> GetAcceptedUserOrderRequests(string id)
        {
            try
            {
                var acceptedRequests = await _context.OrderRequest
                    .Where(o => o.UserId == id && o.Status == "Accept")
                    .ToListAsync();

                if (acceptedRequests == null || !acceptedRequests.Any())
                {
                    return NotFound(new { StatusCode = 404, Message = "No accepted order requests found for the specified user." });
                }
                var response = new
                {
                    StatusCode = 200,
                    Message = "Order requests retrieved successfully",
                    Data = acceptedRequests
                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = ex.Message });
            }
        }

        //get accepted orderRequest of Cook
        [HttpGet("request_cook/accept/{id}")]
        public async Task<ActionResult> GetAcceptedCookOrderRequests(string id)
        {
            try
            {
                var acceptedRequests = await _context.OrderRequest
                    .Where(o => o.CookInfoId == id && o.Status == "Accept")
                    .ToListAsync();

                if (acceptedRequests == null || !acceptedRequests.Any())
                {
                    return NotFound(new { StatusCode = 404, Message = "No accepted order requests found for the specified user." });
                }
                var response = new
                {
                    StatusCode = 200,
                    Message = "Order requests retrieved successfully",
                    Data = acceptedRequests
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = ex.Message });
            }
        }
        //get decline orderRequest of users
        [HttpGet("request_user/decline/{id}")]
        public async Task<ActionResult> GetDeclineUserOrderRequests(string id)
        {
            try
            {
                var acceptedRequests = await _context.OrderRequest
                    .Where(o => o.UserId == id && o.Status == "Decline")
                    .ToListAsync();

                if (acceptedRequests == null || !acceptedRequests.Any())
                {
                    return NotFound(new { StatusCode = 404, Message = "No accepted order requests found for the specified user." });
                }
                var response = new
                {
                    StatusCode = 200,
                    Message = "Order requests retrieved successfully",
                    Data = acceptedRequests
                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = ex.Message });
            }
        }

        //get decline orderRequest of Cook
        [HttpGet("request_cook/decline/{id}")]
        public async Task<ActionResult> GetDeclineCookOrderRequests(string id)
        {
            try
            {
                var acceptedRequests = await _context.OrderRequest
                    .Where(o => o.CookInfoId == id && o.Status == "Decline")
                    .ToListAsync();

                if (acceptedRequests == null || !acceptedRequests.Any())
                {
                    return NotFound(new { StatusCode = 404, Message = "No accepted order requests found for the specified user." });
                }
                var response = new
                {
                    StatusCode = 200,
                    Message = "Order requests retrieved successfully",
                    Data = acceptedRequests
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = ex.Message });
            }
        }
        //get pending orderRequest of users
        [HttpGet("request_user/pending/{id}")]
        public async Task<ActionResult> GetPendingUserOrderRequests(string id)
        {
            try
            {
                var acceptedRequests = await _context.OrderRequest
                    .Where(o => o.UserId == id && o.Status == "Pending")
                    .ToListAsync();

                if (acceptedRequests == null || !acceptedRequests.Any())
                {
                    return NotFound(new { StatusCode = 404, Message = "No accepted order requests found for the specified user." });
                }
                var response = new
                {
                    StatusCode = 200,
                    Message = "Order requests retrieved successfully",
                    Data = acceptedRequests
                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = ex.Message });
            }
        }

        //get pending orderRequest of Cook
        [HttpGet("request_cook/pending/{id}")]
        public async Task<ActionResult> GetPendingCookOrderRequests(string id)
        {
            try
            {
                var acceptedRequests = await _context.OrderRequest
                    .Where(o => o.CookInfoId == id && o.Status == "Pending")
                    .ToListAsync();

                if (acceptedRequests == null || !acceptedRequests.Any())
                {
                    return NotFound(new { StatusCode = 404, Message = "No accepted order requests found for the specified user." });
                }
                var response = new
                {
                    StatusCode = 200,
                    Message = "Order requests retrieved successfully",
                    Data = acceptedRequests
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500,new { StatusCode = 500, Message = ex });
            }
        }

        // GET: api/OrderRequests/5
        [HttpGet("request/get/{id}")]
        public async Task<ActionResult<OrderRequest>> GetOrderRequest(int id)
        {
            try
            {
                if (_context.OrderRequest == null)
                {
                    return NotFound(new { StatusCode = 400, Message = "Entity set 'AppDbContext.OrderRequest'  is null." });
                }
                var orderRequest = await _context.OrderRequest.FindAsync(id);

                if (orderRequest == null)
                {
                    return NotFound(new { StatusCode = 400, Message = "OrderRequest'  is not found." });
                }
                var response = new
                {
                    StatusCode = 200,
                    Message = "post order successfully",
                    Data = orderRequest
                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = ex.Message });
            }
        }

        // PUT: api/OrderRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("request/update/{id}")]
        public async Task<IActionResult> PutOrderRequest(int id, OrderRequest orderRequest)
        {
            if (id != orderRequest.RqID)
            {
                return BadRequest(new { StatusCode = 404, Message = "given id is not matched with orderRequest id" });
            }

            _context.Entry(orderRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderRequestExists(id))
                {
                    return NotFound(new { StatusCode = 400, Message = "updation of OrderRequest'  is unsuccessfull." });
                }
                else
                {
                    throw;
                }
            }

            var response = new
            {
                StatusCode = 200,
                Message = "update order request successfully",
                Data = orderRequest
            };
            return Ok(response);
        }

        // POST: api/OrderRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("request/post")]
        public async Task<ActionResult<OrderRequest>> PostOrderRequest(OrderRequestModel orderRequest)
        {
            if (_context.OrderRequest == null)
            {
                return NotFound(new { StatusCode = 400, Message = "Entity set 'AppDbContext.OrderRequest'  is null." });
            }
            try
            {
                var request = new OrderRequest
                {
                    Desc = orderRequest.Desc,
                    Date = orderRequest.Date,
                    Time = orderRequest.Time,
                    selectedService = orderRequest.selectedService,
                    Price = orderRequest.Price,
                    UserContact = orderRequest.UserContact,
                    UserAddress = orderRequest.UserAddress,
                    Status = orderRequest.Status,
                    UserId = orderRequest.UserId,
                    CookInfoId = orderRequest.CookInfoId
                };
                _context.OrderRequest.Add(request);
                await _context.SaveChangesAsync();

                var response = new
                {
                    StatusCode = 200,
                    Message = "post order request successfully",
                    Data = request
                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = ex.Message });
            }
            
        }

        // DELETE: api/OrderRequests/5
        [HttpDelete("request/delete/{id}")]
        public async Task<IActionResult> DeleteOrderRequest(int id)
        {
            if (_context.OrderRequest == null)
            {
                return NotFound(new { StatusCode = 400, Message = "Entity set 'AppDbContext.OrderRequest'  is null." });
            }
            try
            {
                var orderRequest = await _context.OrderRequest.FindAsync(id);
                if (orderRequest == null)
                {
                    return NotFound(new { StatusCode = 400, Message = "OrderRequest'  is not found." });
                }

                _context.OrderRequest.Remove(orderRequest);
                await _context.SaveChangesAsync();

                var response = new
                {
                    StatusCode = 200,
                    Message = "delete order request successfully",
                    Data = orderRequest
                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = ex.Message });
            }
            
        }

        private bool OrderRequestExists(int id)
        {
            return (_context.OrderRequest?.Any(e => e.RqID == id)).GetValueOrDefault();
        }
    }
}
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
        public async Task<ActionResult> GetOrderRequest(string id)
        {
            if (_context.OrderRequest == null)
            {
                return NotFound(new { StatusCode = 400, Message = "Entity set 'AppDbContext.OrderRequest' is null." });
            }
            try
            {
                // Query the database for order requests associated with the specified user ID
                var orderRequests = await _context.OrderRequest
                    .Where(o => o.UserId == id)
                    .Include(o => o.CookInfo)
                    .ToListAsync();

                if (orderRequests == null || !orderRequests.Any())
                {
                    return NotFound(new { StatusCode = 404, Message = "No order requests found for the specified user." });
                }

                var response = new
                {
                    StatusCode = 200,
                    Message = "Order requests retrieved successfully",
                    Data = orderRequests.Select(o => new
                    {
                        o.RqID,
                        o.Desc,
                        o.selectedService,
                        o.Price,
                        o.UserAddress,
                        o.Status,
                        o.Date,
                        Cook = new
                        {
                            o.CookInfo.FirstName,
                            o.CookInfo.LastName,
                            o.CookInfo.Email,
                            o.CookInfo.SkillsAndSpecialties,
                            o.CookInfo.SignatureDishes,
                            o.CookInfo.ServicesProvided,
                            o.CookInfo.ExperienceYears,
                            o.CookInfo.CulinarySchoolName,
                            o.CookInfo.HasCulinaryDegree,
                            o.CookInfo.Degree,
                            o.CookInfo.Certificates,
                            o.CookInfo.EligibleToWork
                        }
                    })
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = ex.Message });
            }
        }


        private async Task<ActionResult> GetUserRequestsByStatus(string userId, string status)
        {
            if (_context.OrderRequest == null)
            {
                return NotFound(new { StatusCode = 400, Message = "Entity set 'AppDbContext.OrderRequest' is null." });
            }
            try
            {
                // Validate status input
                var validStatuses = new[] { "Accepted", "Declined", "Pending" };
                if (!validStatuses.Contains(status))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid status value. Valid values are: Accepted, Declined, Pending." });
                }
                // Query the database for order requests associated with the specified user ID and status
                var orderRequests = await _context.OrderRequest
                    .Where(o => o.UserId == userId && o.Status == status)
                    .Include(o => o.CookInfo)
                    .ToListAsync();

                if (orderRequests == null || !orderRequests.Any())
                {
                    return NotFound(new { StatusCode = 404, Message = $"No {status.ToLower()} order requests found for the specified user." });
                }

                var response = new
                {
                    StatusCode = 200,
                    Message = $"{status} order requests retrieved successfully",
                    Data = orderRequests.Select(o => new
                    {
                        o.CookInfoId,
                        o.Status,
                        o.Date,
                        Cook = new
                        {
                            o.CookInfo.FirstName,
                            o.CookInfo.LastName,
                            o.CookInfo.Email,
                            o.CookInfo.SkillsAndSpecialties,
                            o.CookInfo.SignatureDishes,
                            o.CookInfo.ServicesProvided,
                            o.CookInfo.ExperienceYears,
                            o.CookInfo.CulinarySchoolName,
                            o.CookInfo.HasCulinaryDegree,
                            o.CookInfo.Degree,
                            o.CookInfo.Certificates,
                            o.CookInfo.EligibleToWork
                        }
                    })
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = ex.Message });
            }
        }

        public async Task<ActionResult> GetCookRequestByStatus(string cookId, string status)
        {
            try
            {
                // Validate status input
                var validStatuses = new[] { "Accepted", "Declined", "Pending" };
                if (!validStatuses.Contains(status))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid status value. Valid values are: Accepted, Declined, Pending." });
                }

                // Retrieve order requests for the specified cook and status
                var orderRequests = await _context.OrderRequest
                    .Where(o => o.CookInfoId == cookId && o.Status == status)
                    .ToListAsync();

                if (orderRequests == null || !orderRequests.Any())
                {
                    return NotFound(new { StatusCode = 404, Message = $"No order requests found for cook with ID {cookId} and status {status}." });
                }

                var response = new
                {
                    StatusCode = 200,
                    Message = $"Order requests with status {status} retrieved successfully for cook with ID {cookId}.",
                    Data = orderRequests
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An error occurred while retrieving the order requests.", ErrorDetails = ex.Message });
            }
        }


        // GET: api/OrderRequests/user/accepted/5
        [HttpGet("request/getUserAcceptedRequests/{id}")]
        public async Task<ActionResult> GetUserAcceptedRequests(string id)
        {
            return await GetUserRequestsByStatus(id, "Accepted");
        }


        //get accepted orderRequest of Cook
        [HttpGet("request/getCookAcceptedRequests/{id}")]
        public async Task<ActionResult> GetAcceptedCookOrderRequests(string id)
        {
            return await GetCookRequestByStatus(id, "Accepted");
        }
        //get decline orderRequest of users
        // GET: api/OrderRequests/user/declined/5
        [HttpGet("request/getUserDeclinedRequests/{id}")]
        public async Task<ActionResult> GetUserDeclinedRequests(string id)
        {
            return await GetUserRequestsByStatus(id, "Declined");
        }


        //get decline orderRequest of Cook
        [HttpGet("request/getCookDeclinedRequests/{id}")]
        public async Task<ActionResult> GetDeclineCookOrderRequests(string id)
        {
            return await GetCookRequestByStatus(id, "Declined");
        }

        // GET: api/OrderRequests/user/pending/5
        [HttpGet("request/getUserPendingRequests/{id}")]
        public async Task<ActionResult> GetUserPendingRequests(string id)
        {
            return await GetUserRequestsByStatus(id, "Pending");
        }


        //get pending orderRequest of Cook
        [HttpGet("request/getCookPendingRequests/{id}")]
        public async Task<ActionResult> GetPendingCookOrderRequests(string id)
        {
            return await GetCookRequestByStatus(id, "Pending");
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
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = ex.Message });
            }
        }

        // PUT: api/OrderRequests/5
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = ex.Message });
            }

        }

        // 1. Return count of total orders received by a cook
        [HttpGet("orders/count/cook/received/{cookId}")]
        public async Task<ActionResult> GetTotalOrdersReceivedByCook(string cookId)
        {
            try
            {
                var count = await _context.OrderRequest.CountAsync(o => o.CookInfoId == cookId);
                return Ok(new { Count = count, Message = "Total recieved order requests by cook" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An error occurred while retrieving the count of orders received by the cook.", ErrorDetails = ex.Message });
            }
        }

        // 2. Return count of total orders received by a user
        [HttpGet("orders/count/user/received/{userId}")]
        public async Task<ActionResult> GetTotalOrdersReceivedByUser(string userId)
        {
            try
            {
                var count = await _context.OrderRequest.CountAsync(o => o.UserId == userId);
                return Ok(new { Count = count, Message = "Total user's order requests" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An error occurred while retrieving the count of orders received by the user.", ErrorDetails = ex.Message });
            }
        }

        // 3. Return count of total orders accepted by a cook
        [HttpGet("orders/count/cook/accepted/{cookId}")]
        public async Task<ActionResult> GetTotalOrdersAcceptedByCook(string cookId)
        {
            try
            {
                var count = await _context.OrderRequest.CountAsync(o => o.CookInfoId == cookId && o.Status == "Accepted");
                return Ok(new { Count = count, Message = "Total accepted order requests by cook" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An error occurred while retrieving the count of orders accepted by the cook.", ErrorDetails = ex.Message });
            }
        }

        // 4. Return count of total orders accepted by a user
        [HttpGet("orders/count/user/accepted/{userId}")]
        public async Task<ActionResult> GetTotalOrdersAcceptedByUser(string userId)
        {
            try
            {
                var count = await _context.OrderRequest.CountAsync(o => o.UserId == userId && o.Status == "Accepted");
                return Ok(new { Count = count, Message = "Total declined user's order requests" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An error occurred while retrieving the count of orders accepted by the user.", ErrorDetails = ex.Message });
            }
        }

        // 5. Return count of total orders declined by a cook
        [HttpGet("orders/count/cook/declined/{cookId}")]
        public async Task<ActionResult> GetTotalOrdersDeclinedByCook(string cookId)
        {
            try
            {
                var count = await _context.OrderRequest.CountAsync(o => o.CookInfoId == cookId && o.Status == "Declined");
                return Ok(new { Count = count, Message = "Total declined order requests by cook" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An error occurred while retrieving the count of orders declined by the cook.", ErrorDetails = ex.Message });
            }
        }

        // 6. Return count of total orders declined by a user
        [HttpGet("orders/count/user/declined/{userId}")]
        public async Task<ActionResult> GetTotalOrdersDeclinedByUser(string userId)
        {
            try
            {
                var count = await _context.OrderRequest.CountAsync(o => o.UserId == userId && o.Status == "Declined");
                return Ok(new { Count = count, Message = "Total declined user's order requests" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An error occurred while retrieving the count of orders declined by the user.", ErrorDetails = ex.Message });
            }
        }

        private bool OrderRequestExists(int id)
        {
            return (_context.OrderRequest?.Any(e => e.RqID == id)).GetValueOrDefault();
        }
    }
}
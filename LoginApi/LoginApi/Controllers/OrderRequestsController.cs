﻿using System;
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

            // Query the database for order requests associated with the specified user ID
            var orderRequests = await _context.OrderRequest
                .Where(o => o.UserId == id)
                .ToListAsync();

            if (orderRequests == null || !orderRequests.Any())
            {
                return NotFound(new { StatusCode = 404, Message = "No order requests found for the specified user." });
            }

            var response = new
            {
                StatusCode = 200,
                Message = "Order requests retrieved successfully",
                Data = orderRequests
            };
            return Ok(response);
        }

        // GET: api/OrderRequests/5
        [HttpGet("request/get/{id}")]
        public async Task<ActionResult<OrderRequest>> GetOrderRequest(int id)
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
            var request = new OrderRequest
            {
                Desc = orderRequest.Desc,
                Date = orderRequest.Date,
                Time = orderRequest.Time,
<<<<<<< HEAD
                selectedService = orderRequest.selectedService,
                Price = orderRequest.Price,
                UserContact = orderRequest.UserContact,
                UserAddress = orderRequest.UserAddress,
                Status = orderRequest.Status,
                UserId = orderRequest.UserId,
                CookInfoId = orderRequest.CookInfoId,
=======
                selectedService=orderRequest.selectedService,
                Price= orderRequest.Price,
                UserContact= orderRequest.UserContact,
                UserAddress= orderRequest.UserAddress,
                Status= orderRequest.Status,
                UserId= orderRequest.UserId,
                CookInfoId= orderRequest.CookInfoId,    
>>>>>>> b5f5cac315ea017550c1cea16401117156de7dde
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

        // DELETE: api/OrderRequests/5
        [HttpDelete("request/delete/{id}")]
        public async Task<IActionResult> DeleteOrderRequest(int id)
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

        private bool OrderRequestExists(int id)
        {
            return (_context.OrderRequest?.Any(e => e.RqID == id)).GetValueOrDefault();
        }
    }
}
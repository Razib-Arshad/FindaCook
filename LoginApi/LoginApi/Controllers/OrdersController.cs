﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoginApi.Models;
using static NuGet.Packaging.PackagingConstants;
using LoginApi.ViewModels;

namespace LoginApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet("orders/get")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            if (_context.Order == null)
            {
                return NotFound(new { StatusCode = 404, Message = "Orders not found" });
            }
            var orders = await _context.Order.ToListAsync();
            var response = new
            {
                StatusCode = 200,
                Message = "orders retrieved successfully",
                Data = orders
            };
            return Ok(response);
        }

        // GET: api/Orders/5
        [HttpGet("order/get/{id}")]
        public async Task<ActionResult<Order>> GetOrder(string id)
        {
            if (_context.Order == null)
            {
                return NotFound(new { StatusCode = 404, Message = "Order table is empty" });
            }

            try
            {
                var order = await _context.Order.FindAsync(id);

                if (order == null)
                {
                    return NotFound(new { StatusCode = 404, Message = "Order not found with this " + id });
                }

                var response = new
                {
                    StatusCode = 200,
                    Message = "order retrieved successfully",
                    Data = order
                };

                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = ex.Message });
            }
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("order/update/{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderViewModel order)
        {
            if (id != order.OrderId)
            {
                return BadRequest(new { StatusCode = 400, Message = "Invalid Request" });
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                var response = new
                {
                    StatusCode = 200,
                    Message = "update order successfully"
                };
                return Ok(response);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound(new { StatusCode = 404, Message = "Order not found with this " + id });
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("order/post")]
        public async Task<ActionResult<Order>> PostOrder(OrderViewModel orderViewModel)
        {
            try
            {
                if (_context.Order == null)
                {
                    return NotFound(new { StatusCode = 400, Message = "Entity set 'AppDbContext.Order' is null." });
                }

                var order = new Order
                {
                    RqId = orderViewModel.RqId
                };

                _context.Order.Add(order);
                await _context.SaveChangesAsync();

                var response = new
                {
                    StatusCode = 200,
                    Message = "Post order successful"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = ex.Message });
            }
        }


        // DELETE: api/Orders/5
        [HttpDelete("order/delete/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Order == null)
            {
                return NotFound(new { StatusCode = 400, Message = "Entity set 'AppDbContext.Order'  is null." });
            }
            try
            {
                var order = await _context.Order.FindAsync(id);
                if (order == null)
                {
                    return NotFound(new { StatusCode = 400, Message = "Order'  is not found wiht this " + id });
                }

                _context.Order.Remove(order);
                await _context.SaveChangesAsync();

                var response = new
                {
                    StatusCode = 200,
                    Message = "delete order successfully"
                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = ex.Message });
            }
        }

        private bool OrderExists(int id)
        {
            return (_context.Order?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
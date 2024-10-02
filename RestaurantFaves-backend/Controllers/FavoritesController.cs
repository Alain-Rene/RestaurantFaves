using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantFaves.Models;

namespace RestaurantFaves.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        RestaurantDbContext dbContext = new RestaurantDbContext();

        [HttpGet()]
        public IActionResult GetAll(string? restaurant = null, bool? orderAgain = null)
        {
            List<Order> result = dbContext.Orders.ToList();
            if (restaurant != null)
            {
                result = result.Where(p => p.Restaurant == restaurant).ToList();
            }
            if (orderAgain != null)
            {
                result = result.Where(p => p.OrderAgain == orderAgain).ToList();
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Order result = dbContext.Orders.FirstOrDefault(p => p.Id == id);
            if(result == null)
            {
                return NotFound("No matching id");
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost]
        public IActionResult AddOrder([FromBody] Order newOrder)
        {
            if(newOrder.Rating < 1 || newOrder.Rating > 5)
            {
                return BadRequest("Rating must be between 1 and 5 stars.");
            }
            newOrder.Id = 0;
            dbContext.Orders.Add(newOrder);
            dbContext.SaveChanges();
            
            return Created($"/api/Orders/{newOrder.Id}", newOrder);
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] Order updated)
        {
            if(updated.Id != id) {return BadRequest("Ids dont match"); }
            if(dbContext.Orders.Any(p => p.Id == id) == false) {return NotFound("No matching ids");}

            dbContext.Orders.Update(updated);
            dbContext.SaveChanges();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            Order result = dbContext.Orders.FirstOrDefault(p => p.Id == id);
            if (result == null)
            {
                return NotFound("No matching id");
            }
            else
            {
                dbContext.Orders.Remove(result);
                dbContext.SaveChanges();
                return NoContent();
            }
        }
    }
}
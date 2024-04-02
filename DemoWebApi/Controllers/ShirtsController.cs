using DemoWebApi.Data;
using DemoWebApi.Filters;
using DemoWebApi.Filters.ActionFilters;
using DemoWebApi.Filters.AuthFilters;
using DemoWebApi.Filters.ExceptionFilters;
using DemoWebApi.Models;
using DemoWebApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [JwtTokenAuthFilter]
    public class ShirtsController(ApplicationDbContext db) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetShirts()
        {
            return Ok(db.Shirt.ToList());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        public IActionResult GetShirtsByID(int id)
        {
            return Ok(HttpContext.Items["shirt"]);
        }

        [HttpPost]
        [TypeFilter(typeof(Shirt_ValidateCreateShirtFilterAttribute))]
        public IActionResult CreateShirts([FromBody]Shirt shirt)
        {
            db.Shirt.Add(shirt);
            db.SaveChanges();

            return CreatedAtAction(nameof(GetShirtsByID), new { id = shirt.ShirtId }, shirt);
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        [Shirt_ValidateUpdateShirtFilter]
        [TypeFilter(typeof(Shirt_HandleUpdateExceptionFilterAttribute))]
        public IActionResult UpdateShirts(int id, Shirt shirt)
        {
            var shirtToUpdate = HttpContext.Items["shirt"] as Shirt;
            if (shirtToUpdate != null)
            {
                shirtToUpdate.Brand = shirt.Brand;
                shirtToUpdate.price = shirt.price;
                shirtToUpdate.Size = shirt.Size;
                shirtToUpdate.Color = shirt.Color;
                shirtToUpdate.Gender = shirt.Gender;
            }
            db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        public IActionResult DeleteShirts(int id)
        {
            var shirtToDelete = HttpContext.Items["shirt"] as Shirt;

            db.Shirt.Remove(shirtToDelete);
            db.SaveChanges();
            
            return Ok(shirtToDelete);
        }
    }
}

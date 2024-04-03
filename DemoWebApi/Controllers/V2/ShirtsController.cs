using DemoWebApi.Attributes;
using DemoWebApi.Data;
using DemoWebApi.Filters;
using DemoWebApi.Filters.ActionFilters;
using DemoWebApi.Filters.ActionFilters.V2;
using DemoWebApi.Filters.AuthFilters;
using DemoWebApi.Filters.ExceptionFilters;
using DemoWebApi.Models;
using DemoWebApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebApi.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiController]
    //[Route("api/v{v:apiVersion}/[controller]")]
    [JwtTokenAuthFilter]
    public class ShirtsController(ApplicationDbContext db) : ControllerBase
    {
        [HttpGet]
        [RequiredClaim("read", "true")]
        public IActionResult GetShirts()
        {
            return Ok(db.Shirt.ToList());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        [RequiredClaim("read", "true")]
        public IActionResult GetShirtsByID(int id)
        {
            return Ok(HttpContext.Items["shirt"]);
        }

        [HttpPost]
        [TypeFilter(typeof(Shirt_ValidateCreateShirtFilterAttribute))]
        [Shirt_EnsureDescriptionIsPresentFilter]
        [RequiredClaim("write", "true")]
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
        [Shirt_EnsureDescriptionIsPresentFilter]
        [RequiredClaim("write", "true")]
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
                shirtToUpdate.Description = shirt.Description;
            }
            db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        [RequiredClaim("delete", "true")]
        public IActionResult DeleteShirts(int id)
        {
            var shirtToDelete = HttpContext.Items["shirt"] as Shirt;

            db.Shirt.Remove(shirtToDelete);
            db.SaveChanges();
            
            return Ok(shirtToDelete);
        }
    }
}

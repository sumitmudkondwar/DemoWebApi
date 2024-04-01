using DemoWebApi.Data;
using DemoWebApi.Filters;
using DemoWebApi.Filters.ActionFilters;
using DemoWebApi.Filters.ExceptionFilters;
using DemoWebApi.Models;
using DemoWebApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [Shirt_ValidateCreateShirtFilter]
        public IActionResult CreateShirts([FromForm]Shirt shirt)
        {
            ShirtRepository.AddShirt(shirt);

            return CreatedAtAction(nameof(GetShirtsByID), new { id = shirt.ShirtId }, shirt);
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        [Shirt_ValidateUpdateShirtFilter]
        [Shirt_HandleUpdateExceptionFilter]
        public IActionResult UpdateShirts(int id, Shirt shirt)
        {
            ShirtRepository.UpdateShirt(shirt);
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        public IActionResult DeleteShirts(int id)
        {
            var shirt = ShirtRepository.GetShirtById(id);
            ShirtRepository.DeleteShirt(id);

            return Ok(shirt);
        }
    }
}

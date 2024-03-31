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
    public class ShirtsController: ControllerBase
    {
        [HttpGet]
        public IActionResult GetShirts()
        {
            return Ok(ShirtRepository.GetShirts());
        }

        [HttpGet("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult GetShirtsByID(int id)
        {
            return Ok(ShirtRepository.GetShirtById(id));
        }

        [HttpPost]
        [Shirt_ValidateCreateShirtFilter]
        public IActionResult CreateShirts([FromForm]Shirt shirt)
        {
            ShirtRepository.AddShirt(shirt);

            return CreatedAtAction(nameof(GetShirtsByID), new { id = shirt.ShirtId }, shirt);
        }

        [HttpPut("{id}")]
        [Shirt_ValidateShirtIdFilter]
        [Shirt_ValidateUpdateShirtFilter]
        [Shirt_HandleUpdateExceptionFilter]
        public IActionResult UpdateShirts(int id, Shirt shirt)
        {
            ShirtRepository.UpdateShirt(shirt);
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult DeleteShirts(int id)
        {
            var shirt = ShirtRepository.GetShirtById(id);
            ShirtRepository.DeleteShirt(id);

            return Ok(shirt);
        }
    }
}

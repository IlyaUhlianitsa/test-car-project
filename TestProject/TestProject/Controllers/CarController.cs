using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestProject.Services;
using TestProject.Services.Models;

namespace TestProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> Get([FromRoute]long id)
        {
            Car car = await _carService.Get(id);
            if (car == null)
                return NotFound();

            return Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Car car)
        {
            Car newCar = await _carService.Create(car);
            if (newCar == null)
                return BadRequest();

            return Ok(newCar);
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody]Car car)
        {
            bool updated = await _carService.Update(car);
            if (!updated)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete([FromRoute]long id)
        {
            bool deleted = await _carService.Delete(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
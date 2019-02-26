using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestProject.Services;
using TestProject.Services.Models;

namespace TestProject.Controllers
{
    [Route("api/[controller]")]
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
            return Ok(car);
        }
    }
}
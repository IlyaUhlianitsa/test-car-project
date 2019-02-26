using System.Threading.Tasks;
using TestProject.Data.Entities;
using TestProject.Repositories;
using TestProject.Services.Models;

namespace TestProject.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Car> Get(long carId)
        {
            CarEntity carEntity = await _carRepository.Get(carId);
            if (carEntity == null)
                return null;

            GarageEntity garage = carEntity.Garage;
            return new Car // we could use AutoMapper here
            {
                Id = carId,
                Description = carEntity.Description,
                Title = carEntity.Title,
                GarageId = garage.Id,
                GarageName = garage.Name,
                AreaId = garage.AreaId,
                AreaName = garage.Area.Name,
                CategoryId = carEntity.CategoryId,
                CategoryName = carEntity.Category.Name
            };
        }
    }
}

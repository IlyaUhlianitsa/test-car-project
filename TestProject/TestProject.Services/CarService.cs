using System;
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

            return ConvertToModel(carEntity);
        }

        public async Task<Car> Create(Car car)
        {
            var carEntity = new CarEntity
            {
                Title = car.Title,
                Description = car.Description,
                DateCreated = DateTime.Now,
                CategoryId = car.CategoryId,
                GarageId = car.GarageId
            };

            bool created = await _carRepository.Create(carEntity);
            if (!created)
                return null;

            CarEntity newCar = await _carRepository.Get(carEntity.Id);
            return ConvertToModel(newCar);
        }

        public async Task<bool> Update(Car car)
        {
            if (!car.Id.HasValue)
                return false;
            CarEntity carEntity = await _carRepository.Get(car.Id.Value);
            if (carEntity == null)
                return false;

            await _carRepository.Update(carEntity, car.Description);
            return true;
        }

        public async Task<bool> Delete(long id)
        {
            CarEntity carEntity = await _carRepository.Get(id);
            if(carEntity == null)
                return false;

            await _carRepository.Delete(carEntity);
            return true;
        }

        private static Car ConvertToModel(CarEntity carEntity)
        {
            if (carEntity == null)
                return null;

            GarageEntity garage = carEntity.Garage;
            return new Car // we could use AutoMapper here
            {
                Id = carEntity.Id,
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

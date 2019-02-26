using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestProject.Data;
using TestProject.Data.Entities;

namespace TestProject.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarDbContext _context;

        public CarRepository(CarDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CarEntity>> GetByGarage(long garageId)
        {
            return await _context.Cars
                .Where(x => x.GarageId == garageId)
                .ToListAsync();
        }

        public async Task<CarEntity> Get(long carId)
        {
            return await _context.Cars.FirstOrDefaultAsync(x => x.Id == carId);
        }

        public async Task Create(CarEntity car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CarEntity car, string description)
        {
            car.Description = description;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long carId)
        {
            _context.Cars.Remove(new CarEntity() { Id = carId });
            await _context.SaveChangesAsync();
        }
    }
}

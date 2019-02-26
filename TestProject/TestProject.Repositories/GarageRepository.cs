using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestProject.Data;
using TestProject.Data.Entities;

namespace TestProject.Repositories
{
    public class GarageRepository : IGarageRepository
    {
        private readonly CarDbContext _context;

        public GarageRepository(CarDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GarageEntity>> GetAll()
        {
            return await _context.Garages.ToListAsync();
        }

        public async Task<IEnumerable<GarageEntity>> GetByArea(string areaName)
        {
            return await _context.Garages
                .Where(x => x.Area.Name == areaName)
                .ToListAsync();
        }

        public async Task Create(GarageEntity garage)
        {
            await _context.AddAsync(garage);
            await _context.SaveChangesAsync();
        }

        public async Task Update(GarageEntity garage, string name)
        {
            garage.Name = name;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long garageId)
        {
            _context.Garages.Remove(new GarageEntity {Id = garageId});
            await _context.SaveChangesAsync();
        }
    }
}

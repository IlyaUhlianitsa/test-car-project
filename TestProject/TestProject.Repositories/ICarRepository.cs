using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.Data.Entities;

namespace TestProject.Repositories
{
    public interface ICarRepository
    {
        Task<IEnumerable<CarEntity>> GetByGarage(long garageId);

        Task<CarEntity> Get(long carId);

        Task Create(CarEntity car);

        Task Update(CarEntity car, string description);

        Task Delete(long carId);
    }
}
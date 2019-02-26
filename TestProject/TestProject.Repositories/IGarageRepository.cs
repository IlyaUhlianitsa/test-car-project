using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.Data.Entities;

namespace TestProject.Repositories
{
    public interface IGarageRepository
    {
        Task<IEnumerable<GarageEntity>> GetAll();

        Task<IEnumerable<GarageEntity>> GetByArea(string areaName);

        Task Create(GarageEntity garage);

        Task Update(GarageEntity garage, string name);

        Task Delete(long garageId);
    }
}
using System.Threading.Tasks;
using TestProject.Services.Models;

namespace TestProject.Services
{
    public interface ICarService
    {
        Task<Car> Get(long carId);

        Task<Car> Create(Car car);

        Task<bool> Update(Car car);

        Task<bool> Delete(long id);
    }
}
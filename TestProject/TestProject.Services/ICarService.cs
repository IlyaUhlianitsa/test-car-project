using System.Threading.Tasks;
using TestProject.Services.Models;

namespace TestProject.Services
{
    public interface ICarService
    {
        Task<Car> Get(long carId);
    }
}
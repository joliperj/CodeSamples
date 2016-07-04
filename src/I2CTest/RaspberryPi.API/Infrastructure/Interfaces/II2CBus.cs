using System.Threading.Tasks;

namespace RaspberryPi.API.Infrastructure.Interfaces
{
    public interface II2CBus
    {
        Task InitializeI2CBus(int deviceAddress);
    }
}

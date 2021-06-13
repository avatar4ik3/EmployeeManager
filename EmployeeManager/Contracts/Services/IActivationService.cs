using System.Threading.Tasks;

namespace EmployeeManager.Contracts.Services
{
    public interface IActivationService
    {
        Task ActivateAsync(object activationArgs);
    }
}

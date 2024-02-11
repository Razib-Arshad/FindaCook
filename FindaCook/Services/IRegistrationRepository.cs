using System.Threading.Tasks;
using FindaCook.Maui.Models;


namespace FindaCook.Services
{
    public interface IRegistrationRepository
    {
        Task<RegistrationResultClass> Register(string name, string email, string password, string retypePassword);
    }
}

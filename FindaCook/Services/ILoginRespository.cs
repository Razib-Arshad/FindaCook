
using FindaCook.Maui.Models;

namespace FindaCook.Maui.Models
{
    public interface ILoginRespository
    {
        Task<User> Login(string email, string password);
        
    }
}

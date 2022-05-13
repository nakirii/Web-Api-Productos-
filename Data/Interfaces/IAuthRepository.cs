using System.Threading.Tasks;
using WebApiCRUD.Models;

namespace WebApiCRUD.Data.Interfaces
{
    public interface IAuthRepository
    {
        Task<Usuario>Registrar(Usuario usuario, string password);
        Task<Usuario>Login(string Email, string password);

        Task<bool> ExisteUsuaruio(string Email);
    }
}
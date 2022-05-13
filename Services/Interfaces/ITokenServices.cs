using WebApiCRUD.Models;

namespace WebApiCRUD.Services.Interfaces
{
    public interface ITokenServices
    {
        string CreateToken(Usuario usuario);
    }
}
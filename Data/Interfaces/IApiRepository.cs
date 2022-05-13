using System.Threading.Tasks;
using System.Collections.Generic;
using WebApiCRUD.Models;

namespace WebApiCRUD.Data.Interfaces
{
    public interface IApiRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<IEnumerable<Usuario>> GetUsuariosAsync();
        Task<Usuario>GetUsuarioByIdAsync(int id);
        Task<Usuario>GetUsuarioByNombreAsync(string nombre);

        Task<IEnumerable<Producto>>GetProductosAsync();
        Task<Producto>GetProductoByIdAsync(int id);
        Task<Producto>GetProductoByNombreAsync(string nombre);

    }
}
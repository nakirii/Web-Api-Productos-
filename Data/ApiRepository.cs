using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiCRUD.Data.Interfaces;
using WebApiCRUD.Models;

namespace WebApiCRUD.Data
{   
    public class ApiRepository : IApiRepository
    {
        private readonly DataContext _context;

        public ApiRepository (DataContext context){
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            var producto = await  _context.Productos.FirstOrDefaultAsync(u => u.Id == id);
            return producto;
        }

        public async Task<Producto> GetProductoByNombreAsync(string nombre)
        {
            var producto = await  _context.Productos.FirstOrDefaultAsync(u => u.Nombre == nombre);
            return producto;
        }

        public async Task<IEnumerable<Producto>> GetProductosAsync()
        {
            var producto = await  _context.Productos.ToListAsync();
            return producto;
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
           var usuario = await  _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            return usuario;
        }

        public async Task<Usuario> GetUsuarioByNombreAsync(string nombre)
        {
            var usuario = await  _context.Usuarios.FirstOrDefaultAsync(u => u.Nombre == nombre);
            return usuario;
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
        {
            var usuario = await  _context.Usuarios.ToListAsync();
            return usuario;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
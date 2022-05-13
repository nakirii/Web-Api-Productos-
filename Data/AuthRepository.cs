using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiCRUD.Data.Interfaces;
using WebApiCRUD.Models;

namespace WebApiCRUD.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> ExisteUsuaruio(string email)
        {
            if(await _context.Usuarios.AnyAsync(x=> x.Email == email))
                return true;

            return false;
        }

        public async Task<Usuario> Login(string email, string password)
        {
           var usuario = await _context.Usuarios.FirstOrDefaultAsync(x=> x.Email == email);
           if(usuario == null)
                return null;
           if(!VerifyPasswordHash(password,usuario.PasswordHash, usuario.PasswordSalt))
                return null;
            return usuario;
        }

        private bool VerifyPasswordHash(string password,byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(PasswordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0; i< computeHash.Length; i++)
                {
                    if(computeHash[i] != PasswordHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<Usuario> Registrar(Usuario usuario, string password)
        {
           byte[] PasswordHash;
           byte[] PasswordSalt;
           CreatePasswordHash(password, out PasswordHash, out PasswordSalt);
           usuario.PasswordHash= PasswordHash;
           usuario.PasswordSalt = PasswordSalt;

           await _context.Usuarios.AddAsync(usuario);
           await _context.SaveChangesAsync();

           return usuario;
        }


        public void CreatePasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512()){
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
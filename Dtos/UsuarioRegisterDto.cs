using System;

namespace WebApiCRUD.Dtos
{
    public class UsuarioRegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string Nombre { get; set; }
        public DateTime FechaDeAlta { get; set; }
        public bool Activo { get; set; }

        public UsuarioRegisterDto ()
        {
            FechaDeAlta = DateTime.Now;
            Activo = true;
        }

    }
}
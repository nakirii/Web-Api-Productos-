using System;
namespace WebApiCRUD.Dtos
{
    public class CreateProductoDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Decimal Precio { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaDeAlta{ get; set; }


        public CreateProductoDto()
        {
            FechaDeAlta = DateTime.Now;
            Activo = true;
        }
    }
}
using System;
namespace WebApiCRUD.Dtos
{
    public class UpdateProductoDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public Decimal Precio { get; set; }
    }
}
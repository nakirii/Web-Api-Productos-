using System;

namespace WebApiCRUD.Dtos
{
    public class ListPtoductosDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Decimal Precio { get; set; }
    }
}
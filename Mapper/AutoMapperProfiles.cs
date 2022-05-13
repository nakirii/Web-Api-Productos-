using AutoMapper;
using WebApiCRUD.Dtos;
using WebApiCRUD.Models;

namespace WebApiCRUD.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //POST O CREATE
            CreateMap<CreateProductoDto,Producto>();
            //PUT o UPDATE
            CreateMap<UpdateProductoDto,Producto>();
            //GET o LIST
            CreateMap<Producto, ListPtoductosDto>();

            //usuario
            CreateMap<UsuarioRegisterDto, Usuario>();
            CreateMap<UsuarioLoginDto, Usuario>();
            CreateMap<Usuario, ListUsuarioDto>();
        }
        // .ReverseMap() para realizar de ambos sentidos 
    }
}
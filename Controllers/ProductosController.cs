using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiCRUD.Data.Interfaces;
using WebApiCRUD.Dtos;
using WebApiCRUD.Models;

namespace WebApiCRUD.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IApiRepository _repo;
        private readonly IMapper _mapper;


        public ProductosController(IApiRepository iapiRepository, IMapper mapper)
        {
            _repo = iapiRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> OptenerProductos(){
            var producto = await _repo.GetProductosAsync();
            return Ok(producto);
        }


        [HttpGet("nombre/{nombre}")]
         public async Task<IActionResult> optenerProductoxNombre(string nombre){
            var productoNombre= await _repo.GetProductoByNombreAsync(nombre);
            if(productoNombre == null)
                return NotFound("Producto no encontrado");
            return Ok(productoNombre);
        }

       //permite que cualquiera pueda acceder a este metodo [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> OptenerProductoxId(int id){

            var productoId= await _repo.GetProductoByIdAsync(id);
            if(productoId == null)
                return NotFound("Producto no encontrado");


            var listproductos = new ListPtoductosDto();
            /*listproductos.Id = productoId.Id;
            listproductos.Descripcion = productoId.Descripcion;
            listproductos.Nombre = productoId.Nombre;
            listproductos.Precio = productoId.Precio;*/

            _mapper.Map(productoId,listproductos);

            return Ok(listproductos);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarProducto(CreateProductoDto productoDto){

           /* var productoToCreate= new Producto();
            productoToCreate.Nombre = productoDto.Nombre;
            productoToCreate.Descripcion = productoDto.Descripcion;
            productoToCreate.Precio = productoDto.Precio;*/

            var productoToCreate= _mapper.Map<Producto>(productoDto);

            _repo.Add(productoToCreate);
            if(await _repo.SaveAll())
                return Ok(productoToCreate);
            return BadRequest();
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, UpdateProductoDto productoDto){

            if (id != productoDto.Id)
                return BadRequest("El producto no se pudo actualizar");

            var productoToUpdate = await _repo.GetProductoByIdAsync(productoDto.Id);

            if(productoToUpdate == null)
                return BadRequest();

            /*productoToUpdate.Descripcion = productoDto.Descripcion;
            productoToUpdate.Precio = productoDto.Precio;*/

            _mapper.Map(productoDto,productoToUpdate);

            if(!await _repo.SaveAll())
                return NoContent();
            return Ok(productoToUpdate);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id){
            var productoToDelete= await _repo.GetProductoByIdAsync(id);
            
            if(productoToDelete == null)
                return NotFound("Producto no Encontrado");
            
            _repo.Delete(productoToDelete);

            if(!await _repo.SaveAll())
                return BadRequest("No se Pudo eliminar el Producto");

            return Ok("Producto Eliminado");

        }

    }
}
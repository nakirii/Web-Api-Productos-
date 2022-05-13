using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiCRUD.Data.Interfaces;
using WebApiCRUD.Dtos;
using WebApiCRUD.Models;
using WebApiCRUD.Services.Interfaces;

namespace WebApiCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly ITokenServices _tokenService;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repo,ITokenServices tokenService,IMapper mapper)
        {
            _repo = repo;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UsuarioRegisterDto usuarioRegisterDto)
        {
            usuarioRegisterDto.Email = usuarioRegisterDto.Email.ToLower();
            if(await _repo.ExisteUsuaruio(usuarioRegisterDto.Email))
                return BadRequest("Usuario ya se encuentra Registrado");
            var usuarioNuevo = _mapper.Map<Usuario>(usuarioRegisterDto);
            var usuarioCreado = await _repo.Registrar(usuarioNuevo,usuarioRegisterDto.Password);
            var usuarioCreadoDto = _mapper.Map<ListUsuarioDto>(usuarioCreado);
            return Ok(usuarioCreadoDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto)
        {
            var usuarioFromRepo = await _repo.Login(usuarioLoginDto.Email, usuarioLoginDto.Password);
            if(usuarioFromRepo == null)
                return Unauthorized();

            var usuario = _mapper.Map<ListUsuarioDto>(usuarioFromRepo);

            var token = _tokenService.CreateToken(usuarioFromRepo);
            return Ok(new{
                token = token,
                usuario = usuario
            });
        }       

    }
}
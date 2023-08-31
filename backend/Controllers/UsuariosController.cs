using backend.connection;
using backend.entidades;
using backend.servicios;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[EnableCors("CorsDev")] //ESTE PERMITE LA ENTRA DE DE HTTP DES DE FUERA AÑANIENDO EL cors de Program.cs
[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly string? connectionString;

    public UsuariosController(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = _configuration["SqlConnectionString:DefaultConnection"];
        BDManager.GetInstance.ConnectionString = connectionString;
    }

    [HttpGet]
    [Route("GetAllUsuarios")]
    public IActionResult GetAllUsuarios()
    {
        try
        {
            var result = UsuariosServicios.ObtenerTodo2<Usuarios>();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("GetTop10Usuarios")]
    public IActionResult GetTop10Usuarios()
    {
        try
        {
            var result = UsuariosServicios.ObtenerTodoTopDiez<Usuarios>();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("GetUsuariosById")]
    public IActionResult GetUsuariosById([FromQuery] int id)
    {
        try
        {
            var result = UsuariosServicios.ObtenerById2<Usuarios>(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("AddUsuario")]
    public IActionResult AddUsuario(Usuarios usuarios)
    {
        try
        {
            var result = UsuariosServicios.InsertUsuario2(usuarios);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete]
    [Route("DeleteUsuarioById")]
    public IActionResult DeleteUsuarioById([FromQuery] int id)
    {
        try
        {
            UsuariosServicios.EliminarPorIdUsuarios(id);
            return Ok("Usuario eliminado correctamente");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPatch]
    [Route("UpdateUsuario")]
    public IActionResult UpdateUsuario(Usuarios usuarios)
    {
        try
        {
            UsuariosServicios.ActualizarUsuarios(usuarios);
            return Ok("Usuario actualizado correctamente");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

}
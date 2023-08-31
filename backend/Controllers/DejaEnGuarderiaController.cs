using backend.connection;
using backend.entidades;
using backend.servicios;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[EnableCors("CorsDev")] //ESTE PERMITE LA ENTRA DE DE HTTP DES DE FUERA AÑANIENDO EL cors de Program.cs
[ApiController]
[Route("api/[controller]")]
public class DejaEnGuarderiaController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly string? connectionString;

    public DejaEnGuarderiaController(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = _configuration["SqlConnectionString:DefaultConnection"];
        BDManager.GetInstance.ConnectionString = connectionString;
    }

    [HttpGet]
    [Route("GetLastTen")]
    public IActionResult GetLastTen()
    {
        try
        {
            var result = DejaEnGuarderiaServicio.ObtenerLestTen<DejaEnGuarderia>();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("AddDejaEnGuarderia")]
    public IActionResult AddDejaEnGuarderia(DejaEnGuarderia dejaEnGuarderia)
    {
        try
        {
            var result = DejaEnGuarderiaServicio.InsertDejaEnGuarderiaServicio(dejaEnGuarderia);
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
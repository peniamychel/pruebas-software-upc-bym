using backend.connection;
using backend.entidades;
using backend.servicios;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[EnableCors("CorsDev")] //ESTE PERMITE LA ENTRA DE DE HTTP DES DE FUERA AÑANIENDO EL cors de Program.cs
[ApiController]
[Route("api/[controller]")]
public class NinioController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly string? connectionString;

    public NinioController(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = _configuration["SqlConnectionString:DefaultConnection"];
        BDManager.GetInstance.ConnectionString = connectionString;
    }

    [HttpGet]
    [Route("GetNiniosLestTen")]
    public IActionResult GetNiniosLestTen()
    {
        try
        {
            var result = NinioServicios.ObtenerLestTen<Ninio>();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("GetNinioById")]
    public IActionResult GetNinioById([FromQuery] int id)
    {
        try
        {
            var result = NinioServicios.ObtenerById<Ninio>(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("AddNinio")]
    public IActionResult AddNinio(Ninio ninio)
    {
        try
        {
            var result = NinioServicios.InsertNinioServicio(ninio);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete]
    [Route("DeleteNinioById")]
    public IActionResult DeleteNinioById([FromQuery] int id)
    {
        try
        {
            NinioServicios.EliminarPorIdNinio(id);
            //var objecto = new dynamic();
            //objecto.result = "Ninio eliminado correctamente";
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    [HttpPatch]
    [Route("UpdateNinio")]
    public IActionResult UpdateNinio(Ninio ninio)
    {
        try
        {
            NinioServicios.ActualizarNinio(ninio);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
}
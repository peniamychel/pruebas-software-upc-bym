using backend.connection;
using backend.entidades;
using backend.servicios;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DetalleCarritoController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly string? connectionString;

    public DetalleCarritoController(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = _configuration["SqlConnectionString:DefaultConnection"];
        BDManager.GetInstance.ConnectionString = connectionString;
    }

    [HttpGet]
    [Route("GetAllDetalleCarrito")]
    public IActionResult GetAllDetalleCarrito()
    {
        try
        {
            var result = DetalleCarritoServicios.ObtenerTodo<DetalleCarrito>();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    [HttpGet]
    [Route("GetDetalleCarritoById")]
    public IActionResult GetDetalleCarritoById([FromQuery] int id)
    {
        try
        {
            var result = DetalleCarritoServicios.ObtenerById<DetalleCarrito>(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("AddDetalleCarrito")]
    public IActionResult AddDetalleCarrito(DetalleCarrito detalleCarrito)
    {
        try
        {
            var result = DetalleCarritoServicios.InsertDetalleCarrito(detalleCarrito);
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
            DetalleCarritoServicios.EliminarPorId(id);
            return Ok("Usuario eliminado correctamente");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    [Route("UpdateDetalleCarrito")]
    public IActionResult UpdateDetalleCarrito([FromBody] DetalleCarrito detalleCarrito)
    {
        try
        {
            var result = DetalleCarritoServicios.PrucedureUpdateDetalleCarrito(detalleCarrito);

            if (result != null)
            {
                return Ok("Detalle de carrito actualizado correctamente");
            }
            else
            {
                return NotFound("No se encontró el detalle de carrito con el ID especificado");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
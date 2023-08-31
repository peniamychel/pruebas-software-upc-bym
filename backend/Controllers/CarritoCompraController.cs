using backend.connection;
using backend.entidades;
using backend.servicios;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarritoCompraController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly string? connectionString;

    public CarritoCompraController(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = _configuration["SqlConnectionString:DefaultConnection"];
        BDManager.GetInstance.ConnectionString = connectionString;
    }

    [HttpGet]
    [Route("GetAllCarritoCompra")]
    public IActionResult GetAllCarritoCompra()
    {
        try
        {
            var result = CarritoCompraServicios.ObtenerTodo<CarritoCompra>();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    [HttpGet]
    [Route("GetCarritoCompraById")]
    public IActionResult GetCarritoCompraById([FromQuery] int id)
    {
        try
        {
            var result = CarritoCompraServicios.ObtenerById<CarritoCompra>(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("AddCarritoCompra")]
    public IActionResult AddCarritoCompra(CarritoCompra carritoCompra)
    {
        try
        {
            var result = CarritoCompraServicios.InsertCarritoCompra(carritoCompra);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    [Route("ActualizarCarritoCompra")]
    public IActionResult UpdateCarritoCompra(CarritoCompra carritoCompra)
    {
        try
        {
            var result = CarritoCompraServicios.PrucedureUpdateCarritoCompra(carritoCompra);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete]
    [Route("DeleteDeleteById")]
    public IActionResult DeleteDeleteById([FromQuery] int id)
    {
        try
        {
            CarritoCompraServicios.EliminarPorIdCarritoCompra(id);
            return Ok("CarritoCompra eliminado correctamente");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    [HttpPost]
    [Route("InsertCarritoCompraReturnId")]
    public IActionResult InsertCarritoCompraReturnId(CarritoCompra carritoCompra)
    {
        try
        {
            var result = CarritoCompraServicios.InsertCarritoCompraReturnId(carritoCompra);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("GetCarritoCompraEnd")]
    public IActionResult GetCarritoCompraEnd()
    {
        try
        {
            var result = CarritoCompraServicios.GetCarritoCompraEnd<CarritoCompra>();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

}

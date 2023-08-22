using backend.connection;
using backend.entidades;
using backend.servicios;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductoController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly string? connectionString;

    public ProductoController(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = _configuration["SqlConnectionString:DefaultConnection"];
        BDManager.GetInstance.ConnectionString = connectionString;
    }

    [HttpGet]
    [Route("GetAllProductos")]
    public IActionResult GetAllProductos()
    {
        try
        {
            var result = ProductoServicios.ObtenerTodo<Producto>();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    [HttpGet]
    [Route("GetProductoById")]
    public IActionResult GetProductoById([FromQuery] int id)
    {
        try
        {
            var result = ProductoServicios.ObtenerById<Producto>(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("AddProducto")]
    public IActionResult AddProducto(Producto producto)
    {
        try
        {
            var result = ProductoServicios.InsertProducto(producto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
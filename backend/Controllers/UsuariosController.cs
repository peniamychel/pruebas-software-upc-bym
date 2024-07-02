using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.connection;
using backend.entidades;
using backend.servicios;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

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

    [HttpPost]
    [Route("Login")]
    public dynamic IniciarSesion([FromBody] Object inData)
    {
        var data = JsonConvert.DeserializeObject<dynamic>(inData.ToString());

        string user = data.user.ToString();
        string password = data.pass.ToString();

        Usuarios usuario = UsuarioLoginServicios.ObtenerByUserAndPass<Usuarios>(user, password);

        if (usuario == null)
        {
            return new
            {
                success = false,
                message = "Usuariono encotrado",
                result = ""
            };
        }
        else{
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                // new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
                // new Claim("DateOfJoing", userInfo.DateOfJoing.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),

                new Claim("id", usuario.Id.ToString()),
                new Claim("usuario", usuario.UserName.ToString())
            };

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new
            {
                success = true,
                message = "Usuario encotrado",
                result = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }

}
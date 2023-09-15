using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ESTO SE HACER PARA LA SEGURIDAD Y QUE LOS METOS HTTP PUEDAN SER ESCUCHADOS
builder.Services.AddCors(option => option.AddPolicy("CorsDev",
    builder => builder
        .AllowAnyMethod()
        .AllowAnyOrigin()
        .WithMethods("POST","GET","PUT","DELETE","PATCH")
        .AllowAnyHeader()));

//Servisio para la auteticacion
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsDev"); // DEBES DE AGREGAR EL Cors de arriba

app.UseHttpsRedirection();
app.UseAuthentication(); // es para las autentificaciones
app.UseAuthorization();

app.MapControllers();

app.Run();

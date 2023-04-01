using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MoviessApi.Models;

var builder = WebApplication.CreateBuilder(args);



var conntecion_String = builder.Configuration.GetConnectionString("DefualtConnection");

builder.Services.AddDbContext<DbContainer>(options => 
options.UseSqlServer(conntecion_String));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(Options =>
{
    Options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "TestApi"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

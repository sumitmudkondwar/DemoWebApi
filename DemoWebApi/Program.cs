using DemoWebApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShirtStoreManagement"));
});

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

//Routing

//app.MapGet("/shirts", () =>
//{
//    return "Reading all shirts";
//});

//app.MapGet("/shirts/{id}", (int id) =>
//{
//    return $"Reading all shirts with ID: {id}";
//});

//app.MapPost("/shirts", () =>
//{
//    return "Creating a new Shirt";
//});

//app.MapPut("/shirts/{id}", (int id) =>
//{
//    return $"Updating the shirt with ID: {id}";
//});

//app.MapDelete("/shirts/{id}", (int id) =>
//{
//    return $"Deleting the Shirt with ID: {id}";
//});

app.MapControllers();

app.Run();


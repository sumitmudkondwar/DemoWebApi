using DemoWebApi.Data;
using DemoWebApi.Filters.OperationFilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options => {
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    //options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
});

builder.Services.AddDbContext<ApplicationDbContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShirtStoreManagement"));
});

//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddVersionedApiExplorer(option => option.GroupNameFormat = "'v'VVV");
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Web API v1", Version = "Version 1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "My Web API v2", Version = "Version 2" });
    c.OperationFilter<AuthorizationHeaderOperationFilter>();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { 
        Scheme = "Bearer",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });
});

// Add services to the container.

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "WebAPI v2");
    });
}

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


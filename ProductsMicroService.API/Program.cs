using BusinessLogicLayer;
using DataAccessLayer;
using FluentValidation.AspNetCore;
using ProductsMicroService.API.ApiEndpoints;
using ProductsMicroService.API.Middlewares;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ioc container

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer();

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddCors(ops =>
{
    ops.AddDefaultPolicy(bldr =>
    {
        bldr.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.ConfigureHttpJsonOptions(cfg =>
{
    cfg.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// request pipeline

var app = builder.Build();

app.UseExceptionHandlingMiddleware();

app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapProductApiEndpoints();

app.Run();

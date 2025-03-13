using FluentValidation.AspNetCore;
using ProductsMicroService.API.Middlewares;
using ProductsMicroService.Core;
using System.Text.Json.Serialization;
using ProductsMicroService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// ioc container

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer();

builder.Services.AddControllers()
    .AddJsonOptions(cnfg =>
    {
        cnfg.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        cnfg.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// request pipeline

var app = builder.Build();

app.UseExceptionHandlingMiddleware();

app.UseRouting();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

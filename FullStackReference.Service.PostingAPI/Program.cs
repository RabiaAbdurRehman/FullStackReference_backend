
using FullStackReference.Service.PostingAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Routing
//app.MapGet("/shirt", () =>
//{
//    return "reading all shirts";
//});
//app.MapGet("/shirt/{id}", (int id) =>
//{
//    return $"{id}";
//});

app.UseAuthorization();

app.MapControllers();

app.Run();

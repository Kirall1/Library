using Library.DataAccess;
using Library.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataAccess(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();

await SeedData.SeedDatabaseAsync(scope.ServiceProvider.GetRequiredService<DatabaseContext>(), scope.ServiceProvider.GetRequiredService<UserManager<User>>());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

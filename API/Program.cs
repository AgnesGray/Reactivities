using API.Extensions;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);


var app = builder.Build();

// We add Middleware here
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();  Middleware

app.UseCors("CorsPolicy"); //Middleware

app.UseAuthorization();    //Middleware

app.MapControllers();

//we created this scope to get access to a service
using var scope = app.Services.CreateScope(); //specified using because this scope won't be used after it's finished it's job
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync(); //creates database if it doesn't exist; like Update-Database
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during the migration.");
}

app.Run();

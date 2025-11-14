using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using PropEase.API.Data;
using PropEase.API.Services;
using PropEase.API.Repositories;
using System.Text.Json.Serialization;
using AutoMapper;
using PropEase.API.Mapping;

var builder = WebApplication.CreateBuilder(args);


var configuredConn = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(configuredConn))
{
  var fallbackPath = Path.Combine(builder.Environment.ContentRootPath, "propease.db");
  configuredConn = $"Data Source={fallbackPath}";
}



builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseSqlite(configuredConn));

builder.Services.AddScoped<ImovelService>();
builder.Services.AddScoped<ProprietarioService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers().AddJsonOptions(opt =>
{
  opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
  var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
  dbContext.Database.Migrate();

}

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PropEase.API v1");
  });
}

app.UseAuthorization();
app.MapControllers();
app.Run();

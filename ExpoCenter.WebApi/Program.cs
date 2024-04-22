using ExpoCenter.Repositorios.SqlServer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var expoCenterConnectionString = builder.Configuration.GetConnectionString("ExpoCenterConnection");

builder.Services.AddDbContext<ExpoCenterDbContext>(options =>
    options
    .UseLazyLoadingProxies()
    .UseSqlServer(expoCenterConnectionString));

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

app.UseCors(c => c
.AllowAnyHeader()
.AllowAnyMethod()
.AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();

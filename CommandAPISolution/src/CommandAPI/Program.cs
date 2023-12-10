using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using CommandAPI.Data;

// IConfiguration configuration = new ConfigurationBuilder()
//     .AddJsonFile("appsettings.json")
//     .Build();


var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

// Add services to the container.
// SqliteConnection defined in User secrets
builder.Services.AddDbContext<CommandContext>(opt => opt.UseSqlite(
    configuration["SqliteConnection"]));

builder.Services.AddControllers().AddNewtonsoftJson(s =>
{
  s.SerializerSettings.ContractResolver = new
  CamelCasePropertyNamesContractResolver();
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Mock data
//builder.Services.AddScoped<ICommandAPIRepo, MockCommandAPIRepo>();

// from Db
builder.Services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();

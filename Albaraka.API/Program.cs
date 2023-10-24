using Albaraka.API.Data;
using Albaraka.API.Interfaces;
using Albaraka.API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// connection string -> appsettings dosyasında
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ABCBankContext>(options =>
    options.UseSqlServer(connectionString));


// dependecy injection eklenmesi
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using FileKeeper_server_.net.Data;
using FileKeeper_server_.net.Core.Interfaces.Repositories;
using FileKeeper_server_.net.Data.Repositories;
using FileKeeper_server_.net.Core.Interfaces.Services;
using FileKeeper_server_.net.Service.Services;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

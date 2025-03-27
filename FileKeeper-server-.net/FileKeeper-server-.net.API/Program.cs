using Microsoft.EntityFrameworkCore;
using FileKeeper_server_.net.Data;
using FileKeeper_server_.net.Core.Interfaces.Repositories;
using FileKeeper_server_.net.Data.Repositories;
using FileKeeper_server_.net.Core.Interfaces.Services;
using FileKeeper_server_.net.Service.Services;
using FileKeeper_server_.net.API.Middleware; 

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:3000") // החלף עם הדומיין של הקליינט שלך
            .AllowAnyMethod()
            .AllowAnyHeader());
});


var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();
app.Run();

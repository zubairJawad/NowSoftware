using Domain;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Data;
using Application.Users.Commands;
using Application.Users.Queries;
using Infrastructure.Security;
using WebUI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSingleton<IJwtTokenGenerator>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new JwtTokenGenerator(configuration);
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SignUpCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AuthenticateUserCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetBalanceQueryHandler).Assembly));


// Register the database connection
builder.Services.AddScoped<IDbConnection>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new SqlConnection(connectionString);
});

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseGlobalExceptionHandling();
app.UseHttpsRedirection();
app.UseAuthentication();  // Make sure authentication is used
app.UseAuthorization();

app.MapControllers();

app.Run();

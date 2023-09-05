using Application;
using Hangfire;
using Infrastructure;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

builder.Services.ApplicationServices();
builder.Services.InfrastructureServices(configuration);

builder.Services.ConfigureIdentityOptions(configuration);
builder.Services.ConfigureJwtAuthentication(configuration);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy =>
        policy.RequireRole("Admin"));
    options.AddPolicy("RequireUserRole", policy =>
        policy.RequireRole("User"));
    options.AddPolicy("RequireManagerRole", policy =>
        policy.RequireRole("Manager"));
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard("/hangfire");
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseErrorHandlingMiddleware();
app.MapControllers();
app.Run();

using Application;
using Hangfire;
using Infrastructure;
using Infrastructure.Seeds;
using Microsoft.AspNetCore.Identity;
using Serilog;
using SerilogLogger = Serilog;
using WebAPI.Extensions;


var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(hostingContext.Configuration));
Log.Information("Starting up");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

builder.Services.ApplicationServices();
builder.Services.InfrastructureServices(configuration);
builder.Services.ConfigureIdentityOptions(configuration);
builder.Services.ConfigureJwtAuthentication(configuration);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
    options.AddPolicy("RequireManagerRole", policy => policy.RequireRole("Manager"));
});
builder.Services.AddMemoryCache();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = configuration.GetConnectionString("RedisCache");
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();
Log.Information("Start");
app.UseHangfireDashboard("/hangfire");
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseErrorHandlingMiddleware();
app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    try
    {
        Log.Information("Adding new accounts");
        await StandardUsers.SeedStandardAsync(userManager, roleManager);
        Log.Information("Accounts were addedd");
    }
    catch (Exception ex)
    {
        Log.Information(ex.Message);
    }
}

app.Run();
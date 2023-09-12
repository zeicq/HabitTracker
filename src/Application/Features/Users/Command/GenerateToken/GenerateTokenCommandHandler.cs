using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Users.Command.GenerateToken;

public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, Response<string>>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IDistributedCache _distributedCache;
    private readonly ILogger<GenerateTokenCommandHandler> _logger;

    public GenerateTokenCommandHandler(UserManager<IdentityUser> userManager, IConfiguration configuration,
        IDistributedCache distributedCache, ILogger<GenerateTokenCommandHandler> logger)
    {
        _userManager = userManager;
        _configuration = configuration;
        _distributedCache = distributedCache;
        _logger = logger;
    }

    public async Task<Response<string>> Handle(GenerateTokenCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, command.Password))
        {
            var token = GenerateJwtToken(user);
            var result = token.Result;
            await _distributedCache.SetStringAsync("{user.Id}", user.Id);

            return new Response<string>() { Data = result, Succeeded = true };
        }

        return new Response<string>("Invalid credential");
    }

    private async Task<string> GenerateJwtToken(IdentityUser user)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var expireMinutes = Convert.ToInt32(_configuration["Jwt:ExpireMinutes"]);

        var tokenHandler = new JwtSecurityTokenHandler();
        var userRoles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
        };

        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expireMinutes + 120),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
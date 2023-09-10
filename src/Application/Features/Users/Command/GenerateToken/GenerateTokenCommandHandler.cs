using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Exceptions;
using Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Users.Command.GenerateToken;

public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, Response<string>>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IMemoryCache _memoryCache;

    public GenerateTokenCommandHandler(UserManager<IdentityUser> userManager, IConfiguration configuration, IMemoryCache memoryCache)
    {
        _userManager = userManager;
        _configuration = configuration;
        _memoryCache = memoryCache;
    }

    public async Task <Response<string>> Handle(GenerateTokenCommand command, CancellationToken cancellationToken)
    {

        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, command.Password))
        {
            var token = GenerateJwtToken(user);
            var result = token.Result;
            _memoryCache.Set("UserId", user.Id);
            return new Response<string>() {Data = result,Succeeded = true};
            
        }

        return new Response<string>("Invalid credenitial");
    }
    private async Task<string> GenerateJwtToken(IdentityUser user)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var expireMinutes = Convert.ToInt32(_configuration["Jwt:ExpireMinutes"]);

        var tokenHandler = new JwtSecurityTokenHandler();
    
        // Pobieranie ról użytkownika
        var userRoles = await _userManager.GetRolesAsync(user);
    
        // Tworzenie claimów tożsamości, w tym ról użytkownika
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
        };
    
        // Dodawanie ról użytkownika do claimów
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
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }


}
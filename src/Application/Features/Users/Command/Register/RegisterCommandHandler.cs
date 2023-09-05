using System.Net;
using Application.Services;
using Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Command.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<Unit>>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RegisterCommandHandler(UserManager<IdentityUser> userManager, IEmailService emailService,
        IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _emailService = emailService;
    }

    public async Task<Response<Unit>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var user = new IdentityUser { Email = command.Email, UserName = command.Email };

        var createUserResult = await _userManager.CreateAsync(user, command.Password);
        if (!createUserResult.Succeeded)
        { 
            return new Response<Unit>(createUserResult.Errors.Select(e => e.Description).ToList());
        }
        var addRoleResult = await _userManager.AddToRoleAsync(user, "User");


        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var emailConfirmationLink = GenerateEmailConfirmationLink(user, token);
        
        
        await _emailService.SendEmailAsync(user.Email, "Registration Confirmation",
            $"Click this link to confirm your registration: {emailConfirmationLink}");


        return new Response<Unit>(true);


        string GenerateEmailConfirmationLink(IdentityUser user, string token)
        {
            var host = _httpContextAccessor.HttpContext.Request.Host;
            var scheme = _httpContextAccessor.HttpContext.Request.Scheme;
            var callbackUrl =
                $"{scheme}://{host}/Account/ConfirmEmail?userId={user.Id}&token={WebUtility.UrlEncode(token)}";

            return callbackUrl;
        }
    }
}
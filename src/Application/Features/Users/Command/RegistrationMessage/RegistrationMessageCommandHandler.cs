using System.Net;
using Application.Features.Users.Command.AddRole;
using Application.Services;
using Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Command.RegistrationMessage;

public class RegistrationMessageCommandHandler : IRequestHandler<RegistrationMessageCommand, Response<Unit>>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RegistrationMessageCommandHandler(UserManager<IdentityUser> userManager, IEmailService emailService,
        IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _emailService = emailService;
    }

    public async Task<Response<Unit>> Handle(RegistrationMessageCommand request, CancellationToken cancellationToken)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(request.IdentityUser);
        var emailConfirmationLink = GenerateEmailConfirmationLink(request.IdentityUser, token);


        await _emailService.SendEmailAsync(request.IdentityUser.Email, "Registration Confirmation",
            $"Click this link to confirm your registration: {emailConfirmationLink}. \n Your role: {request.Role}");


        string GenerateEmailConfirmationLink(IdentityUser user, string token)
        {
            var host = _httpContextAccessor.HttpContext.Request.Host;
            var scheme = _httpContextAccessor.HttpContext.Request.Scheme;
            var callbackUrl =
                $"{scheme}://{host}/Account/ConfirmEmail?userId={user.Id}&token={WebUtility.UrlEncode(token)}";
            return callbackUrl;
        }

        return new Response<Unit>(true);
    }
}
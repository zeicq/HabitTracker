using System.Net.Sockets;
using Application.Exceptions;
using Application.Shared;
using Domain.Entity;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Command.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<Unit>>
{
    private readonly UserManager<IdentityUser> _userManager;

    public RegisterCommandHandler(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<Response<Unit>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var user = new IdentityUser
        {
            Email = command.Email,
            UserName = command.Email
        };

        var result = await _userManager.CreateAsync(user, command.Password);

        if (result.Succeeded)
        {
            return new Response<Unit>(Unit.Value);
        }
        
        var errors = result.Errors
            .Where(e => e.Code != "DuplicateUserName") 
            .Select(e => e.Description)
            .ToList();
        return new Response<Unit>(errors);
    }
    
}
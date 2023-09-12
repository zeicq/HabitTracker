using Application.Enums;
using Application.Features.Users.Command.AddRole;
using Application.Features.Users.Command.Register;
using Application.Features.UsersProfile;
using Application.Shared;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTest.Features;

public class RegisterCommandHandlerTests
{
    [Fact]
    public async Task returns_success_response()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
        var mediatorMock = new Mock<IMediator>();
        var loggerMock = new Mock<ILogger<RegisterCommandHandler>>();
        var command = new RegisterCommand
        {
            Email = "test@example.com",
            Password = "password123",
            Role = Roles.User
        };

        userManagerMock.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        userManagerMock.Setup(u => u.FindByEmailAsync(command.Email))
            .ReturnsAsync(new IdentityUser { Id = "GuidID" });

        var handler = new RegisterCommandHandler(userManagerMock.Object, mediatorMock.Object, loggerMock.Object);

        // Act
        var response = await handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Succeeded.Should().BeTrue();
        response.Errors.Should().BeNullOrEmpty();
        userManagerMock.Verify(m => m.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()), Times.Once);
        userManagerMock.Verify(m => m.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        mediatorMock.Verify(m => m.Send(It.IsAny<AddRoleCommand>(), CancellationToken.None), Times.Once);
        mediatorMock.Verify(m => m.Send(It.IsAny<CreateUserProfileCommand>(), CancellationToken.None), Times.Once);



    }

    [Fact]
    public async Task returns_failed_response()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
        var mediatorMock = new Mock<IMediator>();
        var loggerMock = new Mock<ILogger<RegisterCommandHandler>>();
        
        var command = new RegisterCommand
        {
            Email = "test@example.com",
            Password = "password123",
            Role = Roles.User
        };

        userManagerMock.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "failed" }));
        userManagerMock.Setup(u => u.FindByEmailAsync(command.Email))
            .ReturnsAsync(new IdentityUser { Id = "GuidID" });


        mediatorMock.Setup(m => m.Send(It.IsAny<AddRoleCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response<Unit>(true));
        mediatorMock.Setup(m => m.Send(It.IsAny<CreateUserProfileCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response<Unit>(true));


        var handler = new RegisterCommandHandler(userManagerMock.Object, mediatorMock.Object,loggerMock.Object);

        // Act
        var response = await handler.Handle(command, CancellationToken.None);

        // Assert
        response.Succeeded.Should().BeFalse();
        response.Errors.Should().NotBeNullOrEmpty();
        userManagerMock.Verify(m => m.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()), Times.Once);
        userManagerMock.Verify(m => m.FindByEmailAsync(It.IsAny<string>()), Times.Never);
        mediatorMock.Verify(m => m.Send(It.IsAny<AddRoleCommand>(), CancellationToken.None), Times.Never);
        mediatorMock.Verify(m => m.Send(It.IsAny<CreateUserProfileCommand>(), CancellationToken.None), Times.Never);

    }
}
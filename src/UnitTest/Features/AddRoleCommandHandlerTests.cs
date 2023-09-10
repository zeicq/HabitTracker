using Application.Enums;
using Application.Features.Users.Command.AddRole;
using Application.Features.Users.Command.RegistrationMessage;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace UnitTest.Features;

public class AddRoleCommandHandlerTests
{
    [Fact]
    public async Task add_user_role_success()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);

        var mediatorMock = new Mock<IMediator>();

        var user = new IdentityUser();
        var command = new AddRoleCommand(user, Roles.User);

        userManagerMock.Setup(u => u.AddToRoleAsync(user, Roles.User.ToString()))
            .ReturnsAsync(IdentityResult.Success);

        var handler = new AddRoleCommandHandler(userManagerMock.Object, mediatorMock.Object);

        // Act
        var response = await handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Succeeded.Should().BeTrue();
        userManagerMock.Verify(m => m.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()), Times.Once);
        mediatorMock.Verify(m => m.Send(It.IsAny<RegistrationMessageCommand>(), CancellationToken.None), Times.Once);

    }

    [Fact]
    public async Task add_fake_role_failed()
    {
     var userManagerMock = new Mock<UserManager<IdentityUser>>(
         Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
     var mediatorMock = new Mock<IMediator>();
     var handler = new AddRoleCommandHandler(userManagerMock.Object, mediatorMock.Object);
     var user = new IdentityUser();
     var command = new AddRoleCommand(user,  (Roles)100);
     
     // Act
     var response = await handler.Handle(command, CancellationToken.None);

     // Assert
     response.Succeeded.Should().BeFalse();
     response.Message.Should().Be("Unknown role specified.");

     userManagerMock.Verify(m => m.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()), Times.Never);
     mediatorMock.Verify(m => m.Send(It.IsAny<RegistrationMessageCommand>(), CancellationToken.None), Times.Never);
    }
}
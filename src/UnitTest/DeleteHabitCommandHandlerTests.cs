using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Habits.Command.Delete;
using Bogus;
using Domain.Entity;
using Domain.Interfaces;
using MediatR;
using Xunit;
using Xunit.Sdk;

namespace UnitTest;

public class DeleteHabitCommandHandlerTests

{
    private readonly Faker<Habit> _habitFaker;
    private readonly Faker<DeleteHabitCommand> _deleteHabitCommandFaker;
    private readonly Mock<IHabitRepository> _habitRepositoryMock;

    public DeleteHabitCommandHandlerTests()
    {
        _habitFaker = new Faker<Habit>()
            .RuleFor(h => h.Id, f => f.Random.Int())
            .RuleFor(h => h.Name, f => f.Lorem.Word())
            .RuleFor(h => h.Description, f => f.Lorem.Sentence());

        _deleteHabitCommandFaker = new Faker<DeleteHabitCommand>()
            .RuleFor(cmd => cmd.Id, f => f.Random.Int());

        _habitRepositoryMock = new Mock<IHabitRepository>();
    }
    
    [Fact]
    public async Task handle_valid_command_should_return_success_response()
    {
        // Arrange
        var habitToDelete = _habitFaker.Generate();
        var deleteHabitCommand = _deleteHabitCommandFaker.Generate();

        _habitRepositoryMock.Setup(repo => repo.GetByIdAsync(deleteHabitCommand.Id))
            .ReturnsAsync(habitToDelete);

        var handler = new DeleteHabitCommandHandler(_habitRepositoryMock.Object);

        // Act
        var result = await handler.Handle(deleteHabitCommand, CancellationToken.None);
        // Assert
        result.Data.Should().Be(Unit.Value);

        _habitRepositoryMock.Verify(repo => repo.GetByIdAsync(deleteHabitCommand.Id), Times.Once);
        _habitRepositoryMock.Verify(repo => repo.DeleteAsync(habitToDelete), Times.Once);
    }
    
    [Fact]
    public async Task handle_invalid_command_should_throw_exception()
    {
        // Arrange
        var deleteHabitCommand = _deleteHabitCommandFaker.Generate();

        _habitRepositoryMock.Setup(repo => repo.GetByIdAsync(deleteHabitCommand.Id))
            .ThrowsAsync(new Exception("exception"));

        var handler = new DeleteHabitCommandHandler(_habitRepositoryMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => handler.Handle(deleteHabitCommand, CancellationToken.None));
    }


}
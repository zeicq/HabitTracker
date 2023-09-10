using Application.Features.Habits.Command.Create;
using Application.Features.Habits.Queries;
using AutoMapper;
using Domain.Entity;
using Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace UnitTest.Features;

public class CreateHabitCommandHandlerTests
{
    private readonly Mock<IHabitRepository> _habitRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IMemoryCache> _memoryCache;
    public CreateHabitCommandHandlerTests()
    {
        _habitRepositoryMock = new Mock<IHabitRepository>();
        _mapperMock = new Mock<IMapper>();
        _memoryCache = new Mock<IMemoryCache>();
    }

    [Fact]
    public async Task should_return_habit_view_model()
    {
        // Arrange
        var command = new CreateHabitCommand();
        
        _mapperMock.Setup(mapper => mapper.Map<HabitViewModel>(It.IsAny<Habit>()))
            .Returns(new HabitViewModel());

        var handler = new CreateHabitCommandHandler(_habitRepositoryMock.Object, _mapperMock.Object,_memoryCache.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
        result.Data.Should().BeOfType<HabitViewModel>();
    }
}
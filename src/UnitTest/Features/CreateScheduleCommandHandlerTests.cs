using Application.Features.Schedules.Command.Create;
using Application.Features.Schedules.Queries;
using AutoMapper;
using Bogus;
using Domain.Entity;
using Domain.Interfaces;
using FluentAssertions;
using MediatR;
using Moq;

namespace UnitTest.Features;

public class CreateScheduleCommandHandlerTests
{
    [Fact]
    public async Task return_valid_response()
    {
        // Arrange
        var scheduleRepositoryMock = new Mock<IScheduleRepository>();
        var mapperMock = new Mock<IMapper>();
        var mediatorMock = new Mock<IMediator>();
        var handler = new CreateScheduleCommandHandler(scheduleRepositoryMock.Object, mapperMock.Object, mediatorMock.Object);

        var fakeSchedule = new Faker<Schedule>().Generate();
        var fakeViewModel = new Faker<ScheduleViewModel>().Generate();

        scheduleRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Schedule>()))
            .ReturnsAsync(fakeSchedule);
        mapperMock.Setup(mapper => mapper.Map<ScheduleViewModel>(fakeSchedule))
            .Returns(fakeViewModel);

        // Act
        var response = await handler.Handle(new CreateScheduleCommand(), CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Data.TimeOfDay.Should().Be(fakeViewModel.TimeOfDay);
        response.Data.StartData.Should().Be(fakeViewModel.StartData);
        response.Data.DaysOfWeek.Should().BeSameAs(fakeViewModel.DaysOfWeek);
        response.Data.HabitId.GetType().Should().Be(typeof(int));
    }

    
}
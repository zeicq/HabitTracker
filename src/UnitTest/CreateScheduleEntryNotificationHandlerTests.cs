using Application.Features.ScheduleEntries.Add;
using Application.Features.Schedules.Notifiaction;
using Application.Features.Schedules.Queries;
using Domain.Entity;
using Domain.Enums;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace UnitTest;

public class CreateScheduleEntryNotificationHandlerTests
{
    private readonly ScheduleViewModel _scheduleViewModel;
    private readonly Mock<IScheduleEntryRepository> _repositoryMock;
    private readonly CreateScheduleEntryNotificationHandler _handler;
    private readonly ScheduleCreatedNotification _createdNotification;

    public CreateScheduleEntryNotificationHandlerTests()
    {
        _scheduleViewModel = new ScheduleViewModel
        {
            DaysOfWeek = new List<DaysOfWeekEnum> { DaysOfWeekEnum.Monday, DaysOfWeekEnum.Wednesday },
            StartData = new DateTime(2023, 1, 1),
            TimeOfDay = TimeSpan.Parse("09:00"),
            Id = 1
        };

        _repositoryMock = new Mock<IScheduleEntryRepository>();
        _handler = new CreateScheduleEntryNotificationHandler(_repositoryMock.Object);
        _createdNotification = new ScheduleCreatedNotification { ScheduleViewModel = _scheduleViewModel };
    }

    [Fact]
    public async Task create_entries_with_correct_day_interval()
    {
        // Arrange

        // Act
        await _handler.Handle(_createdNotification, CancellationToken.None);

        // Assert
        var scheduleEntries = new List<ScheduleEntry>();
        var startDate = _scheduleViewModel.StartData;
        foreach (var entry in scheduleEntries)
        {
            entry.ScheduleId.Should().Be(_scheduleViewModel.Id);
            entry.IsCompleted.Should().BeFalse();
            entry.Date.Should().BeOnOrAfter(startDate);
            entry.Date.TimeOfDay.Should().Be(_scheduleViewModel.TimeOfDay);
            startDate = startDate.AddDays(1);
        }
    }

    [Fact]
    public async Task creates_correct_number_of_schedule_entries()
    {
        // Arrange
        /*      All arrange are prepared in the constructor      */

        // Act
        await _handler.Handle(_createdNotification, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<ScheduleEntry>()),
            Times.Exactly(66));
    }

    [Fact]
    public async Task create_entries_with_correct_schedule()
    {
        // Arrange

        // Act
        await _handler.Handle(_createdNotification, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(
            r => r.AddAsync(It.Is<ScheduleEntry>(entry => entry.ScheduleId == _scheduleViewModel.Id)),
            Times.Exactly(66));
    }

    [Fact]
    public async Task create_entries_with_completed_to_false()
    {
        // Arrange

        // Act
        await _handler.Handle(_createdNotification, CancellationToken.None);

        // Assert
        var scheduleEntries = new List<ScheduleEntry>();

        foreach (var entry in scheduleEntries)
        {
            entry.IsCompleted.Should().BeFalse();
            entry.Date.Should().Be(null);
        }
    }

    [Fact]
    public async Task create_entries_with_correct_date_and_time_of_day()
    {
        // Arrange

        // Act
        await _handler.Handle(_createdNotification, CancellationToken.None);
        // Assert
        _repositoryMock.Verify(
            r => r.AddAsync(It.Is<ScheduleEntry>(se =>
                se.Date >= _scheduleViewModel.StartData && se.Date.TimeOfDay == _scheduleViewModel.TimeOfDay)),
            Times.Exactly(66));
    }
}
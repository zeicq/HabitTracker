using Application.Features.Habits.Command.Create;
using Domain.Interfaces;
using FluentValidation.TestHelper;
using FluentAssertions;
using Moq;
using Xunit;
using Bogus;


namespace UnitTest;

public class CreateHabitValidatorTests
{
    private readonly CreateHabitValidator _validator;
    private readonly Mock<IHabitRepository> _habitRepositoryMock;
    private readonly Faker<CreateHabitCommand> _fakeHabitCommand;

    public CreateHabitValidatorTests()
    {
        _habitRepositoryMock = new Mock<IHabitRepository>();
        _habitRepositoryMock.Setup(repo => repo.IsUniqueHabitAsync(It.IsAny<string>())).ReturnsAsync(true);
        _validator = new CreateHabitValidator(_habitRepositoryMock.Object);
        _fakeHabitCommand = new Faker<CreateHabitCommand>()
            .RuleFor(c => c.Name, f => f.Random.Words(5))
            .RuleFor(c => c.Description, f => f.Random.Words(400));
    }

    [Fact]
    public async Task should_pass_validation()
    {
        // Arrange
        var createHabitCommand = new CreateHabitCommand { Name = "Gym", Description = "My new habit" };

        // Act
        var validationResult = await _validator.TestValidateAsync(createHabitCommand);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x => x.Name);
        validationResult.ShouldNotHaveValidationErrorFor(x => x.Description);
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task should_fail_validation_when_name_is_not_unique()
    {
        // Arrange
        var existingHabitName = "Testing";
        _habitRepositoryMock.Setup(repo => repo.IsUniqueHabitAsync(existingHabitName)).ReturnsAsync(false);
        var command = new CreateHabitCommand { Name = existingHabitName, Description = "Description" };

        // Act
        var result = await _validator.TestValidateAsync(command);
        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name)
            .WithErrorMessage("Name already exists.");
    }
    
    [Fact]
    public async Task should_fail_when_name_is_null()
    {
        // Arrange
        var command = new CreateHabitCommand { Name = "", Description = "ValidDescription" };
       
        // Act
        var result = await _validator.TestValidateAsync(command);
        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name);


    }

    [Fact]
    public void should_fail_when_name_is_too_short()
    {
        // Arrange
        var command = new CreateHabitCommand { Name = "t1", Description = "ValidDescription"};

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name);
    }
    [Fact]
    public async Task should_fail_when_name_is_too_long()
    {
        // Arrange
        var command = new CreateHabitCommand
        {
            Name = "Lorem ipsum dolor sit amet, cons",
            Description = "ValidDescription"
        };
        // Act
        var result =await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name);
    }
    
    
    [Fact]
    public async Task should_fail_when_name_contain_nonnumeric_alpha_characters()
    {
        // Arrange
        var command = new CreateHabitCommand
        {
            Name = "!@#!@#Test",
            Description = "ValidDescription"
        };
        // Act
        var result =await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name);
    }
    
    [Fact]
    public async Task should_fail_when_name_contain_letter_at_sign()
    {
        // Arrange
        var command = new CreateHabitCommand
        {
            Name = "Test@",
            Description = "ValidDescription"
        };
        // Act
        var result =await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name);
    }
    
    
    [Fact]
    public async Task should_fail_when_description_contain_more_than_500_charakters()
    {
        // Arrange
        var command = new CreateHabitCommand
        {
            Name = "Test",
            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Pulvinar proin gravida hendrerit lectus a. Ultricies lacus sed turpis tincidunt id aliquet risus feugiat in. Tellus integer feugiat scelerisque varius morbi enim. Vitae ultricies leo integer malesuada nunc vel risus commodo. Vulputate mi sit amet mauris commodo quis imperdiet. Nullam non nisi est sit amet facilisis magna etiam tempor. Aliquet nec ullamcorper sit amdset risus."
        };
        // Act
        var result =await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Description);
    }


}
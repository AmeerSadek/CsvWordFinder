using AutoFixture;
using FluentAssertions;
using Xunit;

namespace CsvWordFinder.Tests;

public class ConsoleInputValidatorTests
{
    private readonly Fixture _fixture;

    public ConsoleInputValidatorTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void Validate_Should_ReturnFalseWithMessage_When_InputArrayIsEmpty()
    {
        // Arrange
        var input = Array.Empty<string>();

        var expectedResult = _fixture.Build<ValidationResult>()
            .With(x => x.IsValid, false)
            .With(x => x.Message, StaticMessages.InvalidNumberOfArguments)
            .Create();

        var sut = _fixture.Create<ConsoleInputValidator>();

        // Act
        var result = sut.Validate(input);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ValidationResult>();
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void Validate_Should_ReturnFalseWithMessage_When_InputArraySizeIsLessThanThree()
    {
        // Arrange
        var input = new string[] { "Arg1" };

        var expectedResult = _fixture.Build<ValidationResult>()
            .With(x => x.IsValid, false)
            .With(x => x.Message, StaticMessages.InvalidNumberOfArguments)
            .Create();

        var sut = _fixture.Create<ConsoleInputValidator>();

        // Act
        var result = sut.Validate(input);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ValidationResult>();
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void Validate_Should_ReturnFalseWithMessage_When_InputArraySizeIsGreaterThanThree()
    {
        // Arrange
        var input = new string[] { "Arg1", "Arg2", "Arg3", "Arg4" };

        var expectedResult = _fixture.Build<ValidationResult>()
            .With(x => x.IsValid, false)
            .With(x => x.Message, StaticMessages.InvalidNumberOfArguments)
            .Create();

        var sut = _fixture.Create<ConsoleInputValidator>();

        // Act
        var result = sut.Validate(input);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ValidationResult>();
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Theory]
    [InlineData(".pdf")]
    [InlineData(".png")]
    [InlineData(".xlxs")]
    public void Validate_Should_ReturnFalseWithMessage_When_FilePathExtensionIsNotCsv(string extension)
    {
        // Arrange
        var input = new string[] { $"file{extension}", "Arg2", "Arg3" };

        var expectedResult = _fixture.Build<ValidationResult>()
            .With(x => x.IsValid, false)
            .With(x => x.Message, $"{StaticMessages.InvalidFileType}\n{StaticMessages.ImposeCsvFileType}")
            .Create();

        var sut = _fixture.Create<ConsoleInputValidator>();

        // Act
        var result = sut.Validate(input);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ValidationResult>();
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Theory]
    [InlineData("Test")]
    [InlineData("1p")]
    [InlineData("-")]
    public void Validate_Should_ReturnFalseWithMessage_When_ColumnNumberIsNotValidNumber(string columnNumber)
    {
        // Arrange
        var input = new string[] { "File.csv", columnNumber, "Arg3" };

        var expectedResult = _fixture.Build<ValidationResult>()
            .With(x => x.IsValid, false)
            .With(x => x.Message, StaticMessages.InvalidColumnNumber)
            .Create();

        var sut = _fixture.Create<ConsoleInputValidator>();

        // Act
        var result = sut.Validate(input);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ValidationResult>();
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void Validate_Should_ReturnTrueWithNoMessage_When_EverythingIsCorrect()
    {
        // Arrange
        var input = new string[] { "File.csv", "2", "Arg3" };

        var expectedResult = _fixture.Build<ValidationResult>()
            .With(x => x.IsValid, true)
            .Without(x => x.Message)
            .Create();

        var sut = _fixture.Create<ConsoleInputValidator>();

        // Act
        var result = sut.Validate(input);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ValidationResult>();
        result.Should().BeEquivalentTo(expectedResult);
    }
}

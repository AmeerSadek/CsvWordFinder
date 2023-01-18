using AutoFixture;
using FluentAssertions;
using Xunit;

namespace CsvWordFinder.Tests;

public class CsvFileWordFinderTests
{
    private readonly Fixture _fixture;
    private readonly string _filePath;
    private readonly string _fileContent;

    public CsvFileWordFinderTests()
    {
        _fixture = new Fixture();
        _filePath = Path.GetTempFileName();
        _fileContent = "1,Rossi,Fabio,01/06/1990\n2,Gialli,Alessandro,02/07/1989\n3,Verdi,Alberto,03/08/1987\n";
        File.WriteAllText(_filePath, _fileContent);
    }

    [Fact]
    public async Task FindAsync_Should_ReturnExpectedRawRecord_When_InputIsAllValid()
    {
        // Arrange
        var columnNumber = 2;
        var targetedWord = "Rossi";

        var sut = _fixture.Create<CsvFileWordFinder>();

        // Act
        var result = await sut.FindAsync(_filePath, columnNumber, targetedWord);

        // Assert
        result.Should().Be("1,Rossi,Fabio,01/06/1990\n");
    }

    [Fact]
    public async Task FindAsync_Should_ReturnFileNotFoundMessage_WhenFilePathIsInvalid()
    {
        // Arrange
        var columnNumber = 2;
        var targetedWord = "Rossi";

        var sut = _fixture.Create<CsvFileWordFinder>();

        // Act
        var result = await sut.FindAsync("invalid_file_path.csv", columnNumber, targetedWord);

        // Assert
        result.Should().Be(StaticMessages.FileNotFound);
    }

    [Fact]
    public async Task FindAsync_Should_ReturnNoResultMessage_When_FilePathIsValidAndColumnNumberIsValidAndTargetedWordIsInvalid()
    {
        // Arrange
        var columnNumber = 2;
        var targetedWord = "Invalid Word";

        var sut = _fixture.Create<CsvFileWordFinder>();

        // Act
        var result = await sut.FindAsync(_filePath, columnNumber, targetedWord);

        // Assert
        result.Should().Be(StaticMessages.NoResultsFound);
    }

    [Fact]
    public async Task FindAsync_Should_ReturnNoResultMessage_When_TargetedWordIsCommented()
    {
        // Arrange
        var filePath = Path.GetTempFileName();
        var fileContent = "1,#Rossi,Fabio,01/06/1990\n2,Gialli,Alessandro,02/07/1989\n3,Verdi,Alberto,03/08/1987\n";
        File.WriteAllText(filePath, fileContent);

        var columnNumber = 2;
        var targetedWord = "Rossi";

        var sut = _fixture.Create<CsvFileWordFinder>();

        // Act
        var result = await sut.FindAsync(filePath, columnNumber, targetedWord);

        // Assert
        result.Should().Be(StaticMessages.NoResultsFound);
    }
}

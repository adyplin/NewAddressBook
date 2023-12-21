using ConsoleApp.Services;
using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Tests;

/// <summary>
/// Unit test
/// </summary>
public class FileManager_Tests
{
    /// <summary>
    /// Verifies that SaveContentToFile method returns true if the filepath exists
    /// </summary>
    [Fact]

    public void SaveToFileShould_ReturnTrue_IfTheFilePathExists()
    {
        // Arrange

        IFileManager fileManager = new FileManager(@"C:\Projects-Education\testing.json");
        string content = "Test content";

        // Act

        bool result = fileManager.SaveContentToFile(content);

        // Assert

        Assert.True(result);
    }

    /// <summary>
    /// Verifies that SaveContentToFile method returns false if the filepath does not exists
    /// </summary>

    [Fact]
    public void SaveToFileShould_ReturnFalse_IfTheFilePathDoNotExist()
    {
        // Arrange

        IFileManager fileManager = new FileManager(@$"C:\{Guid.NewGuid()}testing.json");
        string content = "Test content";

        // Act

        bool result = fileManager.SaveContentToFile(content);

        // Assert

        Assert.False(result);
    }
}

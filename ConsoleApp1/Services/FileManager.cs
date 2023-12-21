using ConsoleApp1.Interfaces;
using System.Diagnostics;
namespace ConsoleApp.Services;

    /// <summary>
    /// Implementation of the IFileManager interface for managing file-related operations,
    /// such as reading content from a file and saving content to a file.
    /// </summary>
public class FileManager(string filePath) : IFileManager
{
    private readonly string _filePath = filePath;

    /// <summary>
    /// Saves the provided content to the associated file
    /// </summary>
    /// <param name="content">The data to be saved</param>
    /// <returns>True if the content was successfully saved otherwise false</returns>
    public bool SaveContentToFile(string content)
    {
        try
        {
            using (var sw = new StreamWriter(_filePath))
            {
                sw.WriteLine(content);
            }
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;

       
    }

    /// <summary>
    /// Retrieves the content from the associated file
    /// </summary>
    /// <returns>the content of the file as a string or null if the file does not exist</returns>
    public string GetContentFromFile()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                using (var sr = new StreamReader(_filePath))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;

        
    }
}

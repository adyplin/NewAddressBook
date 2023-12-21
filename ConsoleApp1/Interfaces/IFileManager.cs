namespace ConsoleApp1.Interfaces
{
    public interface IFileManager
    {
        string GetContentFromFile();
        bool SaveContentToFile(string content);
    }
}
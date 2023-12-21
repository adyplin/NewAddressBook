using ConsoleApp1.Enums;

namespace ConsoleApp1.Interfaces
{
    public interface IServiceResult
    {
        object Result { get; set; }
        ServiceStatus Status { get; set; }
    }
}
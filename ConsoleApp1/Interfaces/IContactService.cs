

namespace ConsoleApp1.Interfaces
{
    public interface IContactService
    {
        IServiceResult AddContactToList(IContact contact);
        IServiceResult GetContactFromList(string email);

        IServiceResult GetContactsFromList();
        IServiceResult DeleteContactFromList(string email);
    }
}
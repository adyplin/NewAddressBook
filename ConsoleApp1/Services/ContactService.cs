using ConsoleApp.Services;
using ConsoleApp1.Interfaces;
using Newtonsoft.Json;
using ConsoleApp1.Models.Responses;
using System.Diagnostics;


namespace ConsoleApp1.Services;

/// <summary>
/// Service responsible for managing contacts, including adding, deleting, and retrieving contacts from a file.
/// </summary>
public class ContactService : IContactService
{

    private List<IContact> _contacts = new List<IContact>();
    private readonly IFileManager _fileManager = new FileManager(@"C:\Projects-Education\contacts.json");
    

    /// <summary>
    /// Adds a new contact to the list and saves the updated list to a JSON file.
    /// </summary>
    /// <param name="contact">The contact to be added</param>
    /// <returns>Service result shows if it fails or succeeeds</returns>
    public IServiceResult AddContactToList(IContact contact)
    {
        IServiceResult response = new ServiceResult();
        try
        {
            // Checks if a contact with the same email already exists
            if(!_contacts.Any(c => c.Email == contact.Email)) 
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                };
                _contacts.Add(contact);
                _fileManager.SaveContentToFile(JsonConvert.SerializeObject(_contacts, settings));
                response.Status = Enums.ServiceStatus.SUCCEEDED;
            }
            else
            {
                response.Status = Enums.ServiceStatus.ALREADY_EXISTS;
            }
        }
        catch (Exception ex) 
        {
            Debug.WriteLine(ex.Message);
            response.Status = Enums.ServiceStatus.FAILED;
            response.Result = ex.Message;
        }

        return response;
    }

    /// <summary>
    /// Deletes a contact from the list and saves the updated list to the JSON file.
    /// </summary>
    /// <param name="email">The email of the contact to be deleted</param>
    /// <returns>Service result shows success, contact not found or failure to remove</returns>
    public IServiceResult DeleteContactFromList(string email)
    {
        IServiceResult response = new ServiceResult();
        try
        {
            var contactToRemove = _contacts.FirstOrDefault(c => c.Email == email);

            if (contactToRemove != null)
            {
                _contacts.Remove(contactToRemove);
                _fileManager.SaveContentToFile(JsonConvert.SerializeObject(_contacts));
                response.Status = Enums.ServiceStatus.SUCCEEDED;
            }
            else
            {
                response.Status = Enums.ServiceStatus.NOT_FOUND; 
                response.Result = "Contact not found.";
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            response.Status = Enums.ServiceStatus.FAILED;
            response.Result = ex.Message;
        }

        return response;
    }

    /// <summary>
    /// Retrieves a contact from the list by email
    /// </summary>
    /// <param name="email">Email of the contact to retrieve</param>
    /// <returns>Service result contains the contact or an error message</returns>
    public IServiceResult GetContactFromList(string email)
    {
        IServiceResult response = new ServiceResult();
        try
        {
            var contact = _contacts.FirstOrDefault(c => c.Email == email);

            if (contact != null)
            {
                response.Result = contact;
                response.Status = Enums.ServiceStatus.SUCCEEDED;
            }
            else
            {
                response.Status = Enums.ServiceStatus.NOT_FOUND;
                response.Result = "Contact not found.";
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            response.Status = Enums.ServiceStatus.FAILED;
            response.Result = ex.Message;
        }

        return response;
    }

    /// <summary>
    /// Retrieves all contacts from the list
    /// </summary>
    /// <returns>Service result containing the list or an error message</returns>
    public IServiceResult GetContactsFromList()
    {
        IServiceResult response = new ServiceResult();

        try
        {
            var content = _fileManager.GetContentFromFile();

            if (!string.IsNullOrEmpty(content))
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                _contacts = JsonConvert.DeserializeObject<List<IContact>>(content, settings) ?? new List<IContact>()!;



                response.Status = Enums.ServiceStatus.SUCCEEDED;
                response.Result = _contacts;
            }
            else
            {
                response.Status = Enums.ServiceStatus.NOT_FOUND;
                response.Result = "No content found in the file.";
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in GetContactsFromList: {ex.Message}");
            response.Status = Enums.ServiceStatus.FAILED;
            response.Result = "Error retrieving contacts.";
        }

        return response;
    }

}

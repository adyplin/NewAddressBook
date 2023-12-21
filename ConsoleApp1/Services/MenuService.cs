using ConsoleApp1.Interfaces;
using ConsoleApp1.Models;


namespace ConsoleApp1.Services;


/// <summary>
/// Implementation of the IMenuService interface for managing the main menu and user interactions
/// </summary>
public class MenuService : IMenuService
{
    private readonly IContactService _contactService = new ContactService();

    /// <summary>
    /// Displays the main menu and handles user input 
    /// </summary>
    public void ShowMainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("# ADDRESS BOOK #");
            Console.WriteLine();
            Console.WriteLine($"{"1.",-2} Add Contact");
            Console.WriteLine($"{"2.",-2} Show Contact");
            Console.WriteLine($"{"3.",-2} Show All Contacts");
            Console.WriteLine($"{"4.",-2} Remove a Contact");
            Console.WriteLine($"{"0.",-2} Exit");
            Console.Write("\nEnter your Option: ");


            int option;

           
            if (int.TryParse(Console.ReadLine(), out option))
            {
                switch (option)
                {
                    case 1:
                        Console.Clear();
                        AddContactOption();
                        break;

                    case 2:
                        Console.Clear();
                        ShowOneContactOption();
                        break;

                    case 3:
                        Console.Clear();
                        ShowAllContactsOption();
                        break;

                    case 4:
                        Console.Clear();
                        RemoveContactOption();
                        break;

                    case 0:
                        ExitProgramOption();
                        break;

                    default:
                        Console.WriteLine("\nInvalid Option. Press any key try again.");
                        Console.ReadKey();
                        break;
                }
            }

            else
            {
                Console.WriteLine("\nInvalid input. Please enter a number.");
                Console.ReadKey();
            }
        }
    }

    /// <summary>
    /// Handles the logic to add a new contact
    /// </summary>
    private void AddContactOption()
    {
        IContact contact = new Contact();

        Console.Write("\nEnter First name: ");
        contact.FirstName = Console.ReadLine()!;

        Console.Write("\nEnter Last name: ");
        contact.LastName = Console.ReadLine()!;

        Console.Write("\nEnter Email: ");
        contact.Email = Console.ReadLine()!;

        Console.Write("\nEnter Phone number: ");
        contact.PhoneNumber = Console.ReadLine()!;

        Console.Write("\nEnter Address: ");
        contact.Address = Console.ReadLine()!;

        Console.Write("\nEnter City: ");
        contact.City = Console.ReadLine()!;

        Console.Write("\nEnter Postal code: ");
        contact.PostalCode = Console.ReadLine()!;

       var res = _contactService.AddContactToList(contact);

        switch(res.Status)
        {
            case Enums.ServiceStatus.SUCCEEDED:
                Console.WriteLine("\n\n ** The contact was added successfully **");
                break;

            case Enums.ServiceStatus.ALREADY_EXISTS:
                Console.WriteLine("\nThe contact already exists");
                break;

            case Enums.ServiceStatus.FAILED:
                Console.WriteLine("Failed adding the contact to the list");
                Console.WriteLine("See error message :: " + res.Result.ToString());
                break;
        }

        DisplayPressAnyKey();
    }

    /// <summary>
    /// Handles the logic to show one specific contact
    /// </summary>
    private void ShowOneContactOption()
    {
        Console.Write("\nEnter the email address of the contact you want more information about: ");
        var email = Console.ReadLine()!;

        var res = _contactService.GetContactFromList(email);

        switch (res.Status)
        {
            case Enums.ServiceStatus.SUCCEEDED:
                if (res.Result is IContact contact)
                {
                    Console.WriteLine("\nContact information:\n");
                    Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
                    Console.WriteLine($"Email: {contact.Email}");
                    Console.WriteLine($"Phone Number: {contact.PhoneNumber}");
                    Console.WriteLine($"Address: {contact.Address}, {contact.City}, {contact.PostalCode}");

                }
                break;

            case Enums.ServiceStatus.NOT_FOUND:
                Console.WriteLine("\nContact not found");
                break;

            case Enums.ServiceStatus.FAILED:
                Console.WriteLine("Failed to retrieve contact details");
                Console.WriteLine("See error message: " + res.Result.ToString());
                break;
        }

        DisplayPressAnyKey();
    }
    /// <summary>
    /// Handles the logic to show all contacts
    /// </summary>
    private void ShowAllContactsOption()
    {
        var res = _contactService.GetContactsFromList();

        switch (res.Status)
        {
            case Enums.ServiceStatus.SUCCEEDED:
                if (res.Result is List<IContact> contactlist)
                {
                    Console.WriteLine("\nAll Contacts:\n");
                    foreach (var contact in contactlist)
                    {
                        Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
                        Console.WriteLine($"Email: {contact.Email}");
                        Console.WriteLine("\n---------------");
                    }
                }
                break;

            case Enums.ServiceStatus.NOT_FOUND:
                Console.WriteLine("No contacts available.");
                break;
        }

        DisplayPressAnyKey();
    }

    /// <summary>
    /// Handles the logic to remove a contact
    /// </summary>
    private void RemoveContactOption()
    {
        Console.Write("\nEnter the Email address of the contact you want to remove: ");
        var email = Console.ReadLine();

        var res = _contactService.DeleteContactFromList(email);

        switch (res.Status)
        {
            case Enums.ServiceStatus.SUCCEEDED:
                Console.WriteLine("\n\n ** Contact was removed successfully **");
            break;

            case Enums.ServiceStatus.NOT_FOUND:
                Console.WriteLine("\nContact not found");
                break;

            case Enums.ServiceStatus.FAILED:
                Console.WriteLine("Failed to remove contact");
                Console.WriteLine("See error message: " + res.Result.ToString());
                break;
        }

        DisplayPressAnyKey();
    }

    /// <summary>
    /// Handles logic to close the application
    /// </summary>
    private void ExitProgramOption()
    {
        Console.Clear();
        Console.Write("\nAre you sure you want to close this application? (y/n): ");
        var option = Console.ReadLine() ?? "";

        if (option.Equals("y", StringComparison.OrdinalIgnoreCase))
            Environment.Exit(0);
    }
    /// <summary>
    /// Displays a message to the user of the application to press a key to continue
    /// </summary>
    private void DisplayPressAnyKey()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
}

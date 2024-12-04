using ContactBook.Core.Entity;

namespace ContactBook.Core.Services;

public interface IContactService
{
    Task<IEnumerable<Contact>> ReadAll();
    Task<Contact> GetById(int id);
    Task<bool> Create(string FirstName, string LastName, List<string> email, List<string> phoneNumber);
    Task<bool> Create(Contact contact);
    Task<IEnumerable<Contact>> FindByAll(string firstName, string lastName, string phoneNumber, string email);
    Task<IEnumerable<Contact>> FindByAll(string query);
    
    //Task<IEnumerable<Contact>> FindByAll(string firstName);
    Task<IEnumerable<Contact>> FindByLastName(string lastName);
    
    //  метод для поиска по номеру телефона
    Task<IEnumerable<Contact>> FindByPhone(string phoneNumber);
    Task<IEnumerable<Contact>> FindByEmail(string email);
    Task<bool> Clear();
    Task<IEnumerable<Contact>> FindByFirstName(string name);
    int Count();
}
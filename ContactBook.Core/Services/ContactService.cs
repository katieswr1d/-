using ContactBook.Core.Entity;
using ContactBook.DAL;

namespace ContactBook.Core.Services;

public class ContactService : IContactService
{
    
    private readonly IRepositoty _repository;

    public ContactService(IRepositoty repository)
    {
        _repository = repository;
    }

    public async Task<bool> Create(string firstName, string lastName, List<string> emailList, List<string> phoneNumberList)
    {
        await _repository.Create(firstName, lastName, emailList, phoneNumberList);
        return true;
    }

    public async Task<bool> Clear()
    {
        await _repository.Clear();
        return true;
    }

    public async Task<IEnumerable<Contact>> ReadAll()
    {
        return await _repository.ReadAll();
    }

    public async Task<IEnumerable<Contact>> FindByAll(string firstName, string lastName, string phoneNumber, string email)
    {
        var contacts = await _repository.FilterContacts(contact =>
            string.Equals(contact.FirstName, firstName, StringComparison.CurrentCultureIgnoreCase) &&
            string.Equals(contact.LastName, lastName, StringComparison.CurrentCultureIgnoreCase) &&
            contact.Phones.Any(phone => phone.Value == phoneNumber) &&
            contact.Emails.Any(mail => mail.Value == email));
        return contacts;
    }

    public async Task<IEnumerable<Contact>> FindByPhone(string phoneNumber)
    {
        var contacts = await _repository.FilterContacts(contact =>
            contact.Phones.Any(phone => phone.Value == phoneNumber));
        return contacts;
    }

    public async Task<IEnumerable<Contact>> FindByFirstName(string firstName)
    {
        var contacts = await _repository.FilterContacts(contact =>
            string.Equals(contact.FirstName, firstName, StringComparison.CurrentCultureIgnoreCase));
        return contacts;
    }

    public async Task<IEnumerable<Contact>> FindByLastName(string lastName)
    {
        var contacts = await _repository.FilterContacts(contact =>
            string.Equals(contact.LastName, lastName, StringComparison.CurrentCultureIgnoreCase));
        return contacts;
    }

    public async Task<IEnumerable<Contact>> FindByEmail(string email)
    {
        var contacts = await _repository.FilterContacts(contact =>
            contact.Emails.Any(mail => mail.Value == email));
        return contacts;
    }
    public int Count()
    {
        return _repository.Count();
    }

}

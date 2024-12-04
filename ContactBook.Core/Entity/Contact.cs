using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactBook.Core.Entity;

public class Contact
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Автоинкремент
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;//чтобы была статическая пустая строка а не создавалось миллион этих строк
    public string LastName { get; set; } = string.Empty;
    public virtual List<Email> Emails { get; set; } = [];
    public virtual List<PhoneNumber> Phones { get; set; } = [];
    
    public Contact() { }
    
    public Contact(string firstName, string lastName, List<string> email, List<string> phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;

        Emails = email.Select(e => new Email(e)).ToList();
        Phones = phoneNumber.Select(p => new PhoneNumber(p)).ToList();
    }

    
    public override string ToString() {
        return "First Name: " + FirstName + "\nLast Name: " + LastName + 
               "\nEmails: " + string.Join("\n", Emails) + "\nPhoneNumbers: " + 
               string.Join("\n", Phones);
    }
}
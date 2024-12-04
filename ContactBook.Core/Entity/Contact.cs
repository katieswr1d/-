using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactBook.Core.Entity;

public class Contact
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;//чтобы была статическая пустая строка а не создавалось миллион этих строк
    public string LastName { get; set; } = string.Empty;
    public List<Email> EmailList { get; set; } = [];
    public List<PhoneNumber> PhoneNumberList { get; set; } = [];
    
    public Contact() { }
    
    public Contact(int id, string firstName, string lastName, List<string> email, List<string> phoneNumber)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;

        EmailList = email.Select(e => new Email(e)).ToList();

        PhoneNumberList = phoneNumber.Select(p => new PhoneNumber(p)).ToList();
    }

    
    public override string ToString() {
        return "First Name: " + FirstName + "\nLast Name: " + LastName + 
               "\nEmails: " + string.Join("\n", EmailList) + "\nPhoneNumbers: " + 
               string.Join("\n", PhoneNumberList);
    }
}
/*
 * using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ContactBook.Core.Entity;

public class Contact
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;//чтобы была статическая пустая строка а не создавалось миллион этих строк
    public string LastName { get; set; } = string.Empty;
    public DbSet<Email> EmailList { get; set; }
    public DbSet<PhoneNumber> PhoneNumberList { get; set; }

    public Contact() { }

    public Contact(int id, string firstName, string lastName, List<string> emails, List<string> phoneNumbers)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        EmailList = new InternalDbSet<Email>()
        EmailList.AddRange(emails.Select(e => new Email(e)));//создаем новый экземпляр емейла

        //PhoneNumberList = phoneNumber.Select(p => new PhoneNumber(p)).ToList();
        PhoneNumberList.AddRange((phoneNumbers.Select(p => new PhoneNumber(p))));
    }


    public override string ToString() {
        return "First Name: " + FirstName + "\nLast Name: " + LastName +
               "\nEmails: " + string.Join("\n", EmailList) + "\nPhoneNumbers: " +
               string.Join("\n", PhoneNumberList);
    }
}
*/
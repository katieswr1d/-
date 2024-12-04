using ContactBook.Core.Entity;
using ContactBook.Core.Services;
using ContactBook.DAL;
using ContactBook.DAL.Data;
using ContactBook.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace ContactBook.API;

class Program
{

    static async Task Main(string[] args)
    {
        var options = new DbContextOptionsBuilder<ContactBookDbContext>()
            .UseSqlite("Data Source=Contacts.db")
            .Options;

        var context = new ContactBookDbContext(options);
        await context.Database.EnsureCreatedAsync();

        ContactService service = new ContactService(new FileRepository(context));
        string option = string.Empty;
        while (option != "0")
        {
            Console.WriteLine("Выберите действие:\n" +
                              "1. Вывести все контакты\n" +
                              "2. Поиск контакта\n" +
                              "3. Добавить новый контакт\n" +
                              "0. Завершить работу");

            option = Console.ReadLine()!;
            switch (option)
            {
                case "1":
                    var contacts = await service.ReadAll();
                    Console.WriteLine(string.Join("\n", contacts));
                    break;

                case "2":
                    Console.WriteLine("Выберите опцию:\n" +
                                      "1. Поиск по всем параметрам\n" +
                                      "2. Поиск по конкретному полю");
                    var findOption = Console.ReadLine();
                    if (findOption == "1")
                    {
                        Console.WriteLine("Введите имя, фамилию, номер телефона и email контакта");
                        var name = Console.ReadLine()!;
                        var surname = Console.ReadLine()!;
                        var number = Console.ReadLine()!;
                        var mail = Console.ReadLine()!;
                        var foundContacts = await service.FindByAll(name, surname, number, mail);
                        Console.WriteLine($"Found contacts:\n{string.Join('\n', foundContacts)}");
                    }

                    if (findOption == "2")
                    {
                        Console.WriteLine("Выберите действие:\n" +
                                          "1. Поиск по имени\n" +
                                          "2. Поиск по фамилии\n" +
                                          "3. Поиск по email\n" +
                                          "4. Поиск по номеру телефона");
                        var parameterOption = Console.ReadLine();
                        switch (parameterOption)
                        {
                            case "1":
                                Console.WriteLine("Введите имя контакта");
                                var nameToFind = Console.ReadLine()!;
                                var contactsByName = await service.FindByFirstName(nameToFind);
                                Console.WriteLine($"Found contacts:\n{string.Join('\n', contactsByName)}");
                                break;

                            case "2":
                                Console.WriteLine("Введите фамилию контакта");
                                var surnameToFind = Console.ReadLine()!;
                                var contactsByLastName = await service.FindByLastName(surnameToFind);
                                Console.WriteLine($"Found contacts:\n{string.Join('\n', contactsByLastName)}");
                                break;

                            case "3":
                                Console.WriteLine("Введите email");
                                var emailToFind = Console.ReadLine()!;
                                var contactsByEmail = await service.FindByEmail(emailToFind);
                                Console.WriteLine($"Found contacts:\n{string.Join('\n', contactsByEmail)}");
                                break;

                            case "4":
                                Console.WriteLine("Введите номер телефона");
                                var phoneToFind = Console.ReadLine()!;
                                var contactsByPhone = await service.FindByPhone(phoneToFind);
                                Console.WriteLine($"Found contacts:\n{string.Join('\n', contactsByPhone)}");
                                break;
                        }
                    }

                    break;

                case "3":
                    Console.WriteLine("Введите имя контакта");
                    string firstName = Console.ReadLine()!;

                    Console.WriteLine("Введите фамилию контакта");
                    string lastName = Console.ReadLine()!;

                    Console.WriteLine("Введите email (0 если закончить)");
                    List<string> emailList = new();
                    string email = Console.ReadLine()!;
                    while (email != "0")
                    {
                        if (email.Length > 0) emailList.Add(email);
                        email = Console.ReadLine()!;
                    }

                    Console.WriteLine("Введите номер телефона (0 если закончить)");
                    List<string> phoneList = new();
                    string phone = Console.ReadLine()!;
                    while (phone != "0")
                    {
                        if (phone.Length > 0) phoneList.Add(phone);
                        phone = Console.ReadLine()!;
                    }

                    await service.Create(firstName, lastName, emailList, phoneList);
                    break;

                case "0":
                    break;
            }
        }
    }
}

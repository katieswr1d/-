using ContactBook.Core.Entity;
using ContactBook.Core.Services;
using ContactBook.DAL.Data;
using ContactBook.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCollectionOrderer(ordererTypeName: "ContactBook.Tests.Orderers.DisplayNameOrderer", ordererAssemblyName: "ContactBook.Tests")]
namespace ContactBook.Tests
{
    //три коллекции тестов : 1 чистит 2 создает 3 читает/ищет (запускаются по очереди)
    
    [Collection("A Test Collection")]
    public class ContactServiceTestA
    {
        public IContactService service { get; set; }
        public ContactServiceTestA()
        {
            var options = new DbContextOptionsBuilder<ContactBookDbContext>().UseSqlite("Data Source=Contacts.db").Options;
            var context = new ContactBookDbContext(options); // создаем контекст
            service = new ContactService(new FileRepository(context)); // передаем контекст в репозиторий
        }
        [Fact]
        public void TestClear()
        {
            TestClearAsync().Wait();
        }
        public async Task TestClearAsync()
        {
            Assert.True(await service.Clear());
            Assert.Equal(0, service.Count());
        }
    }
    [Collection("B Test Collection")]
    public class ContactServiceTestB
    {
        public IContactService service { get; set; }
        public ContactServiceTestB()
        {
            var options = new DbContextOptionsBuilder<ContactBookDbContext>().UseSqlite("Data Source=Contacts.db").Options;
            var context = new ContactBookDbContext(options); // создаем контекст
            service = new ContactService(new FileRepository(context)); // передаем контекст в репозиторий
        }
        [Theory]
        [MemberData(nameof(PersonData))]
        public async Task TestCreateContact(string FirstName, string LastName, List<string> email, List<string> phoneNumber)
        {
            TestCreateContactAsync(FirstName, LastName, email,phoneNumber).Wait();
        }
        public async Task TestCreateContactAsync(string FirstName, string LastName, List<string> email, List<string> phoneNumber)//
        {
            Assert.True(await service.Create(FirstName, LastName, email, phoneNumber));
        }
        public static IEnumerable<object[]> PersonData =>
            new List<object[]>
            {
                new object[] {"Ivan", "Sergeev", new List<string> { "ivan@mail.ru", "ivans@mail.ru" }, new List<string> { "7-927-4325832", "7-937-0004221" }},
                new object[] {"Sergey", "Mironov", new List<string> { "sermir@mail.ru", "sermir2020@mail.ru" }, new List<string> { "7-901-3643658", "7-907-4221412", "8-647-3261125" } },
                new object[] {"Artem", "Ivanov", new List<string> { "artv@mail.ru" }, new List<string> { "7-911-3463464", "7-912-6146442" }},
                new object[] {"Ruslan", "Minaev", new List<string> { "minrus@mail.ru" }, new List<string> { "7-927-3643400" } },
                new object[] {"Nail", "Minaev", new List<string> { "nailrus@mail.ru" }, new List<string> { "7-927-3355434" }},
                new object[] {"Michael", "Pleshakov", new List<string> { "mplesh@mail.ru" }, new List<string> { "7-987-3464611", "8-864-6464887" }},
                new object[] {"Sergey", "Pleshakov", new List<string> { "seple@mail.ru" }, new List<string> { "7-987-3464310", "8-864-6464887" }},
                new object[] {"Artem", "Sergeev", new List<string> { "arser@mail.ru" }, new List<string> { "7-914-3755684", "7-942-4646662" }},
                new object[] {"Roman", "Stepanov", new List<string> { "roman@mail.ru" }, new List<string> { "7-925-5535581" }},
                new object[] {"Sergey", "Stepanov", new List<string> { "sergs@mail.ru" }, new List<string> { "7-925-5556344" }},
                new object[] {"Olga", "Mironova", new List<string> { "olmir@mail.ru", "sermir2020@mail.ru" }, new List<string> { "8-647-3261125", "7-917-4121555" }},
                new object[] {"Oleg", "Minaev", new List<string> { "minaev@mail.ru" }, new List<string> { "8-647-3261125", "7-912-5555134" }}
            };
    }
    [Collection("C Test Collection")]
    public class ContactServiceTestC
    {
        public IContactService service { get; set; }
        public ContactServiceTestC()
        {
            var options = new DbContextOptionsBuilder<ContactBookDbContext>().UseSqlite("Data Source=Contacts.db").Options;
            var context = new ContactBookDbContext(options); // создаем контекст
            service = new ContactService(new FileRepository(context)); // передаем контекст в репозиторий
        }
        [Theory]
        [InlineData(12)]
        public async Task TestReadAll(int num)
        {
            IEnumerable<Contact> contacts =await service.ReadAll();
            Assert.Equal(num, contacts.Count());
        }
        [Theory]
        [InlineData("Sergey", 3)]
        public async Task TestFindByFirstName(string Name, int num)
        {
            IEnumerable<Contact> contacts = await service.FindByFirstName(Name);
            Assert.Equal(num, contacts.Count());
            for (int i = 0; i < num; i++)
            {
                Assert.Equal(Name, contacts.ElementAt(i).FirstName);
            }
        }
        [Theory]
        [InlineData("Minaev", 3)]
        public async Task TestFindByLastName(string Name, int num)
        {
            IEnumerable<Contact> contacts = await service.FindByLastName(Name);
            Assert.Equal(num, contacts.Count());
            for (int i = 0; i < num; i++)
            {
                Assert.Equal(Name, contacts.ElementAt(i).LastName);
            }
        }
        [Theory]
        [InlineData("8-647-3261125",new string[3]{ "Mironov", "Mironova", "Minaev" })]
        public async Task TestFindByLastPhone(string Phone, string[] names)
        {
            IEnumerable<Contact> contacts = await service.FindByPhone(Phone);
            Assert.Equal(names.Length, contacts.Count());
            for (int i = 0; i < names.Length; i++)
            {
                Assert.Equal(names[i], contacts.ElementAt(i).LastName);
            }
        }
        [Theory]
        [InlineData("Ruslan", "Minaev", "7-927-3643400", "minrus@mail.ru")]
        public async Task TestFindByAll(string Name, string LastName, string Phone, string Email)
        {
            IEnumerable<Contact> contacts = await service.FindByAll(Name, LastName, Phone, Email);
            Assert.Single(contacts);
            Assert.Equal(Name, contacts.ElementAt(0).FirstName);
            Assert.Equal(LastName, contacts.ElementAt(0).LastName);
        }

    }
}
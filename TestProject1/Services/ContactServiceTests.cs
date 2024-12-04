using System;
using System.Collections.Generic;
using ContactBook.Core.Entity;
using ContactBook.Core.Services;
using ContactBook.DAL;
using Moq;
using Xunit;
using System.Linq;


namespace TestProject1.Services
{
  /*  public class ContactServiceTests
    {
        private readonly Mock<IRepositoty> _repositoryMock;
        private readonly ContactService _contactService;

        public ContactServiceTests()
        {
            _repositoryMock = new Mock<IRepositoty>();
            _contactService = new ContactService(_repositoryMock.Object);
        }

        [Fact]
        public void ReadAll_ShouldReturnAllContacts()
        {
            // Arrange
            var contacts = new List<Contact>
            {
                new Contact { Id = 1, FirstName = "John", LastName = "Doe" },
                new Contact { Id = 2, FirstName = "Jane", LastName = "Smith" }
            };
            _repositoryMock.Setup(repo => repo.ReadAll()).Returns(contacts);

            // Act
            var result = _contactService.ReadAll();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.FirstName == "John" && c.LastName == "Doe");
        }

        [Fact]
        public void Create_ShouldCallRepositoryCreate()
        {
            // Arrange
            var newContact = new Contact { Id = 3, FirstName = "Alice", LastName = "Johnson" };

            // Act
            _contactService.Create(newContact);

            // Assert
            _repositoryMock.Verify(repo => repo.Create(newContact), Times.Once);
        }

        [Fact]
        public void FindByFirstName_ShouldReturnMatchingContacts()
        {
            // Arrange
            var contacts = new List<Contact>
            {
                new Contact { Id = 1, FirstName = "John", LastName = "Doe" },
                new Contact { Id = 2, FirstName = "Alice", LastName = "Johnson" }
            };
            _repositoryMock.Setup(repo => repo.FilterContacts(It.IsAny<Predicate<Contact>>()))
                           .Returns((Predicate<Contact> predicate) => contacts.Where(c => predicate(c)).ToList());

            // Act
            var result = _contactService.FindByFirstName("Alice");

            // Assert
            Assert.Single(result);
            Assert.Contains(result, c => c.FirstName == "Alice");
        }
    }*/
}
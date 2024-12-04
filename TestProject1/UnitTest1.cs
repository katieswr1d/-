using ContactBook.Core.Entity;
using Moq;
using System;
using Xunit;

namespace TestProject1
{
    

    
    
        public class UnitTest1
        {
            [Fact]
            public void Id_ShouldSetAndGetCorrectly()
            {
                // Arrange
                var email = new Email();
                int testId = 1;

                // Act
                email.Id = testId;

                // Assert
                Assert.Equal(testId, email.Id);
            }

            [Fact]
            public void Value_ShouldSetAndGetCorrectly()
            {
                // Arrange
                var email = new Email();
                string testValue = "test@example.com";

                // Act
                email.Value = testValue;

                // Assert
                Assert.Equal(testValue, email.Value);
            }

            [Fact]
            public void ToString_ShouldReturnValue()
            {
                // Arrange
                var email = new Email { Value = "test@example.com" };

                // Act
                var result = email.ToString();

                // Assert
                Assert.Equal("test@example.com", result);
            }

        }
    }



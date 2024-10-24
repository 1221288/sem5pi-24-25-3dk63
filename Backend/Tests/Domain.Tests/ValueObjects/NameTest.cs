using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain.Shared;
using Xunit;

namespace Backend.Tests.Domain.Tests.ValueObjects
{
    public class NameTest
    {
        [Fact]
        public void GivenValidFirstNameAndLastName_WhenConstructed_ThenNameShouldBeCreated()
        {
            // Arrange
            var firstName = "Rodrigo";
            var lastName = "Botelho";

            // Act
            var name = new Name(firstName, lastName);

            // Assert
            Assert.Equal(firstName, name.FirstName);
            Assert.Equal(lastName, name.LastName);
            Assert.Equal("Rodrigo Botelho", name.FullName);
        }

        [Theory]
        [InlineData("", "Botelho")]
        [InlineData("Rodrigo", "")]
        [InlineData("   ", "Botelho")]
        [InlineData("Rodrigo", "   ")]
        [InlineData(null, "Botelho")]
        [InlineData("Rodrigo", null)]
        public void GivenInvalidFirstNameOrLastName_WhenConstructed_ThenBusinessRuleValidationExceptionShouldBeThrown(string firstName, string lastName)
        {
            // Act & Assert
            var exception = Assert.Throws<BusinessRuleValidationException>(() => new Name(firstName, lastName));
            Assert.Equal("First and Last names cannot be empty.", exception.Message);
        }

        [Fact]
        public void ToString_ShouldReturnFullName()
        {
            // Arrange
            var name = new Name("Rodrigo", "Botelho Nabo");

            // Act
            var fullName = name.ToString();

            // Assert
            Assert.Equal("Rodrigo Botelho Nabo", fullName);
        }

    }
}
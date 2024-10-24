using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Users;
using Xunit;

namespace Backend.Tests.Domain.Tests.ValueObjects
{
    public class EmailTests
    {
        [Fact]
        public void GivenValidEmail_WhenConstructed_ThenEmailShouldBeCreated()
        {
            // Arrange
            var validEmail = "joaofurtado@gmail.com";

            // Act
            var email = new Email(validEmail);

            // Assert
            Assert.Equal(validEmail, email.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void GivenEmptyEmail_WhenConstructed_ThenBusinessRuleValidationExceptionShouldBeThrown(string emptyEmail)
        {
            // Act & Assert
            var exception = Assert.Throws<BusinessRuleValidationException>(() => new Email(emptyEmail));
            Assert.Equal("Email cannot be empty.", exception.Message);
        }

        [Theory]
        [InlineData("invalid-email")]
        [InlineData("invalid@.com")]
        [InlineData("invalid@gmail.")]
        [InlineData("invalid@gmail.c")]
        [InlineData("invalid@gmail..com")]
        public void GivenInvalidEmail_WhenConstructed_ThenBusinessRuleValidationExceptionShouldBeThrown(string invalidEmail)
        {
            // Act & Assert
            var exception = Assert.Throws<BusinessRuleValidationException>(() => new Email(invalidEmail));
            Assert.Equal("Invalid email format.", exception.Message);
        }

        [Fact]
        public void ToString_ShouldReturnEmailValue()
        {
            // Arrange
            var validEmail = "marianaarruda@gmail.com";
            var email = new Email(validEmail);

            // Act
            var result = email.ToString();

            // Assert
            Assert.Equal(validEmail, result);
        }
    }
}
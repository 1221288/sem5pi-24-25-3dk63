using System;
using Xunit;
using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain.Shared;

namespace Backend.Tests.Domain.Tests.ValueObjects
{
    public class PhoneNumberTest
    {
        [Fact]
        public void ValidPhoneNumber_ShouldBeInstantiated()
        {
            // Arrange
            string validNumber = "+1234567890";

            // Act
            var phoneNumber = new PhoneNumber(validNumber);

            // Assert
            Assert.NotNull(phoneNumber);
            Assert.Equal(validNumber, phoneNumber.Number);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void EmptyPhoneNumber_ShouldThrowArgumentException(string invalidNumber)
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new PhoneNumber(invalidNumber));
            Assert.Equal("Phone number cannot be empty.", exception.Message);
        }

        [Theory]
        [InlineData("+12345")]
        [InlineData("+1234567890123456")]
        [InlineData("1234567890")]
        [InlineData("+123abc456")]
        public void InvalidPhoneNumber_ShouldThrowArgumentException(string invalidNumber)
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new PhoneNumber(invalidNumber));
            Assert.Equal("Invalid phone number format.", exception.Message);
        }

        [Fact]
        public void Equals_ShouldReturnTrueForSamePhoneNumber()
        {
            // Arrange
            var phoneNumber1 = new PhoneNumber("+1234567890");
            var phoneNumber2 = new PhoneNumber("+1234567890");

            // Act & Assert
            Assert.True(phoneNumber1.Equals(phoneNumber2));
        }

        [Fact]
        public void Equals_ShouldReturnFalseForDifferentPhoneNumber()
        {
            // Arrange
            var phoneNumber1 = new PhoneNumber("+1234567890");
            var phoneNumber2 = new PhoneNumber("+0987654321");

            // Act & Assert
            Assert.False(phoneNumber1.Equals(phoneNumber2));
        }

        [Fact]
        public void GetHashCode_ShouldReturnSameValueForSamePhoneNumber()
        {
            // Arrange
            var phoneNumber1 = new PhoneNumber("+1234567890");
            var phoneNumber2 = new PhoneNumber("+1234567890");

            // Act & Assert
            Assert.Equal(phoneNumber1.GetHashCode(), phoneNumber2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldReturnDifferentValueForDifferentPhoneNumber()
        {
            // Arrange
            var phoneNumber1 = new PhoneNumber("+1234567890");
            var phoneNumber2 = new PhoneNumber("+0987654321");

            // Act & Assert
            Assert.NotEqual(phoneNumber1.GetHashCode(), phoneNumber2.GetHashCode());
        }
    }
}
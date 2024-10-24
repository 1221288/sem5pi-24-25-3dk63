using Xunit;
using DDDSample1.Domain.Users;
using DDDSample1.Domain.Shared;

namespace Backend.Tests.Domain.Tests.ValueObjects
{
    public class UsernameTests
    {
        [Fact]
        public void Username_ShouldBeInstantiated_WithValidValue()
        {
            // Arrange
            string validUsername = "D2024000001";

            // Act
            var username = new Username(validUsername);

            // Assert
            Assert.NotNull(username);
            Assert.Equal(validUsername, username.Value);
        }

        [Fact]
        public void Username_ShouldThrowBusinessRuleValidationException_WhenValueIsEmpty()
        {
            // Arrange
            string invalidUsername = "";

            // Act & Assert
            var exception = Assert.Throws<BusinessRuleValidationException>(() => new Username(invalidUsername));
            Assert.Equal("Username cannot be empty.", exception.Message);
        }

        [Fact]
        public void Username_ShouldThrowBusinessRuleValidationException_WhenValueIsWhitespace()
        {
            // Arrange
            string invalidUsername = "   ";

            // Act & Assert
            var exception = Assert.Throws<BusinessRuleValidationException>(() => new Username(invalidUsername));
            Assert.Equal("Username cannot be empty.", exception.Message);
        }

        [Theory]
        [InlineData("N2024000023")]
        [InlineData("D2024000045")]
        [InlineData("O2024000067")]
        public void Equals_ShouldReturnTrue_ForSameUsername(string usernameValue)
        {
            // Arrange
            var username1 = new Username(usernameValue);
            var username2 = new Username(usernameValue);

            // Act & Assert
            Assert.True(username1.Equals(username2));
        }

        [Theory]
        [InlineData("O2024000067", "O2024000089")]
        [InlineData("D2024000001", "N2024000023")]
        public void Equals_ShouldReturnFalse_ForDifferentUsernames(string usernameValue1, string usernameValue2)
        {
            // Arrange
            var username1 = new Username(usernameValue1);
            var username2 = new Username(usernameValue2);

            // Act & Assert
            Assert.False(username1.Equals(username2));
        }

        [Theory]
        [InlineData("D2024000045")]
        [InlineData("N2024000023")]
        public void GetHashCode_ShouldReturnSameValue_ForSameUsername(string usernameValue)
        {
            // Arrange
            var username1 = new Username(usernameValue);
            var username2 = new Username(usernameValue);

            // Act & Assert
            Assert.Equal(username1.GetHashCode(), username2.GetHashCode());
        }

        [Theory]
        [InlineData("O2024000067", "O2024000089")]
        [InlineData("O2024000045", "N2024000023")]
        public void GetHashCode_ShouldReturnDifferentValue_ForDifferentUsernames(string usernameValue1, string usernameValue2)
        {
            // Arrange
            var username1 = new Username(usernameValue1);
            var username2 = new Username(usernameValue2);

            // Act & Assert
            Assert.NotEqual(username1.GetHashCode(), username2.GetHashCode());
        }

        [Theory]
        [InlineData("O2024000045")]
        [InlineData("N2024000023")]
        public void ToString_ShouldReturnUsernameValue(string usernameValue)
        {
            // Arrange
            var username = new Username(usernameValue);

            // Act & Assert
            Assert.Equal(usernameValue, username.ToString());
        }
    }
}

using Xunit;
using DDDSample1.Domain.Users;

namespace Backend.Tests.Domain.Tests.ValueObjects
{
    public class RoleTest
    {
        [Fact]
        public void Role_ShouldBeInstantiated_WithValidRoleType()
        {
            // Arrange
            RoleType roleType = RoleType.Admin;

            // Act
            var role = new Role(roleType);

            // Assert
            Assert.NotNull(role);
            Assert.Equal(roleType, role.Value);
        }

        [Theory]
        [InlineData(RoleType.Admin)]
        [InlineData(RoleType.Doctor)]
        [InlineData(RoleType.Nurse)]
        [InlineData(RoleType.Technician)]
        [InlineData(RoleType.Patient)]
        public void Equals_ShouldReturnTrue_ForSameRole(RoleType roleType)
        {
            // Arrange
            var role1 = new Role(roleType);
            var role2 = new Role(roleType);

            // Act & Assert
            Assert.True(role1.Equals(role2));
        }

        [Theory]
        [InlineData(RoleType.Admin, RoleType.Doctor)]
        [InlineData(RoleType.Nurse, RoleType.Technician)]
        public void Equals_ShouldReturnFalse_ForDifferentRoles(RoleType roleType1, RoleType roleType2)
        {
            // Arrange
            var role1 = new Role(roleType1);
            var role2 = new Role(roleType2);

            // Act & Assert
            Assert.False(role1.Equals(role2));
        }

        [Theory]
        [InlineData(RoleType.Admin)]
        [InlineData(RoleType.Doctor)]
        [InlineData(RoleType.Nurse)]
        [InlineData(RoleType.Technician)]
        [InlineData(RoleType.Patient)]
        public void GetHashCode_ShouldReturnSameValue_ForSameRole(RoleType roleType)
        {
            // Arrange
            var role1 = new Role(roleType);
            var role2 = new Role(roleType);

            // Act & Assert
            Assert.Equal(role1.GetHashCode(), role2.GetHashCode());
        }

        [Theory]
        [InlineData(RoleType.Admin, "Admin")]
        [InlineData(RoleType.Doctor, "Doctor")]
        [InlineData(RoleType.Nurse, "Nurse")]
        [InlineData(RoleType.Technician, "Technician")]
        [InlineData(RoleType.Patient, "Patient")]
        public void ToString_ShouldReturnRoleName(RoleType roleType, string expectedString)
        {
            // Arrange
            var role = new Role(roleType);

            // Act & Assert
            Assert.Equal(expectedString, role.ToString());
        }
    }
}

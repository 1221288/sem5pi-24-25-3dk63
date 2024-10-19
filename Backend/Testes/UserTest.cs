// using Xunit;
// using Backend.Domain.Users;
// using Backend.Domain.Users.ValueObjects;
// using Moq;
// using System;
// using DDDSample1.Domain.Users;

// namespace DDDSample1.Domain.Tests
// {
//     public class UserTests
//     {
//         private readonly Mock<Role> _mockRole;
//         private readonly Mock<Email> _mockEmail;
//         private readonly Mock<Name> _mockName;

//         public UserTests()
//         {
//             _mockRole = new Mock<Role>();
//             _mockEmail = new Mock<Email>();
//             _mockName = new Mock<Name>();
//         }

//         [Fact]
//         public void WhenPassingValidData_UserIsInstantiated()
//         {
//             // Arrange
//             var role = new Role(RoleType.Doctor);
//             var email = new Email("catarinamoreira@email.pt");
//             var name = new Name("Catarina", "Moreira");
//             var recruitmentYear = 2024;
//             var domain = "example.com";
//             var sequentialNumber = 1;

//             // Act
//              new User(role, email, name, recruitmentYear, domain, sequentialNumber);

//         }

//         [Fact]
//         public void WhenPassingNullRole_ThrowsArgumentNullException()
//         {
//             // Arrange
//             var email = new Email("catarinamoreira@email.pt");
//             var name = new Name("Catarina", "Moreira");
//             var recruitmentYear = 2024;
//             var domain = "example.com";
//             var sequentialNumber = 1;

//             // Assert
//             Assert.Throws<ArgumentNullException>(() =>
//             {
//                 // Act
//                 new User(null, email, name, recruitmentYear, domain, sequentialNumber);
//             });
//         }
//     }
// }

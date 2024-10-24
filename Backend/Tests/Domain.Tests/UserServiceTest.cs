using Xunit;
using Moq;
using DDDSample1.Domain.Users;
using DDDSample1.Domain.Shared;
using AutoMapper;
using DDDSample1.Users;
using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain;

namespace Backend.Tests.Services
{
    public class UserServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserService _userService;

        public UserServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _mapperMock = new Mock<IMapper>();

            _userService = new UserService(
                _unitOfWorkMock.Object,
                _userRepositoryMock.Object,
                _configurationMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnUserDTOList()
        {
            // Arrange
            var users = new List<User>
            {
                new User(new Role(RoleType.Admin), new Email("diogoalmeida@google.com"), new Name("Diogo", "Almeida"), 2024, "MyHospital.com", 1, new PhoneNumber("+351937625401"))
            };

            _userRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);
            _mapperMock.Setup(m => m.Map<List<UserDTO>>(users)).Returns(new List<UserDTO>
            {
                new UserDTO
                {
                    Email = new Email("diogoalmeida@google.com"),
                    Name = new Name("Diogo", "Almeida")
                }
            });

            // Act
            var result = await _userService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(users.Count, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUserDTO()
        {
            // Arrange
            var userId = new UserId(Guid.NewGuid());
            var user = new User(new Role(RoleType.Admin), new Email("diogoalmeida@google.com"), new Name("Diogo", "Almeida"), 2024, "MyHospital.com", 1, new PhoneNumber("+351937625401"));

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<UserDTO>(user)).Returns(new UserDTO
            {
                Email = new Email("diogoalmeida@google.com"),
                Name = new Name("Diogo", "Almeida")
            });

            // Act
            var result = await _userService.GetByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("diogoalmeida@google.com", result.Email.Value);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException_WhenEmailExists()
        {
            // Arrange
            var dto = new CreatingUserDto(
                new Role(RoleType.Admin),
                new Email("diogoalmeida@google.com"),
                "Diogo",
                "Almeida",
                new PhoneNumber("+351937625401")
            );

            var existingUser = new User(new Role(RoleType.Admin), new Email("diogoalmeida@google.com"), new Name("Diogo", "Almeida"), 2024, "MyHospital.com", 1, new PhoneNumber("+351937625401"));


            _userRepositoryMock.Setup(repo => repo.FindByEmailAsync(It.IsAny<Email>())).ReturnsAsync(existingUser);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<BusinessRuleValidationException>(() => _userService.AddAsync(dto));
            Assert.Equal("Email já existe no sistema, por favor tente novamente com outro email.", exception.Message);
        }
    }
}

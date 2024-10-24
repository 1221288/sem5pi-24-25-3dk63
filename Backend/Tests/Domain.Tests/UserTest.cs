namespace Domain.Tests;

using System;
using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Users;
using Xunit;


/* fazer testes para user:

    - passar dados corretos
    - passar email vazio
    - passar email com formato inválido
    - passar número de telefone inválido
    - mudar estado para inativo
    - mudar estado para ativo
    - mudar email
    - mudar role
    - mudar nome
    - mudar telefone
    - mudar sequencial
    - confirmacao do token

*/
public class UserTest
{
    [Theory]
    [InlineData(RoleType.Doctor, "anasilva@gmail.com", "Ana", "Silva", 2024, "MyHospital.com", 2, "+351987654321")]
    [InlineData(RoleType.Nurse, "pedrosantos@gmail.com", "Pedro", "Santos", 2023, "MyHospital.com", 3, "+351912345678")]
    [InlineData(RoleType.Technician, "joanacosta@gmail.com", "Joana", "Costa", 2025, "MyHospital.com", 4, "+351932112233")]
    [InlineData(RoleType.Admin, "mariaoliveira@gmail.com", "Maria", "Oliveira", 2022, "MyHospital.com", 5, "+351965432198")]
    [InlineData(RoleType.Patient, "carlosalmeida@gmail.com", "Carlos", "Almeida", 2026, "MyHospital.com", 6, "+351923456789")]

    public void WhenPassingCorrectData_ThenUserIsInstantiated(RoleType role, string email, string firstName, string lastName, int recruitmentYear, string domain, int sequentialNumber, string phoneNumber)
    {
        var user = new User(
            new Role(role),
            new Email(email),
            new Name(firstName, lastName),
            recruitmentYear,
            domain,
            sequentialNumber,
            new PhoneNumber(phoneNumber)
        );

        Assert.NotNull(user);
        Assert.True(user.Active);
        Assert.Equal(role, user.Role.Value);
        Assert.Equal(email, user.Email.Value);
        Assert.Equal(firstName, user.Name.FirstName);
        Assert.Equal(lastName, user.Name.LastName);
        Assert.Equal(phoneNumber, user.PhoneNumber.Number);
    }


    [Theory]
    [InlineData(RoleType.Doctor, "", "Gonçalo", "Carvalho", 2000, "MyHospital.com", 1, "+351927123456")]
    [InlineData(RoleType.Nurse, "", "Sofia", "Pereira", 2024, "MyHospital.com", 2, "+351987654321")]
    [InlineData(RoleType.Technician, "", "Tiago", "Lima", 2023, "MyHospital.com", 3, "+351965432198")]
    [InlineData(RoleType.Admin, "", "Catarina", "Gomes", 2025, "MyHospital.com", 4, "+351932112233")]
    [InlineData(RoleType.Patient, "", "João", "Martins", 2026, "MyHospital.com", 5, "+351923456789")]
    public void WhenPassingEmptyEmail_ThenThrowsException(RoleType role, string email, string firstName, string lastName, int recruitmentYear, string domain, int sequentialNumber, string phoneNumber)
    {
        var exception = Assert.Throws<BusinessRuleValidationException>(() =>
        {
            var user = new User(
                new Role(role),
                new Email(email),
                new Name(firstName, lastName),
                recruitmentYear,
                domain,
                sequentialNumber,
                new PhoneNumber(phoneNumber)
            );
        });

        Assert.Equal("Email cannot be empty.", exception.Message);
    }

    [Theory]
    [InlineData(RoleType.Doctor, "anasilva.gmail.com", "Ana", "Silva", 2024, "MyHospital.com", 2, "+351987654321")]
    [InlineData(RoleType.Nurse, "pedrosantosgmailcom", "Pedro", "Santos", 2023, "MyHospital.com", 3, "+351912345678")]
    [InlineData(RoleType.Technician, "joanacosta@.gmail.com", "Joana", "Costa", 2025, "MyHospital.com", 4, "+351932112233")]
    [InlineData(RoleType.Admin, "mariaoliveira@myhospital", "Maria", "Oliveira", 2022, "MyHospital.com", 5, "+351965432198")]
    [InlineData(RoleType.Patient, "carlosalmeida.com", "Carlos", "Almeida", 2026, "MyHospital.com", 6, "+351923456789")]
    public void WhenPassingInvalidEmailFormat_ThenThrowsException(RoleType role, string email, string firstName, string lastName, int recruitmentYear, string domain, int sequentialNumber, string phoneNumber)
    {
        var exception = Assert.Throws<BusinessRuleValidationException>(() =>
        {
            var user = new User(
                new Role(role),
                new Email(email),
                new Name(firstName, lastName),
                recruitmentYear,
                domain,
                sequentialNumber,
                new PhoneNumber(phoneNumber)
            );
        });

        Assert.Equal("Invalid email format.", exception.Message);
    }

    [Theory]
    [InlineData(RoleType.Doctor, "anasilva@gmail.com", "Ana", "Silva", 2024, "MyHospital.com", 2, "+351")]
    [InlineData(RoleType.Nurse, "pedrosantos@gmail.com", "Pedro", "Santos", 2023, "MyHospital.com", 3, "912345678")]
    [InlineData(RoleType.Technician, "joanacosta@gmail.com", "Joana", "Costa", 2025, "MyHospital.com", 4, "+93782123")]
    [InlineData(RoleType.Admin, "mariaoliveira@gmail.com", "Maria", "Oliveira", 2022, "MyHospital.com", 5, "+3519999999999999")]
    [InlineData(RoleType.Patient, "carlosalmeida@gmail.com", "Carlos", "Almeida", 2026, "MyHospital.com", 6, "+351923")]

    public void WhenPassingInvalidPhoneNumber_ThenThrowsException(RoleType role, string email, string firstName, string lastName, int recruitmentYear, string domain, int sequentialNumber, string phoneNumber)
    {
        var exception = Assert.Throws<ArgumentException>(() =>
        {
            var user = new User(
                new Role(role),
                new Email(email),
                new Name(firstName, lastName),
                recruitmentYear,
                domain,
                sequentialNumber,
                new PhoneNumber(phoneNumber)
            );
        });

        Assert.Equal("Invalid phone number format.", exception.Message);
    }

    [Theory]
    [InlineData(RoleType.Doctor, "dianaarruda@gmail.com", "Diana", "Arruda", 2024, "MyHospital.com", 1, "+351123456789")]
    [InlineData(RoleType.Nurse, "anasilva@gmail.com", "Ana", "Silva", 2023, "MyHospital.com", 2, "+351912345678")]
    [InlineData(RoleType.Technician, "joanacosta@gmail.com", "Joana", "Costa", 2025, "MyHospital.com", 3, "+351987654321")]
    [InlineData(RoleType.Admin, "mariaoliveira@gmail.com", "Maria", "Oliveira", 2022, "MyHospital.com", 4, "+351999999999")]
    [InlineData(RoleType.Patient, "carlosalmeida@gmail.com", "Carlos", "Almeida", 2026, "MyHospital.com", 5, "+351932112233")]
    public void WhenChangingActiveToFalse_UserIsInactive(RoleType role, string email, string firstName, string lastName, int recruitmentYear, string domain, int sequentialNumber, string phoneNumber)
    {
        var user = new User(
            new Role(role),
            new Email(email),
            new Name(firstName, lastName),
            recruitmentYear,
            domain,
            sequentialNumber,
            new PhoneNumber(phoneNumber)
        );

        user.ChangeActiveFalse();

        Assert.False(user.Active);
    }

    [Theory]
    [InlineData(RoleType.Doctor, "dianaarruda@gmail.com", "Diana", "Arruda", 2024, "MyHospital.com", 1, "+351123456789")]
    [InlineData(RoleType.Nurse, "anasilva@gmail.com", "Ana", "Silva", 2023, "MyHospital.com", 2, "+351912345678")]
    [InlineData(RoleType.Technician, "joanacosta@gmail.com", "Joana", "Costa", 2025, "MyHospital.com", 3, "+351987654321")]
    [InlineData(RoleType.Admin, "mariaoliveira@gmail.com", "Maria", "Oliveira", 2022, "MyHospital.com", 4, "+351999999999")]
    [InlineData(RoleType.Patient, "carlosalmeida@gmail.com", "Carlos", "Almeida", 2026, "MyHospital.com", 5, "+351932112233")]
    public void WhenChangingActiveToTrue_UserIsActive(RoleType role, string email, string firstName, string lastName, int recruitmentYear, string domain, int sequentialNumber, string phoneNumber)
    {
        var user = new User(
            new Role(role),
            new Email(email),
            new Name(firstName, lastName),
            recruitmentYear,
            domain,
            sequentialNumber,
            new PhoneNumber(phoneNumber)
        );

        user.ChangeActiveFalse();
        user.ChangeActiveTrue();
        Assert.True(user.Active);
    }


    [Theory]
    [InlineData(RoleType.Doctor, "dianaarruda@gmail.com", "Diana", "Arruda", 2024, "MyHospital.com", 1, "+351123456789", RoleType.Nurse)]
    [InlineData(RoleType.Nurse, "anasilva@gmail.com", "Ana", "Silva", 2023, "MyHospital.com", 2, "+351912345678", RoleType.Admin)]
    [InlineData(RoleType.Technician, "joanacosta@gmail.com", "Joana", "Costa", 2025, "MyHospital.com", 3, "+351987654321", RoleType.Doctor)]
    [InlineData(RoleType.Admin, "mariaoliveira@gmail.com", "Maria", "Oliveira", 2022, "MyHospital.com", 4, "+351999999999", RoleType.Technician)]
    [InlineData(RoleType.Patient, "carlosalmeida@gmail.com", "Carlos", "Almeida", 2026, "MyHospital.com", 5, "+351932112233", RoleType.Nurse)]
    public void WhenChangingRole_UserRoleIsUpdated(RoleType role, string email, string firstName, string lastName, int recruitmentYear, string domain, int sequentialNumber, string phoneNumber, RoleType newRoleType)
    {
        var user = new User(
            new Role(role),
            new Email(email),
            new Name(firstName, lastName),
            recruitmentYear,
            domain,
            sequentialNumber,
            new PhoneNumber(phoneNumber)
        );

        var newRole = new Role(newRoleType);
        user.ChangeRole(newRole);

        Assert.Equal(newRole, user.Role);
    }

    [Theory]
    [InlineData(RoleType.Doctor, "dianaarruda@gmail.com", "Diana", "Arruda", 2024, "MyHospital.com", 1, "+351123456789")]
    [InlineData(RoleType.Nurse, "anasilva@gmail.com", "Ana", "Silva", 2023, "MyHospital.com", 2, "+351912345678")]
    [InlineData(RoleType.Technician, "joanacosta@gmail.com", "Joana", "Costa", 2025, "MyHospital.com", 3, "+351987654321")]
    [InlineData(RoleType.Admin, "mariaoliveira@gmail.com", "Maria", "Oliveira", 2022, "MyHospital.com", 4, "+351999999999")]
    [InlineData(RoleType.Patient, "carlosalmeida@gmail.com", "Carlos", "Almeida", 2026, "MyHospital.com", 5, "+351932112233")]
    public void WhenChangingRoleOfInactiveUser_ThrowsException(RoleType role, string email, string firstName, string lastName, int recruitmentYear, string domain, int sequentialNumber, string phoneNumber)
    {
        var user = new User(
            new Role(role),
            new Email(email),
            new Name(firstName, lastName),
            recruitmentYear,
            domain,
            sequentialNumber,
            new PhoneNumber(phoneNumber)
        );

        user.ChangeActiveFalse();

        var exception = Assert.Throws<BusinessRuleValidationException>(() => user.ChangeRole(new Role(RoleType.Nurse)));

        Assert.Equal("User cannot be changed in this state", exception.Message);
    }

    [Theory]
    [InlineData(RoleType.Doctor, "dianaarruda@gmail.com", "Diana", "Arruda", 2024, "MyHospital.com", 1, "+351123456789", "dianaarruda02@gmail.com")]
    [InlineData(RoleType.Nurse, "anasilva@gmail.com", "Ana", "Silva", 2023, "MyHospital.com", 2, "+351912345678", "ana.silva02@gmail.com")]
    [InlineData(RoleType.Technician, "joanacosta@gmail.com", "Joana", "Costa", 2025, "MyHospital.com", 3, "+351987654321", "joana.costa02@gmail.com")]
    [InlineData(RoleType.Admin, "mariaoliveira@gmail.com", "Maria", "Oliveira", 2022, "MyHospital.com", 4, "+351999999999", "maria.oliveira02@gmail.com")]
    [InlineData(RoleType.Patient, "carlosalmeida@gmail.com", "Carlos", "Almeida", 2026, "MyHospital.com", 5, "+351932112233", "carlos.almeida02@gmail.com")]
    public void WhenChangingEmail_EmailIsUpdated(RoleType role, string email, string firstName, string lastName, int recruitmentYear, string domain, int sequentialNumber, string phoneNumber, string newEmailValue)
    {
        var user = new User(
            new Role(role),
            new Email(email),
            new Name(firstName, lastName),
            recruitmentYear,
            domain,
            sequentialNumber,
            new PhoneNumber(phoneNumber)
        );

        var newEmail = new Email(newEmailValue);
        user.ChangeEmail(newEmail);

        Assert.Equal(newEmail, user.Email);
    }

    [Theory]
    [InlineData(RoleType.Doctor, "dianaarruda@gmail.com", "Diana", "Arruda", 2024, "MyHospital.com", 1, "+351123456789", "NewFirst", "NewLast")]
    [InlineData(RoleType.Nurse, "anasilva@gmail.com", "Ana", "Silva", 2023, "MyHospital.com", 2, "+351912345678", "NewAna", "Silva")]
    [InlineData(RoleType.Technician, "joanacosta@gmail.com", "Joana", "Costa", 2025, "MyHospital.com", 3, "+351987654321", "Joana", "NewCosta")]
    [InlineData(RoleType.Admin, "mariaoliveira@gmail.com", "Maria", "Oliveira", 2022, "MyHospital.com", 4, "+351999999999", "Maria", "NewOliveira")]
    [InlineData(RoleType.Patient, "carlosalmeida@gmail.com", "Carlos", "Almeida", 2026, "MyHospital.com", 5, "+351932112233", "Carlos", "NewAlmeida")]
    public void WhenChangingName_NameIsUpdated(RoleType role, string email, string firstName, string lastName, int recruitmentYear, string domain, int sequentialNumber, string phoneNumber, string newFirstName, string newLastName)
    {
        var user = new User(
            new Role(role),
            new Email(email),
            new Name(firstName, lastName),
            recruitmentYear,
            domain,
            sequentialNumber,
            new PhoneNumber(phoneNumber)
        );

        var newName = new Name(newFirstName, newLastName);
        user.ChangeName(newName);

        Assert.Equal(newName, user.Name);
    }

    [Theory]
    [InlineData(RoleType.Doctor, "dianaarruda@gmail.com", "Diana", "Arruda", 2024, "MyHospital.com", 1, "+351123456789")]
    [InlineData(RoleType.Nurse, "anasilva@gmail.com", "Ana", "Silva", 2023, "MyHospital.com", 2, "+351912345678")]
    [InlineData(RoleType.Technician, "joanacosta@gmail.com", "Joana", "Costa", 2025, "MyHospital.com", 3, "+351987654321")]
    [InlineData(RoleType.Admin, "mariaoliveira@gmail.com", "Maria", "Oliveira", 2022, "MyHospital.com", 4, "+351999999999")]
    [InlineData(RoleType.Patient, "carlosalmeida@gmail.com", "Carlos", "Almeida", 2026, "MyHospital.com", 5, "+351932112233")]
    public void WhenGeneratingConfirmationToken_TokenIsGenerated(RoleType role, string email, string firstName, string lastName, int recruitmentYear, string domain, int sequentialNumber, string phoneNumber)
    {
        var user = new User(
            new Role(role),
            new Email(email),
            new Name(firstName, lastName),
            recruitmentYear,
            domain,
            sequentialNumber,
            new PhoneNumber(phoneNumber)
        );

        user.generateConfirmationToken();

        Assert.False(string.IsNullOrEmpty(user.ConfirmationToken));
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Users;
using DDDSample1.Infrastructure.Users;
using Microsoft.Extensions.Configuration;

namespace DDDSample1.Users
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _configuration = configuration;
        }
        public async Task<List<UserDTO>> GetAllAsync()
        {
            var list = await this._userRepository.GetAllAsync();
            List<UserDTO> listDto = list.ConvertAll(user => new UserDTO
            {
                Id = user.Id.AsGuid(),
                Role = user.Role,
                Username = user.Username,
                Email = user.Email
            });
            return listDto;
        }

        public async Task<UserDTO> GetByIdAsync(UserId id)
        {
            var user = await this._userRepository.GetByIdAsync(id);
            if (user == null) return null;

            return new UserDTO
            {
                Id = user.Id.AsGuid(),
                Role = user.Role,
                Username = user.Username,
                Email = user.Email
            };
        }

        public async Task<UserDTO> AddAsync(CreatingUserDto dto)
        {

            int sequentialNumber = await this._userRepository.GetNextSequentialNumberAsync();

            string domain = _configuration["DNS_DOMAIN"];

            int recruitmentYear = DateTime.Now.Year;
            var role = new Role(dto.Role.Value);
            var user = new User(role, new Email(dto.Email.Value), recruitmentYear,domain,sequentialNumber);



            await this._userRepository.AddAsync(user);
            await this._unitOfWork.CommitAsync();

            return new UserDTO { Id = user.Id.AsGuid(), Role = user.Role, Username = user.Username, Email = user.Email };
        }



        public async Task<UserDTO> UpdateAsync(UserDTO dto)
        {
            var user = await this._userRepository.GetByIdAsync(new UserId(dto.Id));
            if (user == null) return null;

            // Alterar todos os campos
            user.ChangeRole(dto.Role);
            user.ChangeUsername(dto.Username);
            user.ChangeEmail(dto.Email);

            await this._unitOfWork.CommitAsync();

            return new UserDTO
            {
                Id = user.Id.AsGuid(),
                Role = user.Role,
                Username = user.Username,
                Email = user.Email
            };
        }

        public async Task<UserDTO> DeleteAsync(UserId id)
        {
            var user = await this._userRepository.GetByIdAsync(id);
            if (user == null) return null;

            if (user.Active)
                throw new BusinessRuleValidationException("It is not possible to delete an active user");

            this._userRepository.Remove(user);
            await this._unitOfWork.CommitAsync();

            return new UserDTO
            {
                Id = user.Id.AsGuid(),
                Role = user.Role,
                Username = user.Username,
                Email = user.Email
            };
        }
    }
}

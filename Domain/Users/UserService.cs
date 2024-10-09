using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Users;

namespace DDDSample1.Users
{
    public class UserService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;


        public UserService(IUnitOfWork unitOfWork, IUserRepository
        userRepository) { this._unitOfWork = unitOfWork; this._userRepository = userRepository; }


        public async Task<List<UserDTO>> GetAllAsync()
        {

            var list = await this._userRepository.GetAllAsync();
            List<UserDTO> listDto = list.ConvertAll<UserDTO>(user => new UserDTO { Id = user.Id.AsGuid(), Role = user.Role, Username = user.Username, Email = user.Email });
            return listDto;
        }


        public async Task<UserDTO> GetByIdAsync(UserId id)
        {
            var user = await this._userRepository.GetByIdAsync(id);
            if (user == null) return null;
            return new UserDTO { Id = user.Id.AsGuid(), Role = user.Role, Username = user.Username, Email = user.Email };
        }


        public async Task<UserDTO> AddAsync(CreatingUserDto dto)
        {
            var user = new User(dto.Username, dto.Role, dto.Email);
            await this._userRepository.AddAsync(user);
            await this._unitOfWork.CommitAsync();
            return new UserDTO { Id = user.Id.AsGuid(), Role = user.Role, Username = user.Username, Email = user.Email };
        }

        public async Task<UserDTO> UpdateAsync(UserDTO dto) {

            var user = await this._userRepository.GetByIdAsync(new UserId(dto.Id));

            if (user == null)   return null;

            // change all fields
            user.ChangeRole(dto.Role);
            user.ChangeUsername(dto.Username);
            user.ChangeEmail(dto.Email);

            await this._unitOfWork.CommitAsync();

            return new UserDTO { Id = user.Id.AsGuid(), Role = user.Role, Username = user.Username, Email = user.Email };
        }

     

        public async Task<UserDTO> DeleteAsync(UserId id) {

            var user = await this._userRepository.GetByIdAsync(id);

            if (user == null) return null;

            if (user.Active)
                throw new BusinessRuleValidationException("It is not possible to delete an active user");

            this._userRepository.Remove(user);
            await this._unitOfWork.CommitAsync();

            return new UserDTO { Id = user.Id.AsGuid(), Role = user.Role, Username = user.Username, Email = user.Email };
        }

    }
}
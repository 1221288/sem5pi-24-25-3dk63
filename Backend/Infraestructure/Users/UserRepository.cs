using System;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain;
using DDDSample1.Domain.Users;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

using DDDSample1.Users;

namespace DDDSample1.Infrastructure.Users
{
    public class UserRepository : BaseRepository<User, UserId>, IUserRepository
    {
        private readonly DDDSample1DbContext _context;

        public UserRepository(DDDSample1DbContext context) : base(context.Users)
        {
            _context = context;
        }

        public async Task<int> GetNextSequentialNumberAsync()
        {
            var lastUser = await _context.Users
                .OrderByDescending(u => u.SequentialNumber)
                .FirstOrDefaultAsync();

            return lastUser != null ? lastUser.SequentialNumber + 1 : 1;
        }

        public async Task<User> FindByEmailAsync(Email email)
        {
            return await _context.Users .FirstOrDefaultAsync(u => u.Email.Equals(email));
        }
    }
}

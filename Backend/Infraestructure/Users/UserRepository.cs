using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain;
using DDDSample1.Domain.Users;
using DDDSample1.Users;
using DDDSample1.Infrastructure.Shared;
using DDDSample1.Infrastructure;

namespace DDDSample1.Infraestructure.Users
{
    public class UserRepository : BaseRepository<User, UserId>, IUserRepository
    {

        public UserRepository(DDDSample1DbContext context) : base(context.Users)  { }
    }
}
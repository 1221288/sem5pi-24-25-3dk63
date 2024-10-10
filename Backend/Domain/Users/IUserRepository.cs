using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Users;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain;

namespace DDDSample1.Users
{
    public interface IUserRepository : IRepository<User,UserId>
    {
    }
}
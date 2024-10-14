using System;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain;
using DDDSample1.Domain.OperationsType;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

using DDDSample1.OperationsType;
using Backend.Domain.Users.ValueObjects;

namespace DDDSample1.Infrastructure.OperationsType
{
    public class OperationTypeRepository : BaseRepository<OperationType, OperationTypeId>, IOperationTypeRepository
    {
        private readonly DDDSample1DbContext _context;

        public OperationTypeRepository(DDDSample1DbContext context) : base(context.OperationsTypes)
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

        public async Task<OperationType> FindByNameAsync(Name name)
        {
            return await _context.OperationsTypes.FirstOrDefaultAsync(u => u.Name.Equals(name));
        }
    }
}

using DDDSample1.Domain;
using DDDSample1.Domain.OperationsType;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

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

        public async Task<OperationType> GetByNameAsync(Name name)
{
    return await _context.OperationsTypes
        .FirstOrDefaultAsync(o => o.Name.FirstName == name.FirstName && o.Name.LastName == name.LastName);
}

    }
}

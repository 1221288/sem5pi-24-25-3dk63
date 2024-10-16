using DDDSample1.Domain;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

using DDDSample1.Domain.OperationRequests;

namespace DDDSample1.Infrastructure.OperationRequests
{
    public class OperationRequestRepository : BaseRepository<OperationRequest, OperationRequestId>, IOperationRequestRepository
    {
        private readonly DDDSample1DbContext _context;

        public OperationRequestRepository(DDDSample1DbContext context) : base(context.OperationRequests)
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

        public async Task<List<OperationRequest>> GetByPriorityAsync(Priority priority) 
        {
            return await _context.OperationRequests
                .Where(x => x.priority == priority)
                .ToListAsync();



        }
    }
}

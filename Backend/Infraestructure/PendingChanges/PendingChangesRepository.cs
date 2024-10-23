using Microsoft.EntityFrameworkCore;
using DDDSample1.Domain.Users;
using DDDSample1.Domain.PendingChange;

namespace DDDSample1.Infrastructure.PendingChange
{
    public class PendingChangesRepository : IPendingChangesRepository
    {
        private readonly DDDSample1DbContext _context;

        public PendingChangesRepository(DDDSample1DbContext context)
        {
            _context = context;
        }

        public async Task<PendingChanges?> GetPendingChangesByUserIdAsync(UserId userId)
        {
            return await _context.PendingChanges
                .FirstOrDefaultAsync(pc => pc.UserId == userId);
        }

        public async Task AddPendingChangesAsync(PendingChanges pendingChanges)
        {
            await _context.PendingChanges.AddAsync(pendingChanges);
            await _context.SaveChangesAsync();
        }
        
        public async Task RemovePendingChangesAsync(UserId userId)
        {
            var pendingChanges = await GetPendingChangesByUserIdAsync(userId);
            if (pendingChanges != null)
            {
                _context.PendingChanges.Remove(pendingChanges);
                await _context.SaveChangesAsync();
            }
        }
    }
}

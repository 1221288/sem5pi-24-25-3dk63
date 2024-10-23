using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.PendingChange
{
    public interface IPendingChangesRepository
    {
        Task<PendingChanges?> GetPendingChangesByUserIdAsync(UserId userId);
        Task AddPendingChangesAsync(PendingChanges pendingChanges);
        Task RemovePendingChangesAsync(UserId userId);
    }
}

using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.Staff
{
    public interface IStaffRepository : IRepository<Staff, LicenseNumber>
    {
        Task<Staff> GetByLicenseNumberAsync(LicenseNumber licenseNumber);
        Task<Staff> GetByUserIdAsync(UserId userId);
        Task<List<Staff>> SearchStaffAsync(string? name, string? email, string? specialization, int page, int pageSize);
        IQueryable<Staff> GetQueryable();
        Task<List<Staff>> GetAllAsync();
        Task UpdateStaffAsync(Staff staff);
    }
}

using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.Staff
{
    public interface IStaffRepository : IRepository<Staff, LicenseNumber>
    {
        Task<Staff> GetByLicenseNumberAsync(LicenseNumber licenseNumber);

        Task<Staff> GetByUserIdAsync(UserId userId);

    }
}

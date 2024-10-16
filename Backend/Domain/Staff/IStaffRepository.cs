using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Staff
{
    public interface IStaffRepository : IRepository<Staff, LicenseNumber>
    {
        
        Task<LicenseNumber> GetLastIssuedLicenseNumberAsync();

        Task<Staff> GetByLicenseNumberAsync(LicenseNumber licenseNumber);

    }
}

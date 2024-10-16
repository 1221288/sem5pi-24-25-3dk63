using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Staff;

namespace DDDSample1.Domain.Specialization
{
    public interface ISpecializationRepository : IRepository<Specialization, LicenseNumber>
    {
        Task<int> GetNextSequentialNumberAsync();
        Task<Specialization> FindByLicenseNumberAsync(LicenseNumber licenseNumber);

    }
}

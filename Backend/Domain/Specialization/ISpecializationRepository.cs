using Backend.Domain.Specialization.ValueObjects;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Specialization
{
    public interface ISpecializationRepository : IRepository<Specialization, SpecializationId>
    {
        Task<Specialization> FindByIdAsync(SpecializationId specializationId);
        Task<int> GetNextSequentialNumberAsync();
        Task<Specialization> GetByDescriptionAsync(Description description);
    }
}

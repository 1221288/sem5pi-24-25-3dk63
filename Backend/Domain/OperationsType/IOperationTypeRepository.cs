using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Users;


namespace DDDSample1.Domain.OperationsType
{
    public interface IOperationTypeRepository : IRepository<OperationType, OperationTypeId>
    {
        Task<OperationType> GetByNameAsync(Name name);

    }
}

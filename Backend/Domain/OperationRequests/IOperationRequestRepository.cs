using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain.Shared;


namespace DDDSample1.Domain.OperationRequests
{
    public interface IOperationRequestRepository : IRepository<OperationRequest, OperationRequestId>
    {
        Task<int> GetNextSequentialNumberAsync();
        Task<List<OperationRequest>> GetByPriorityAsync(Priority priority);

    }
}

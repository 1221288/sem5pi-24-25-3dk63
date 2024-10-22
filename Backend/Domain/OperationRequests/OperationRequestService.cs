using DDDSample1.Domain;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.OperationRequests;
using DDDSample1.Domain.Staff;
using DDDSample1.Domain.OperationsType;

namespace DDDSample1.OperationRequests
{
    public class OperationRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOperationRequestRepository _operationRequestRepository;
        private readonly IConfiguration _configuration;

        public OperationRequestService(IUnitOfWork unitOfWork, IOperationRequestRepository operationRequestRepository, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _operationRequestRepository = operationRequestRepository;
            _configuration = configuration;
        }

        // Obtém todos os requests
        public async Task<List<OperationRequestDTO>> GetAllAsync()
        {
            var list = await this._operationRequestRepository.GetAllAsync();
            List<OperationRequestDTO> listDto = list.ConvertAll(operationRequest => new OperationRequestDTO
            {
                Id = operationRequest.Id.AsGuid(),
                Deadline = operationRequest.deadline,
                Priority = operationRequest.priority,
                LicenseNumber = operationRequest.licenseNumber,
                MedicalRecordNumber = operationRequest.medicalRecordNumber,
                OperationTypeId = operationRequest.operationTypeId
            });
            return listDto;
        }

        // Obtém um request pelo ID
        public async Task<OperationRequestDTO> GetByIdAsync(OperationRequestId id)
        {
            var operationRequest = await this._operationRequestRepository.GetByIdAsync(id);
            if (operationRequest == null) return null;

            return new OperationRequestDTO
            {
                Id = operationRequest.Id.AsGuid(),
                Deadline = operationRequest.deadline,
                Priority = operationRequest.priority,
                LicenseNumber = operationRequest.licenseNumber,
                MedicalRecordNumber = operationRequest.medicalRecordNumber,
                OperationTypeId = operationRequest.operationTypeId
            };
        }

        // Adiciona um novo OperationRequest
        public async Task<OperationRequestDTO> AddAsync(CreatingOperationRequestDTO dto)
        {
            bool isDuplicate = await _operationRequestRepository.IsDuplicateRequestAsync(dto.OperationTypeId, dto.MedicalRecordNumber);

            if (isDuplicate)
            {
                throw new InvalidOperationException("There is already an open operation request for this type and patient.");
            }

            var deadline = dto.Deadline;
            var priority = dto.Priority;
            var licenseNumber = dto.LicenseNumber;
            var medicalRecordNumber = dto.MedicalRecordNumber;
            var operationTypeId = dto.OperationTypeId;

            var operationRequest = new OperationRequest(operationTypeId, deadline, priority, licenseNumber, medicalRecordNumber);

            await this._operationRequestRepository.AddAsync(operationRequest);
            await _unitOfWork.CommitAsync();

            return new OperationRequestDTO
            {
                Id = operationRequest.Id.AsGuid(),
                Deadline = operationRequest.deadline,
                Priority = operationRequest.priority,
                LicenseNumber = operationRequest.licenseNumber,
                MedicalRecordNumber = operationRequest.medicalRecordNumber,
                OperationTypeId = operationRequest.operationTypeId
            };
        }

        // Atualiza um OperationRequest existente
        public async Task<OperationRequestDTO> UpdateAsync(OperationRequestDTO dto)
        {
            var operationRequest = await this._operationRequestRepository.GetByIdAsync(new OperationRequestId(dto.Id));
            if (operationRequest == null) return null;

            operationRequest.ChangeDeadLine(dto.Deadline);
            operationRequest.ChangePriority(dto.Priority);

            await _unitOfWork.CommitAsync();

            return new OperationRequestDTO
            {
                Id = operationRequest.Id.AsGuid(),
                Deadline = operationRequest.deadline,
                Priority = operationRequest.priority,
                LicenseNumber = operationRequest.licenseNumber,
                MedicalRecordNumber = operationRequest.medicalRecordNumber,
                OperationTypeId = operationRequest.operationTypeId
            };
        }

        // Deleta um OperationRequest
        public async Task <OperationRequestDTO> DeleteAsync(OperationRequestId id) {
            var operationRequest = await this._operationRequestRepository.GetByIdAsync(id);
            if (operationRequest == null) return null;
            
            if (operationRequest.Active) throw new BusinessRuleValidationException("Não é possível excluir um pedido operação ativo.");

            this._operationRequestRepository.Remove(operationRequest);
            await this._unitOfWork.CommitAsync();

            return new OperationRequestDTO
            {
                Id = operationRequest.Id.AsGuid(),
                Deadline = operationRequest.deadline,
                Priority = operationRequest.priority,
                LicenseNumber = operationRequest.licenseNumber,
                MedicalRecordNumber = operationRequest.medicalRecordNumber,
                OperationTypeId = operationRequest.operationTypeId
            };
        }

        // Obtém um request pela prioridade
        public async Task<List<OperationRequestDTO>> GetByPriorityAsync(Priority priority)
        {
            var list = await this._operationRequestRepository.GetByPriorityAsync(priority);
            List<OperationRequestDTO> listDto = list.ConvertAll(operationRequest => new OperationRequestDTO
            {
                Id = operationRequest.Id.AsGuid(),
                Deadline = operationRequest.deadline,
                Priority = operationRequest.priority,
                LicenseNumber = operationRequest.licenseNumber,
                MedicalRecordNumber = operationRequest.medicalRecordNumber,
                OperationTypeId = operationRequest.operationTypeId
            });
            return listDto;
        }
    }

    
}

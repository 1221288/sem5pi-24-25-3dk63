using DDDSample1.Domain;
using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.OperationsType;
using Backend.Domain.Shared;
using Backend.Domain.Specialization.ValueObjects;
using DDDSample1.Domain.Specialization;

namespace DDDSample1.OperationsType
{

    public class OperationTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOperationTypeRepository _operationTypeRepository;
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IConfiguration _configuration;
       private readonly AuditService _auditService;
        public OperationTypeService(IUnitOfWork unitOfWork, IOperationTypeRepository operationTypeRepository, IConfiguration configuration, AuditService auditService, ISpecializationRepository specializationRepository)
        {
            _unitOfWork = unitOfWork;
            _operationTypeRepository = operationTypeRepository;
            _configuration = configuration;
            _auditService = auditService;
            _specializationRepository = specializationRepository;
        }

         // Obtém todos os tipos de operações
        public async Task<List<OperationTypeDTO>> GetAllAsync()
        {
            var list = await this._operationTypeRepository.GetAllAsync();
            List<OperationTypeDTO> listDto = list.ConvertAll(operationType => new OperationTypeDTO
            {
                Id = operationType.Id.AsGuid(),
                Name = operationType.Name,
                Duration = operationType.Duration,
                RequiredStaff = operationType.RequiredStaff,
                SpecializationId = operationType.SpecializationId
            });
            return listDto;
        }

        // Obtém uma operation pelo ID

        public async Task<OperationTypeDTO> GetByIdAsync(OperationTypeId id)
        {
            try {
                var operationType = await this._operationTypeRepository.GetByIdAsync(id);
                if (operationType == null) return null;

                return new OperationTypeDTO
                {
                    Id = operationType.Id.AsGuid(),
                    Name = operationType.Name,
                    Duration = operationType.Duration,
                    RequiredStaff = operationType.RequiredStaff,
                    SpecializationId = operationType.SpecializationId,
                    Active = operationType.Active
                };

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<OperationTypeDTO> AddAsync(CreatingOperationTypeDTO dto, string adminEmail)
        {

            var name = new OperationName(dto.Name);

            var operation =  await this._operationTypeRepository.GetByNameAsync(dto.Name);

            if (operation != null)
            {
                throw new BusinessRuleValidationException("Operation type já existe no sistema, por favor tente novamente com outro nome.");
            }

            var duration = new Duration(dto.Preparation, dto.Surgery, dto.Cleaning);

            var requiredStaff = new RequiredStaff(dto.RequiredStaff);

            var specialization = await this._specializationRepository.GetByDescriptionAsync(new Description(dto.speciality));

            if (specialization == null) throw new BusinessRuleValidationException("A especialização não existe.");

            var operationType = new OperationType(name, duration, requiredStaff, specialization.Id);

            _auditService.LogCreateOperationType(operationType, adminEmail);
            await this._operationTypeRepository.AddAsync(operationType);
            await _unitOfWork.CommitAsync();

            return new OperationTypeDTO
            {
                Id = operationType.Id.AsGuid(),
                Name = operationType.Name,
                Duration = operationType.Duration,
                RequiredStaff = operationType.RequiredStaff,
                SpecializationId = operationType.SpecializationId,
                Active = operationType.Active
            };
        }

        public async Task <OperationTypeDTO> DeleteAsync(OperationTypeId id) {
            var operationType = await this._operationTypeRepository.GetByIdAsync(id);
            if (operationType == null) return null;
            
            if (operationType.Active) throw new BusinessRuleValidationException("Não é possível excluir um tipo de operação ativo.");

            this._operationTypeRepository.Remove(operationType);
            await this._unitOfWork.CommitAsync();

            return new OperationTypeDTO
            {
                Id = operationType.Id.AsGuid(),
                Name = operationType.Name,
                Duration = operationType.Duration,
                RequiredStaff = operationType.RequiredStaff,
                SpecializationId = operationType.SpecializationId
            };
        }

        public async Task<OperationTypeDTO> UpdateAsync(OperationTypeDTO dto)
        {
            var operationType = await this._operationTypeRepository.GetByIdAsync(new OperationTypeId(dto.Id));
            if (operationType == null) return null;

            operationType.ChangeName(dto.Name);
            operationType.ChangeDuration(dto.Duration);
            operationType.ChangeRequiredStaff(dto.RequiredStaff);

            await this._unitOfWork.CommitAsync();

            return new OperationTypeDTO
            {
                Id = operationType.Id.AsGuid(),
                Name = operationType.Name,
                Duration = operationType.Duration,
                RequiredStaff = operationType.RequiredStaff,
                SpecializationId = operationType.SpecializationId
            };

        }

         // Obtém uma operation pelo Nome
        public async Task<OperationTypeDTO> GetByNameAsync(String name)
        {
            var operationType = await this._operationTypeRepository.GetByNameAsync(name);
            if (operationType == null) return null;

            return new OperationTypeDTO
            {
                Id = operationType.Id.AsGuid(),
                Name = operationType.Name,
                Duration = operationType.Duration,
                RequiredStaff = operationType.RequiredStaff,
                SpecializationId = operationType.SpecializationId
            };

    }

    public async Task<OperationTypeDTO> DeactivateAsync (OperationTypeId id, string adminEmail) {
        var operationType = await this._operationTypeRepository.GetByIdAsync(id);
        if (operationType == null) return null;

        if (!operationType.Active) throw new BusinessRuleValidationException("O tipo de operação já se encontra inativo.");

        _auditService.LogDeactivateOperationType(operationType, adminEmail);

        operationType.Deactivate();

        await this._unitOfWork.CommitAsync();

        return new OperationTypeDTO
        {
            Id = operationType.Id.AsGuid(),
            Name = operationType.Name,
            Duration = operationType.Duration,
            RequiredStaff = operationType.RequiredStaff,
            SpecializationId = operationType.SpecializationId
        };
    }
    }
}


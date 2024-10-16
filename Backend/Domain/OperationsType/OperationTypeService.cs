using DDDSample1.Domain;
using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.OperationsType;

namespace DDDSample1.OperationsType
{

    public class OperationTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOperationTypeRepository _operationTypeRepository;
        private readonly IConfiguration _configuration;

        public OperationTypeService(IUnitOfWork unitOfWork, IOperationTypeRepository operationTypeRepository, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _operationTypeRepository = operationTypeRepository;
            _configuration = configuration;
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
                RequiredStaff = operationType.RequiredStaff
            });
            return listDto;
        }

        // Obtém uma operation pelo ID

        public async Task<OperationTypeDTO> GetByIdAsync(OperationTypeId id)
        {
            var operationType = await this._operationTypeRepository.GetByIdAsync(id);
            if (operationType == null) return null;

            return new OperationTypeDTO
            {
                Id = operationType.Id.AsGuid(),
                Name = operationType.Name,
                Duration = operationType.Duration,
                RequiredStaff = operationType.RequiredStaff
            };
        }

        public async Task<OperationTypeDTO> AddAsync(CreatingOperationTypeDTO dto)
        {
            int sequentialNumber = await this._operationTypeRepository.GetNextSequentialNumberAsync();

            string domain = _configuration["DNS_DOMAIN"];
            if (string.IsNullOrEmpty(domain))
            {
                throw new BusinessRuleValidationException("DNS_DOMAIN is not defined in the configuration file");
            }

            var name =  new Name(dto.FirstName, dto.LastName);
            var duration = new Duration(dto.duration.Value);
            var requiredStaff = new RequiredStaff(dto.requiredStaff.Value);


            var operationType = new OperationType(name, duration, requiredStaff);

            await this._operationTypeRepository.AddAsync(operationType);
            await _unitOfWork.CommitAsync();

            return new OperationTypeDTO
            {
                Id = operationType.Id.AsGuid(),
                Name = operationType.Name,
                Duration = operationType.Duration,
                RequiredStaff = operationType.RequiredStaff
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
                RequiredStaff = operationType.RequiredStaff
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
                RequiredStaff = operationType.RequiredStaff
            };

        }

         // Obtém uma operation pelo Nome
        public async Task<OperationTypeDTO> GetByNameAsync(Name name)
        {
            var operationType = await this._operationTypeRepository.FindByNameAsync(name);
            if (operationType == null) return null;

            return new OperationTypeDTO
            {
                Id = operationType.Id.AsGuid(),
                Name = operationType.Name,
                Duration = operationType.Duration,
                RequiredStaff = operationType.RequiredStaff
            };

    }
    }
}


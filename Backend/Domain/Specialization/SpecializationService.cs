using Backend.Domain.Specialization.ValueObjects;
using DDDSample1.Domain;
using DDDSample1.Domain.Shared;
using AutoMapper;

namespace DDDSample1.Domain.Specialization
{
    public class SpecializationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IMapper _mapper;

        public SpecializationService(IUnitOfWork unitOfWork, ISpecializationRepository specializationRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _specializationRepository = specializationRepository;
            _mapper = mapper;
        }

        public async Task<List<SpecializationDTO>> GetAllAsync()
        {
            var specializations = await _specializationRepository.GetAllAsync();
            return _mapper.Map<List<SpecializationDTO>>(specializations);
        }

        public async Task<SpecializationDTO?> GetBySpecializationIdAsync(SpecializationId specializationId)
        {
            var specialization = await _specializationRepository.FindByIdAsync(specializationId);
            return specialization == null ? null : _mapper.Map<SpecializationDTO>(specialization);
        }

        public async Task<SpecializationDTO> AddAsync(CreatingSpecializationDTO dto)
        {
            int sequentialNumber = await _specializationRepository.GetNextSequentialNumberAsync();
            var specializationId = new SpecializationId(Guid.NewGuid());
            var specialization = new Specialization(
                specializationId,
                new Description(dto.Description),
                sequentialNumber
            );

            await _specializationRepository.AddAsync(specialization);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<SpecializationDTO>(specialization);
        }

        public async Task<SpecializationDTO?> UpdateAsync(SpecializationDTO dto)
        {
            var specialization = await _specializationRepository.FindByIdAsync(new SpecializationId(dto.Id));
            if (specialization == null) return null;

            specialization.UpdateDescription(new Description(dto.Description));
            await _unitOfWork.CommitAsync();

            return _mapper.Map<SpecializationDTO>(specialization);
        }

        public async Task<SpecializationDTO?> DeleteAsync(SpecializationId specializationId)
        {
            var specialization = await _specializationRepository.FindByIdAsync(specializationId);
            if (specialization == null) return null;

            _specializationRepository.Remove(specialization);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<SpecializationDTO>(specialization);
        }
    }
}

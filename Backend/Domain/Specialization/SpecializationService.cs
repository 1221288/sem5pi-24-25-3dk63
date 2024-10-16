using Backend.Domain.Specialization.ValueObjects;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Specialization;
using DDDSample1.Domain.Staff;


namespace DDDSample1.Domain.Specialization
{
    public class SpecializationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISpecializationRepository _specializationRepository;

        public SpecializationService(IUnitOfWork unitOfWork, ISpecializationRepository specializationRepository)
        {
            _unitOfWork = unitOfWork;
            _specializationRepository = specializationRepository;
        }

        public async Task<List<SpecializationDTO>> GetAllAsync()
        {
            var specializations = await _specializationRepository.GetAllAsync();
            var specializationDTOs = new List<SpecializationDTO>();

            foreach (var specialization in specializations)
            {
                specializationDTOs.Add(CreatingSpecializationDTO.FromDomain(specialization));
            }

            return specializationDTOs;
        }

        public async Task<SpecializationDTO?> GetByLicenseNumberAsync(LicenseNumber licenseNumber)
        {
            var specialization = await _specializationRepository.FindByLicenseNumberAsync(licenseNumber);
            return specialization == null ? null : CreatingSpecializationDTO.FromDomain(specialization);
        }

        public async Task<SpecializationDTO> AddAsync(CreatingSpecializationDTO dto)
        {
            var specialization = dto.ToDomain();
            await _specializationRepository.AddAsync(specialization);
            await _unitOfWork.CommitAsync();

            return CreatingSpecializationDTO.FromDomain(specialization);
        }

        public async Task<SpecializationDTO> UpdateAsync(SpecializationDTO dto)
        {
            var specialization = await _specializationRepository.FindByLicenseNumberAsync(dto.LicenseNumber);
            if (specialization == null) return null;

            specialization.UpdateDescription(new Description(dto.Description));
            await _unitOfWork.CommitAsync();

            return CreatingSpecializationDTO.FromDomain(specialization);
        }

        public async Task<SpecializationDTO> DeleteAsync(LicenseNumber licenseNumber)
        {
            var specialization = await _specializationRepository.FindByLicenseNumberAsync(licenseNumber);
            if (specialization == null) return null;

            _specializationRepository.Remove(specialization);
            await _unitOfWork.CommitAsync();

            return CreatingSpecializationDTO.FromDomain(specialization);
        }
    }
}

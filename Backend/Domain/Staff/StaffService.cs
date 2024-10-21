using Backend.Domain.Staff.ValueObjects;
using DDDSample1.Domain.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Users;
using DDDSample1.Domain.Specialization;
using AutoMapper;

namespace DDDSample1.Domain.Staff
{
    public class StaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public StaffService(IUnitOfWork unitOfWork, IStaffRepository staffRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _staffRepository = staffRepository;
            _mapper = mapper;
        }

        public async Task<List<StaffDTO>> GetAllAsync()
        {
            var staffList = await _staffRepository.GetAllAsync();
            return _mapper.Map<List<StaffDTO>>(staffList);
        }

        public async Task<StaffDTO?> GetByLicenseNumberAsync(LicenseNumber licenseNumber)
        {
            var staff = await _staffRepository.GetByLicenseNumberAsync(licenseNumber);
            return staff == null ? null : _mapper.Map<StaffDTO>(staff);
        }

        public async Task<StaffDTO> AddAsync(CreatingStaffDTO dto)
        {
            var availabilitySlots = new AvailabilitySlots();
            
            if (dto.AvailabilitySlots != null)
            {
                foreach (var slot in dto.AvailabilitySlots)
                {
                    availabilitySlots.AddSlot(slot.Start, slot.End);
                }
            }            

            var staff = new Staff(
                new UserId(dto.UserId),
                new LicenseNumber(dto.LicenseNumber),
                new SpecializationId(dto.SpecializationId),
                availabilitySlots
            );

            await _staffRepository.AddAsync(staff);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<StaffDTO>(staff);
        }

        public async Task<StaffDTO?> UpdateAsync(StaffDTO dto)
        {
            var licenseNumber = new LicenseNumber(dto.LicenseNumber.ToString());
            var staff = await _staffRepository.GetByLicenseNumberAsync(licenseNumber);
            if (staff == null) return null;

            var updatedAvailabilitySlots = new AvailabilitySlots();
            foreach (var slot in dto.AvailabilitySlots)
            {
                updatedAvailabilitySlots.AddSlot(slot.Start, slot.End);
            }

            staff.UpdateAvailabilitySlots(updatedAvailabilitySlots);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<StaffDTO>(staff);
        }

        public async Task<StaffDTO?> DeleteAsync(LicenseNumber licenseNumber)
        {
            var staff = await _staffRepository.GetByLicenseNumberAsync(licenseNumber);
            if (staff == null) return null;

            _staffRepository.Remove(staff);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<StaffDTO>(staff);
        }

        public async Task<StaffDTO?> FindByUserIdAsync(UserId userId)
        {
            var staff = await _staffRepository.GetByUserIdAsync(userId);
            return staff == null ? null : _mapper.Map<StaffDTO>(staff);
        }
    }
}

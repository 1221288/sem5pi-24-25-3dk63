using Backend.Domain.Staff.ValueObjects;
using DDDSample1.Domain.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.Staff
{
    public class StaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _staffRepository;

        public StaffService(IUnitOfWork unitOfWork, IStaffRepository staffRepository)
        {
            _unitOfWork = unitOfWork;
            _staffRepository = staffRepository;
        }

        public async Task<List<StaffDTO>> GetAllAsync()
        {
            var staffList = await _staffRepository.GetAllAsync();
            return staffList.Select(staff => CreatingStaffDTO.CreateFromDomain(staff)).ToList();
        }

        public async Task<StaffDTO?> GetByLicenseNumberAsync(LicenseNumber licenseNumber)
        {
            var staff = await _staffRepository.GetByLicenseNumberAsync(licenseNumber);
            return staff == null ? null : CreatingStaffDTO.CreateFromDomain(staff);
        }

        public async Task<StaffDTO> AddAsync(CreatingStaffDTO dto)
        {
            var availabilitySlots = new AvailabilitySlots();
            foreach (var slot in dto.AvailabilitySlots)
            {
                availabilitySlots.AddSlot(slot.Start, slot.End);
            }

            var licenseNumber = new LicenseNumber(dto.LicenseNumber);
            var staff = new Staff(dto.UserId, licenseNumber, availabilitySlots);

            await _staffRepository.AddAsync(staff);
            await _unitOfWork.CommitAsync();

            return CreatingStaffDTO.CreateFromDomain(staff);
        }

        public async Task<StaffDTO?> UpdateAsync(StaffDTO dto)
        {
            var licenseNumber = new LicenseNumber(dto.LicenseNumber.Value);
            var staff = await _staffRepository.GetByLicenseNumberAsync(licenseNumber);
            if (staff == null) return null;

            var updatedAvailabilitySlots = new AvailabilitySlots();
            foreach (var slot in dto.AvailabilitySlots)
            {
                updatedAvailabilitySlots.AddSlot(slot.Start, slot.End);
            }

            staff.UpdateAvailabilitySlots(updatedAvailabilitySlots);
            await _unitOfWork.CommitAsync();

            return CreatingStaffDTO.CreateFromDomain(staff);
        }

        public async Task<StaffDTO?> DeleteAsync(LicenseNumber licenseNumber)
        {
            var staff = await _staffRepository.GetByLicenseNumberAsync(licenseNumber);
            if (staff == null) return null;

            _staffRepository.Remove(staff);
            await _unitOfWork.CommitAsync();

            return CreatingStaffDTO.CreateFromDomain(staff);
        }

        public async Task<StaffDTO?> FindByUserIdAsync(UserId userId)
        {
            var staff = await _staffRepository.GetByUserIdAsync(userId);
            return staff == null ? null : CreatingStaffDTO.CreateFromDomain(staff);
        }
    }
}

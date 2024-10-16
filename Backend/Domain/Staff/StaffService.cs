using Backend.Domain.Staff.ValueObjects;
using DDDSample1.Domain.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDSample1.Domain.Staff
{
    public class StaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _staffRepository;
        private readonly IConfiguration _configuration;

        public StaffService(IUnitOfWork unitOfWork, IStaffRepository staffRepository, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _staffRepository = staffRepository;
            _configuration = configuration;
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

            var licenseNumber = await GetNextLicenseNumberAsync();
            var staff = new Staff(licenseNumber, availabilitySlots);

            await _staffRepository.AddAsync(staff);
            await _unitOfWork.CommitAsync();

            return CreatingStaffDTO.CreateFromDomain(staff);
        }

        private LicenseNumber GenerateNextLicenseNumber(LicenseNumber lastIssued)
        {
            var number = int.Parse(lastIssued.Value);
            var nextNumber = number + 1;
            return new LicenseNumber(nextNumber.ToString());
        }

        private async Task<LicenseNumber> GetNextLicenseNumberAsync()
        {
            LicenseNumber? lastLicenseNumber = await _staffRepository.GetLastIssuedLicenseNumberAsync();
            return lastLicenseNumber == null ? new LicenseNumber("1") : GenerateNextLicenseNumber(lastLicenseNumber);
        }

        public async Task<StaffDTO> UpdateAsync(StaffDTO dto)
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


        public async Task<StaffDTO> DeleteAsync(LicenseNumber licenseNumber)
        {
            var staff = await _staffRepository.GetByLicenseNumberAsync(licenseNumber);
            if (staff == null) return null;

            _staffRepository.Remove(staff);
            await _unitOfWork.CommitAsync();

            return CreatingStaffDTO.CreateFromDomain(staff);
        }

        public async Task<StaffDTO> FindByLicenseNumberAsync(string licenseNumber)
        {
            var staff = await _staffRepository.GetByLicenseNumberAsync(new LicenseNumber(licenseNumber));
            return staff == null ? null : CreatingStaffDTO.CreateFromDomain(staff);
        }
    }
}

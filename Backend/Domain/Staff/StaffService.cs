using Backend.Domain.Staff.ValueObjects;
using DDDSample1.Domain.Shared;


namespace DDDSample1.Domain.Staff
{
    public class StaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _staffRepository;
        private readonly IConfiguration _configuration;
        private static int _lastIssuedLicenseNumber = 0;

        public StaffService(IUnitOfWork unitOfWork, IStaffRepository staffRepository, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _staffRepository = staffRepository;
            _configuration = configuration;
        }

        public async Task<List<StaffDTO>> GetAllAsync()
        {
            var staffList = await _staffRepository.GetAllAsync();
            var staffDtoList = staffList.Select(staff => CreatingStaffDTO.CreateFromDomain(staff)).ToList();
            return staffDtoList;
        }

        public async Task<StaffDTO?> GetByLicenseNumberAsync(LicenseNumber licenseNumber)
        {
            var staff = await _staffRepository.GetByLicenseNumberAsync(licenseNumber);
            if (staff == null) return null;

            return CreatingStaffDTO.CreateFromDomain(staff);
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

            return new StaffDTO
            {
                LicenseNumber = staff.LicenseNumber,
                AvailabilitySlots = staff.AvailabilitySlots.Slots
                    .Select(slot => new AvailabilitySlotDTO
                    {
                        Start = slot.Start,
                        End = slot.End
                    }).ToList()
            };
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
    
            if (lastLicenseNumber == null)
            {
                return new LicenseNumber("1");
            }

            var newLicenseNumber = GenerateNextLicenseNumber(lastLicenseNumber);
            return newLicenseNumber;
        }


        public async Task<StaffDTO> UpdateAsync(StaffDTO dto)
        {
            var staff = await _staffRepository.GetByLicenseNumberAsync(dto.LicenseNumber);
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
            if (staff == null) return null;

            return CreatingStaffDTO.CreateFromDomain(staff);
        }
    }
}

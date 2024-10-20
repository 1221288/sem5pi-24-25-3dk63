using Backend.Domain.Staff.ValueObjects;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Users;
using DDDSample1.Domain.Specialization;
using AutoMapper;
using DDDSample1.Users;
using Backend.Domain.Users.ValueObjects;

namespace DDDSample1.Domain.Staff
{
    public class StaffService
    {
        private readonly UserService _userService;
        private readonly IStaffRepository _staffRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StaffService(UserService userService, IStaffRepository staffRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userService = userService;
            _staffRepository = staffRepository;
            _unitOfWork = unitOfWork;
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

        public async Task<StaffDTO> CreateStaffWithUserAsync(CreatingStaffDTO staffDto)
        {
            try
            {
                var role = Enum.Parse<RoleType>(staffDto.Role);
                var creatingUserDto = new CreatingUserDto(
                    new Role(role),
                    new Email(staffDto.Email),
                    staffDto.FirstName,
                    staffDto.LastName,
                    new PhoneNumber(staffDto.PhoneNumber)
                );

                var createdUser = await _userService.AddAsync(creatingUserDto);
                
                try
                {
                    var staff = new Staff(
                        new UserId(createdUser.Id),
                        new LicenseNumber(staffDto.LicenseNumber),
                        new SpecializationId(staffDto.SpecializationId),
                        new AvailabilitySlots(staffDto.AvailabilitySlots)
                    );

                    await _staffRepository.AddAsync(staff);
                    await _unitOfWork.CommitAsync();

                    return _mapper.Map<StaffDTO>(staff);
                }
                catch (Exception ex)
                {
                    await _userService.DeleteAsync(new UserId(createdUser.Id)); 
                    throw new Exception("Failed to create staff, user creation rolled back.", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the user and staff.", ex);
            }
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

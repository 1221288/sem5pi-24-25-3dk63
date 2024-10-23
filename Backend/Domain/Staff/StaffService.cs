using Backend.Domain.Staff.ValueObjects;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Users;
using DDDSample1.Domain.Specialization;
using AutoMapper;
using DDDSample1.Users;
using Backend.Domain.Users.ValueObjects;
using Backend.Domain.Specialization.ValueObjects;
using Microsoft.EntityFrameworkCore;
using DDDSample1.Infrastructure;
using Backend.Domain.Shared;

namespace DDDSample1.Domain.Staff
{
    public class StaffService
    {
        private readonly UserService _userService;
        private readonly IStaffRepository _staffRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

private readonly AuditService _auditService;
        private readonly DDDSample1DbContext _context;

        public StaffService(UserService userService, IStaffRepository staffRepository, IUserRepository userRepository, ISpecializationRepository specializationRepository, IUnitOfWork unitOfWork, IMapper mapper, AuditService auditService, DDDSample1DbContext context)
        {
            _userService = userService;
            _staffRepository = staffRepository;
            _userRepository = userRepository;
            _specializationRepository = specializationRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditService = auditService;
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
            var createdUserId = Guid.Empty;

            var role = Enum.Parse<RoleType>(staffDto.Role);
            var creatingUserDto = new CreatingUserDto(
                new Role(role),
                new Email(staffDto.Email),
                staffDto.FirstName,
                staffDto.LastName,
                new PhoneNumber(staffDto.PhoneNumber)
            );

            try
            {
                var createdUser = await _userService.AddAsync(creatingUserDto);
                createdUserId = createdUser.Id;

                var specialization = await _specializationRepository.GetByDescriptionAsync(new Description(staffDto.SpecializationDescription));
                if (specialization == null)
                    throw new ArgumentException($"Specialization '{staffDto.SpecializationDescription}' not found.");

                var staff = new Staff(
                    new UserId(createdUser.Id),
                    new LicenseNumber(staffDto.LicenseNumber),
                    specialization.Id,
                    new AvailabilitySlots(staffDto.AvailabilitySlots ?? new List<AvailabilitySlot>())
                );

                await _staffRepository.AddAsync(staff);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<StaffDTO>(staff);
            }
            catch (Exception ex)
            {
                if (createdUserId != Guid.Empty)
                {
                    await _userService.DeleteFailureAsync(new UserId(createdUserId));
                }

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

        public async Task<List<StaffDTO>> SearchStaffAsync(string? name = null, string? email = null, string? specializationDescription = null)
        {
            var query = from staff in _staffRepository.GetQueryable()
                        join user in _userRepository.GetQueryable() on staff.UserId equals user.Id
                        select new { staff, user };

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(s => s.user.Name.FirstName.Contains(name) || s.user.Name.LastName.Contains(name));
            }

            var results = await query.ToListAsync();

            if (!string.IsNullOrEmpty(email))
            {
                results = results.Where(s => s.user.Email.Value == email).ToList();
            }

            if (!string.IsNullOrEmpty(specializationDescription))
            {
                var specialization = await _specializationRepository.GetByDescriptionAsync(new Description(specializationDescription));

                if (specialization != null)
                {
                    results = results.Where(s => s.staff.SpecializationId == specialization.Id).ToList();
                }
            }

            var paginatedStaff = results.Select(s => s.staff).ToList();

            return _mapper.Map<List<StaffDTO>>(paginatedStaff);
        }


        public async Task<StaffDTO?> DeactivateAsync(LicenseNumber licenseNumber, string adminEmail)
        {
            var staff = await _staffRepository.GetByLicenseNumberAsync(licenseNumber);
            if (staff == null) return null;

            _auditService.LogDeactivateStaff(staff, adminEmail);

            staff.Deactivate();
            await _unitOfWork.CommitAsync();

            return _mapper.Map<StaffDTO>(staff);
        }
    }
}

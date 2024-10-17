using DDDSample1.Domain.Staff;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using DDDSample1.Domain.Users;

namespace DDDSample1.Infrastructure.Staffs
{
    public class StaffRepository : BaseRepository<Staff, LicenseNumber>, IStaffRepository
    {
        private readonly DDDSample1DbContext _context;

        public StaffRepository(DDDSample1DbContext context) : base(context.Staffs)
        {
            _context = context;
        }

        public async Task<Staff> GetByLicenseNumberAsync(LicenseNumber licenseNumber)
        {
            return await _context.Staffs
                .FirstOrDefaultAsync(s => s.Id.Equals(licenseNumber));
        }

        public async Task<Staff> GetByUserIdAsync(UserId userId)
        {
            return await _context.Staffs
                .FirstOrDefaultAsync(s => s.UserId.Equals(userId));
        }
    }
}

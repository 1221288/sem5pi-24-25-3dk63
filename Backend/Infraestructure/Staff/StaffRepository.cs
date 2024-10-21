using DDDSample1.Domain.Staff;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using DDDSample1.Domain.Users;
using DDDSample1.Domain;

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

        public async Task<List<Staff>> SearchStaffAsync(string? name, string? email, string? specialization, int page, int pageSize)
        {
            var query = from staff in _context.Set<Staff>()
                        join user in _context.Set<User>() on staff.UserId equals user.Id
                        select new { staff, user };

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(s => s.user.Name.FirstName.Contains(name) || s.user.Name.LastName.Contains(name));
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(s => s.user.Email.Value.Contains(email));
            }

            if (!string.IsNullOrEmpty(specialization))
            {
                query = query.Where(s => s.staff.SpecializationId.ToString().Contains(specialization));
            }

            return await query.Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .Select(s => s.staff)
                              .ToListAsync();
        }

        public IQueryable<Staff> GetQueryable()
        {
            return _context.Staffs.AsQueryable();
        }

        public async Task<List<Staff>> GetAllAsync()
        {
            return await _context.Staffs.ToListAsync();
        }
    }
}

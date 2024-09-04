using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TestForHH.ORM;

namespace TestForHH.Services
{
    public class DoctorService
    {
        private readonly HospitalContext _context;

        public DoctorService(HospitalContext context)
        {
            _context = context;
        }

        public async Task AddDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDoctorAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Doctor>> GetDoctorsAsync(int startIndex, int count, string sortBy, bool ascending)
        {
            var query = _context.Doctors.AsQueryable();

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                var propertyInfo = typeof(Doctor).GetProperty(sortBy, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    query = ascending ? query.OrderBy(e => EF.Property<object>(e, propertyInfo.Name))
                                      : query.OrderByDescending(e => EF.Property<object>(e, propertyInfo.Name));
                }
            }

            query = query.Skip(startIndex).Take(count).Include(d => d.Cabinet).
                                                       Include(d => d.Specialization).
                                                       Include(d => d.Area);

            return await query.ToListAsync();
        }

        public async Task<Doctor> GetDoctorForUpdateAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) return null;

            return doctor;
        }
    }
}

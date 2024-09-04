using Microsoft.EntityFrameworkCore;
using TestForHH.ORM;

namespace TestForHH.Services
{
    public class PatientService
    {
        private readonly HospitalContext _context;

        public PatientService(HospitalContext context)
        {
            _context = context;
        }

        public async Task AddPatientAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            //_context.Entry(patient).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Patient>> GetPatientsAsync(int startIndex, int count, string sortBy, bool ascending = true)
        {
            var query = _context.Patients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                var propertyInfo = typeof(Patient).GetProperty(sortBy, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    query = ascending ? query.OrderBy(e => EF.Property<object>(e, propertyInfo.Name))
                                      : query.OrderByDescending(e => EF.Property<object>(e, propertyInfo.Name));
                }
            }

            query = query.Skip(startIndex).Take(count).Include(p => p.Area);

            return await query.ToListAsync();
        }

        public async Task<Patient> GetPatientForUpdateAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return null;

            return patient;
        }
    }
}

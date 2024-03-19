using Microsoft.EntityFrameworkCore;
using VideoService.Domain;
using VideoService.Domain.Exceptions;
using VideoService.Domain.Interfaces;
using VideoService.Persistence;

namespace VideoService.Persistence.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly VideoContext _context;

        public DoctorRepository(VideoContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Doctor doctor, CancellationToken cancellationToken = default)
        {
            await _context.Doctors.AddAsync(doctor, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Doctor> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.Id == id);
            if (doctor == null)
            {
                throw new DoctorNotFoundException(id);
            }
            return doctor;
        }

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.Id == id);
            if (doctor == null)
            {
                throw new DoctorNotFoundException(id);
            }
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

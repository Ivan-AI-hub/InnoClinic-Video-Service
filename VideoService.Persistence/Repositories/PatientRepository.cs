using Microsoft.EntityFrameworkCore;
using VideoService.Domain;
using VideoService.Domain.Exceptions;
using VideoService.Domain.Interfaces;
using VideoService.Persistence;

namespace VideoService.Persistence.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly VideoContext _context;

        public PatientRepository(VideoContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Patient patient, CancellationToken cancellationToken = default)
        {
            await _context.Patients.AddAsync(patient, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Patient> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(x => x.Id == id);
            if (patient == null)
            {
                throw new PatientNotFoundException(id);
            }
            return patient;
        }

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(x => x.Id == id);
            if (patient == null)
            {
                throw new PatientNotFoundException(id);
            }
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using VideoService.Domain;
using VideoService.Domain.Exceptions;
using VideoService.Domain.Interfaces;

namespace VideoService.Persistence.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly VideoContext _context;

        public AppointmentRepository(VideoContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Appointment appointment, CancellationToken cancellationToken = default)
        {
            await _context.Appointments.AddAsync(appointment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Appointment> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(x => x.Id == id);
            if (appointment == null)
            {
                throw new AppointmentNotFoundException(id);
            }
            return appointment;
        }

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(x => x.Id == id);
            if (appointment == null)
            {
                throw new AppointmentNotFoundException(id);
            }
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

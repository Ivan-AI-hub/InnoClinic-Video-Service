using VideoService.Application.Abstractions.Models;

namespace VideoService.Application.Abstractions
{
    public interface IAppointmentService
    {
        public Task AddAsync(AppointmentDTO appointment, CancellationToken cancellationToken = default);
        public Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<AppointmentDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}

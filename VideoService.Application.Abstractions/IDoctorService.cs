using VideoService.Application.Abstractions.Models;

namespace VideoService.Application.Abstractions
{
    public interface IDoctorService
    {
        public Task AddAsync(DoctorDTO patient, CancellationToken cancellationToken = default);
        public Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<DoctorDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}

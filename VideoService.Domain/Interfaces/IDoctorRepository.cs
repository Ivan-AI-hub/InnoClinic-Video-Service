namespace VideoService.Domain.Interfaces
{
    public interface IDoctorRepository
    {
        public Task AddAsync(Doctor doctor, CancellationToken cancellationToken = default);
        public Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<Doctor> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}

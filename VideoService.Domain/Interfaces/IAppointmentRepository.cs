namespace VideoService.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        public Task AddAsync(Appointment appointment, CancellationToken cancellationToken = default);
        public Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<Appointment> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}

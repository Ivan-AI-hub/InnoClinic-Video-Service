using AutoMapper;
using VideoService.Application.Abstractions;
using VideoService.Application.Abstractions.Models;
using VideoService.Domain;
using VideoService.Domain.Interfaces;

namespace VideoService.Application
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(AppointmentDTO appointment, CancellationToken cancellationToken = default)
        {
            var dataAppointment = _mapper.Map<Appointment>(appointment);
            await _appointmentRepository.AddAsync(dataAppointment, cancellationToken);
        }

        public async Task<AppointmentDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<AppointmentDTO>(appointment);
        }

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _appointmentRepository.RemoveAsync(id, cancellationToken);
        }
    }
}

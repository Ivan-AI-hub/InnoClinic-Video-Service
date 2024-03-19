using AutoMapper;
using VideoService.Application.Abstractions;
using VideoService.Application.Abstractions.Models;
using VideoService.Domain;
using VideoService.Domain.Interfaces;

namespace VideoService.Application
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(PatientDTO patient, CancellationToken cancellationToken = default)
        {
            var dataPatient = _mapper.Map<Patient>(patient);
            await _patientRepository.AddAsync(dataPatient, cancellationToken);
        }

        public async Task<PatientDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var patient = await _patientRepository.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<PatientDTO>(patient);
        }

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _patientRepository.RemoveAsync(id, cancellationToken);
        }
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoService.Application.Abstractions.Models;
using VideoService.Application.Abstractions;
using VideoService.Domain.Interfaces;
using VideoService.Domain;

namespace VideoService.Application
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public DoctorService(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(DoctorDTO doctor, CancellationToken cancellationToken = default)
        {
            var dataDoctor = _mapper.Map<Doctor>(doctor);
            await _doctorRepository.AddAsync(dataDoctor, cancellationToken);
        }

        public async Task<DoctorDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<DoctorDTO>(doctor);
        }

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _doctorRepository.RemoveAsync(id, cancellationToken);
        }
    }
}

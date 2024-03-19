using MassTransit;
using SharedEvents.Models;
using VideoService.Application.Abstractions;
using VideoService.Application.Abstractions.Models;

namespace VideoSevice.Presentation.Consumers
{
    public class PatientCreatedConsumer : IConsumer<PatientCreated>
    {
        private readonly IPatientService _patientService;

        public PatientCreatedConsumer(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task Consume(ConsumeContext<PatientCreated> context)
        {
            var message = context.Message;
            await _patientService.AddAsync(new PatientDTO(message.Id, message.Email));
        }
    }
}

using MassTransit;
using SharedEvents.Models;
using VideoService.Application.Abstractions;

namespace VideoSevice.Presentation.Consumers
{
    public class PatientDeletedConsumer : IConsumer<PatientDeleted>
    {
        private readonly IPatientService _patientService;

        public PatientDeletedConsumer(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task Consume(ConsumeContext<PatientDeleted> context)
        {
            var patient = context.Message;
            await _patientService.RemoveAsync(patient.Id, context.CancellationToken);
        }
    }
}

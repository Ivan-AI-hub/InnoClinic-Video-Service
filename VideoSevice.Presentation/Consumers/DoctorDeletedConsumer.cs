using MassTransit;
using SharedEvents.Models;
using VideoService.Application.Abstractions;

namespace VideoSevice.Presentation.Consumers
{
    public class DoctorDeletedConsumer : IConsumer<DoctorDeleted>
    {
        private readonly IDoctorService _doctorService;

        public DoctorDeletedConsumer(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public async Task Consume(ConsumeContext<DoctorDeleted> context)
        {
            var doctor = context.Message;
            await _doctorService.RemoveAsync(doctor.Id, context.CancellationToken);
        }
    }
}

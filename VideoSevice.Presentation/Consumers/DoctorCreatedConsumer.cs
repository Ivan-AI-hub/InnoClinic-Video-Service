using MassTransit;
using SharedEvents.Models;
using VideoService.Application.Abstractions;
using VideoService.Application.Abstractions.Models;

namespace VideoSevice.Presentation.Consumers
{
    public class DoctorCreatedConsumer : IConsumer<DoctorCreated>
    {
        private readonly IDoctorService _doctorService;

        public DoctorCreatedConsumer(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public async Task Consume(ConsumeContext<DoctorCreated> context)
        {
            var message = context.Message;
            await _doctorService.AddAsync(new DoctorDTO(message.Id, message.Email));
        }
    }
}

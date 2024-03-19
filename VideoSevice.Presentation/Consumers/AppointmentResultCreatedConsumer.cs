using MassTransit;
using SharedEvents.Models;
using VideoService.Application.Abstractions;

namespace VideoSevice.Presentation.Consumers
{
    public class AppointmentResultCreatedConsumer : IConsumer<AppointmentResultCreated>
    {
        private IAppointmentService _appointmentService;

        public AppointmentResultCreatedConsumer(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task Consume(ConsumeContext<AppointmentResultCreated> context)
        {
            var message = context.Message;
            //Delete appointment from Db
        }
    }
}

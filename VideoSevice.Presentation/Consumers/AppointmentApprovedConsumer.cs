using MassTransit;
using SharedEvents.Models;
using VideoService.Application.Abstractions;

namespace VideoSevice.Presentation.Consumers
{
    public class AppointmentApprovedConsumer : IConsumer<AppointmentApproved>
    {
        private IAppointmentService _appointmentService;

        public AppointmentApprovedConsumer(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task Consume(ConsumeContext<AppointmentApproved> context)
        {
            var message = context.Message;
            //Create appointment
        }
    }
}

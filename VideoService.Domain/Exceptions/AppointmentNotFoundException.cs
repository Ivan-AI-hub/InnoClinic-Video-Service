namespace VideoService.Domain.Exceptions
{
    public class AppointmentNotFoundException : NotFoundException
    {
        public AppointmentNotFoundException(Guid id)
            : base($"The appointment with the identifier {id} was not found.")
        {
        }
    }
}

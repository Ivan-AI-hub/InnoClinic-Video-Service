namespace VideoService.Domain.Exceptions
{
    public class DoctorNotFoundException : NotFoundException
    {
        public DoctorNotFoundException(Guid id)
            : base($"The doctor with the identifier {id} was not found.")
        {
        }
    }
}

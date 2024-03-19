namespace VideoService.Domain
{
    public class Doctor
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Email { get; private set; }

        private Doctor() { }
        public Doctor(string email)
        {
            Email = email;
        }
    }
}

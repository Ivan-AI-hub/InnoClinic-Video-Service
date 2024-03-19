namespace VideoService.Application.Abstractions.Models
{
    public class AppointmentDTO
    {
        public Guid Id { get; set; }
        public DoctorDTO Doctor { get; set; }
        public PatientDTO Patient { get; set; }

        public AppointmentDTO(Guid id, DoctorDTO doctor, PatientDTO patient)
        {
            Id = id;
            Doctor = doctor;
            Patient = patient;
        }
    }
}

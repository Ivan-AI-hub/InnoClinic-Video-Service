using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VideoService.Domain;

namespace VideoService.Persistence.EntityConfigurations
{
    public class AppointmentConfigurator : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Patient).WithMany().HasForeignKey(x => x.PatientId);
            builder.HasOne(x => x.Doctor).WithMany().HasForeignKey(x => x.DoctorId);
        }
    }
}

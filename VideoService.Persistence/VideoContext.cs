using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using VideoService.Domain;
using VideoService.Persistence.EntityConfigurations;

namespace VideoService.Persistence
{
    public class VideoContext : DbContext
    {
        public VideoContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PatientConfigurator());
            modelBuilder.ApplyConfiguration(new DoctorConfigurator());
            modelBuilder.ApplyConfiguration(new AppointmentConfigurator());
        }
    }
}

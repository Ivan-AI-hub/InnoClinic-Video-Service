using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoService.Domain
{
    public class Appointment
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid DoctorId { get; private set; }
        public Guid PatientId { get; private set; }

        public Patient? Patient { get; private set; }
        public Doctor? Doctor { get; private set; }

        private Appointment() { }

        public Appointment(Guid doctorId, Guid patientId)
        {
            DoctorId = doctorId;
            PatientId = patientId;
        }
    }
}

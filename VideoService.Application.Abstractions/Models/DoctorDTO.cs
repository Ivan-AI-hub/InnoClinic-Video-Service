using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoService.Application.Abstractions.Models
{
    public class DoctorDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public DoctorDTO(Guid id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}

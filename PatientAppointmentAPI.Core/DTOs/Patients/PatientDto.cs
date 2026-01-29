using PatientAppointmentAPI.Core.DTOs.Appointments;
using PatientAppointmentAPI.Domain.Entities;

namespace PatientAppointmentAPI.Core.DTOs.Patients
{
    public class PatientDto : PatientFieldsDto
    {
        public PatientDto() { }

        public PatientDto(Patient patient)
        {
            Id = patient.Id;
            NhsNumber = patient.NhsNumber;
            Title = patient.Title;
            FirstName = patient.FirstName;
            LastName = patient.LastName;
            Postcode = patient.Postcode;
            DateOfBirth = patient.DateOfBirth;
            Appointments = patient.Appointments.Select(a => new AppointmentDto(a)).ToList();
        }

        public Guid Id { get; set; }

        public string FullName { 
            get
            {
                return $"{FirstName} {LastName}";
            } 
        }

        public List<AppointmentDto> Appointments { get; set; } = new();
    }
}
using PatientAppointmentAPI.Domain.Enums;

namespace PatientAppointmentAPI.Core.DTOs.Patients
{
    // Further demographic data would be added later for analysis such as gender, sex. Middle name also. A separate entity would contain addresses and contact details.
    public abstract class PatientFieldsDto
    {
        public string NhsNumber { get; set; } = default!;
        public Title Title { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Postcode { get; set; } = default!;
        public DateTimeOffset DateOfBirth { get; set; }
    }
}

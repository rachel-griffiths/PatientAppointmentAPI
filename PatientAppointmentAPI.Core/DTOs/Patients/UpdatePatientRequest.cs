namespace PatientAppointmentAPI.Core.DTOs.Patients
{
    public sealed class UpdatePatientRequest : PatientFieldsDto
    {
        public Guid Id { get; set; }
    }
}

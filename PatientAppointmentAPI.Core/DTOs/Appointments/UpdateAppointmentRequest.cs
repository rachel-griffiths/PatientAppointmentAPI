namespace PatientAppointmentAPI.Core.DTOs.Appointments
{
    public class UpdateAppointmentRequest : AppointmentFieldsDto
    {
        public Guid Id { get; set; }
    }
}

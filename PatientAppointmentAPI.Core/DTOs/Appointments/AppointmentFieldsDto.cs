using PatientAppointmentAPI.Domain.Enums;

namespace PatientAppointmentAPI.Core.DTOs.Appointments
{
    public abstract class AppointmentFieldsDto
    {
        public Guid PatientId { get; set; }

        public DateTimeOffset StartTime { get; set; }
        public int DurationMinutes { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

        public Guid ClinicianId { get; set; }
        public Department Department { get; set; }

        public DateTimeOffset EndTime { get; set; }
    }
}

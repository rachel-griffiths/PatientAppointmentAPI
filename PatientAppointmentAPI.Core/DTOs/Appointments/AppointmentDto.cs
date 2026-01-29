using PatientAppointmentAPI.Domain.Entities;
using PatientAppointmentAPI.Domain.Enums;

namespace PatientAppointmentAPI.Core.DTOs.Appointments
{
    public class AppointmentDto
    {
        public AppointmentDto() { }

        public AppointmentDto(Appointment appointment)
        {
            Id = appointment.Id;
            PatientId = appointment.PatientId;
            StartTime = appointment.StartTime;
            DurationMinutes = appointment.DurationMinutes;
            Status = appointment.Status;
            ClinicianId = appointment.ClinicianId;
            DurationMinutes = appointment.DurationMinutes;
        }

        public Guid Id { get; private set; }
        public Guid PatientId { get; private set; }

        public DateTimeOffset StartTime { get; private set; }
        public int DurationMinutes { get; private set; }

        public AppointmentStatus Status { get; private set; }

        public Guid? ClinicianId { get; private set; }
        public Department Department { get; private set; }

        public DateTimeOffset EndTime => StartTime.AddMinutes(DurationMinutes);
    }
}

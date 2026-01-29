

using PatientAppointmentAPI.Domain.Enums;

namespace PatientAppointmentAPI.Domain.Entities
{
    public class Appointment
    {
        protected Appointment() { }

        public Appointment(Guid id, Guid patientId, DateTimeOffset startTime, int durationMin, Guid clinicianId, Department dept, AppointmentStatus? status)
        {
            Id = id;
            PatientId = patientId;
            StartTime = startTime;
            DurationMinutes = durationMin;
            ClinicianId = clinicianId;
            Department = dept;            
            Status = status ?? AppointmentStatus.Scheduled;
        }
        public Guid Id { get; private set; }
        public Guid PatientId { get; private set; }

        public DateTimeOffset StartTime { get; private set; }
        public int DurationMinutes { get; private set; }

        public AppointmentStatus Status { get; private set; }

        public Guid ClinicianId { get; private set; }
        public Department Department { get; private set; }

        public DateTimeOffset EndTime => StartTime.AddMinutes(DurationMinutes);

        public void Cancel()
        {
            if (Status == AppointmentStatus.Cancelled)
                return;

            if (Status == AppointmentStatus.Attended)
                throw new InvalidOperationException("Attended appointments cannot be cancelled.");

            Status = AppointmentStatus.Cancelled;
        }

        public void MarkAttended()
        {
            EnsureNotCancelled();
            Status = AppointmentStatus.Attended;
        }

        public void MarkMissedIfOverdue(DateTimeOffset now)
        {
            if (Status != AppointmentStatus.Scheduled)
                return;

            if (now > EndTime)
                Status = AppointmentStatus.Missed;
        }

        public void UpdateDetails(DateTimeOffset startTime, int durationMinutes, Guid clinicianId, Department department)
        {
            EnsureNotCancelled();

            StartTime = startTime;
            DurationMinutes = durationMinutes;
            ClinicianId = clinicianId;
            Department = department;
        }

        private void EnsureNotCancelled()
        {
            if (Status == AppointmentStatus.Cancelled)
                throw new InvalidOperationException("Cancelled appointments cannot be modified.");
        }
    }
}
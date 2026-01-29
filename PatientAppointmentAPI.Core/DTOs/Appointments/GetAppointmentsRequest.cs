using PatientAppointmentAPI.Domain.Enums;

namespace PatientAppointmentAPI.Core.DTOs.Appointments
{
    public class GetAppointmentsRequest
    {
        public Guid? PatientId { get; set; }

        public string? PatientName { get; set; }

        public AppointmentStatus? Status { get; set; }

        public Guid? ClinicianId { get; set; }

        public DateTimeOffset? FromDate { get; set; }

        public DateTimeOffset? ToDate { get; set; }

        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 25;
    }
}

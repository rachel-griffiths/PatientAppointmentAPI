using PatientAppointmentAPI.Core.DTOs.Appointments;

namespace PatientAppointmentAPI.Core.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> GetByIdAsync(Guid id, CancellationToken ct);

        Task<List<AppointmentDto>> GetByPatientIdAsync(Guid id, CancellationToken ct);

        Task<List<AppointmentDto>> SearchAsync(GetAppointmentsRequest query, CancellationToken ct);

        Task CancelAsync(Guid appointmentId, CancellationToken ct);

        Task<AppointmentDto> CreateAsync(CreateAppointmentRequest request, CancellationToken ct);

        Task<AppointmentDto> UpdateAsync(UpdateAppointmentRequest request, CancellationToken ct);

        Task MarkAsAttended(Guid id, CancellationToken ct);

        Task MarkMissedIfOverdue(Guid id, CancellationToken ct);
    }
}

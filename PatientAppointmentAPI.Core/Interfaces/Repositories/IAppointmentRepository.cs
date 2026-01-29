using PatientAppointmentAPI.Domain.Entities;

namespace PatientAppointmentAPI.Infrastructure.Repositories
{
    public interface IAppointmentRepository
    {
        Task<Appointment?> GetByIdAsync(Guid id, CancellationToken ct);

        Task<List<Appointment>> GetByPatientIdAsync(Guid patientId, CancellationToken ct);

        Task<List<Appointment>> SearchAsync(
            Func<IQueryable<Appointment>, IQueryable<Appointment>>? queryModifier,
            CancellationToken ct);

        Task AddAsync(Appointment appointment, CancellationToken ct);
        void Update(Appointment appointment);
        void Remove(Appointment appointment);
    }
}

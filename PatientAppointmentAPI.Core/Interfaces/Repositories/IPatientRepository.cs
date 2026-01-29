using PatientAppointmentAPI.Domain.Entities;

namespace PatientAppointmentAPI.Core.Interfaces.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<Patient?> GetByIdWithAppointmentsAsync(Guid id,
             CancellationToken ct);

        Task<IReadOnlyList<Patient>> ListAsync(
            Func<IQueryable<Patient>, IQueryable<Patient>>? queryModifier,
            CancellationToken ct);

        Task AddAsync(Patient patient, CancellationToken ct);
        void Update(Patient patient);
        void Remove(Patient patient);
    }
}
using Microsoft.EntityFrameworkCore;
using PatientAppointmentAPI.Core.Interfaces.Repositories;
using PatientAppointmentAPI.Domain.Entities;

namespace PatientAppointmentAPI.Infrastructure.Repositories
{
    public sealed class PatientRepository : IPatientRepository
    {
        private readonly PatientAppointmentAPIContext _db;

        public PatientRepository(PatientAppointmentAPIContext db) => _db = db;

        public async Task<Patient?> GetByIdAsync(Guid id, CancellationToken ct)
            => await _db.Patients.FirstOrDefaultAsync(p => p.Id == id, ct);

        public Task<Patient?> GetByIdWithAppointmentsAsync(Guid id, CancellationToken ct)
            => _db.Patients
                .Include(p => p.Appointments)
                .FirstOrDefaultAsync(p => p.Id == id, ct);

        public async Task<IReadOnlyList<Patient>> ListAsync(
            Func<IQueryable<Patient>, IQueryable<Patient>>? queryModifier,
            CancellationToken ct)
        {
            IQueryable<Patient> query = _db.Patients;

            if (queryModifier is not null)
            {
                query = queryModifier(query);
            }

            return await query.ToListAsync(ct);
        }

        public Task AddAsync(Patient patient, CancellationToken ct)
            => _db.Patients.AddAsync(patient, ct).AsTask();

        public void Update(Patient patient) => _db.Patients.Update(patient);

        public void Remove(Patient patient) => _db.Patients.Remove(patient);
    }
}

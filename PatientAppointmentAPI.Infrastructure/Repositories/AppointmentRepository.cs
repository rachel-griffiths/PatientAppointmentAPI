using Microsoft.EntityFrameworkCore;
using PatientAppointmentAPI.Core.Interfaces.Repositories;
using PatientAppointmentAPI.Domain.Entities;

namespace PatientAppointmentAPI.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly PatientAppointmentAPIContext _db;
        public AppointmentRepository(PatientAppointmentAPIContext db) => _db = db;
        public async Task AddAsync(Appointment appointment, CancellationToken ct)
        {
            await _db.Appointments.AddAsync(appointment, ct);
        }

        public async Task<Appointment?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _db.Appointments.FirstOrDefaultAsync(p => p.Id == id, ct);
        }

        public async Task<List<Appointment>> GetByPatientIdAsync(Guid patientId, CancellationToken ct)
        {
            return await _db.Appointments.Where(p => p.PatientId == patientId).ToListAsync(ct);
        }

        public void Remove(Appointment appointment)
        {
            _db.Appointments.Remove(appointment);
        }

        public async Task<List<Appointment>> SearchAsync(Func<IQueryable<Appointment>, IQueryable<Appointment>>? queryModifier, CancellationToken ct)
        {
            IQueryable<Appointment> query = _db.Appointments.AsNoTracking();

            if (queryModifier is not null)
                query = queryModifier(query);

            return await query.ToListAsync(ct);
        }

        public void Update(Appointment appointment)
        {
            throw new NotImplementedException();
        }
    }
}

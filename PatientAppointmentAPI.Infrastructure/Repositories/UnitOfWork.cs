using PatientAppointmentAPI.Core.Interfaces.Repositories;

namespace PatientAppointmentAPI.Infrastructure.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly PatientAppointmentAPIContext _db;
        public UnitOfWork(PatientAppointmentAPIContext db) => _db = db;

        public Task<int> SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
    }
}

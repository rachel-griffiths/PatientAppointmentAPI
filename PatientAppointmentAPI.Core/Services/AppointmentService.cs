using PatientAppointmentAPI.Core.DTOs.Appointments;
using PatientAppointmentAPI.Core.Interfaces;
using PatientAppointmentAPI.Core.Interfaces.Repositories;
using PatientAppointmentAPI.Domain.Entities;
using PatientAppointmentAPI.Infrastructure.Repositories;

public sealed class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUnitOfWork _uow;

    public AppointmentService(IAppointmentRepository appointmentRepository, IUnitOfWork uow) 
    {
        _appointmentRepository = appointmentRepository;
        _uow = uow;
    }

    public async Task<AppointmentDto> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var appt = await _appointmentRepository.GetByIdAsync(id, ct);
        if (appt is null)
        {
            throw new KeyNotFoundException($"Appointment not found: {id}");
        }
        // during MVP, we will check this when touching record
        appt.MarkMissedIfOverdue(DateTimeOffset.UtcNow);
        return new AppointmentDto(appt);
    }

    public async Task<List<AppointmentDto>> GetByPatientIdAsync(Guid patientId, CancellationToken ct)
    {
        var appointments = await _appointmentRepository.GetByPatientIdAsync(patientId, ct);
        return appointments.Select(a => new AppointmentDto(a))
        .ToList();
    }

    public async Task<List<AppointmentDto>> SearchAsync(GetAppointmentsRequest query, CancellationToken ct)
    {
        Func<IQueryable<Appointment>, IQueryable<Appointment>>? queryModifier = q => q;

        if (query.PatientId.HasValue)
            queryModifier = queryModifier => queryModifier.Where(a => a.PatientId == query.PatientId.Value);

        if (query.ClinicianId.HasValue)
            queryModifier = q => queryModifier(q).Where(a => a.ClinicianId == query.ClinicianId.Value);

        if (query.FromDate.HasValue)
            queryModifier = q => queryModifier(q).Where(a => a.StartTime >= query.FromDate.Value);

        if (query.ToDate.HasValue)
            queryModifier = q => queryModifier(q).Where(a => a.StartTime <= query.ToDate.Value);

        if (query.Status.HasValue)
            queryModifier = q => queryModifier(q).Where(a => a.Status == query.Status.Value);


        var appointments = await _appointmentRepository.SearchAsync(queryModifier, ct);
        return appointments.Select(a => new AppointmentDto(a))
        .ToList();
    }

    public async Task CancelAsync(Guid id, CancellationToken ct)
    {
        var appt = await _appointmentRepository.GetByIdAsync(id, ct);
        if (appt is null)
        {
            throw new KeyNotFoundException($"Appointment not found: {id}");
        }

        appt.Cancel();

        await _uow.SaveChangesAsync(ct);
    }

    public async Task<AppointmentDto> UpdateAsync(UpdateAppointmentRequest request, CancellationToken ct)
    {
        var appt = await _appointmentRepository.GetByIdAsync(request.Id, ct);
        if (appt is null)
        {
            throw new KeyNotFoundException($"Appointment not found: {request.Id}");
        }

        appt.UpdateDetails(
            request.StartTime,
            request.DurationMinutes,
            request.ClinicianId,
            request.Department);

        await _uow.SaveChangesAsync(ct);
        return new AppointmentDto(appt);
    }

    public async Task<AppointmentDto> CreateAsync(CreateAppointmentRequest request, CancellationToken ct)
    {
        var appt = new Appointment(Guid.NewGuid(), request.PatientId, request.StartTime,
            request.DurationMinutes, request.ClinicianId, request.Department, request.Status);
        await _appointmentRepository.AddAsync(appt, ct);
        await _uow.SaveChangesAsync(ct);
        return new AppointmentDto(appt);
    }

    public async Task MarkAsAttended(Guid id, CancellationToken ct)
    {
        var appt = await _appointmentRepository.GetByIdAsync(id, ct);
        if (appt is null)
        {
            throw new KeyNotFoundException($"Appointment not found: {id}");
        }
        appt.MarkAttended();
        await _uow.SaveChangesAsync(ct);
    }

    public async Task MarkMissedIfOverdue(Guid id, CancellationToken ct)
    {
        var appt = await _appointmentRepository.GetByIdAsync(id, ct);
        if (appt is null)
        {
            throw new KeyNotFoundException($"Appointment not found: {id}");
        }
        appt.MarkMissedIfOverdue(DateTimeOffset.UtcNow);
        await _uow.SaveChangesAsync(ct);
    }
}

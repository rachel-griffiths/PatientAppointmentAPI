using PatientAppointmentAPI.Core.DTOs.Patients;

namespace PatientAppointmentAPI.Core.Interfaces
{
    public interface IPatientService
    {
        Task<PatientDto> CreatePatientAsync(
            CreatePatientRequest request,
            CancellationToken ct);

        Task<PatientDto> UpdatePatientAsync(
            UpdatePatientRequest request,
            CancellationToken ct);

        Task<PatientDto> GetPatientByIdAsync(
            Guid id,
            CancellationToken ct);

        Task DeletePatientAsync(
            Guid id,
            CancellationToken ct);

        Task<PatientDto> GetPatientByIdwithApptsAsync(
            Guid id, 
            CancellationToken ct);
    }

}

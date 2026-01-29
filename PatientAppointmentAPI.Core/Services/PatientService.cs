using PatientAppointmentAPI.Core.DTOs.Patients;
using PatientAppointmentAPI.Core.Interfaces;
using PatientAppointmentAPI.Core.Interfaces.Repositories;
using PatientAppointmentAPI.Core.Shared;
using PatientAppointmentAPI.Domain.Entities;

namespace PatientAppointmentAPI.Core.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _uow;

        public PatientService(IPatientRepository patientRepository, IUnitOfWork uow)
        {
            _patientRepository = patientRepository;
            _uow = uow;
        }

        public async Task<PatientDto> CreatePatientAsync(CreatePatientRequest request, CancellationToken ct)
        {
            var newPatient = new Patient(
                    Guid.NewGuid(),
                    request.NhsNumber,
                    request.Title,
                    request.FirstName,
                    request.LastName,
                    PostcodeNormaliser.Normalise(request.Postcode),
                    request.DateOfBirth);
            await _patientRepository.AddAsync(newPatient, ct);
            await _uow.SaveChangesAsync(ct);
            return new PatientDto(newPatient);
        }

        public async Task<PatientDto> UpdatePatientAsync(UpdatePatientRequest request, CancellationToken ct)
        {
            var patient = await _patientRepository.GetByIdAsync(request.Id, ct);
            if (patient == null)
            {
                throw new KeyNotFoundException($"Patient not found: {request.Id}");
            }
            patient.UpdateDetails(
                request.NhsNumber,
                request.Title,
                request.FirstName,
                request.LastName,
                PostcodeNormaliser.Normalise(request.Postcode),
                request.DateOfBirth);
            _patientRepository.Update(patient);
            await _uow.SaveChangesAsync(ct);
            return new PatientDto(patient);
        }

        public async Task DeletePatientAsync(Guid id, CancellationToken ct)
        {
            var patient = await _patientRepository.GetByIdAsync(id, ct);

            if (patient is null)
            {
                throw new KeyNotFoundException($"Patient not found: {id}");
            }
                
            _patientRepository.Remove(patient);
            await _uow.SaveChangesAsync(ct);

            //we would decide whether to just allow delete if they have appointments as here (they will cascade delete)
            //or to send an error and force the user to deal with appts first
        }

        public async Task<PatientDto> GetPatientByIdAsync(Guid id, CancellationToken ct)
        {
            var patient = await _patientRepository.GetByIdAsync(id, ct);

            if (patient is null)
            {
                throw new KeyNotFoundException($"Patient not found: {id}");
            }
            return new PatientDto(patient);
        }

        public async Task<PatientDto> GetPatientByIdwithApptsAsync(Guid id, CancellationToken ct)
        {
            var patient = await _patientRepository.GetByIdWithAppointmentsAsync(id, ct);

            if (patient is null)
            {
                throw new KeyNotFoundException($"Patient not found: {id}");
            }
            return new PatientDto(patient);
        }
    }
}

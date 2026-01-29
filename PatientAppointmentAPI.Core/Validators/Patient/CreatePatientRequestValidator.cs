using FluentValidation;
using PatientAppointmentAPI.Core.DTOs.Patients;

namespace PatientAppointmentAPI.Core.Validators.Patient
{
    public sealed class CreatePatientRequestValidator
        : AbstractValidator<CreatePatientRequest>
    {
        public CreatePatientRequestValidator()
        {
            Include(new PatientFieldsValidator());
        }
    }
}

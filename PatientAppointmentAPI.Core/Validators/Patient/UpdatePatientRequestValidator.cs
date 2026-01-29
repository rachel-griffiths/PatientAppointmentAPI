using FluentValidation;
using PatientAppointmentAPI.Core.DTOs.Patients;

namespace PatientAppointmentAPI.Core.Validators.Patient
{
    public sealed class UpdatePatientRequestValidator
        : AbstractValidator<UpdatePatientRequest>
    {
        public UpdatePatientRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            Include(new PatientFieldsValidator());
        }
    }
}

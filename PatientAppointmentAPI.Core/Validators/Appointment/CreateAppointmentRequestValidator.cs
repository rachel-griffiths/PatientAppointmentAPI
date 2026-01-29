using FluentValidation;
using PatientAppointmentAPI.Core.DTOs.Appointments;

namespace PatientAppointmentAPI.Core.Validators.Appointment
{
    public class CreateAppointmentRequestValidator : AbstractValidator<CreateAppointmentRequest>
    {
        public CreateAppointmentRequestValidator()
        {
            Include(new AppointmentFieldsValidator());

            RuleFor(x => x.PatientId)
                .NotEmpty();

            RuleFor(x => x.StartTime)
                .NotEmpty();

            RuleFor(x => x.DurationMinutes)
                .NotEmpty();

            RuleFor(x => x.ClinicianId)
                .NotEmpty();

            RuleFor(x => x.Department)
                .NotEmpty();
        }
    }
}

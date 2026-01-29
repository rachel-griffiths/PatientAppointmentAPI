using FluentValidation;
using PatientAppointmentAPI.Core.DTOs.Appointments;

namespace PatientAppointmentAPI.Core.Validators.Appointment
{
    public sealed class AppointmentFieldsValidator : AbstractValidator<AppointmentFieldsDto>
    {
        public AppointmentFieldsValidator() 
        {

            RuleFor(x => x.DurationMinutes)
                .Must(x => x > 0)
                .WithMessage("Duration must be greater than 0");
        }
    }
}

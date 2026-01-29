using FluentValidation;
using PatientAppointmentAPI.Core.DTOs.Appointments;

namespace PatientAppointmentAPI.Core.Validators.Appointment
{
    public class GetAppointmentsRequestValidator : AbstractValidator<GetAppointmentsRequest>
    {
        public GetAppointmentsRequestValidator()
        {
            RuleFor(x => x)
                .Must(x => ToDateAfterFromDate(x))
                .When(x => x.FromDate != null && x.ToDate != null)
                .WithMessage("To Date must be the same or after From Date when both have values");
        }

        private bool ToDateAfterFromDate(GetAppointmentsRequest x)
        {
            return x.ToDate >= x.FromDate;
        }
    }
}

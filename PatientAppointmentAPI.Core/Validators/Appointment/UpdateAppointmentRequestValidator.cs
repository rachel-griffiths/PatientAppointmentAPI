using FluentValidation;
using PatientAppointmentAPI.Core.DTOs.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientAppointmentAPI.Core.Validators.Appointment
{
    public class UpdateAppointmentRequestValidator: AbstractValidator<UpdateAppointmentRequest>
    {
        public UpdateAppointmentRequestValidator() 
        {
            Include(new AppointmentFieldsValidator());

            RuleFor(x => x.Status)
                .NotEqual(Domain.Enums.AppointmentStatus.Cancelled)
                .WithMessage("Cancelled appointments cannot be updated");
        }
    }
}

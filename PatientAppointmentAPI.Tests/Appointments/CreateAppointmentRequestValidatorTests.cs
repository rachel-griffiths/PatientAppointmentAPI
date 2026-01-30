using FluentValidation.TestHelper;
using PatientAppointmentAPI.Core.Validators.Appointment;
using PatientAppointmentAPI.Core.Validators.Patient;
using PatientAppointmentAPI.Tests.Patients;

namespace PatientAppointmentAPI.Tests.Appointments
{
    public class CreateAppointmentRequestValidatorTests
    {
        private readonly CreateAppointmentRequestValidator _validator = new();

        [Fact]
        public void Should_fail_when_duration_negative()
        {
            var req = AppointmentRequests.ValidCreate(r => r.DurationMinutes = -20);
            var result = _validator.TestValidate(req);

            result.ShouldHaveValidationErrorFor(x => x.DurationMinutes);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_pass_with_valid_request()
        {
            var req = AppointmentRequests.ValidCreate();

            var result = _validator.TestValidate(req);

            result.ShouldNotHaveAnyValidationErrors();
            Assert.True(result.IsValid);
        }
    }
}

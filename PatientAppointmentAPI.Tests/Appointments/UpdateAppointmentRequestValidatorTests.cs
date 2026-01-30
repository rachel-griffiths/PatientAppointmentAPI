using FluentValidation.TestHelper;
using PatientAppointmentAPI.Core.Validators.Appointment;
using PatientAppointmentAPI.Domain.Enums;
using PatientAppointmentAPI.Tests.Patients;

namespace PatientAppointmentAPI.Tests.Appointments
{
    public class UpdateAppointmentRequestValidatorTests
    {
        private readonly UpdateAppointmentRequestValidator _validator = new();

        [Fact]
        public void Should_fail_cancelled()
        {
            var req = AppointmentRequests.ValidUpdate(r => r.Status = AppointmentStatus.Cancelled);

            var result = _validator.TestValidate(req);

            result.ShouldHaveValidationErrorFor(x => x.Status);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_fail_when_duration_negative()
        {
            var req = AppointmentRequests.ValidUpdate(r => r.DurationMinutes = -20);
            var result = _validator.TestValidate(req);

            result.ShouldHaveValidationErrorFor(x => x.DurationMinutes);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_pass_with_valid_request()
        {
            var req = AppointmentRequests.ValidUpdate();

            var result = _validator.TestValidate(req);

            result.ShouldNotHaveAnyValidationErrors();
            Assert.True(result.IsValid);
        }
    }
}

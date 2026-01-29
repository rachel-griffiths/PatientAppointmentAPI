using FluentValidation.TestHelper;
using PatientAppointmentAPI.Core.Validators.Patient;

namespace PatientAppointmentAPI.Tests
{
    public class UpdatePatientRequestValidatorTests
    {
        private readonly UpdatePatientRequestValidator _validator = new();

        [Fact]
        public void Should_fail_when_nhs_number_wrong_length()
        {
            var req = PatientRequests.ValidUpdate(r => r.NhsNumber = "123");

            var result = _validator.TestValidate(req);

            result.ShouldHaveValidationErrorFor(x => x.NhsNumber);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_fail_when_invalid_postcode()
        {
            var req = PatientRequests.ValidUpdate(r => r.Postcode = "S1 BB");
            var result = _validator.TestValidate(req);

            result.ShouldHaveValidationErrorFor(x => x.Postcode);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_pass_with_valid_request()
        {
            var req = PatientRequests.ValidUpdate();

            var result = _validator.TestValidate(req);

            result.ShouldNotHaveAnyValidationErrors();
            Assert.True(result.IsValid);
        }
    }
}

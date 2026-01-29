using FluentValidation.TestHelper;
using PatientAppointmentAPI.Core.Validators.Patient;

namespace PatientAppointmentAPI.Tests
{
    public class CreatePatientRequestValidatorTests
    {
        private readonly CreatePatientRequestValidator _validator = new();

        [Fact]
        public void Should_fail_when_nhs_number_wrong_length()
        {
            var req = PatientRequests.ValidCreate(r => r.NhsNumber = "123");

            var result = _validator.TestValidate(req);

            result.ShouldHaveValidationErrorFor(x => x.NhsNumber);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_fail_when_invalid_postcode()
        {
            var req = PatientRequests.ValidCreate(r => r.Postcode = "S1 BB");
            var result = _validator.TestValidate(req);

            result.ShouldHaveValidationErrorFor(x => x.Postcode);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_pass_with_valid_request()
        {
            var req = PatientRequests.ValidCreate();

            var result = _validator.TestValidate(req);

            result.ShouldNotHaveAnyValidationErrors();
            Assert.True(result.IsValid);
        }
    }
}

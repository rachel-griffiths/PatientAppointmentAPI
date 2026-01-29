using FluentValidation;
using PatientAppointmentAPI.Core.DTOs.Patients;
using PatientAppointmentAPI.Core.Shared;

namespace PatientAppointmentAPI.Core.Validators.Patient
{
    public sealed class PatientFieldsValidator : AbstractValidator<PatientFieldsDto>
    {
        public PatientFieldsValidator()
        {
            RuleFor(x => x.NhsNumber)
                .NotEmpty()
                .Length(10)
                .Must(AllDigits)
                .WithMessage("NHS number must be exactly 10 digits.")
                .Must(BeValidNhsNumber)
                .WithMessage("NHS number failed checksum validation.");

            RuleFor(x => x.Title)
                .IsInEnum();

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Postcode)
                .NotEmpty()
                .Must(p => PostcodeValidator.ValidateUKPostcode(
                    PostcodeNormaliser.Normalise(p)))
                .WithMessage("Invalid UK postcode.");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTimeOffset.UtcNow)
                .WithMessage("Date of birth must be in the past.");
        }

        private static bool AllDigits(string value)
            => value.All(char.IsDigit);

        private static bool BeValidNhsNumber(string nhsNumber)
            => NHSNumberValidator.Validate(nhsNumber);

        private static bool BeValidPostcode(string nhsNumber)
            => PostcodeValidator.ValidateUKPostcode(nhsNumber);
    }
}

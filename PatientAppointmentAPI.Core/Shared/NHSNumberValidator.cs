namespace PatientAppointmentAPI.Core.Shared
{
    internal static class NHSNumberValidator
    {
        internal static bool Validate(string nhsNumber)
        {
            if (string.IsNullOrWhiteSpace(nhsNumber))
            {
                return false;
            }

            if (nhsNumber.Length != 10)
            {
                return false;
            }

            if(nhsNumber.Any(c => !Char.IsDigit(c)))
            {
                return false;
            }
            var (remainder, checkDigit) = GetCheckDigit(nhsNumber);
            if (checkDigit == 10)
            {
                return false;
            }
            return checkDigit == remainder;
        }

        internal static (int,int) GetCheckDigit(string nhsNumber)
        {
            var sum = 0;
            var multiplier = 10;
            for(var i = 0; i < 10; i++)
            {
                sum += (int)nhsNumber[i] * multiplier;
                multiplier--;
            }

            int remainder = sum % 11;
            var checkDigit = 11 - remainder;
            checkDigit = checkDigit == 11 ? 0 : checkDigit;
            return (remainder, checkDigit);
        }
    }
}

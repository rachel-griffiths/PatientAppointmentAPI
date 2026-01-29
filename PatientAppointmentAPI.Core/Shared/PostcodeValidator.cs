using System.Text.RegularExpressions;

namespace PatientAppointmentAPI.Core.Shared
{
    /// <summary>
    /// Validator for UK postcode. If expanding to other countries, extra validators and logic to select the right one would be needed.
    /// </summary>
    internal static class PostcodeValidator
    {
        /// <summary>
        /// A method to validate a UK postcode
        /// </summary>
        /// <param name="postcode"></param>
        /// <returns>bool isValid</returns>
        internal static bool ValidateUKPostcode(string postcode)
        {
            // regex from: https://www.oreilly.com/library/view/regular-expressions-cookbook/9781449327453/ch04s16.html
            var UKRegex = "^[A-Z]{1,2}[0-9R][0-9A-Z]?\\s?[0-9][ABD-HJLNP-UW-Z]{2}$";
            return Regex.IsMatch(postcode, UKRegex);
        }
    }
}

using System.Text.RegularExpressions;

namespace PatientAppointmentAPI.Core.Shared
{
    internal class PostcodeNormaliser
    {
        internal static string Normalise(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Remove all whitespace and uppercase
            var compact = Regex.Replace(input.Trim(), @"\s+", "").ToUpperInvariant();

            // Postcodes are typically 5–7 chars without space (e.g. "M11AE"=5, "SW1A1AA"=7)
            // If it's outside this, leave as-is; validation will fail.
            if (compact.Length < 5 || compact.Length > 7)
                return compact;

            // Insert a single space before the last 3 characters
            return compact.Insert(compact.Length - 3, " ");
        }
    }
}

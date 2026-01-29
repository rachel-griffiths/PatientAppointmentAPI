using PatientAppointmentAPI.Core.DTOs.Patients;
using PatientAppointmentAPI.Domain.Enums;

namespace PatientAppointmentAPI.Tests
{
    public static class PatientRequests
    {
        public static CreatePatientRequest ValidCreate(Action<CreatePatientRequest>? mutate = null)
        {
            var req = new CreatePatientRequest
            {
                NhsNumber = "9434765919",
                Title = Title.Mr,
                FirstName = "John",
                LastName = "Smith",
                Postcode = "SW1A 1AA",
                DateOfBirth = new DateTimeOffset(1990, 1, 1, 0, 0, 0, TimeSpan.Zero)
            };

            mutate?.Invoke(req);
            return req;
        }

        public static UpdatePatientRequest ValidUpdate(Action<UpdatePatientRequest>? mutate = null)
        {
            var req = new UpdatePatientRequest
            {
                Id = Guid.NewGuid(),
                NhsNumber = "9434765919",
                Title = Title.Mr,
                FirstName = "John",
                LastName = "Smith",
                Postcode = "SW1A 1AA",
                DateOfBirth = new DateTimeOffset(1990, 1, 1, 0, 0, 0, TimeSpan.Zero)
            };

            mutate?.Invoke(req);
            return req;
        }
    }
}

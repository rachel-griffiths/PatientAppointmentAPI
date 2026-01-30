using PatientAppointmentAPI.Core.DTOs.Appointments;
using PatientAppointmentAPI.Domain.Enums;

namespace PatientAppointmentAPI.Tests.Appointments
{
    public static class AppointmentRequests
    {
        public static CreateAppointmentRequest ValidCreate(Action<CreateAppointmentRequest>? mutate = null)
        {
            var req = new CreateAppointmentRequest
            {
                PatientId = Guid.NewGuid(),
                ClinicianId = Guid.NewGuid(),
                Department = Department.GeneralPractice,
                StartTime = DateTime.Now.AddDays(1),
                DurationMinutes = 15,
                Status = AppointmentStatus.Scheduled
            };

            mutate?.Invoke(req);
            return req;
        }

        public static UpdateAppointmentRequest ValidUpdate(Action<UpdateAppointmentRequest>? mutate = null)
        {
            var req = new UpdateAppointmentRequest
            {
                PatientId = new Guid(),
                ClinicianId = new Guid(),
                Department = Department.GeneralPractice,
                StartTime = DateTime.Now.AddDays(1),
                DurationMinutes = 15,
                Status = AppointmentStatus.Scheduled
            };

            mutate?.Invoke(req);
            return req;
        }
    }
}

namespace PatientAppointmentAPI.Core
{
    public static class Errors
    {
        public const string AppointmentNotFound = "appointment.not_found";
        public const string AppointmentCancelled = "appointment.cancelled_cannot_modify";
        public const string PatientNotFound = "patient.not_found";
    }
}

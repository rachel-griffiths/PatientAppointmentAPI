namespace PatientAppointmentAPI.Domain.Entities
{
    /// <summary>
    /// An entity representing clinician - basic stub right now.
    /// </summary>
    public class Clinician
    {
        protected Clinician() { }

        public Guid Id { get; private set; } = default!;

        public string Name { get; private set; } = string.Empty;
    }
}

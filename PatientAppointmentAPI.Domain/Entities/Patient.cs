using PatientAppointmentAPI.Domain.Enums;

namespace PatientAppointmentAPI.Domain.Entities
{
    /// <summary>
    /// Entity to hold a patient. Extra demographic data would likely be desirable and fields such as sex & gender added.
    /// Full address details would also be needed; it is limited to postcode here to demonstrate the validation.
    /// </summary>
    public class Patient
    {
        protected Patient()
        {

        }

        public Patient(Guid id, string nhsNumber, Title title, string fName, string lName, string postcode, DateTimeOffset dob) { 
            Id = id;
            NhsNumber = nhsNumber;
            Title = title;
            FirstName = fName;
            LastName = lName;
            Postcode = postcode;
            DateOfBirth = dob;
        }

        public Guid Id { get; private set; }

        public string NhsNumber { get; private set; } = default!;

        public Title Title { get; private set; } = default!;
        public string FirstName { get; private set; } = default!;
        public string LastName { get; private set; } = default!;
        public string Postcode { get; private set; } = default!;
        public DateTimeOffset DateOfBirth { get; private set; }

        public ICollection<Appointment> Appointments { get; private set; }
            = new List<Appointment>();

        public void UpdateDetails(
            string nhsNumber,
            Title title,
            string firstName,
            string lastName,
            string postcode,
            DateTimeOffset dateOfBirth)
        {
            NhsNumber = nhsNumber;
            Title = title;
            FirstName = firstName;
            LastName = lastName;
            Postcode = postcode;
            DateOfBirth = dateOfBirth;
        }
    }
}

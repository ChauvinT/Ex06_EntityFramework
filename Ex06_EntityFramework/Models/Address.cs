namespace Ex06_EntityFramework.Models
{
    public class Address : ValueObject
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }
        public string State { get; private set; }

        public Address(string street, string city, string postalCode, string country, string state)
        {
            Street = street;
            City = city;
            PostalCode = postalCode;
            Country = country;
            State = state;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return PostalCode;
            yield return Country;
            yield return State;
        }
    }
}

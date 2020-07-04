using CSharpFunctionalExtensions;
using System;

namespace SampleRestApi.Data.UserValueObjects
{
    public class UserInformation : ValueObject<UserInformation>
    {
        public string FirstName { get; }

        public string LastName { get; }

        public static UserInformation DefaultUser => new UserInformation("John", "Doe");

        protected UserInformation()
        {

        }

        private UserInformation(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static UserInformation Create(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                throw new ArgumentNullException("Customer name should not be empty");

            if (firstName.Length > 100 || lastName.Length > 100)
                throw new ArgumentOutOfRangeException("Customer name is too long");

            return new UserInformation(firstName, lastName);
        }

        protected override bool EqualsCore(UserInformation other)
        {
            return FirstName.Equals(other.FirstName, StringComparison.InvariantCultureIgnoreCase)
                && LastName.Equals(other.LastName, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override int GetHashCodeCore()
        {
            return FirstName.GetHashCode() ^ LastName.GetHashCode();
        }
    }
}

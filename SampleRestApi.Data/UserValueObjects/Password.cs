using CSharpFunctionalExtensions;
using LinkerPad.Task.Common;
using System;

namespace SampleRestApi.Data.UserValueObjects
{
    public class Password : ValueObject<Password>
    {
        public string Value { get; }

        protected Password()
        {

        }

        private Password(string plainPassword)
        {
            Value = plainPassword;
        }

        public static Password Create(string plainPassword)
        {
            if (string.IsNullOrEmpty(plainPassword))
                throw new ArgumentException("password should not be empty");

            if (plainPassword.Length < 6)
                throw new ArgumentException("Customer name is too short");

            return new Password(HashManagement.Md5Hash(plainPassword));
        }

        protected override bool EqualsCore(Password other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static explicit operator Password(string password)
        {
            return new Password(password);
        }

        public static implicit operator string(Password password)
        {
            return password.Value;
        }
    }
}

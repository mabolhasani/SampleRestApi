using CSharpFunctionalExtensions;
using LinkerPad.Task.Common;
using System;

namespace SampleRestApi.Data.UserValueObjects
{
    public class TokenInformation : ValueObject<TokenInformation>
    {
        public bool IsExpired => TokenExpirationDate <= DateTime.Now;

        public DateTime TokenExpirationDate { get; }

        public string VerificationToken { get; }

        public int ConfirmationToken { get; }

        protected TokenInformation() { }

        private TokenInformation(DateTime tokenExpirationDate, string verificationToken)
        {
            TokenExpirationDate = tokenExpirationDate;
            VerificationToken = verificationToken;
        }

        private TokenInformation(DateTime tokenExpirationDate, int confirmationToken)
        {
            TokenExpirationDate = tokenExpirationDate;
            ConfirmationToken = confirmationToken;
        }

        public static TokenInformation CreateVerificationToken(Guid userId)
        {
            string verificationToken = HashManagement.Md5Hash($"{userId}{DateTime.Now}");

            return new TokenInformation(DateTime.Now.AddMinutes(3), verificationToken);
        }

        public static TokenInformation CreateConfirmCode(int confirmCode)
        {
            if (confirmCode.ToString().Length != 5)
                throw new ArgumentOutOfRangeException("confirm code should be 5 digit only");

            return new TokenInformation(DateTime.Now.AddMinutes(3), confirmCode);
        }

        public static TokenInformation CreateDefault()
        {
            return new TokenInformation();
        }

        protected override bool EqualsCore(TokenInformation other)
        {
            return TokenExpirationDate.Equals(other.TokenExpirationDate)
                  && VerificationToken.Equals(other.VerificationToken)
                  && ConfirmationToken.Equals(other.ConfirmationToken);
        }

        protected override int GetHashCodeCore()
        {
            return TokenExpirationDate.GetHashCode()
                  ^ VerificationToken.GetHashCode()
                  ^ ConfirmationToken.GetHashCode();
        }
    }
}

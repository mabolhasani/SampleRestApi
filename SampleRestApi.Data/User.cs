using SampleRestApi.Data.UserValueObjects;
using SampleRestApi.Data.utils;
using System;
using System.Net.Mail;

namespace SampleRestApi.Data
{
    public class User : Entity
    {
        #region private member
        private string _password;
        private string _email;
        #endregion

        public virtual Password Password
        {
            get => (Password)_password;
            protected set => _password = value;
        }

        public virtual MailAddress Email
        {
            get => new MailAddress(_email);
            protected set => _email = value.Address;
        }

        public virtual UserInformation UserInformation { get; protected set; }

        public virtual TokenInformation TokenInformation { get; protected set; }

        public virtual bool EmailConfirmed { get; protected set; }


        protected User() { }
        public User(
           UserInformation userInformation,
            MailAddress email, Password password,
            bool emailConfirmed = false)
        {
            UserInformation = userInformation;
            Email = email;
            Password = password;
            CreateDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
            EmailConfirmed = emailConfirmed;
            TokenInformation = TokenInformation.CreateDefault();
        }

    }
}

using System;

namespace SampleRestApi.Model.Account
{
    public class TokenInformationViewModel
    {
        public string Token { get; set; }

        public DateTime ExpirationDate { get; set; }

        public UserInformationModel UserInformationModel { get; set; }
    }
}
using System;
using SampleRestApi.Data;

namespace SampleRestApi.Model.Account
{
    public class UserInformationModel
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }


        public static UserInformationModel GetUserInformationModel(User user)
        {
            return new UserInformationModel
            {
                UserId = user.Id,
                FirstName = user.UserInformation.FirstName,
                LastName = user.UserInformation.LastName,
                //Email = user.Email.Address
            };
        }

        public static UserInformationModel GetUserInformationModel(RegisterViewModel registerViewModel)
        {
            return new UserInformationModel
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                //Email = registerViewModel.Email
            };
        }
    }
}
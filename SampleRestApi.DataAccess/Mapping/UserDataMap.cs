using System;
using FluentNHibernate.Mapping;
using SampleRestApi.Data;

namespace SampleRestApi.DataAccess.Mapping
{
    class UserDataMap : ClassMap<User>
    {
        public UserDataMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.CreateDate).CustomType<DateTime>().Not.Nullable();
            Map(x => x.ModifiedDate).CustomType<DateTime>().Not.Nullable();

            Component(x => x.UserInformation, y =>
            {
                y.Map(x => x.FirstName).Length(100).Not.Nullable();
                y.Map(x => x.LastName).Length(100).Not.Nullable();
            });

            Component(x => x.TokenInformation, y =>
            {
                y.Map(x => x.TokenExpirationDate).CustomType<DateTime>().Not.Nullable();
                y.Map(x => x.VerificationToken).Length(40001).Nullable();
                y.Map(x => x.ConfirmationToken).CustomType<int>().Not.Nullable().Default("0");
            });


            Map(x => x.Email).CustomType<string>().Access.CamelCaseField(Prefix.Underscore).Length(350).Not.Nullable();
            Map(x => x.Password).CustomType<string>().Access.CamelCaseField(Prefix.Underscore).Length(50).Not.Nullable();
            Map(x => x.EmailConfirmed).CustomType<bool>().Not.Nullable();

            Table("Tbl_User");
        }
    }
}

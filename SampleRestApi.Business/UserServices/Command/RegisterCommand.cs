using CSharpFunctionalExtensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using SampleRestApi.Data.UserValueObjects;
using System.Net.Mail;
using SampleRestApi.Data;
using SampleRestApi.DataAccess.Repository;
using SampleRestApi.DataAccess.EntityInterface;
using System.Linq;
using SampleRestApi.Business.Common;

namespace SampleRestApi.Business.UserServices.Command
{
    public sealed class RegisterCommand : IRequest<Result>
    {
        public UserInformation UserInformation { get; }

        public MailAddress Email { get; }

        public Password Password { get; }

        public RegisterCommand(UserInformation userInformation, MailAddress email, Password passowrd)
        {
            UserInformation = userInformation;
            Email = email;
            Password = passowrd;
        }

        public static User GetUser(RegisterCommand registerCommand, bool isMobileNumberConfirmed)
        {
            return new User(registerCommand.UserInformation,
                registerCommand.Email,
                registerCommand.Password,
                isMobileNumberConfirmed);
        }

        internal sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IGenericRepository<User> _userRepository;

            public RegisterCommandHandler(IUnitOfWork unitOfWork, IGenericRepository<User> userRepository)
            {
                _unitOfWork = unitOfWork;
                _userRepository = userRepository;
            }

            public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                if (UserExist(request.Email))
                    return Result.Failure(ResponseMessagesModel.MobileExist);

                await CreateCustomer(request);

                return Result.Ok();
            }

            private bool UserExist(MailAddress email)
            {
                return _userRepository.GetAll().FirstOrDefault(u => u.Email.Address == email.Address) != null
                   ? true
                   : false;
            }

            private async Task CreateCustomer(RegisterCommand command)
            {
                _unitOfWork.BeginTransaction();

                User user = GetUser(command, false);

                _userRepository.Create(user);

                await _unitOfWork.CommitAsync();
            }
        }
    }
}
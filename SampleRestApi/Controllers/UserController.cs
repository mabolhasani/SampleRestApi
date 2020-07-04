using CSharpFunctionalExtensions;
using LinkerPad.Task.Controllers;
using LinkerPad.Task.Utils;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleRestApi.Business.Common;
using SampleRestApi.Business.UserServices.Command;
using SampleRestApi.Data.UserValueObjects;
using SampleRestApi.Model.Account;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SampleRestApi.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]/[action]")]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, ResponseMessagesModel.InputError, typeof(Envelope))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, ResponseMessagesModel.MobileExist, typeof(Envelope))]
        [SwaggerResponse((int)HttpStatusCode.Created, ResponseMessagesModel.UserCreated, typeof(UserInformationModel))]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            RegisterCommand command = new RegisterCommand(
                UserInformation.Create(
                    registerViewModel.FirstName,
                    registerViewModel.LastName),
                new MailAddress(registerViewModel.Email),
                Password.Create(registerViewModel.Password)
                );

            Result result = await _mediator.Send(command);

            return result.IsSuccess
                ? Created(
                Url.Action("GetUserInformation", "Account").ToLower(),
                UserInformationModel.GetUserInformationModel(registerViewModel))
                : Conflict(result.Error);
        }
    }
}

using LinkerPad.Task.Utils;
using Microsoft.AspNetCore.Mvc;
using SampleRestApi.Business.Common;
using System;
using System.Net;

namespace LinkerPad.Task.Controllers
{
    public class BaseController : Controller
    {
        protected new IActionResult Ok()
        {
            return base.Ok(Envelope.Ok());
        }

        protected IActionResult Ok<T>(T result)
        {
            return base.Ok(Envelope.Ok(result));
        }

        protected IActionResult Created<T>(string uri, T result)
        {
            return base.Created(uri, Envelope.Ok(result));
        }

        protected IActionResult BadRequest(string errorMessage)
        {
            return base.BadRequest(Envelope.Error(errorMessage));
        }

        protected IActionResult Forbidden(string errorMessage)
        {
            return StatusCode((int)HttpStatusCode.Forbidden, Envelope.Error(errorMessage));
        }

        protected IActionResult Unauthorized(string errorMessage)
        {
            return base.Unauthorized(Envelope.Error(errorMessage));
        }
        
        protected IActionResult Conflict(string errorMessage)
        {
            return base.Conflict(Envelope.Error(errorMessage));
        }
        
        protected ActionResult NotFound(string errorMessage)
        {
            return base.NotFound(Envelope.Error(errorMessage));
        }

        protected IActionResult Error(Error error)
        {
            switch (error.Code)
            {
                case HttpStatusCode.Forbidden:
                    return Forbidden(error.Message);
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(error.Message); 
                case HttpStatusCode.BadRequest:
                    return BadRequest(error.Message);
                case HttpStatusCode.NotFound:
                    return NotFound(error.Message);

                default:
                    throw new ArgumentException();
            }
        }
    }
}

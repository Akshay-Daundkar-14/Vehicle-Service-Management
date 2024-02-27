using System;
using System.Net;
using System.Security.Cryptography.Xml;
using System.Text.Json;
using VehicleServiceManagement.API.Controllers;
using VehicleServiceManagement.API.Data;
using VehicleServiceManagement.API.Models.Domain;
using VehicleServiceManagement.API.Models.DTO;
using VehicleServiceManagement.API.Repository.Interface;

namespace VehicleServiceManagement.API.Helpers.CustomMiddleware
{
    public class ExceptionHandlingMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IServerErrorRepository _serverErrorRepos)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, _serverErrorRepos);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, IServerErrorRepository _serverErrorRepos)
        {
            context.Response.ContentType = "application/json";

            var response = context.Response;


            var errorResponse = new ErrorResponse()
            {
                IsSuccess = false
            };

            var errorEntity = new Error()
            {
                 CreatedDate = DateTime.Now,
                 Message = exception.Message,
                 Source = exception.Source,
                 StaclTrace = exception.StackTrace
            };

            switch (exception)
            {

                case ApplicationException ex:
                    if(ex.Message.Contains("Invaild Token"))
                    {
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        errorResponse.Message = ex.Message;
                        break;
                    }
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = ex.Message;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = "Internal server error!";
                    break;
            }


            _logger.LogError(exception.Message);
            await _serverErrorRepos.CreateErrorAsync(errorEntity);
            var result = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(result);
             
        }

    }
}

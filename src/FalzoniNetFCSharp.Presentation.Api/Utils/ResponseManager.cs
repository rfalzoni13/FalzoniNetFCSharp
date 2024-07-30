using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using System;
using FalzoniNetFCSharp.Presentation.Api.Models.Common;
using FalzoniNetFCSharp.Utils.Helpers;

namespace FalzoniNetFCSharp.Presentation.Api.Utils
{
    public static class ResponseManager
    {
        public static HttpResponseMessage ReturnExceptionNotFound(HttpResponseException ex, HttpRequestMessage request, Logger logger, string action, string message)
        {
            logger.Warn(action + " - Error: " + ex);

            StatusCodeModel status = new StatusCodeModel
            {
                Status = ex.Response.StatusCode,
                Message = "Nenhum registro encontrado!"
            };

            logger.Info(action + " - Finalizado");
            return request.CreateResponse(HttpStatusCode.NotFound, status);
        }

        public static HttpResponseMessage ReturnExceptionInternalServerError(Exception ex, HttpRequestMessage request, Logger logger, string action)
        {
            logger.Error(action + " - Error: " + ex);

            StatusCodeModel status = new StatusCodeModel
            {
                Status = HttpStatusCode.InternalServerError,
                Message = ExceptionHelper.CatchMessageFromException(ex)
            };

            logger.Info(action + " - Finalizado");
            return request.CreateResponse(HttpStatusCode.InternalServerError, status);
        }

        public static HttpResponseMessage ReturnBadRequest(HttpRequestMessage request, Logger logger, string action, string message)
        {
            logger.Warn(action + " - " + message);

            StatusCodeModel status = new StatusCodeModel
            {
                Status = HttpStatusCode.BadRequest,
                Message = message
            };

            logger.Info(action + " - Finalizado");
            return request.CreateResponse(HttpStatusCode.BadRequest, status);
        }

        public static HttpResponseMessage ReturnErrorResult(HttpRequestMessage request, Logger logger, string action, IEnumerable<string> errors)
        {
            string message = string.Empty;

            int i = 0;

            string[] errorsArray = errors.ToArray();

            do
            {
                message += errorsArray[i] + " ";
                i++;
            }
            while (i < errorsArray.Count());

            logger.Warn(action + " - " + message);

            StatusCodeModel status = new StatusCodeModel
            {
                Status = HttpStatusCode.BadRequest,
                ErrorsResult = errorsArray
            };

            logger.Info(action + " - Finalizado");
            return request.CreateResponse(HttpStatusCode.BadRequest, status);
        }
    }
}
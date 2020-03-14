using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProjectX.IdentityContext.Application.Exceptions;
using ProjectX.IdentityContext.Application.Services.Interfaces.Audits;
using ProjectX.IdentityContext.Application.Services.Interfaces.Loggers;
using ProjectX.IdentityContext.Domain.Entities.Audits;
using ProjectX.IdentityContext.Persistence.UnitOfWork;
using Skoruba.IdentityServer4.Admin.Api.Helpers;

namespace Skoruba.IdentityServer4.Admin.Api.Middlewares
{
    /// <summary>
    ///     Use As Unit Of Work Middleware
    ///     Use as Exception Handler
    ///     Audit Event Handler
    /// </summary>
    public class ProjectXMiddleWare
    {
        private readonly RequestDelegate requestDelegate;

        public ProjectXMiddleWare(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        public async Task Invoke(
            HttpContext context,
            IUnitOfWork unitOfWork,
            ILoggerService logger,
            IAuditService auditService)
        {
            try
            {
                if (context.Request.Method == HttpMethods.Post ||
                    context.Request.Method == HttpMethods.Put ||
                    context.Request.Method == HttpMethods.Delete)
                {
                    unitOfWork.Begin();

                    await requestDelegate(context);

                    StoreAudits(logger, auditService);

                    unitOfWork.Commit();
                }
                else
                {
                    await requestDelegate(context);
                }
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                await HandleExceptionAsync(context, ex);
            }
        }

        private static void StoreAudits(ILoggerService logger, IAuditService auditService)
        {
            foreach (var log in logger.DomainEvents)
                auditService.Add(
                    new Audit
                    {
                        AggregateId = log.AggregateId,
                        EventName = log.GetType().Name,
                        EventTime = log.EventTime,
                        Data = JsonConvert.SerializeObject(log.Flatten())
                    });
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;

            if (ex is EntityNotFoundException || ex != null) code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(new {error = ex?.Message});
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;
            return context.Response.WriteAsync(result);
        }
    }
}
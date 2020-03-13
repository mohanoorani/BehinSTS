using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectX.IdentityContext.Application.Services.Interfaces.Audits;
using ProjectX.IdentityContext.Application.Services.Interfaces.Loggers;
using ProjectX.IdentityContext.Domain.Entities.Audits;
using UAParser;

namespace Skoruba.IdentityServer4.Admin.Api.Filters
{
    public class DomainEventLoggerFilter : ActionFilterAttribute
    {
        private readonly IAuditService auditService;
        
        private readonly IHttpContextAccessor httpContextAccessor;
        
        private readonly ILoggerService logger;

        public DomainEventLoggerFilter(
            ILoggerService logger, 
            IAuditService auditService,
            IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.auditService = auditService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //todo : Add UserId or username in this layer 
            //var username = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var ip = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            var uaString = httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
            var parser = Parser.GetDefault();
            var userAgent = parser.ParseUserAgent(uaString).ToString();

            foreach (var log in logger.DomainEvents)
                auditService.Add(
                    new Audit
                    {
                        AggregateId = log.AggregateId,
                        EventName = log.GetType().Name,
                        EventTime = log.EventTime,
                        Data = log.ToString(),
                        Ip = ip,
                        UserAgent = userAgent
                        //todo : Add UserId 
                    });
        }
    }
}
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectX.IdentityContext.Application.Services.Interfaces.Audits;
using ProjectX.IdentityContext.Application.Services.Interfaces.Loggers;

namespace Skoruba.IdentityServer4.Admin.Api.Filters
{
    public class DomainEventLoggerFilter : ActionFilterAttribute
    {
        private readonly IAuditService _auditService;
        private readonly ILoggerService logger;

        public DomainEventLoggerFilter(ILoggerService logger, IAuditService auditService)
        {
            this.logger = logger;
            _auditService = auditService;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            foreach (var log in logger.DomainEvents)
                _auditService.Add(log.AggregateId, log.GetType().Name, log.EventTime, log.ToString());
        }
    }
}
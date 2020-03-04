using System;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectX.Application.Services.Interfaces;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Services.Interfaces;

namespace Skoruba.IdentityServer4.Admin.Api.Filters
{
    public class DomainEventLoggerFilter : ActionFilterAttribute
    {
        private readonly ILoggerService logger;
        private readonly IUserEventService userEventService;

        public DomainEventLoggerFilter(ILoggerService logger,IUserEventService userEventService)
        {
            this.logger = logger;
            this.userEventService = userEventService;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            foreach (var log in logger.DomainEvents)
                userEventService.Add(log.GetType().Name, log.ToString());
        }
    }
}
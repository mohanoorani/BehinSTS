﻿using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ProjectX.IdentityContext.IntegrationTest.BypassAuthentication
{
    public class BypassAuthenticationHandler : AuthenticationHandler<BypassAuthenticationOptions>
    {
        public BypassAuthenticationHandler(
            IOptionsMonitor<BypassAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authenticationTicket = new AuthenticationTicket(
                new ClaimsPrincipal(Options.Identity),
                new AuthenticationProperties(),
                Constants.Scheme);

            return Task.FromResult(AuthenticateResult.Success(authenticationTicket));
        }
    }
}
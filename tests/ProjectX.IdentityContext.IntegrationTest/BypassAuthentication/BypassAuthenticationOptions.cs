using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace ProjectX.IdentityContext.IntegrationTest.BypassAuthentication
{
    public class BypassAuthenticationOptions : AuthenticationSchemeOptions
    {
        public ClaimsIdentity Identity
        {
            get
            {
                var nameIdentifier = Guid.Parse("FAF7C685-1B52-4A05-BC70-5643539EF883").ToString();
                var claims = new List<Claim> {new Claim(ClaimTypes.NameIdentifier, nameIdentifier)};

                return new ClaimsIdentity(claims, Constants.Type);
            }
        }
    }
}
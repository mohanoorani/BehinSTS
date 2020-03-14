using System;
using System.Linq;
using System.Security.Claims;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using UAParser;

namespace ProjectX.IdentityContext.Application.UserDescriptor
{
    public class UserDescriptor<TUser, TKey> : IUserDescriptor
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        
        private readonly UserManager<TUser> userManager;

        public UserDescriptor(IHttpContextAccessor httpContextAccessor,UserManager<TUser> userManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public string GetUserAgent()
        {
            var uaString = httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();

            var parser = Parser.GetDefault();
            return parser.ParseUserAgent(uaString).ToString();
        }

        public string GetUserId()
        {
            return httpContextAccessor.HttpContext.User.GetSubjectId();
        }

        public string GetUsername()
        {
            return userManager.FindByIdAsync(GetUserId()).GetAwaiter().GetResult().UserName;
        }

        public string GetUserIp()
        {
            return httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
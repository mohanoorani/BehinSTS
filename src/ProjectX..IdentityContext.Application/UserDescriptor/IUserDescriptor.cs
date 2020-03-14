using System;

namespace ProjectX.IdentityContext.Application.UserDescriptor
{
    public interface IUserDescriptor
    {
        string GetUserAgent();

        string GetUserId();

        string GetUsername();

        string GetUserIp();
    }
}
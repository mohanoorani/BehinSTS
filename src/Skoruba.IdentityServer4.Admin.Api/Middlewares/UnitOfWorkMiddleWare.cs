using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProjectX.IdentityContext.Persistence.UnitOfWork;

namespace Skoruba.IdentityServer4.Admin.Api.Middlewares
{
    public class UnitOfWorkMiddleWare
    {
        private readonly RequestDelegate requestDelegate;

        private IUnitOfWork _unitOfWork;

        public UnitOfWorkMiddleWare(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext httpContext, IUnitOfWork unitOfWork)
        {
            try
            {
                if (httpContext.Request.Method == HttpMethods.Post ||
                    httpContext.Request.Method == HttpMethods.Put ||
                    httpContext.Request.Method == HttpMethods.Delete)
                {
                    _unitOfWork = unitOfWork;

                    _unitOfWork.Begin();

                    await requestDelegate(httpContext);

                    _unitOfWork.Commit();
                }
                else
                {
                    await requestDelegate(httpContext);
                }
            }
            catch(Exception ex)
            {
                _unitOfWork.RollBack();
            }
        }
    }
}
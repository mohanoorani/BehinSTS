using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectX.IdentityContext.Application.UserDescriptor;
using ProjectX.IdentityContext.IntegrationTest.BypassAuthentication;
using ProjectX.IdentityContext.IntegrationTest.Fake;
using Serilog;
using Skoruba.IdentityServer4.Admin.Api;
using Skoruba.IdentityServer4.Admin.Api.Helpers;
using Skoruba.IdentityServer4.Admin.Api.Middlewares;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.DbContexts;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.Entities.Identity;

namespace ProjectX.IdentityContext.IntegrationTest
{
    public class StartupProjectX : Startup
    {
        public StartupProjectX(IWebHostEnvironment env, IConfiguration configuration) : base(env, configuration)
        {
        }

        protected override void AddSerilogToProjectXIntegrationTest(IServiceCollection services)
        {
            var logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();

            services.AddLogging(i => i.AddSerilog(logger));
        }

        protected override void RegisterExtraServices(IServiceCollection services)
        {
            services.AddScoped<IUserDescriptor, FakeUserDescriptor>();
        }

        protected override void RegisterAuthentication(IServiceCollection services)
        {
            services.AddIdentity<UserIdentity, UserIdentityRole>(options => { options.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<AdminIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = Constants.Scheme;
                    options.DefaultChallengeScheme = Constants.Scheme;
                }).AddScheme<BypassAuthenticationOptions, BypassAuthenticationHandler>(
                Constants.Scheme,
                Constants.DisplayName,
                i => { });

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultSignInScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultForbidScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            //}).AddCookie(IdentityServerAuthenticationDefaults.AuthenticationScheme);
        }

        public override void RegisterAuthorization(IServiceCollection services)
        {
            services.AddAuthorizationPolicies();
        }

        protected override void UseAuthentication(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseMiddleware<AuthenticatedTestRequestMiddleware>();
        }
    }
}
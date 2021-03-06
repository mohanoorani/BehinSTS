﻿using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectX.IdentityContext.Application.Services.Audits;
using ProjectX.IdentityContext.Application.Services.Groups;
using ProjectX.IdentityContext.Application.Services.Interfaces.Audits;
using ProjectX.IdentityContext.Application.Services.Interfaces.Groups;
using ProjectX.IdentityContext.Application.Services.Interfaces.Loggers;
using ProjectX.IdentityContext.Application.Services.Loggers;
using ProjectX.IdentityContext.Application.UserDescriptor;
using ProjectX.IdentityContext.Persistence.Repositories.Audits;
using ProjectX.IdentityContext.Persistence.Repositories.Groups;
using ProjectX.IdentityContext.Persistence.Repositories.Interfaces.Audits;
using ProjectX.IdentityContext.Persistence.Repositories.Interfaces.Groups;
using ProjectX.IdentityContext.Persistence.UnitOfWork;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Mappers.Configuration;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Resources;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Services;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Services.Interfaces;
using Skoruba.IdentityServer4.Admin.EntityFramework.Identity.Repositories;
using Skoruba.IdentityServer4.Admin.EntityFramework.Identity.Repositories.Interfaces;
using Skoruba.IdentityServer4.Admin.EntityFramework.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AdminServicesExtensions
    {
        public static IMapperConfigurationBuilder AddAdminAspNetIdentityMapping(this IServiceCollection services)
        {
            var builder = new MapperConfigurationBuilder();

            services.AddSingleton<IConfigurationProvider>(sp => new MapperConfiguration(cfg =>
            {
                foreach (var profileType in builder.ProfileTypes)
                    cfg.AddProfile(profileType);
            }));

            services.AddScoped<IMapper>(
                sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));

            return builder;
        }

        public static IServiceCollection AddAdminAspNetIdentityServices<TIdentityDbContext, TPersistedGrantDbContext,
            TLogDbContext, TUser>(
            this IServiceCollection services)
            where TIdentityDbContext : IdentityDbContext<TUser, IdentityRole, string, IdentityUserClaim<string>,
                IdentityUserRole<string>, IdentityUserLogin<string>, IdentityRoleClaim<string>,
                IdentityUserToken<string>>
            where TPersistedGrantDbContext : DbContext, IAdminPersistedGrantDbContext
            where TLogDbContext : DbContext, IAdminLogDbContext
            where TUser : IdentityUser
        {
            return services
                .AddAdminAspNetIdentityServices<TIdentityDbContext, TPersistedGrantDbContext, TLogDbContext,
                    UserDto<string>, string, RoleDto<string>, string, string,
                    string, TUser, IdentityRole, string, IdentityUserClaim<string>, IdentityUserRole<string>,
                    IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>,
                    UsersDto<UserDto<string>, string>, RolesDto<RoleDto<string>, string>,
                    UserRolesDto<RoleDto<string>, string, string>,
                    UserClaimsDto<string>, UserProviderDto<string>, UserProvidersDto<string>,
                    UserChangePasswordDto<string>,
                    RoleClaimsDto<string>, UserClaimDto<string>, RoleClaimDto<string>>();
        }

        public static IServiceCollection AddAdminAspNetIdentityServices<TAdminDbContext, TLogDbContext, TUserDto,
            TUserDtoKey, TRoleDto, TRoleDtoKey, TUserKey,
            TRoleKey, TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken,
            TUsersDto, TRolesDto, TUserRolesDto, TUserClaimsDto,
            TUserProviderDto, TUserProvidersDto, TUserChangePasswordDto, TRoleClaimsDto,
            TUserClaimDto, TRoleClaimDto>(
            this IServiceCollection services)
            where TAdminDbContext : IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim,
                TUserToken>,
            IAdminPersistedGrantDbContext
            where TLogDbContext : DbContext, IAdminLogDbContext
            where TUserDto : UserDto<TUserDtoKey>
            where TUser : IdentityUser<TKey>
            where TRole : IdentityRole<TKey>
            where TKey : IEquatable<TKey>
            where TUserClaim : IdentityUserClaim<TKey>
            where TUserRole : IdentityUserRole<TKey>
            where TUserLogin : IdentityUserLogin<TKey>
            where TRoleClaim : IdentityRoleClaim<TKey>
            where TUserToken : IdentityUserToken<TKey>
            where TRoleDto : RoleDto<TRoleDtoKey>
            where TUsersDto : UsersDto<TUserDto, TUserDtoKey>
            where TRolesDto : RolesDto<TRoleDto, TRoleDtoKey>
            where TUserRolesDto : UserRolesDto<TRoleDto, TUserDtoKey, TRoleDtoKey>
            where TUserClaimsDto : UserClaimsDto<TUserDtoKey>
            where TUserProviderDto : UserProviderDto<TUserDtoKey>
            where TUserProvidersDto : UserProvidersDto<TUserDtoKey>
            where TUserChangePasswordDto : UserChangePasswordDto<TUserDtoKey>
            where TRoleClaimsDto : RoleClaimsDto<TRoleDtoKey>
            where TUserClaimDto : UserClaimDto<TUserDtoKey>
            where TRoleClaimDto : RoleClaimDto<TRoleDtoKey>
        {
            return services
                .AddAdminAspNetIdentityServices<TAdminDbContext, TAdminDbContext, TLogDbContext, TUserDto, TUserDtoKey,
                    TRoleDto, TRoleDtoKey, TUserKey,
                    TRoleKey, TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken,
                    TUsersDto, TRolesDto, TUserRolesDto, TUserClaimsDto,
                    TUserProviderDto, TUserProvidersDto, TUserChangePasswordDto, TRoleClaimsDto, TUserClaimDto,
                    TRoleClaimDto>();
        }

        public static IServiceCollection AddAdminAspNetIdentityServices<TIdentityDbContext, TPersistedGrantDbContext,
            TLogDbContext, TUserDto, TUserDtoKey, TRoleDto, TRoleDtoKey, TUserKey, TRoleKey, TUser, TRole, TKey,
            TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken,
            TUsersDto, TRolesDto, TUserRolesDto, TUserClaimsDto,
            TUserProviderDto, TUserProvidersDto, TUserChangePasswordDto, TRoleClaimsDto, TUserClaimDto, TRoleClaimDto>(
            this IServiceCollection services)
            where TPersistedGrantDbContext : DbContext, IAdminPersistedGrantDbContext
            where TIdentityDbContext : IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin,
                TRoleClaim, TUserToken>
            where TLogDbContext : DbContext, IAdminLogDbContext
            where TUserDto : UserDto<TUserDtoKey>
            where TUser : IdentityUser<TKey>
            where TRole : IdentityRole<TKey>
            where TKey : IEquatable<TKey>
            where TUserClaim : IdentityUserClaim<TKey>
            where TUserRole : IdentityUserRole<TKey>
            where TUserLogin : IdentityUserLogin<TKey>
            where TRoleClaim : IdentityRoleClaim<TKey>
            where TUserToken : IdentityUserToken<TKey>
            where TRoleDto : RoleDto<TRoleDtoKey>
            where TUsersDto : UsersDto<TUserDto, TUserDtoKey>
            where TRolesDto : RolesDto<TRoleDto, TRoleDtoKey>
            where TUserRolesDto : UserRolesDto<TRoleDto, TUserDtoKey, TRoleDtoKey>
            where TUserClaimsDto : UserClaimsDto<TUserDtoKey>
            where TUserProviderDto : UserProviderDto<TUserDtoKey>
            where TUserProvidersDto : UserProvidersDto<TUserDtoKey>
            where TUserChangePasswordDto : UserChangePasswordDto<TUserDtoKey>
            where TRoleClaimsDto : RoleClaimsDto<TRoleDtoKey>
            where TUserClaimDto : UserClaimDto<TUserDtoKey>
            where TRoleClaimDto : RoleClaimDto<TRoleDtoKey>
        {
            return AddAdminAspNetIdentityServices<TIdentityDbContext, TPersistedGrantDbContext, TLogDbContext, TUserDto,
                TUserDtoKey,
                TRoleDto, TRoleDtoKey, TUserKey, TRoleKey, TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin,
                TRoleClaim, TUserToken,
                TUsersDto, TRolesDto, TUserRolesDto, TUserClaimsDto,
                TUserProviderDto, TUserProvidersDto, TUserChangePasswordDto, TRoleClaimsDto, TUserClaimDto,
                TRoleClaimDto>(services, null);
        }

        public static IServiceCollection AddAdminAspNetIdentityServices<TIdentityDbContext, TPersistedGrantDbContext,
            TLogDbContext,
            TUserDto, TUserDtoKey, TRoleDto, TRoleDtoKey, TUserKey, TRoleKey, TUser, TRole, TKey, TUserClaim, TUserRole,
            TUserLogin, TRoleClaim, TUserToken,
            TUsersDto, TRolesDto, TUserRolesDto, TUserClaimsDto,
            TUserProviderDto, TUserProvidersDto, TUserChangePasswordDto, TRoleClaimsDto, TUserClaimDto, TRoleClaimDto>(
            this IServiceCollection services, HashSet<Type> profileTypes)
            where TPersistedGrantDbContext : DbContext, IAdminPersistedGrantDbContext
            where TIdentityDbContext : IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin,
                TRoleClaim, TUserToken>
            where TLogDbContext : DbContext, IAdminLogDbContext
            where TUserDto : UserDto<TUserDtoKey>
            where TUser : IdentityUser<TKey>
            where TRole : IdentityRole<TKey>
            where TKey : IEquatable<TKey>
            where TUserClaim : IdentityUserClaim<TKey>
            where TUserRole : IdentityUserRole<TKey>
            where TUserLogin : IdentityUserLogin<TKey>
            where TRoleClaim : IdentityRoleClaim<TKey>
            where TUserToken : IdentityUserToken<TKey>
            where TRoleDto : RoleDto<TRoleDtoKey>
            where TUsersDto : UsersDto<TUserDto, TUserDtoKey>
            where TRolesDto : RolesDto<TRoleDto, TRoleDtoKey>
            where TUserRolesDto : UserRolesDto<TRoleDto, TUserDtoKey, TRoleDtoKey>
            where TUserClaimsDto : UserClaimsDto<TUserDtoKey>
            where TUserProviderDto : UserProviderDto<TUserDtoKey>
            where TUserProvidersDto : UserProvidersDto<TUserDtoKey>
            where TUserChangePasswordDto : UserChangePasswordDto<TUserDtoKey>
            where TRoleClaimsDto : RoleClaimsDto<TRoleDtoKey>
            where TUserClaimDto : UserClaimDto<TUserDtoKey>
            where TRoleClaimDto : RoleClaimDto<TRoleDtoKey>
        {
            //Repositories
            services
                .AddTransient<
                    IIdentityRepository<TUserKey, TRoleKey, TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin,
                        TRoleClaim, TUserToken>, IdentityRepository<TIdentityDbContext, TUserKey, TRoleKey, TUser, TRole
                        , TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>>();
            services
                .AddTransient<IPersistedGrantAspNetIdentityRepository, PersistedGrantAspNetIdentityRepository<
                    TIdentityDbContext, TPersistedGrantDbContext, TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin,
                    TRoleClaim, TUserToken>>();
            services.AddTransient<IAuditRepository, AuditRepository<TLogDbContext>>();
            services.AddTransient<IGroupRepository, GroupRepository<TLogDbContext>>();

            //Services
            services
                .AddTransient<IIdentityService<TUserDto, TUserDtoKey, TRoleDto, TRoleDtoKey, TUserKey, TRoleKey, TUser,
                        TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken,
                        TUsersDto, TRolesDto, TUserRolesDto, TUserClaimsDto,
                        TUserProviderDto, TUserProvidersDto, TUserChangePasswordDto, TRoleClaimsDto>,
                    IdentityService<TUserDto, TUserDtoKey, TRoleDto, TRoleDtoKey, TUserKey, TRoleKey, TUser, TRole, TKey
                        , TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken,
                        TUsersDto, TRolesDto, TUserRolesDto, TUserClaimsDto,
                        TUserProviderDto, TUserProvidersDto, TUserChangePasswordDto, TRoleClaimsDto>>();
            services.AddTransient<IPersistedGrantAspNetIdentityService, PersistedGrantAspNetIdentityService>();
            
            services.AddTransient<IGroupService, GroupService<TUser, TKey>>();
            services.AddTransient<IAuditService, AuditService>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IUnitOfWork, AdminLogDbContextUnitOfWork<TLogDbContext>>();

            //Resources
            services.AddScoped<IIdentityServiceResources, IdentityServiceResources>();
            services.AddScoped<IPersistedGrantAspNetIdentityServiceResources, 
                PersistedGrantAspNetIdentityServiceResources>();

            //Register mapping
            services.AddAdminAspNetIdentityMapping()
                .UseIdentityMappingProfile<TUserDto, TUserDtoKey, TRoleDto, TRoleDtoKey, TUser, TRole, TKey, TUserClaim,
                    TUserRole, TUserLogin, TRoleClaim, TUserToken,
                    TUsersDto, TRolesDto, TUserRolesDto, TUserClaimsDto,
                    TUserProviderDto, TUserProvidersDto, TUserChangePasswordDto, TRoleClaimsDto, TUserClaimDto,
                    TRoleClaimDto>()
                .AddProfilesType(profileTypes);

            return services;
        }
    }
}
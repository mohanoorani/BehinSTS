using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectX.IdentityContext.Application.Services.Interfaces.Audits;
using ProjectX.IdentityContext.Application.UserDescriptor;
using ProjectX.IdentityContext.Domain.Entities;
using ProjectX.IdentityContext.Domain.Entities.Audits;
using ProjectX.IdentityContext.Persistence.Repositories.Interfaces.Audits;

namespace ProjectX.IdentityContext.Application.Services.Audits
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository auditRepository;

        private readonly IUserDescriptor userDescriptor;

        public AuditService(IAuditRepository auditRepository, IUserDescriptor userDescriptor)
        {
            this.auditRepository = auditRepository;
            this.userDescriptor = userDescriptor;
        }

        public void Add(Audit audit)
        {
            audit.Id = Guid.NewGuid();
            audit.UserId = userDescriptor.GetUserId();
            audit.Ip = userDescriptor.GetUserIp();
            audit.UserAgent = userDescriptor.GetUserAgent();
            auditRepository.Add(audit);
        }

        public Task<List<Audit>> GetAll()
        {
            return auditRepository.GetAll();
        }

        public Task RemoveAll()
        {
            return auditRepository.RemoveAll();
        }
    }
}
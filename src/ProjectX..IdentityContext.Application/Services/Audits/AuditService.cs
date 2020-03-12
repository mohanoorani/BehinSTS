using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectX.IdentityContext.Application.Services.Interfaces.Audits;
using ProjectX.IdentityContext.Domain.Entities;
using ProjectX.IdentityContext.Domain.Entities.Audits;
using ProjectX.IdentityContext.Persistence.Repositories.Interfaces.Audits;

namespace ProjectX.IdentityContext.Application.Services.Audits
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _auditRepository;

        public AuditService(IAuditRepository auditRepository)
        {
            this._auditRepository = auditRepository;
        }

        public void Add(Guid aggregateId, string eventName, DateTime eventTime, string data)
        {
            _auditRepository.Add(new Audit
            {
                Id = Guid.NewGuid(),
                AggregateId = aggregateId,
                EventName = eventName,
                EventTime = eventTime,
                Data = data,
            });
        }

        public Task<List<Audit>> GetAll()
        {
            return _auditRepository.GetAll();
        }

        public Task RemoveAll()
        {
            return _auditRepository.RemoveAll();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectX.IdentityContext.Domain.Entities.Audits;

namespace ProjectX.IdentityContext.Application.Services.Interfaces.Audits
{
    public interface IAuditService
    {
        void Add(Guid aggregateId, string eventName, DateTime eventTime, string data);
        
        Task<List<Audit>> GetAll();

        Task RemoveAll();
    }
}
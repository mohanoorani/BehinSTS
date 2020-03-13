using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectX.IdentityContext.Domain.Entities.Audits;

namespace ProjectX.IdentityContext.Application.Services.Interfaces.Audits
{
    public interface IAuditService
    {
        void Add(Audit audit);
        
        Task<List<Audit>> GetAll();

        Task RemoveAll();
    }
}
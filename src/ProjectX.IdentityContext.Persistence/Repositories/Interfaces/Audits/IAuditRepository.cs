using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectX.IdentityContext.Domain.Entities.Audits;

namespace ProjectX.IdentityContext.Persistence.Repositories.Interfaces.Audits
{
    public interface IAuditRepository
    {
        Task Add(Audit log);

        Task<List<Audit>> GetAll();

        Task RemoveAll();
    }
}
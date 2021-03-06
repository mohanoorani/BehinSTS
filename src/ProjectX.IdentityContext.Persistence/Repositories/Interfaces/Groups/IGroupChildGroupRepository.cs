﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectX.IdentityContext.Domain.Entities.Groups;

namespace ProjectX.IdentityContext.Persistence.Repositories.Interfaces.Groups
{
    public partial interface IGroupRepository
    {
        Task AddChildGroup(GroupChildGroup childGroup, string updaterId);

        Task<List<GroupChildGroup>> GetAllChildGroups(Guid id);

        Task RemoveChildGroup(GroupChildGroup childGroup, string updaterId);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Versl.Models;

namespace Versl.Services.Data
{
    public interface IUserProfileDatabaseService : IDatabaseService<UserProfile>
    {
        Task<List<UserProfile>> GetAdminListAsync();
    }
}

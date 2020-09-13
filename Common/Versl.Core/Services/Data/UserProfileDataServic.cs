using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Versl.Models;

namespace Versl.Services.Data
{
    public class UserProfileDataService : DataServiceBase<UserProfile>, IUserProfileDataService
    {
        public UserProfileDataService(IUserProfileDatabaseService dataService) : base(dataService)
        {
        }

        public Task<List<UserProfile>> GetAdminListAsync()
        {
            return ((IUserProfileDatabaseService)DataService).GetAdminListAsync();
        }
    }
}

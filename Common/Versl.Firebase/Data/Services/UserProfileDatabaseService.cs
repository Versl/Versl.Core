using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database.Query;
using Versl.Firebase.Data.DTO;
using Versl.Models;
using Versl.Services.Data;

namespace Versl.Firebase.Data.Services
{
    public class UserProfileDatabaseService : FirebaseDatabaseService<UserProfile, UserProfileDTO>, IUserProfileDatabaseService
    {
        public UserProfileDatabaseService()
        {
        }

        public override string BasePath => "entities/users";

        public async Task<List<UserProfile>> GetAdminListAsync()
        {
            var list = await _client.Child(BasePath).OrderBy("role").StartAt("99").OnceAsync<UserProfile>();

            var retval = new List<UserProfile>();
            foreach (var item in list)
            {
                item.Object.Id = item.Key;
                retval.Add(item.Object);
            }

            return retval;
        }
    }
}

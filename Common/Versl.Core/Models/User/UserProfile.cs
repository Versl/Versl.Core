using System;
using Versl.Enums;

namespace Versl.Models
{        
    public class UserProfile : DataModelBase
    {
        public UserProfile()
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole Role { get; set; }
    }
}

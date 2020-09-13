using System;
using System.IO;

namespace Versl.Models
{
    public class CreateUserRequest : UpdateUserRequest
    {
        public CreateUserRequest()
        {
        }

        public string Password { get; set; }
    }
}

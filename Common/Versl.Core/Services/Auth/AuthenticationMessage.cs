using System;
using MvvmCross.Plugin.Messenger;
using Versl.Models;

namespace Versl.Services.Auth
{
    public class AuthenticationMessage : MvxMessage
    {
        public AuthenticationMessage(object sender, User user) : base(sender)
        {
            CurrentUser = user;
        }

        public User CurrentUser {get; protected set;}
    }
}

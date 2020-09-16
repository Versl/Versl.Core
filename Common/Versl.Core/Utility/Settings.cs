using System;
using Xamarin.Essentials;

namespace Versl.Utility
{
    public static class Settings
    {
        public static string RefreshToken
        {
            get => Preferences.Get(nameof(RefreshToken), string.Empty);
            set => Preferences.Set(nameof(RefreshToken), value);
        }
    }
}

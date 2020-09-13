using System;
using Xamarin.Forms;

namespace Versl.Utility
{
    public enum ApplicationColor
    {
        Primary,
        OnPrimary,
        PrimaryVariant,
        OnPrimaryVariant,
        Secondary,
        OnSecondary,
        SecondaryVariant,
        OnSecondaryVariant,
        Background,
        OnBackground,
        Surface,
        OnSurface,
        Error,
        OnError,
        VerslPurple,
        VerslTeal
    }

    public static class ColorResolver
    {
        public static Color Resolve(ApplicationColor appColor)
        {
            var colorName = $"{appColor.ToString()}Color";

            if (appColor == ApplicationColor.VerslPurple || appColor == ApplicationColor.VerslTeal)
            {
                colorName = appColor.ToString();   
            }            

            Application.Current.Resources.TryGetValue(colorName, out var color);

            return Color.FromHex(color.ToString());
        }
    }
}
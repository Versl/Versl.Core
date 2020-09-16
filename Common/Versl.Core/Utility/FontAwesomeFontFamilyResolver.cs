using System;
using Xamarin.Forms;

namespace Versl.Utility
{
    public enum FontAwesomeFontFamily
    {
        Brands,
        Solid,
        Regular
    }

    public static class FontAwesomeFontFamilyResolver
    {
        public static string Resolve (string family)
        {
            Enum.TryParse<FontAwesomeFontFamily>(family, out var val);

            return FontAwesomeFontFamilyResolver.Resolve(val);
        }

        public static string Resolve (FontAwesomeFontFamily family)
        {
            var fontName = string.Concat("FontAwesome", family.ToString());

            var name = (OnPlatform<string>)Application.Current.Resources[fontName];

            return name;
        }
    }
}

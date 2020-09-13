using System;
using System.Globalization;
using Versl.Enums;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Versl.Converters
{
    public class CTATypeTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (CTALinkType)value;

            switch (val)
            {
                case CTALinkType.VideoHorizontal:
                case CTALinkType.VideoVertical:
                case CTALinkType.Audio:
                    return Utility.FontAwesomeIcons.PlayCircle;
                default:
                case CTALinkType.Web:
                    return Utility.FontAwesomeIcons.Globe;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

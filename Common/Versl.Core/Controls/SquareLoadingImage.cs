using System;
using FFImageLoading.Forms;
using Xamarin.Essentials;

namespace Versl.Controls
{
    public class SquareCachedImage : CachedImage
    {
        public SquareCachedImage()
        {
            HeightRequest = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
        }
    }
}

using System;
using System.Collections.Generic;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Versl.Pages
{
    [MvxModalPresentation(WrapInNavigationPage = false)]
    public partial class VerticalVideoPlayerPage : ContentPageBase
    {
        public VerticalVideoPlayerPage()
        {
            InitializeComponent();
        }
    }
}

using System;
using System.Collections.Generic;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms;

namespace Versl.Pages
{
    [MvxModalPresentation(WrapInNavigationPage = true, Title = "Profile")]
    public partial class ProfilePage : ContentPageBase
    {
        public ProfilePage()
        {
            InitializeComponent();
        }
    }
}

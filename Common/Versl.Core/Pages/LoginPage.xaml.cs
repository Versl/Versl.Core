using System;
using System.Collections.Generic;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms;

namespace Versl.Pages
{
    [MvxModalPresentation(WrapInNavigationPage = false)]
    public partial class LoginPage : ContentPageBase
    {
        public LoginPage()
        {
            InitializeComponent();
        }
    }
}
